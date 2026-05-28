using UnityEngine;

public class PP_CONTROLADOR : MonoBehaviour
{
    private Animator animator;
    public float velocidad = 3f;
    public float minX = -8f;
    public float maxX = 8f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Mover al personaje
        transform.Translate(new Vector2(moveX, 0) * velocidad * Time.deltaTime);

        // Limitar posición
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Actualizar el Animator
        animator.SetFloat("VelocidadX", moveX);
    }
}