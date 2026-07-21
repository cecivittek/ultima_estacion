using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
 
public class AcusacionManager : MonoBehaviour
{
    [Header("Camino")]
    public bool esCaminoMate = false;
    public bool esCaminoCelular = false;
 
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private TextMeshProUGUI textoResultado;
 
    [Header("Colores del fondo del boton")]
    public Color colorNormal = new Color(0.05f, 0.08f, 0.22f);
    public Color colorSeleccionado = new Color(0.2f, 0.8f, 0.2f);
 
    private string culpable;
    private string escenaVictoria;
    private string personajeSeleccionado = "";
    private BotonPersonaje botonActual;
    private iraescena fade;
 
    private void Start()
    {
        // El culpable y la escena de victoria dependen del camino, igual que en DialogoManager.
        if (esCaminoCelular)
        {
            culpable = "Susana";
            escenaVictoria = "escena_victoria_celular";
        }
        else if (esCaminoMate)
        {
            culpable = "Duki";
            escenaVictoria = "Victoria mate";
        }
        else
        {
            culpable = "Milagros";
            escenaVictoria = "escena_victoria";
        }
 
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
 
        Debug.Log("Personaje seleccionado: " + personajeSeleccionado);
        Debug.Log("Culpable: " + culpable);
        Debug.Log("Son iguales: " + (personajeSeleccionado == culpable));
 
        string escenaDestino = (personajeSeleccionado == culpable) ? escenaVictoria : "escena_derrota";
        Debug.Log("Escena destino: " + escenaDestino);
        fade.IrAEscena(escenaDestino);
    }
 
    public void VolverAlSubte()
    {
        fade.IrAEscena("ultima_estacion");
    }
}