using UnityEngine;

public class SeguirCamara : MonoBehaviour
{
    private Transform camara;

    void Start()
    {
        camara = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(camara.position.x, camara.position.y, transform.position.z);
    }
}