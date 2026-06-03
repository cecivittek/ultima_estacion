using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeTrigger : MonoBehaviour
{
    public string nombrePersonaje;
    public GameObject fondoDialogos;
    public GameObject fondoPrincipal;
    public GameObject panelDialogo;
    public DialogoManager dialogoManager;

    void OnMouseDown()
    {
        // Cambiar fondos
        fondoPrincipal.SetActive(false);
        fondoDialogos.SetActive(true);
        panelDialogo.SetActive(true);

        // Iniciar dialogo segun personaje
        switch (nombrePersonaje)
        {
            case "Messi":
                dialogoManager.IniciarDialogoMessi();
                break;
            case "Duki":
                dialogoManager.IniciarDialogoDuki();
                break;
            case "Milagros":
                dialogoManager.IniciarDialogoMilagros();
                break;
            case "Wanda":
                dialogoManager.IniciarDialogoWanda();
                break;
            case "Susana":
                dialogoManager.IniciarDialogoSusana();
                break;
            case "Francella":
                dialogoManager.IniciarDialogoFrancella();
                break;
            case "Vagabunda":
                dialogoManager.IniciarDialogoVagabunda();
                break;
        }
    }
}
