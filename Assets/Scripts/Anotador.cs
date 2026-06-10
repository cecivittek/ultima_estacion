using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Anotador : MonoBehaviour
{
    public static Anotador instancia;

    public GameObject prefabPista;
    public Transform contenido;

    void Awake()
    {
        instancia = this;
    }

    public void AgregarPista(string personaje, string pista)
    {
        if (prefabPista == null || contenido == null) return;

        GameObject nuevaPista = Instantiate(prefabPista, contenido);
        TextMeshProUGUI texto = nuevaPista.GetComponent<TextMeshProUGUI>();
        if (texto != null)
            texto.text = "<b>" + personaje + "</b>\n" + pista;
    }
}
