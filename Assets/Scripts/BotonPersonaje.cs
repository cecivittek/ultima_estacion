using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class BotonPersonaje : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string nombrePersonaje;

    [Header("Color del contorno verde")]
    public Color colorContorno = new Color(0.3f, 1f, 0.3f);

    [Header("Grosor del contorno (porcentaje del tamaño de la foto)")]
    [Range(0.0005f, 0.08f)]
    public float grosorContornoPorcentaje = 0.02f;

    [HideInInspector] public Image fondo;
    private GameObject contornoVerde;
    private bool estaSeleccionado = false;

    private AcusacionManager manager;

    private void Awake()
    {
        fondo = GetComponent<Image>();
        manager = FindFirstObjectByType<AcusacionManager>();

        Image foto = null;
        Image[] imagenes = GetComponentsInChildren<Image>(true);
        foreach (Image img in imagenes)
        {
            if (img != fondo)
            {
                foto = img;
                break;
            }
        }

        if (foto != null)
            CrearContornoVerde(foto);
    }

    private void CrearContornoVerde(Image foto)
    {
        RectTransform rtFoto = foto.GetComponent<RectTransform>();

        contornoVerde = new GameObject("ContornoVerde", typeof(RectTransform));
        contornoVerde.transform.SetParent(foto.transform.parent, false);
        contornoVerde.transform.SetSiblingIndex(0);

        // Asegurarse que el padre no bloquee raycasts
        Image imgPadre = contornoVerde.AddComponent<Image>();
        imgPadre.color = new Color(0, 0, 0, 0);
        imgPadre.raycastTarget = false;

        RectTransform rtContorno = contornoVerde.GetComponent<RectTransform>();
        rtContorno.anchorMin = rtFoto.anchorMin;
        rtContorno.anchorMax = rtFoto.anchorMax;
        rtContorno.pivot = rtFoto.pivot;
        rtContorno.sizeDelta = rtFoto.sizeDelta;
        rtContorno.anchoredPosition = rtFoto.anchoredPosition;

        float menorLado = Mathf.Min(rtFoto.rect.width, rtFoto.rect.height);
        float distancia = menorLado * grosorContornoPorcentaje;

        Vector2[] direcciones =
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };

        foreach (Vector2 dir in direcciones)
        {
            GameObject copia = new GameObject("Copia", typeof(RectTransform), typeof(Image));
            copia.transform.SetParent(contornoVerde.transform, false);

            RectTransform rtCopia = copia.GetComponent<RectTransform>();
            rtCopia.anchorMin = new Vector2(0.5f, 0.5f);
            rtCopia.anchorMax = new Vector2(0.5f, 0.5f);
            rtCopia.pivot = new Vector2(0.5f, 0.5f);
            rtCopia.sizeDelta = rtFoto.sizeDelta;
            rtCopia.anchoredPosition = dir * distancia;

            Image imgCopia = copia.GetComponent<Image>();
            imgCopia.sprite = foto.sprite;
            imgCopia.color = colorContorno;
            imgCopia.preserveAspect = foto.preserveAspect;
            imgCopia.raycastTarget = false;
        }

        contornoVerde.SetActive(false);
    }

    public void MostrarContorno(bool mostrar)
    {
        if (contornoVerde != null)
            contornoVerde.SetActive(mostrar);
    }

    public void MarcarSeleccionado(bool seleccionado)
    {
        estaSeleccionado = seleccionado;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MostrarContorno(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!estaSeleccionado)
            MostrarContorno(false);
    }

    public void AlClickear()
    {
        Debug.Log("AlClickear ejecutado en: " + nombrePersonaje);

        if (manager == null)
            manager = FindFirstObjectByType<AcusacionManager>();

        if (manager == null)
        {
            Debug.LogError("Manager es null!");
            return;
        }

        manager.SeleccionarPersonaje(this);
    }
}