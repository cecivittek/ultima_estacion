using UnityEngine;
using UnityEngine.UI;

public class FondoSubte : MonoBehaviour
{
    [Header("Referencias")]
    public RawImage imagenTunel;
    public GameObject panelEstacion;

    [Header("Scroll túnel")]
    public float velocidadScroll = 0.08f;

    [Header("Estación")]
    public float duracionEstacion = 2.5f;

    private float timerEstacion = 0f;
    private int ultimaEstacion  = -1;

    void Update()
    {
        if (imagenTunel != null)
        {
            Rect uv = imagenTunel.uvRect;
            uv.x += velocidadScroll * Time.deltaTime;
            imagenTunel.uvRect = uv;
        }

        if (HUDManager.instancia != null)
        {
            int estacion = HUDManager.instancia.estacionActual;
            if (estacion != ultimaEstacion)
            {
                ultimaEstacion = estacion;
                MostrarEstacion();
            }
        }

        if (timerEstacion > 0f)
        {
            timerEstacion -= Time.deltaTime;
            if (timerEstacion <= 0f && panelEstacion != null)
                panelEstacion.SetActive(false);
        }
    }

    void MostrarEstacion()
    {
        if (panelEstacion == null) return;
        panelEstacion.SetActive(true);
        timerEstacion = duracionEstacion;
    }
}
