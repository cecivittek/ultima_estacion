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
    public AudioSource narracionSource;
    public AudioClip[] narraciones;
    public GameObject botonAtras; // NUEVO

    private int indiceActual = 0;

    void Start()
    {
        MostrarImagen(0);
    }

    public void Siguiente()
    {
        StartCoroutine(Transicion());
    }

    public void Omitir()
    {
        StopAllCoroutines();
        StartCoroutine(TransicionOmitir());
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

    IEnumerator TransicionOmitir()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadFade;
            pantallaFade.color = new Color(0, 0, 0, t);
            yield return null;
        }

        SceneManager.LoadScene("eleccion_objeto");
    }

    void MostrarImagen(int indice)
    {
        imagenPantalla.sprite = imagenes[indice];

        // NUEVO: ocultar atrás en el primer frame, mostrar en los demás
        if (botonAtras != null)
            botonAtras.SetActive(indice > 0);

        if (audioSource != null && sonidos.Length > indice && sonidos[indice] != null)
        {
            audioSource.clip = sonidos[indice];
            audioSource.Play();
        }

        if (narracionSource != null && narraciones.Length > indice && narraciones[indice] != null)
        {
            narracionSource.clip = narraciones[indice];
            narracionSource.Play();
        }
    }
    public void Atras()
{
    StartCoroutine(TransicionAtras());
}

IEnumerator TransicionAtras()
{
    float t = 0;
    while (t < 1)
    {
        t += Time.deltaTime * velocidadFade;
        pantallaFade.color = new Color(0, 0, 0, t);
        yield return null;
    }

    indiceActual--;
    MostrarImagen(indiceActual);

    t = 1;
    while (t > 0)
    {
        t -= Time.deltaTime * velocidadFade;
        pantallaFade.color = new Color(0, 0, 0, t);
        yield return null;
    }
}
}