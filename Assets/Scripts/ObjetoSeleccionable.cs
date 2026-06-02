using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjetoSeleccionable : MonoBehaviour
{
    public string nombreObjeto;
    public string escenaDestino;
    public Transform jugador;
    public float distancia = 2f;

    private Vector3 escalaOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        if (jugador == null) return;

        float dist = Vector3.Distance(transform.position, jugador.position);

        if (dist < distancia)
        {
            transform.localScale = escalaOriginal * 1.2f;
        }
        else
        {
            transform.localScale = escalaOriginal;
        }
    }

    void OnMouseDown()
    {
        PlayerPrefs.SetString("ObjetoElegido", nombreObjeto);
        SceneManager.LoadScene(escenaDestino);
    }
}