using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class iraescena : MonoBehaviour
{
    public void IrAEscena(string nombreEscena)
    {
        StartCoroutine(FadeYCambiarEscena(nombreEscena));
    }

    IEnumerator FadeYCambiarEscena(string nombreEscena)
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
        float duracion = 1f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            img.color = new Color(0, 0, 0, tiempo / duracion);
            yield return null;
        }

        PlayerPrefs.SetString("escenaAnterior", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(nombreEscena);
    }
}