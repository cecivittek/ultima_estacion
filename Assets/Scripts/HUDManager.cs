using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instancia;

    [Header("Mapa")]
    public RectTransform marcadorEstacion;
    public float posicionXInicio = -480f; // extremo izquierdo del mapa
    public float posicionXFin = 480f;     // extremo derecho del mapa

    [Header("Paneles")]
    public GameObject panelInventario;
    public GameObject panelAnotador;
    public GameObject fondoOscuro;
    public GameObject panelObjetoNuevo;

    private float timerObjetoNuevo    = 0f;
    private bool  notificacionPendiente = false;
    public int estacionActual = 0;
    private int totalEstaciones = 16;
    private float tiempoTotal = 15f * 60f;
    private float tiempoPorEstacion;
    private float timer = 0f;

    private float tiempoTranscurridoTotal = 0f;
    private bool acusacionDisparada = false;

    void Awake()
    {
        instancia = this;
    }

    void OnEnable()
    {
        if (notificacionPendiente)
        {
            notificacionPendiente = false;
            MostrarObjetoNuevo();
        }
    }

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

        if (timerObjetoNuevo > 0f)
        {
            timerObjetoNuevo -= Time.deltaTime;
            if (timerObjetoNuevo <= 0f && panelObjetoNuevo != null)
                panelObjetoNuevo.SetActive(false);
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

    public void MostrarObjetoNuevo()
    {
        if (panelObjetoNuevo == null) return;

        // Si el HUD está inactivo (durante un diálogo), guardar para después
        if (!gameObject.activeInHierarchy)
        {
            notificacionPendiente = true;
            return;
        }

        CanvasGroup cg = panelObjetoNuevo.GetComponent<CanvasGroup>();
        if (cg == null) cg = panelObjetoNuevo.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;
        cg.interactable   = false;

        panelObjetoNuevo.SetActive(true);
        timerObjetoNuevo = 5f;
    }

    public void IrAAcusacion()
    {
        SceneManager.LoadScene("Acusacion");
    }
}