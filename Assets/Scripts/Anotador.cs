using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Anotador : MonoBehaviour
{
    public static Anotador instancia;

    public GameObject prefabPista;  // un Text con el formato de cada pista
    public Transform contenido;     // el Content del Scroll View

    void Awake()
    {
        instancia = this;
    }

    public void AgregarPista(string personaje, string pista)
    {
        GameObject nuevaPista = Instantiate(prefabPista, contenido);
        TextMeshProUGUI texto = nuevaPista.GetComponent<TextMeshProUGUI>();
        texto.text = "• " + personaje + ": " + pista;
    }
}