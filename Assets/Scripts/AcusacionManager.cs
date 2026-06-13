using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AcusacionManager : MonoBehaviour
{
    [SerializeField] private string culpable = "Duki"; // cambiá al culpable real
    
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private TextMeshProUGUI textoResultado;
    
    private string personajeSeleccionado = "";

    public void SeleccionarPersonaje(string nombre)
    {
        personajeSeleccionado = nombre;
        Debug.Log("Seleccionaste: " + nombre);
    }

    public void HacerAcusacion()
    {
        if (personajeSeleccionado == "")
        {
            textoResultado.text = "¡Seleccioná un personaje primero!";
            panelResultado.SetActive(true);
            return;
        }

        if (personajeSeleccionado == culpable)
            textoResultado.text = "¡CORRECTO! Encontraste al culpable!";
        else
            textoResultado.text = "¡INCORRECTO! Era " + culpable;

        panelResultado.SetActive(true);
    }
}