using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            offset.z );
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
