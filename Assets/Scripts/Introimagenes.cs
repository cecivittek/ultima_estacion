using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Introimagenes : MonoBehaviour
{
    public Sprite[] imagenes;
    public Image imagenPantalla;
    public Image pantallaFade;
    public float velocidadFade = 1f;
    public AudioSource audioSource;
    public AudioClip[] sonidos;

    private int indiceActual = 0;

    void Start()
    {
        MostrarImagen(0);
    }

    public void Siguiente()
    {
        StartCoroutine(Transicion());
    }

    IEnumerator Transicion()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadFade;
            pantallaFade.color = new Color(0, 0, 0, t);
            yield return null;
        }

        indiceActual++;

        if (indiceActual >= imagenes.Length)
        {
            SceneManager.LoadScene("eleccion_objeto");
        }
        else
        {
            MostrarImagen(indiceActual);
        }
        t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime * velocidadFade;
            pantallaFade.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }

    void MostrarImagen(int indice)
    {
        imagenPantalla.sprite = imagenes[indice];

        if (audioSource != null && sonidos.Length > indice && sonidos[indice] != null)
        {
            audioSource.clip = sonidos[indice];
            audioSource.Play();
        }
    }
}