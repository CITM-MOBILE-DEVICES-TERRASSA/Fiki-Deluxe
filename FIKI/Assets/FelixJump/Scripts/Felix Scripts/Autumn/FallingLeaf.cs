using UnityEngine;

public class FallingLeaf : MonoBehaviour
{
    public float minFallSpeed = 1f;         // Minimum fall speed
    public float maxFallSpeed = 2.5f;       // Maximum fall speed
    public float minSwaySpeed = 0.5f;       // Minimum sway speed
    public float maxSwaySpeed = 1.5f;       // Maximum sway speed
    public float minSwayAmount = 0.5f;      // Minimum sway amount
    public float maxSwayAmount = 2f;        // Maximum sway amount

    private float fallSpeed;                // Actual fall speed
    private float swaySpeed;                // Actual sway speed
    private float swayAmount;               // Actual sway amount
    private float swayOffset;               // Random offset to desynchronize each leaf

    void Start()
    {
        // Randomize fall and sway parameters
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        swaySpeed = Random.Range(minSwaySpeed, maxSwaySpeed);
        swayAmount = Random.Range(minSwayAmount, maxSwayAmount);

        // Randomize sway offset to desynchronize each leaf
        swayOffset = Random.Range(0f, 0.5f * Mathf.PI);
    }

    void Update()
    {
        // Calculate leaf's sway displacement
        float sway = Mathf.Sin(Time.time * swaySpeed + swayOffset) * swayAmount;

        // Calculate leaf's fall position
        Vector3 fallPosition = transform.position;
        fallPosition.x += sway * Time.deltaTime;        // Update horizontal position
        fallPosition.y -= fallSpeed * Time.deltaTime;   // Update vertical position
        fallPosition.z = Mathf.Clamp(fallPosition.z, -0.4f, 0.4f);

        // Update leaf's position
        transform.position = fallPosition;

        // If leaf goes below the screen, optionally respawn it at the top (loop effect)
        if (fallPosition.y < -3) // Assume -10 is the bottom screen boundary
        {
            Destroy(gameObject);
            //fallPosition.y = 10;  // Respawn at the top
            //transform.position = fallPosition;
        }
    }
}

