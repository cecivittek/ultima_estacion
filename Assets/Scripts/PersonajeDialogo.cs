using UnityEngine;

public class PersonajeDialogo : MonoBehaviour
{
    private SpriteRenderer sr;
    private Transform camara;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = gameObject.AddComponent<SpriteRenderer>();
        camara = Camera.main.transform;
        Debug.Log("Camara encontrada: " + camara);
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (camara != null)
        {
            transform.position = new Vector3(
                camara.position.x + 3f,
                camara.position.y -1.5f,
                0f
            );
        }
    }

    public void MostrarPersonaje(Sprite sprite)
    {
        Debug.Log("MostrarPersonaje llamado!");
        sr.sprite = sprite;
        gameObject.SetActive(true);
        transform.position = new Vector3(camara.position.x + 3f, camara.position.y, 0f);
        Debug.Log("Posicion: " + transform.position);
    }

    public void OcultarPersonaje()
    {
        gameObject.SetActive(false);
    }
}