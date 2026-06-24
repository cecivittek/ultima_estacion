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
 
    private GameObject borde;
 
    private void Awake()
    {
        Image fondo = GetComponent<Image>();
        RectTransform rtFondo = GetComponent<RectTransform>();
 
        borde = new GameObject("BordeHover", typeof(RectTransform), typeof(Image));
        borde.transform.SetParent(transform.parent, false); // hermano del boton, no hijo
        borde.transform.SetSiblingIndex(0); // primero en la lista = atras de todo
 
        RectTransform rtBorde = borde.GetComponent<RectTransform>();
        rtBorde.anchorMin = rtFondo.anchorMin;
        rtBorde.anchorMax = rtFondo.anchorMax;
        rtBorde.pivot = rtFondo.pivot;
        rtBorde.anchoredPosition = rtFondo.anchoredPosition;
        rtBorde.sizeDelta = rtFondo.sizeDelta + new Vector2(margenBorde, margenBorde) * 2f;
 
        Image imgBorde = borde.GetComponent<Image>();
        imgBorde.sprite = fondo.sprite;
        imgBorde.type = fondo.type;
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
}
 