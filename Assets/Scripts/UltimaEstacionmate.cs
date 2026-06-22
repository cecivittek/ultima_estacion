using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UltimaEstacionmate : MonoBehaviour
{
    public void Volver()
    {
        StartCoroutine(FadeYVolver());
    }

    IEnumerator FadeYVolver()
    {
        GameObject canvasFade = new GameObject("CanvasFade");
        Canvas canvas = canvasFade.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        canvasFade.AddComponent<CanvasScaler>();
        canvasFade.AddComponent<GraphicRaycaster>();

        GameObject imgObj = new GameObject("Fade");
        imgObj.transform.SetParent(canvasFade.transform, false);
        Image img = imgObj.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0);
        RectTransform rect = img.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        float tiempo = 0f;
        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime;
            img.color = new Color(0, 0, 0, tiempo);
            yield return null;
        }

        string anterior = PlayerPrefs.GetString("escenaAnterior", "MenuInicio");
        SceneManager.LoadScene(anterior);
    }
}