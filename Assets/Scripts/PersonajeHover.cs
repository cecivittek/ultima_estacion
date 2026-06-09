using UnityEngine;

public class PersonajeHover : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    void Start()
    {
        highlight.SetActive(false);
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}