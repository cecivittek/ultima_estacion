using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
{
    // OBJETOS DEL JUGADOR
    private bool tienePelota = true; // empieza con la pelota
    private bool tieneCopa = false;
    private bool tieneAutografo = false;
    private bool tieneScreenshots = false;
    private bool tieneCartera = false;
    private bool tieneMartinFierro = false;
    private bool tieneMate = false;

    // ESTADOS POR PERSONAJE (recuerdan en qué diálogo quedaron)
    private int estadoMessi = 0;
    private int estadoDuki = 0;
    private int estadoMilagros = 0;
    private int estadoWanda = 0;
    private int estadoSusana = 0;
    private int estadoFrancella = 0;
    private int estadoVagabunda = 0;

    private int indiceDialogo = 0;
    private bool dialogoActivo = false;
    private string[][] dialogoActual;
    private string personajeActual = "";

    // UI
    public GameObject panelDialogo;
    public Text textoNombre;
    public Text textoDialogo;

    // ===================== DIALOGOS (igual que antes) =====================
    private string[][] dialogoMessiSinObjeto = new string[][]
    {
        new string[] { "Messi", "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi", "Miro mucho. La gente no se da cuenta, pero analizo todo. Como en la cancha. Hay alguien acá que se mueve distinto. No nervioso. Calculado. Como un jugador que ya sabe cuál va a ser la jugada antes de que empiece." },
    };

    private string[][] dialogoMessiConObjeto = new string[][]
    {
        new string[] { "Messi", "Mira que bueno, nunca hubiera conseguido uno. Dale, te cuento." },
        new string[] { "Messi", "Vi a alguien exhalar cuando cerraron las puertas. No de alivio. De satisfacción. Esta persona cree que ya ganó. Que llegar a Catedral está asegurado." },
        new string[] { "Messi", "Cuidamela eh." },
        new string[] { "Messi", "Ya te dije todo lo que sé. ¿Tenés algo para comer?" },
    };

    // ... (el resto de los diálogos igual que los tenías)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) IniciarDialogoMessi();
        if (Input.GetKeyDown(KeyCode.Alpha2)) IniciarDialogoDuki();
        if (Input.GetKeyDown(KeyCode.Alpha3)) IniciarDialogoMilagros();
        if (Input.GetKeyDown(KeyCode.Alpha4)) IniciarDialogoWanda();
        if (Input.GetKeyDown(KeyCode.Alpha5)) IniciarDialogoSusana();
        if (Input.GetKeyDown(KeyCode.Alpha6)) IniciarDialogoFrancella();
        if (Input.GetKeyDown(KeyCode.Alpha7)) IniciarDialogoVagabunda();

        // Avanzar diálogo con click
        if (dialogoActivo && Input.GetMouseButtonDown(0))
        {
            SiguienteLinea();
        }
    }

    // ===================== INICIAR DIALOGOS =====================
    public void IniciarDialogoMessi()
    {
        if (dialogoActivo) return;
        personajeActual = "Messi";

        if (estadoMessi == 0)
        {
            dialogoActual = dialogoMessiSinObjeto;
            estadoMessi = 1;
        }
        else if (estadoMessi == 1 && tienePelota)
        {
            dialogoActual = dialogoMessiConObjeto;
            tienePelota = false;
            estadoMessi = 2;
        }
        else if (estadoMessi == 1 && !tienePelota)
        {
            // objeto equivocado
            dialogoActual = new string[][] {
                new string[] { "Messi", "Ya te dije todo lo que tengo para decirte. ¿Tenés algo para comer?" }
            };
        }
        else
        {
            // diálogo final repetido
            dialogoActual = new string[][] {
                new string[] { "Messi", "Ya te dije todo lo que sé. ¿Tenés algo para comer?" }
            };
        }

        IniciarDialogo();
    }

    public void IniciarDialogoDuki()
    {
        if (dialogoActivo) return;
        personajeActual = "Duki";

        if (estadoDuki == 0)
        {
            dialogoActual = dialogoDukiSinObjeto;
            estadoDuki = 1;
        }
        else if (estadoDuki == 1 && tieneCopa)
        {
            dialogoActual = dialogoDukiConObjeto;
            tieneCopa = false;
            tieneAutografo = true;
            estadoDuki = 2;
        }
        else if (estadoDuki == 1 && !tieneCopa)
        {
            dialogoActual = new string[][] {
                new string[] { "Duki", "Bro, ¿qué es esto? No me sirve. Guardatelo." }
            };
        }
        else
        {
            dialogoActual = new string[][] {
                new string[] { "Duki", "Ya está, bro. No sé nada más. Estoy componiendo un nuevo tema. No me interrumpas." }
            };
        }

        IniciarDialogo();
    }

    public void IniciarDialogoMilagros()
    {
        if (dialogoActivo) return;
        personajeActual = "Milagros";

        if (estadoMilagros == 0)
        {
            dialogoActual = dialogoMilagrosSinObjeto;
            estadoMilagros = 1;
        }
        else if (estadoMilagros == 1 && tieneAutografo)
        {
            dialogoActual = dialogoMilagrosConObjeto;
            tieneAutografo = false;
            tieneScreenshots = true;
            estadoMilagros = 2;
        }
        else if (estadoMilagros == 1 && !tieneAutografo)
        {
            dialogoActual = new string[][] {
                new string[] { "Milagros Pilar", "Ay, que esperabas, ¿un canje? ¿Tenés algo más interesante?" }
            };
        }
        else
        {
            dialogoActual = new string[][] {
                new string[] { "Milagros Pilar", "Ya te conté todo. Dejame editar el reel." }
            };
        }

        IniciarDialogo();
    }

    public void IniciarDialogoWanda()
    {
        if (dialogoActivo) return;
        personajeActual = "Wanda";

        if (estadoWanda == 0)
        {
            dialogoActual = dialogoWandaSinObjeto;
            estadoWanda = 1;
        }
        else if (estadoWanda == 1 && tieneScreenshots)
        {
            dialogoActual = dialogoWandaConObjeto;
            tieneScreenshots = false;
            tieneCartera = true;
            estadoWanda = 2;
        }
        else if (estadoWanda == 1 && !tieneScreenshots)
        {
            dialogoActual = new string[][] {
                new string[] { "Wanda", "¿Esto me lo traés a mí? No, querida. Guardatelo." }
            };
        }
        else
        {
            dialogoActual = new string[][] {
                new string[] { "Wanda", "Ya te dije un montón, no me hables más." }
            };
        }

        IniciarDialogo();
    }

    public void IniciarDialogoSusana()
    {
        if (dialogoActivo) return;
        personajeActual = "Susana";

        if (estadoSusana == 0)
        {
            dialogoActual = dialogoSusanaSinObjeto;
            estadoSusana = 1;
        }
        else if (estadoSusana == 1 && tieneCartera)
        {
            dialogoActual = dialogoSusanaConObjeto;
            tieneCartera = false;
            tieneMartinFierro = true;
            estadoSusana = 2;
        }
        else if (estadoSusana == 1 && !tieneCartera)
        {
            dialogoActual = new string[][] {
                new string[] { "Susana", "¿Y que hago yo con esto? Traeme algo de valor y hablamos." }
            };
        }
        else
        {
            dialogoActual = new string[][] {
                new string[] { "Susana", "Ya te dije todo, querido. ¿Alguien tiene un Valium?" }
            };
        }

        IniciarDialogo();
    }

    public void IniciarDialogoFrancella()
    {
        if (dialogoActivo) return;
        personajeActual = "Francella";

        if (estadoFrancella == 0)
        {
            dialogoActual = dialogoFrancellaSinObjeto;
            estadoFrancella = 1;
        }
        else if (estadoFrancella == 1 && tieneMartinFierro)
        {
            dialogoActual = dialogoFrancellaConObjeto;
            tieneMartinFierro = false;
            tieneMate = true;
            estadoFrancella = 2;
        }
        else if (estadoFrancella == 1 && !tieneMartinFierro)
        {
            dialogoActual = new string[][] {
                new string[] { "Francella", "Ay, no. Esto no lo necesito. Pero aprecio el gesto." }
            };
        }
        else
        {
            dialogoActual = new string[][] {
                new string[] { "Francella", "Ya te dije todo lo que podía decirte, espero que te sea de ayuda." }
            };
        }

        IniciarDialogo();
    }

    public void IniciarDialogoVagabunda()
    {
        if (dialogoActivo) return;
        personajeActual = "Vagabunda";

        if (estadoVagabunda == 0)
        {
            dialogoActual = dialogoVagabundaSinObjeto;
            estadoVagabunda = 1;
        }
        else if (estadoVagabunda == 1 && tieneMate)
        {
            dialogoActual = dialogoVagabundaConObjeto;
            tieneMate = false;
            estadoVagabunda = 2;
        }
        else if (estadoVagabunda == 1 && !tieneMate)
        {
            dialogoActual = new string[][] {
                new string[] { "Vagabunda", "No, gracias. ¿No tenés algo calentito para darme?" }
            };
        }
        else
        {
            dialogoActual = new string[][] {
                new string[] { "Vagabunda", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." }
            };
        }

        IniciarDialogo();
    }

    // ===================== MOTOR DEL DIÁLOGO =====================
    void IniciarDialogo()
    {
        indiceDialogo = 0;
        dialogoActivo = true;
        panelDialogo.SetActive(true);
        MostrarLineaActual();
    }

    void MostrarLineaActual()
    {
        textoNombre.text = dialogoActual[indiceDialogo][0];
        textoDialogo.text = dialogoActual[indiceDialogo][1];
    }

    void SiguienteLinea()
    {
        indiceDialogo++;
        if (indiceDialogo >= dialogoActual.Length)
        {
            dialogoActivo = false;
            panelDialogo.SetActive(false);
            return;
        }
        MostrarLineaActual();
    }
}