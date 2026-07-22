using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenu : MonoBehaviour
{
    [SerializeField] private string nombreEscenaMenu = "MenuInicio";
    [SerializeField] private iraescena cambiadorEscena;

    public void Volver()
    {
        if (cambiadorEscena != null)
            cambiadorEscena.IrAEscena(nombreEscenaMenu);
        else
            SceneManager.LoadScene(nombreEscenaMenu);
    }
}