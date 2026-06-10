using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeTrigger : MonoBehaviour
{
    public string nombrePersonaje;
    private GameObject fondoDialogos;
    private GameObject fondoPrincipal;
    private GameObject panelDialogo;
    private DialogoManager dialogoManager;

    void Start()
    {
        fondoPrincipal = GameObject.Find("fondo_principal");
        dialogoManager = GameObject.Find("DialogoManager").GetComponent<DialogoManager>();

        SpriteRenderer[] todos = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
        foreach (SpriteRenderer sr in todos)
        {
            if (sr.gameObject.scene.isLoaded && sr.gameObject.name == "fondo_dialogos")
                fondoDialogos = sr.gameObject;
        }

        Canvas canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();
        if (canvas != null)
            panelDialogo = canvas.transform.Find("PanelDialogo")?.gameObject;
    }

    void OnMouseDown()
    {
        GameObject personajes = GameObject.Find("PERSONAJES");
        if (personajes != null) personajes.SetActive(false);

        fondoPrincipal.SetActive(false);
        fondoDialogos.SetActive(true);

        if (panelDialogo != null) panelDialogo.SetActive(true);

        switch (nombrePersonaje)
        {
            case "Messi": dialogoManager.IniciarDialogoMessi(); break;
            case "Duki": dialogoManager.IniciarDialogoDuki(); break;
            case "Milagros": dialogoManager.IniciarDialogoMilagros(); break;
            case "Wanda": dialogoManager.IniciarDialogoWanda(); break;
            case "Susana": dialogoManager.IniciarDialogoSusana(); break;
            case "Francella": dialogoManager.IniciarDialogoFrancella(); break;
            case "Vagabundo": dialogoManager.IniciarDialogoVagabundo(); break;
        }
    }
}