using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float slideDuration = 0.5f;
    public float slideSpeed = 5f; // Por si hay que reducir la velocidad al deslizarse

    private Queue<Vector2> positionHistory = new Queue<Vector2>();
    public int positionCheckFrames = 5;
    public float positionTolerance = 0.1f; // Allowed position variation before considering it idle

    private float idleTimer = 0.0f;
    public float idleDuration = 3f; // Tiempo que debe estar quieto para GameOver

    public Collider2D playerCollider;
    public Collider2D slideCollider;
    public Transform spriteTransform; // Para hacer visual el slide

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isSliding = false;
    private int jumpCounter = 0;

    [SerializeField] private ParticleSystem dash;
    [SerializeField] private ParticleSystem run;

    public AudioSource runSound;
    public AudioSource jumpSound;
    public AudioSource slideSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        positionHistory.Enqueue(rb.position);

        if (positionHistory.Count > positionCheckFrames)
        {
            positionHistory.Dequeue();
        }

        if (HasBeenIdle())
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleDuration)
            {
                GameplayManager.Instance.gameOverScreen.SetActive(true);
            }
        }
        else
        {
            idleTimer = 0f;
        }

        if (!isSliding)
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(direction.x * slideSpeed, rb.velocity.y);
        }

        Animator animator = GetComponent<Animator>();
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && !runSound.isPlaying && rb.velocity.x != 0)
        {
            runSound.Play();
        }
    }

    private bool HasBeenIdle()
    {
        if (Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            return false;
        }

        if (positionHistory.Count < positionCheckFrames)
            return false;

        Vector2 firstPosition = positionHistory.Peek();
        foreach (Vector2 position in positionHistory)
        {
            if (Vector2.Distance(firstPosition, position) > positionTolerance)
            {
                return false;
            }
        }
        return true;
    }

    public void ChangeDirection()
    {
        direction = -direction;

        Vector3 localScale = spriteTransform.localScale;
        localScale.x *= -1;
        spriteTransform.localScale = localScale;

        positionHistory.Clear();
        positionHistory.Enqueue(rb.position);

        idleTimer = 0f;
    }

    public void Jump()
    {
        if (isGrounded || jumpCounter < 2)
        {
            run.Play();
            Vector2 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;

            jumpCounter++;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isGrounded = false;

            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Jump");

            jumpSound.Play();
        }
    }

    public void Slide()
    {
        if (isGrounded && !isSliding)
        {
            slideSound.Play();

            StartCoroutine(SlideCoroutine());
        }
    }

    IEnumerator SlideCoroutine()
    {
        isSliding = true;
        playerCollider.enabled = false;
        slideCollider.enabled = true;

        spriteTransform.localScale = new Vector3(1f, 0.5f, 1f); // Reducir visualmente el sprite
        dash.Play();
        yield return new WaitForSeconds(slideDuration);

        playerCollider.enabled = true;
        slideCollider.enabled = false;

        spriteTransform.localScale = Vector3.one; // Restaurar el sprite
        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            run.Play();
            isGrounded = true;
            jumpCounter = 0;
        }
    }
}