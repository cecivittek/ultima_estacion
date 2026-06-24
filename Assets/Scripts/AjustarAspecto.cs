using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AjustarAspecto : MonoBehaviour
{
    // Aspecto objetivo: 16:9
    private float aspectoObjetivo = 16f / 9f;

    void Start()
    {
        AjustarCamara();
    }

    void Update()
    {
        AjustarCamara();
    }

    void AjustarCamara()
    {
        Camera cam = GetComponent<Camera>();
        float aspectoVentana = (float)Screen.width / (float)Screen.height;
        float escala = aspectoVentana / aspectoObjetivo;

        Rect rect = cam.rect;

        if (escala < 1f)
        {
            // Ventana más angosta: barras arriba y abajo
            rect.width = 1f;
            rect.height = escala;
            rect.x = 0;
            rect.y = (1f - escala) / 2f;
        }
        else
        {
            // Ventana más ancha: barras a los costados
            float escalaAncho = 1f / escala;
            rect.width = escalaAncho;
            rect.height = 1f;
            rect.x = (1f - escalaAncho) / 2f;
            rect.y = 0;
        }

        cam.rect = rect;
    }
}