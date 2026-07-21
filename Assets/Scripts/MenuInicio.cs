using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MenuInicio : MonoBehaviour
{
    [Header("Nombre exacto de la escena de juego (Build Settings)")]
    [SerializeField] private string nombreEscenaJuego = "Nivel1";
 
    [Header("Nombre exacto de la escena de Como Jugar (Build Settings)")]
    [SerializeField] private string nombreEscenaComoJugar = "ComoJugar";
 
    [Header("Panel de instrucciones (opcional, si preferis panel en vez de escena)")]
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
 
    // Conectar al botón "Cómo Jugar" -> OnClick() (version escena, con fade)
    public void IrAComoJugar()
    {
        if (cambiadorEscena != null)
            cambiadorEscena.IrAEscena(nombreEscenaComoJugar);
        else
            SceneManager.LoadScene(nombreEscenaComoJugar); // respaldo si no se asigna el fade
    }
 
    // Metodos viejos del panel, por si en algun lado todavia se usan
    public void AbrirComoJugar()
    {
        if (panelComoJugar != null)
            panelComoJugar.SetActive(true);
    }
 
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
 