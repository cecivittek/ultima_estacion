using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instancia;

    public Transform contenido;

    [Header("Sprites de objetos")]
    public Sprite spritePelota;
    public Sprite spriteCopa;
    public Sprite spriteAutografo;
    public Sprite spriteScreenshots;
    public Sprite spriteCartera;
    public Sprite spriteMartinFierro;
    public Sprite spriteMate;

    public float tamanoSlot = 100f;
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
        };
    }

    void OnEnable()
    {
        if (!inicializado)
        {
            inicializado = true;
            if (contenido == null)
                contenido = transform;
            AgregarObjeto("pelota");
        }
    }

    public void AgregarObjeto(string id)
    {
        if (slots.ContainsKey(id)) return;

        GameObject slot = new GameObject(id, typeof(RectTransform), typeof(Image));
        slot.transform.SetParent(contenido, false);

        RectTransform rt = slot.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(0, 0.5f);
        rt.pivot     = new Vector2(0, 0.5f);
        rt.sizeDelta = new Vector2(100f, 100f);
        rt.anchoredPosition = posicionInicio + new Vector2(slots.Count * (tamanoSlot + espaciado), 0);

        float escala = tamanoSlot / 100f;
        slot.transform.localScale = new Vector3(escala, escala, 1f);

        Image img = slot.GetComponent<Image>();
        if (sprites.TryGetValue(id, out Sprite sp) && sp != null)
            img.sprite = sp;
        img.preserveAspect = true;
        img.color = Color.white;

        slots[id] = slot;
        Debug.Log("Inventario: agregado " + id + " en posicion " + rt.anchoredPosition);
    }

    public void QuitarObjeto(string id)
    {
        if (!slots.TryGetValue(id, out GameObject slot)) return;
        Destroy(slot);
        slots.Remove(id);
        RearmarPosiciones();
    }

    void RearmarPosiciones()
    {
        float escala = tamanoSlot / 100f;
        int i = 0;
        foreach (var s in slots.Values)
        {
            if (s == null) continue;
            s.transform.localScale = new Vector3(escala, escala, 1f);
            RectTransform rt = s.GetComponent<RectTransform>();
            rt.anchoredPosition = posicionInicio + new Vector2(i * (tamanoSlot + espaciado), 0);
            i++;
        }
    }

    void OnValidate()
    {
        tamanoSlot = Mathf.Max(tamanoSlot, 10f);
        if (Application.isPlaying)
            RearmarPosiciones();
    }
}
