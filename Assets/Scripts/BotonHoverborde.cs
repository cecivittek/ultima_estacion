using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
[RequireComponent(typeof(Image))]
public class BotonHoverBorde : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Color del borde al pasar el mouse")]
    public Color colorBorde = new Color(0.2f, 0.8f, 0.2f);
 
    [Header("Cuanto mas grande es el borde que el boton, en pixeles")]
    public float margenBorde = 6f;
 
    [Header("Forzar borde simple (sin 9-slice). Activar en botones muy alargados donde el sprite Sliced deforma el contorno.")]
    public bool forzarSimple = false;
 
    private GameObject borde;
 
    private void Awake()
    {
        Image fondo = GetComponent<Image>();
        RectTransform rtFondo = GetComponent<RectTransform>();
 
        borde = new GameObject("BordeHover", typeof(RectTransform), typeof(Image));
        borde.transform.SetParent(transform.parent, false); // hermano del boton, no hijo
        borde.transform.SetSiblingIndex(transform.GetSiblingIndex()); // justo antes que el boton (detras de el, no de todo el Canvas)
 
        RectTransform rtBorde = borde.GetComponent<RectTransform>();
        rtBorde.anchorMin = rtFondo.anchorMin;
        rtBorde.anchorMax = rtFondo.anchorMax;
        rtBorde.pivot = rtFondo.pivot;
        rtBorde.anchoredPosition = rtFondo.anchoredPosition;
        rtBorde.sizeDelta = rtFondo.sizeDelta + new Vector2(margenBorde, margenBorde) * 2f;
 
        Image imgBorde = borde.GetComponent<Image>();
        imgBorde.sprite = fondo.sprite;
        imgBorde.type = forzarSimple ? Image.Type.Simple : fondo.type; // Simple solo si el sprite Sliced deforma el contorno
        imgBorde.color = colorBorde;
        imgBorde.raycastTarget = false;
 
        borde.SetActive(false);
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        borde.SetActive(true);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        borde.SetActive(false);
    }
 
    // Si el boton se desactiva con el mouse encima, Unity NO dispara OnPointerExit,
    // asi que el borde quedaria visible y huerfano. Lo apagamos a mano aca.
    private void OnDisable()
    {
        if (borde != null) borde.SetActive(false);
    }
}
 