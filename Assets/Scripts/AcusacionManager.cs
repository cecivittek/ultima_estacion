using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
 
public class AcusacionManager : MonoBehaviour
{
    [SerializeField] private string culpable = "Milagros";
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private TextMeshProUGUI textoResultado;
    [SerializeField] private iraescena cambiadorEscena; // el script del fade
 
    [Header("Colores del fondo del boton")]
    public Color colorNormal = new Color(0.05f, 0.08f, 0.22f);   // navy 0D1439
    public Color colorSeleccionado = new Color(0.2f, 0.8f, 0.2f); // verde
 
    private string personajeSeleccionado = "";
    private BotonPersonaje botonActual;
 
    private void Start()
    {
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
 
        string escenaDestino = (personajeSeleccionado == culpable) ? "escena_victoria" : "escena_derrota";
 
        if (cambiadorEscena != null)
            cambiadorEscena.IrAEscena(escenaDestino);
        else
            SceneManager.LoadScene(escenaDestino); // respaldo si no se asigna el fade
    }
 
    public void VolverAlSubte()
    {
        if (cambiadorEscena != null)
            cambiadorEscena.IrAEscena("ultima_estacion");
        else
            SceneManager.LoadScene("ultima_estacion");
    }
}
 