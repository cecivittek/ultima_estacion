using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AcusacionManager : MonoBehaviour
{
    [SerializeField] private string culpable = "Milagros";
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private TextMeshProUGUI textoResultado;

    public Color colorSeleccionado = Color.green;
    public Color colorNormal = Color.white;

    private string personajeSeleccionado = "";
    private BotonPersonaje botonActual;

    public void SeleccionarPersonaje(string nombre, BotonPersonaje boton)
    {
        if (botonActual != null)
        {
            botonActual.imagenFondo.color = colorNormal;
            if (botonActual.spritePersonaje != null)
                botonActual.spritePersonaje.color = colorNormal;
        }

        personajeSeleccionado = nombre;
        botonActual = boton;
        boton.imagenFondo.color = colorSeleccionado;

        if (boton.spritePersonaje != null)
            boton.spritePersonaje.color = colorSeleccionado;
    }

    public void HacerAcusacion()
    {
        if (personajeSeleccionado == "")
        {
            if (panelResultado != null)
            {
                textoResultado.text = "¡Seleccioná un personaje primero!";
                panelResultado.SetActive(true);
            }
            return;
        }

        if (personajeSeleccionado == culpable)
            SceneManager.LoadScene("escena_victoria");
        else
            SceneManager.LoadScene("escena_derrota");
    }

    public void VolverAlSubte()
    {
        SceneManager.LoadScene("ultima_estacion");
    }
}

 