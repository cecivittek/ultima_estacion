using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MenuInicio : MonoBehaviour
{
    [Header("Nombre exacto de la escena de juego (Build Settings)")]
    [SerializeField] private string nombreEscenaJuego = "Nivel1";
 
    [Header("Panel de instrucciones (arrastrar el GameObject del panel)")]
    [SerializeField] private GameObject panelComoJugar;
 
    // Conectar al botón "Jugar" -> OnClick()
    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
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

    public void Salir()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SalirDelJuego()
        {
            Application.Quit();
        }

    }