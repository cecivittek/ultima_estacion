using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introimagenes : MonoBehaviour
{
    public Sprite[] imagenes;
    public Image imagenPantalla;

    private int indiceActual = 0;

    void Start()
    {
        MostrarImagen(00);
    }

    public void Siguiente()
    {
        indiceActual++;

        if (indiceActual >= imagenes.Length)
        {
            SceneManager.LoadScene("eleccion_objeto");
        }
        else
        {
            MostrarImagen(indiceActual);
        }
    }

    void MostrarImagen(int indice)
    {
        imagenPantalla.sprite = imagenes[indice];
    }
}