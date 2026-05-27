using UnityEngine;

public class PP_CONTROLADOR : MonoBehaviour
{
    private Animator animator;
    public float velocidad = 3f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Mover al personaje
        transform.Translate(new Vector2(moveX, 0) * velocidad * Time.deltaTime);

        // Actualizar el Animator
        animator.SetFloat("VelocidadX", moveX);
    }
}