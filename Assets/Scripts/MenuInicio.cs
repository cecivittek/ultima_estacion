using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MenuInicio : MonoBehaviour
{
    [Header("Nombre exacto de la escena de juego (Build Settings)")]
    [SerializeField] private string nombreEscenaJuego = "Nivel1";
 
    [Header("Panel de instrucciones (arrastrar el GameObject del panel)")]
    [SerializeField] private GameObject panelComoJugar;
 
    [Header("Script de fade (arrastrar el mismo GameObject o uno con Iraescena)")]
    [SerializeField] private iraescena cambiadorEscena;
 
    // Conectar al botón "Jugar" -> OnClick()
    public void Jugar()
    {
        if (cambiadorEscena != null)
            cambiadorEscena.IrAEscena(nombreEscenaJuego);
        else
            SceneManager.LoadScene(nombreEscenaJuego); // respaldo si no se asigna el fade
    }
 
    // Conectar al botón "Cómo Jugar" -> OnClick()
    public void AbrirComoJugar()
    {
        if (panelComoJugar != null)
            panelComoJugar.SetActive(true);
    }
 
    // Conectar al botón de cerrar dentro del panel de instrucciones
    public void CerrarComoJugar()
    {
        if (panelComoJugar != null)
            panelComoJugar.SetActive(false);
    }
 
    // Conectar al botón "Salir" -> OnClick()
    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
 