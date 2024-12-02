using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private ParticleSystem derecha;
    [SerializeField] private ParticleSystem izquierda;
    public AudioSource slideSound;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            // Detecta la fuerza que aplica el jugador al objeto
            Vector2 pushDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            rb.AddForce(pushDirection * 1f); // Ajusta la fuerza de empuje
            //Debug.Log(rb.velocity.x);
            if (rb.velocity.x > 0)
            {
                if (!slideSound.isPlaying)
                    slideSound.Play();

                //Debug.Log("Particulas a la derecha");
                if (!derecha.isPlaying)
                derecha.Play();

            }
            else if (rb.velocity.x < 0)
            {
                //Debug.Log("Particulas a la izquierda");
                if (!slideSound.isPlaying)
                    slideSound.Play();

                if (!izquierda.isPlaying)
                izquierda.Play();


            }
        }
    }
}
