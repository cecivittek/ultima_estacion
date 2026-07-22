using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MateAcusacionManager : MonoBehaviour
{
    [SerializeField] private string culpable = "Duki";
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private TextMeshProUGUI textoResultado;

    [Header("Colores del fondo del boton")]
    public Color colorNormal = new Color(0.05f, 0.08f, 0.22f);
    public Color colorSeleccionado = new Color(0.2f, 0.8f, 0.2f);

    private string personajeSeleccionado = "";
    private BotonPersonaje botonActual;
    private iraescena fade;

    private void Start()
    {
        fade = gameObject.AddComponent<iraescena>();

        BotonPersonaje[] todos = FindObjectsByType<BotonPersonaje>(FindObjectsSortMode.None);
        foreach (BotonPersonaje boton in todos)
        {
            boton.fondo.color = colorNormal;
            boton.MostrarContorno(false);
        }
    }

    public void SeleccionarPersonaje(BotonPersonaje boton)
    {
        if (botonActual != null)
        {
            botonActual.fondo.color = colorNormal;
            botonActual.MarcarSeleccionado(false);
            botonActual.MostrarContorno(false);
        }

        botonActual = boton;
        personajeSeleccionado = boton.nombrePersonaje;

        boton.fondo.color = colorSeleccionado;
        boton.MarcarSeleccionado(true);
        boton.MostrarContorno(true);
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

        string escenaDestino = (personajeSeleccionado == culpable) ? "Victoria mate" : "escena_derrota";
        fade.IrAEscena(escenaDestino);
    }

    public void VolverAlSubte()
    {
        fade.IrAEscena("ultima_estacion");
    }
}  