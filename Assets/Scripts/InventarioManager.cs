using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instancia;

    public Transform contenido;
    public System.Action<string> alClickearObjeto;
    public bool modoSeleccion = false;

    [Header("Objeto inicial")]
    public string objetoInicial = "pelota";

    [Header("Sprites de objetos")]
    public Sprite spritePelota;
    public Sprite spriteCopa;
    public Sprite spriteAutografo;
    public Sprite spriteScreenshots;
    public Sprite spriteCartera;
    public Sprite spriteMartinFierro;
    public Sprite spriteMate;
    public Sprite spriteCigarrillo;
    public Sprite spriteCelular;

    [Header("Layout")]
    public float escala = 80f;
    public float espaciado = 10f;
    public Vector2 posicionInicio = Vector2.zero;

    private Dictionary<string, Sprite> sprites;
    private Dictionary<string, GameObject> slots = new Dictionary<string, GameObject>();
    private bool inicializado = false;

    void Awake()
    {
        instancia = this;
        sprites = new Dictionary<string, Sprite>
        {
            { "pelota",       spritePelota },
            { "copa",         spriteCopa },
            { "autografo",    spriteAutografo },
            { "screenshots",  spriteScreenshots },
            { "cartera",      spriteCartera },
            { "martinFierro", spriteMartinFierro },
            { "mate",         spriteMate },
            { "cigarrillo",   spriteCigarrillo },
            { "celular",      spriteCelular },
        };
    }

    void OnEnable()
    {
        if (!inicializado)
        {
            inicializado = true;

            if (contenido == null)
                contenido = transform;

            AgregarObjeto(objetoInicial, silencioso: true);
        }
    }

    public void AgregarObjeto(string id, bool silencioso = false)
    {
        if (slots.ContainsKey(id)) return;

        GameObject slot = new GameObject(id, typeof(RectTransform));
        slot.transform.SetParent(contenido, false);
        slot.transform.localScale = Vector3.one;

        LayoutElement le = slot.AddComponent<LayoutElement>();
        le.ignoreLayout = true;

        RectTransform slotRt = slot.GetComponent<RectTransform>();
        slotRt.anchorMin = new Vector2(0, 0.5f);
        slotRt.anchorMax = new Vector2(0, 0.5f);
        slotRt.pivot = new Vector2(0, 0.5f);
        slotRt.sizeDelta = new Vector2(escala, escala);
        slotRt.anchoredPosition = posicionInicio + new Vector2(slots.Count * (escala + espaciado), 0);

        GameObject spriteGO = new GameObject("sprite", typeof(RectTransform), typeof(Image));
        spriteGO.transform.SetParent(slot.transform, false);
        spriteGO.transform.localScale = Vector3.one;

        RectTransform spriteRt = spriteGO.GetComponent<RectTransform>();
        spriteRt.anchorMin = new Vector2(0.5f, 0.5f);
        spriteRt.anchorMax = new Vector2(0.5f, 0.5f);
        spriteRt.pivot = new Vector2(0.5f, 0.5f);
        spriteRt.sizeDelta = new Vector2(escala, escala);
        spriteRt.anchoredPosition = Vector2.zero;

        Image img = spriteGO.GetComponent<Image>();

        if (sprites.TryGetValue(id, out Sprite sp) && sp != null)
            img.sprite = sp;

        img.preserveAspect = true;
        img.color = Color.white;
        img.raycastTarget = true;

        Button boton = spriteGO.AddComponent<Button>();

        string idCapturado = id;

        boton.onClick.AddListener(() =>
        {
            if (clicks.instancia != null)
                clicks.instancia.ReproducirClic();

            Debug.Log("CLICK en objeto: " + idCapturado + " | modoSeleccion: " + modoSeleccion);

            if (modoSeleccion && alClickearObjeto != null)
                alClickearObjeto(idCapturado);
        });

        slots[id] = slot;

        if (!silencioso)
            HUDManager.instancia?.MostrarObjetoNuevo();
    }

    public void QuitarObjeto(string id)
    {
        if (!slots.TryGetValue(id, out GameObject slot)) return;

        Transform spriteT = slot.transform.Find("sprite");

        if (spriteT != null)
        {
            Image img = spriteT.GetComponent<Image>();

            if (img != null)
                img.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    void RearmarPosiciones()
    {
        int i = 0;

        foreach (var s in slots.Values)
        {
            if (s == null) continue;

            RectTransform slotRt = s.GetComponent<RectTransform>();
            slotRt.sizeDelta = new Vector2(escala, escala);
            slotRt.anchoredPosition = posicionInicio + new Vector2(i * (escala + espaciado), 0);

            Transform spriteT = s.transform.Find("sprite");

            if (spriteT != null)
            {
                RectTransform spriteRt = spriteT.GetComponent<RectTransform>();
                spriteRt.sizeDelta = new Vector2(escala, escala);
            }

            i++;
        }
    }

    void OnValidate()
    {
        escala = Mathf.Max(escala, 10f);

        if (Application.isPlaying)
            RearmarPosiciones();
    }
}