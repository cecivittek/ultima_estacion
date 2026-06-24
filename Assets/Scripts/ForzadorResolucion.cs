using UnityEngine;

public class ForzarResolucion : MonoBehaviour
{
    void Awake()
    {
        // Fuerza el juego a 1920x1080 en ventana, siempre igual
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        // Que este objeto no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }
}