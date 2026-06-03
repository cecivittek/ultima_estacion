using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoManager : MonoBehaviour
{
    private string[][] dialogoMessi = new string[][]
    {
        new string[] { "Messi", "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi", "Miro mucho. La gente no se da cuenta, pero analizo todo. Como en la cancha. Hay alguien acá que se mueve distinto. No nervioso. Calculado." },
        new string[] { "Messi", "Vi a alguien exhalar cuando cerraron las puertas. No de alivio. De satisfacción. Esta persona cree que ya ganó." },
        new string[] { "Messi", "Cuidamela eh" },
        new string[] { "Messi", "Ya te dije todo lo que sé. ¿Tenés algo para comer?" },
    };

    private int indiceDialogo = 0;
    private bool dialogoActivo = false;

   void Update()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        IniciarDialogoMessi();
    }
    if (dialogoActivo && Input.GetKeyDown(KeyCode.Space))
    {
        SiguienteLinea();
    }
}

    public void IniciarDialogoMessi()
    {
        indiceDialogo = 0;
        dialogoActivo = true;
        Debug.Log(dialogoMessi[indiceDialogo][0] + ": " + dialogoMessi[indiceDialogo][1]);
    }

    void SiguienteLinea()
    {
        indiceDialogo++;
        if (indiceDialogo >= dialogoMessi.Length)
        {
            dialogoActivo = false;
            Debug.Log("--- Fin del diálogo ---");
            return;
        }
        Debug.Log(dialogoMessi[indiceDialogo][0] + ": " + dialogoMessi[indiceDialogo][1]);
    }
}
