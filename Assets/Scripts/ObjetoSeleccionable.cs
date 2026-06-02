using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjetoSeleccionable : MonoBehaviour
{
    public string nombreObjeto;
    public string escenaDestino;

    void OnMouseDown()
    {
        PlayerPrefs.SetString("ObjetoElegido", nombreObjeto);
        SceneManager.LoadScene(escenaDestino);
    }
}