using UnityEngine;

public class fondoinfinito : MonoBehaviour {
     public Transform player;
    public float smoothSpeed = 5f;
    public float minX = -8f;
    public float maxX = 8f;

    void LateUpdate() {
        Vector3 target = new Vector3(player.position.x, transform.position.y, transform.position.z);
        target.x = Mathf.Clamp(target.x, minX, maxX);
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);
    }
}