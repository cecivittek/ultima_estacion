using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [Header("Mapa")]
    public RectTransform marcadorEstacion;
    public float posicionXInicio = -480f; // extremo izquierdo del mapa
    public float posicionXFin = 480f;     // extremo derecho del mapa

    [Header("Paneles")]
    public GameObject panelInventario;
    public GameObject panelAnotador;
    public GameObject fondoOscuro;

    private int estacionActual = 0;
    private int totalEstaciones = 16;
    private float tiempoTotal = 26f * 60f;
    private float tiempoPorEstacion;
    private float timer = 0f;

    private float tiempoTranscurridoTotal = 0f;
    private bool acusacionDisparada = false;

    void Start()
    {
        tiempoPorEstacion = tiempoTotal / totalEstaciones;
        panelInventario.SetActive(false);
        panelAnotador.SetActive(false);
        ActualizarMarcador();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelInventario.SetActive(false);
            panelAnotador.SetActive(false);
            fondoOscuro.SetActive(false);
        }

        if (!acusacionDisparada)
        {
            tiempoTranscurridoTotal += Time.deltaTime;

            if (tiempoTranscurridoTotal >= tiempoTotal)
            {
                acusacionDisparada = true;
                IrAAcusacion();
                return;
            }
        }

        if (estacionActual >= totalEstaciones) return;

        timer += Time.deltaTime;

        if (timer >= tiempoPorEstacion)
        {
            timer = 0f;
            estacionActual++;
            ActualizarMarcador();
        }
    }

    void ActualizarMarcador()
    {
        float progreso = (float)estacionActual / (totalEstaciones - 1);
        float nuevaX = Mathf.Lerp(posicionXInicio, posicionXFin, progreso);
        marcadorEstacion.anchoredPosition = new Vector2(nuevaX, marcadorEstacion.anchoredPosition.y);
    }

    public void AbrirInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
        fondoOscuro.SetActive(!fondoOscuro.activeSelf);
        panelAnotador.SetActive(false);
    }

    public void CerrarInventario()
    {
        panelInventario.SetActive(false);
        fondoOscuro.SetActive(false);
    }

    public void AbrirAnotador()
    {
        panelAnotador.SetActive(!panelAnotador.activeSelf);
        panelInventario.SetActive(false);
    }

    public void CerrarAnotador()
    {
        panelAnotador.SetActive(false);
    }

    public void IrAAcusacion()
    {
        SceneManager.LoadScene("Acusacion");
    }
}