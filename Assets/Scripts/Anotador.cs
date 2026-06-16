using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Anotador : MonoBehaviour
{
    public static Anotador instancia;

    [Header("Referencia")]
    public GameObject prefabPista;
    public RectTransform ladoIzquierdo;
    public RectTransform ladoDerecho;

    [Header("Layout")]
    public float anchoTexto           = 300f;
    public float alturaMaximaPagina   = 600f;   // <-- poner acá el alto de PaginaIzquierda
    public float paddingTop           = 20f;
    public float paddingLateral       = 15f;
    public float espaciadoEntrePistas = 12f;

    [Header("Estilo de texto")]
    public TMP_FontAsset fuente;
    public float tamanoFuente         = 14f;
    public Color colorTexto           = Color.black;
    public FontStyles estiloNombre    = FontStyles.Bold;

    private static readonly Queue<(string personaje, string pista)> cola =
        new Queue<(string, string)>();

    private bool llenandoDerecho = false;
    private bool procesando      = false;

    void Awake() { instancia = this; }

    void OnEnable()
    {
        if (cola.Count > 0 && !procesando)
            StartCoroutine(ProcesarCola());
    }

    public static void AgregarPista(string personaje, string pista)
    {
        cola.Enqueue((personaje, pista));
        if (instancia != null && instancia.gameObject.activeInHierarchy && !instancia.procesando)
            instancia.StartCoroutine(instancia.ProcesarCola());
    }

    IEnumerator ProcesarCola()
    {
        procesando = true;
        while (cola.Count > 0)
        {
            var item = cola.Dequeue();
            yield return StartCoroutine(AgregarRoutine(item.personaje, item.pista));
        }
        procesando = false;
    }

    float AlturaOcupada(RectTransform lado)
    {
        float total = paddingTop;
        foreach (Transform child in lado)
        {
            RectTransform rt = child as RectTransform;
            if (rt == null) continue;
            total += rt.sizeDelta.y + espaciadoEntrePistas;
        }
        return total;
    }

    IEnumerator AgregarRoutine(string personaje, string pista)
    {
        RectTransform lado    = llenandoDerecho ? ladoDerecho : ladoIzquierdo;
        float         yActual = AlturaOcupada(lado);

        GameObject go = Instantiate(prefabPista, lado);

        LayoutElement le = go.GetComponent<LayoutElement>();
        if (le == null) le = go.AddComponent<LayoutElement>();
        le.ignoreLayout = true;

        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            if (fuente != null) tmp.font = fuente;
            tmp.fontSize           = tamanoFuente;
            tmp.color              = colorTexto;
            tmp.enableWordWrapping = true;
            tmp.alignment          = TextAlignmentOptions.TopLeft;
            tmp.text = "<" + NombreEstilo() + ">" + personaje + "</" + NombreEstilo() + ">\n" + pista;
        }

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin        = new Vector2(0f, 1f);
        rt.anchorMax        = new Vector2(0f, 1f);
        rt.pivot            = new Vector2(0f, 1f);
        rt.sizeDelta        = new Vector2(anchoTexto, 9999f);
        rt.anchoredPosition = new Vector2(paddingLateral, -yActual);

        yield return null;
        yield return null;

        float altura = tmp != null ? tmp.preferredHeight : 20f;
        rt.sizeDelta = new Vector2(anchoTexto, altura);

        // Overflow: si la pista se pasa del límite, moverla al lado derecho
        if (!llenandoDerecho && (yActual + altura) > alturaMaximaPagina)
        {
            float yDer = AlturaOcupada(ladoDerecho);
            go.transform.SetParent(ladoDerecho, false);
            rt.anchoredPosition = new Vector2(paddingLateral, -yDer);
            llenandoDerecho = true;
        }
    }

    string NombreEstilo()
    {
        if (estiloNombre == FontStyles.Bold)   return "b";
        if (estiloNombre == FontStyles.Italic) return "i";
        return "b";
    }

    void OnValidate()
    {
        if (!Application.isPlaying) return;
        ReposicionarLado(ladoIzquierdo);
        ReposicionarLado(ladoDerecho);
    }

    void ReposicionarLado(RectTransform lado)
    {
        if (lado == null) return;
        float y = paddingTop;
        foreach (Transform child in lado)
        {
            RectTransform rt = child as RectTransform;
            if (rt == null) continue;
            rt.anchoredPosition = new Vector2(paddingLateral, -y);
            rt.sizeDelta        = new Vector2(anchoTexto, rt.sizeDelta.y);
            y += rt.sizeDelta.y + espaciadoEntrePistas;
        }
    }
}
