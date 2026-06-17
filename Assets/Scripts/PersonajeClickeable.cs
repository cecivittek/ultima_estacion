using UnityEngine;

public class PersonajeClickeable : MonoBehaviour
{
    [SerializeField] private BotonPersonaje botonAsociado;

    void OnMouseDown()
    {
        botonAsociado.AlClickear();
    }
}