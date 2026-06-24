using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class clicks : MonoBehaviour
{
    public static clicks instancia;
    [Header("--- Canal de Audio ---")]
    [SerializeField] private AudioSource canalSFX; // El "parlante" para los clics
    [Header("--- Sonido de Interaccion ---")]
    public AudioClip sonidoClic; // El archivo .mp3 o .wav del clic
 
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
 
            // Cada vez que se carga CUALQUIER escena, le conectamos el sonido
            // de clic a TODOS los botones que encuentre, sin tocar nada a mano.
            SceneManager.sceneLoaded += ConectarSonidoATodosLosBotones;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    private void ConectarSonidoATodosLosBotones(Scene escena, LoadSceneMode modo)
    {
        // FindObjectsInactive.Include: tambien encuentra botones dentro de objetos
        // desactivados (ej. PanelDialogo apagado al inicio), que de otro modo se saltean.
        Button[] todosLosBotones = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button boton in todosLosBotones)
        {
            // RemoveListener antes de Add evita que el sonido se duplique si el
            // boton ya estaba conectado de una carga de escena anterior.
            boton.onClick.RemoveListener(ReproducirClic);
            boton.onClick.AddListener(ReproducirClic);
        }
    }
 
    public void ReproducirClic()
    {
        if (canalSFX != null && sonidoClic != null)
        {
            canalSFX.PlayOneShot(sonidoClic);
        }
    }
}
