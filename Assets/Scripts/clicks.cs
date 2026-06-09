using UnityEngine;

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
        }
        else
        {
            Destroy(gameObject);
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