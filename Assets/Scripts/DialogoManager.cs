using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    // ===================== REFERENCIAS UI =====================
    public TextMeshProUGUI textNombre;       // Texto del nombre del personaje
    public TextMeshProUGUI textDialogo;      // Texto del dialogo
    public Button botonSiguiente;           // Boton "Siguiente"
    public Button botonDarObjeto;           // Boton "Dar objeto"
    public GameObject panelDialogo;         // El panel azul
    public GameObject fondoDialogos;        // fondo_dialogos
    public GameObject fondoPrincipal;       // fondo_principal

    // ===================== INVENTARIO =====================
    private bool tienePelota = true;        // El jugador empieza con la pelota
    private bool tieneCigarrillo = false;
    private bool tieneScreenshots = false;
    private bool tieneCartera = false;
    private bool tieneMartinFierro = false;
    private bool tieneMate = false;
    private bool tieneAutografo = false;
    private bool tieneCopa = false;

    // ===================== ESTADO DIALOGO =====================
    private int indiceDialogo = 0;
    private string[][] dialogoActual;
    private string personajeActual = "";

    // Indices donde aparece el boton "Dar objeto" (ultima linea antes de dar)
    // y que objeto se entrega a cada personaje
    private int indiceBotonDar = -1;

    // ===================== DIALOGOS MESSI =====================
    private string[][] dialogoMessiSinObjeto = new string[][]
    {
        new string[] { "Messi", "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi", "Miro mucho. La gente no se da cuenta, pero analizo todo. Como en la cancha. Hay alguien acá que se mueve distinto. No nervioso. Calculado. Como un jugador que ya sabe cuál va a ser la jugada antes de que empiece." },
        new string[] { "Messi", "No, esto no es para mí. Pero gracias igual." },
    };

    private string[][] dialogoMessiConObjeto = new string[][]
    {
        new string[] { "Messi", "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi", "Miro mucho. La gente no se da cuenta, pero analizo todo. Como en la cancha. Hay alguien acá que se mueve distinto. No nervioso. Calculado. Como un jugador que ya sabe cuál va a ser la jugada antes de que empiece." },
        new string[] { "Jugador", "[Dar pelota]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Messi", "Mira que bueno, nunca hubiera conseguido uno, Anto se va a volver loca. Dale, te cuento." },
        new string[] { "Messi", "Vi a alguien exhalar cuando cerraron las puertas. No de alivio. De satisfacción. Es la cara de un jugador cuando el partido ya está ganado. Estaba cantando algo. Esta persona cree que ya ganó. Que llegar a Catedral está asegurado." },
        new string[] { "Messi", "Cuidámela eh." },
        new string[] { "Messi", "Ya te dije todo lo que sé. ¿Tenés algo para tomar?" },
    };

    // ===================== DIALOGOS DUKI =====================
    private string[][] dialogoDukiSinObjeto = new string[][]
    {
        new string[] { "Duki", "¿Qué onda? Todo esto es una locura, bro. Venía a bancar a De Paul y ahora estoy huyendo de un T-Rex. Si esto no es una letra de trap, no sé qué es." },
        new string[] { "Jugador", "¿Escuchaste algo raro?" },
        new string[] { "Duki", "Mirá, tenía un auricular puesto pero el otro libre. Escuché una voz decir 'Catedral' con demasiada calma. En medio del caos, eso no es normal, bro. Y escuché algo como 'ya está todo listo'. Dicho bajito. Como para sí mismo." },
        new string[] { "Duki", "Bro, ¿qué es esto? No me sirve. Guardatelo." },
    };

    private string[][] dialogoDukiConObjeto = new string[][]
    {
        new string[] { "Duki", "¿Qué onda? Todo esto es una locura, bro. Venía a bancar a De Paul y ahora estoy huyendo de un T-Rex. Si esto no es una letra de trap, no sé qué es." },
        new string[] { "Jugador", "¿Escuchaste algo raro?" },
        new string[] { "Duki", "Mirá, tenía un auricular puesto pero el otro libre. Escuché una voz decir 'Catedral' con demasiada calma. En medio del caos, eso no es normal, bro. Y escuché algo como 'ya está todo listo'. Dicho bajito. Como para sí mismo." },
        new string[] { "Jugador", "[Dar cigarrillo]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Duki", "¡Naaa, como sabías que necesitaba uno? En este contexto no hago preguntas. Dale, te cuento algo más." },
        new string[] { "Duki", "La voz que escuché era de una mina. Hablaba en voz muy baja pero era claramente una mujer. Dijo algo raro: 'me voy a hacer viral'. En medio de un ataque de dinosaurios. Alguien que piensa en las redes ahora... me parece algo tétrico." },
        new string[] { "Duki", "Tomá. No hago favores pero acá tenés. Mi autógrafo." },
        new string[] { "Duki", "Ya está, bro. No sé nada más. Estoy inspirándome para componer un nuevo tema. No me interrumpas." },
    };

    // ===================== DIALOGOS MILAGROS PILAR =====================
    private string[][] dialogoMilagrosSinObjeto = new string[][]
    {
        new string[] { "Milagros Pilar", "¡Chicos, no se imaginan esto! ¡Un dinosaurio de verdad! Ay, hola. ¿Me seguís? No importa. ¿Tenés buena luz acá? Es una tragedia, obvio. Pero el contenido... el contenido es increíble." },
        new string[] { "Jugador", "¿Viste algo sospechoso?" },
        new string[] { "Milagros Pilar", "Yo capturo todo, es casi un reflejo. Vi a alguien que subió al subte sin correr. Todos corrían y esta persona caminaba. Caminaba como si supiera que el subte la iba a esperar." },
        new string[] { "Milagros Pilar", "Ay, que esperabas, ¿un canje? ¿Tenés algo más interesante?" },
    };

    private string[][] dialogoMilagrosConObjeto = new string[][]
    {
        new string[] { "Milagros Pilar", "¡Chicos, no se imaginan esto! ¡Un dinosaurio de verdad! Ay, hola. ¿Me seguís? No importa. ¿Tenés buena luz acá? Es una tragedia, obvio. Pero el contenido... el contenido es increíble." },
        new string[] { "Jugador", "¿Viste algo sospechoso?" },
        new string[] { "Milagros Pilar", "Yo capturo todo, es casi un reflejo. Vi a alguien que subió al subte sin correr. Todos corrían y esta persona caminaba. Caminaba como si supiera que el subte la iba a esperar." },
        new string[] { "Jugador", "[Dar autógrafo de Duki]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Milagros Pilar", "¡DUKI! ¡Ay, me muero literalmente! ¿Cómo lo conseguiste? ¿Me lo das? ¿Lo puedo postear? Okay okay." },
        new string[] { "Milagros Pilar", "Te cuento algo que no le dije a nadie. Antes de subir, en el caos, vi a alguien con el control en la mano. Lo guardó cuando empezaron a mirar. Era una chica. Con cadenas, ropa oscura." },
        new string[] { "Milagros Pilar", "Mirá estas capturas. Son mi chisme mejor guardado." },
        new string[] { "Milagros Pilar", "Ya te conté todo, gordi. Ahora dejame que tengo que editar el reel de los dinosaurios. ¿Sabés el alcance que va a tener?" },
    };

    // ===================== DIALOGOS WANDA =====================
    private string[][] dialogoWandaSinObjeto = new string[][]
    {
        new string[] { "Wanda", "Esto es un desastre absoluto. No puedo correr con estos tacos. ¿Vos quién sos? No te conozco ¿Trabajas para mí?" },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Wanda", "Miro todo. Es un don. Vi a alguien que cuando apareció el control, no puso cara de sorpresa. Todo el mundo abrió los ojos. Pero esta persona, cara de nada. Yo sé lo que es actuar sorpresa. Lo hice en televisión por años." },
        new string[] { "Wanda", "¿Esto me lo traés a mí? No, querida. Guardátelo." },
    };

    private string[][] dialogoWandaConObjeto = new string[][]
    {
        new string[] { "Wanda", "Esto es un desastre absoluto. No puedo correr con estos tacos. ¿Vos quién sos? No te conozco ¿Trabajas para mí?" },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Wanda", "Miro todo. Es un don. Vi a alguien que cuando apareció el control, no puso cara de sorpresa. Todo el mundo abrió los ojos. Pero esta persona, cara de nada. Yo sé lo que es actuar sorpresa. Lo hice en televisión por años." },
        new string[] { "Jugador", "[Dar screenshots]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Wanda", "Ay, ay, ay. Me muero. ¿Sabes todo lo que puedo conseguir con esta captura de pantalla? Lo hundo como el Titanic en dos segundos si quiero." },
        new string[] { "Wanda", "No te voy a decir el nombre porque me tengo que guardar algo siempre para mí. Pero esta persona estaba parada como si estuviera en el escenario. La gente del espectáculo con experiencia se para así, no es para novatos." },
        new string[] { "Wanda", "Tomá, se le cayó a alguien en el caos y ya tengo esta edición." },
        new string[] { "Wanda", "Ya te dije un montón, no me hables más." },
    };

    // ===================== DIALOGOS SUSANA =====================
    private string[][] dialogoSusanaSinObjeto = new string[][]
    {
        new string[] { "Susana", "¡Esto es una pesadilla! ¡Yo tenía grabación mañana! ¿Y vos quién sos, querido? En esta situación todos somos iguales. Bueno, casi." },
        new string[] { "Jugador", "¿Vio algo sospechoso?" },
        new string[] { "Susana", "Mirá, yo tengo ojo clínico. Años de entrevistar gente. Hay alguien acá que tiene un aura rara. Alguien que parece que no se sorprendió cuando vió los dinosaurios." },
        new string[] { "Susana", "¿Y que hago yo con esto? Traeme algo de valor y hablamos." },
    };

    private string[][] dialogoSusanaConObjeto = new string[][]
    {
        new string[] { "Susana", "¡Esto es una pesadilla! ¡Yo tenía grabación mañana! ¿Y vos quién sos, querido? En esta situación todos somos iguales. Bueno, casi." },
        new string[] { "Jugador", "¿Vio algo sospechoso?" },
        new string[] { "Susana", "Mirá, yo tengo ojo clínico. Años de entrevistar gente. Hay alguien acá que tiene un aura rara. Alguien que parece que no se sorprendió cuando vió los dinosaurios." },
        new string[] { "Jugador", "[Dar cartera Louis Vuitton]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Susana", "¡MI CARTERA! ¡Ay, pensé que la había perdido para siempre! Las fotos de Pacha... el rouge... todo está. Vos sos un ángel." },
        new string[] { "Susana", "Vi la cara del infiltrado, querido. Cuando encontraron el control, todos miraron al control. Esta persona miró a los demás. Era un chico joven. Del ambiente artístico, me parece. Tiene esa presencia escénica. Pero en ese momento no quería que lo vieran. Eso me llamó la atención." },
        new string[] { "Susana", "Merecés un premio por toda esta investigación. Tomá." },
        new string[] { "Susana", "Ya te dije todo, querido. Ahora necesito sentarme. Este vagón está muy frío." },
    };

    // ===================== DIALOGOS FRANCELLA =====================
    private string[][] dialogoFrancellaSinObjeto = new string[][]
    {
        new string[] { "Francella", "¡Eh! ¡Qué cosa, no! ¡Quién lo iba a decir! Dinosaurios. En Buenos Aires. Esto parece de película." },
        new string[] { "Jugador", "¿Notaste algo raro?" },
        new string[] { "Francella", "Notás cosas cuando sos actor. Hay alguien acá que está actuando el miedo. Y el miedo actuado tiene un ritmo distinto al miedo real. Lo noto en la respiración, en los ojos. Años de oficio." },
        new string[] { "Francella", "Ay, no. Esto no lo necesito. Pero aprecio el gesto." },
    };

    private string[][] dialogoFrancellaConObjeto = new string[][]
    {
        new string[] { "Francella", "¡Eh! ¡Qué cosa, no! ¡Quién lo iba a decir! Dinosaurios. En Buenos Aires. Esto parece de película." },
        new string[] { "Jugador", "¿Notaste algo raro?" },
        new string[] { "Francella", "Notás cosas cuando sos actor. Hay alguien acá que está actuando el miedo. Y el miedo actuado tiene un ritmo distinto al miedo real. Lo noto en la respiración, en los ojos. Años de oficio." },
        new string[] { "Jugador", "[Dar Martín Fierro]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Francella", "¡No te puedo creer! ¡Es la verdad, que locura! Ahora yo también sé cuanto pesa la copa del mundo, esta pesadita eh. Está bien. Te cuento todo." },
        new string[] { "Francella", "Vi a alguien acariciar el control con el pulgar. Así, suave. Como quien tiene algo que le da seguridad. Tenía anillos, grandes. Lo vi claramente." },
        new string[] { "Francella", "No tengo nada para vos, disculpá, salí corriendo y no agarré nada. Pero te puedo ofrecer un mate calentito." },
        new string[] { "Francella", "Ya te dije todo lo que podía decirte. El resto lo tenés que resolver vos. Apurate. Tick tock." },
    };

    // ===================== DIALOGOS VAGABUNDA =====================
    private string[][] dialogoVagabundaSinObjeto = new string[][]
    {
        new string[] { "Vagabunda", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador", "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabunda", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Vagabunda", "¿Notenés algo calentito para darme? No sabés el frío que paso acá." },
    };

    private string[][] dialogoVagabundaConObjeto = new string[][]
    {
        new string[] { "Vagabunda", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador", "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabunda", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Jugador", "[Dar mate]" },   // <- aqui aparece boton Dar objeto
        new string[] { "Vagabunda", "Mmm que rico, está un poco lavado pero por lo menos me calienta el cuerpo. Ya que estamos girando el mate, te cuento." },
        new string[] { "Vagabunda", "Vi a alguien que cuando encontraron el control, miró a los demás antes de mirarlo. No sé el nombre, tenía tatuajes creo." },
        new string[] { "Vagabunda", "Tomá igual, en este quilombo no soy la única que necesita uno." },
        new string[] { "Vagabunda", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." },
    };

    // ===================== UNITY START =====================
    void Start()
    {
        botonSiguiente.onClick.AddListener(SiguienteLinea);
        botonDarObjeto.onClick.AddListener(DarObjeto);
        botonDarObjeto.gameObject.SetActive(false);
    }

    // ===================== INICIAR DIALOGOS =====================
    public void IniciarDialogoMessi()
    {
        personajeActual = "Messi";
        dialogoActual = tienePelota ? dialogoMessiConObjeto : dialogoMessiSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoDuki()
    {
        personajeActual = "Duki";
        dialogoActual = tieneCigarrillo ? dialogoDukiConObjeto : dialogoDukiSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoMilagros()
    {
        personajeActual = "Milagros";
        dialogoActual = tieneAutografo ? dialogoMilagrosConObjeto : dialogoMilagrosSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoWanda()
    {
        personajeActual = "Wanda";
        dialogoActual = tieneScreenshots ? dialogoWandaConObjeto : dialogoWandaSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoSusana()
    {
        personajeActual = "Susana";
        dialogoActual = tieneCartera ? dialogoSusanaConObjeto : dialogoSusanaSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoFrancella()
    {
        personajeActual = "Francella";
        dialogoActual = tieneMartinFierro ? dialogoFrancellaConObjeto : dialogoFrancellaSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoVagabunda()
    {
        personajeActual = "Vagabunda";
        dialogoActual = tieneMate ? dialogoVagabundaConObjeto : dialogoVagabundaSinObjeto;
        IniciarDialogo();
    }

    void IniciarDialogo()
    {
        indiceDialogo = 0;
        MostrarLineaActual();
    }

    // ===================== MOSTRAR LINEA =====================
    void MostrarLineaActual()
    {
        string nombre = dialogoActual[indiceDialogo][0];
        string texto = dialogoActual[indiceDialogo][1];

        // Si la linea es del jugador con [Dar objeto], mostrar boton
        if (nombre == "Jugador" && texto.StartsWith("[Dar"))
        {
            textNombre.text = "";
            textDialogo.text = texto;
            botonSiguiente.gameObject.SetActive(false);
            botonDarObjeto.gameObject.SetActive(true);
        }
        else
        {
            textNombre.text = nombre;
            textDialogo.text = texto;
            botonSiguiente.gameObject.SetActive(true);
            botonDarObjeto.gameObject.SetActive(false);
        }
    }

    // ===================== SIGUIENTE LINEA =====================
    void SiguienteLinea()
    {
        indiceDialogo++;

        if (indiceDialogo >= dialogoActual.Length)
        {
            CerrarDialogo();
            return;
        }

        MostrarLineaActual();
    }

    // ===================== DAR OBJETO =====================
    void DarObjeto()
    {
        // Entregar el objeto correcto y recibir el del personaje
        switch (personajeActual)
        {
            case "Messi":
                tienePelota = false;
                tieneCopa = true;
                Debug.Log("Diste la pelota. Recibiste la copa del mundo.");
                break;
            case "Duki":
                tieneCigarrillo = false;
                tieneAutografo = true;
                Debug.Log("Diste el cigarrillo. Recibiste el autógrafo.");
                break;
            case "Milagros":
                tieneAutografo = false;
                tieneScreenshots = true;
                Debug.Log("Diste el autógrafo. Recibiste los screenshots.");
                break;
            case "Wanda":
                tieneScreenshots = false;
                tieneCartera = true;
                Debug.Log("Diste los screenshots. Recibiste la cartera.");
                break;
            case "Susana":
                tieneCartera = false;
                tieneMartinFierro = true;
                Debug.Log("Diste la cartera. Recibiste el Martín Fierro.");
                break;
            case "Francella":
                tieneMartinFierro = false;
                tieneMate = true;
                Debug.Log("Diste el Martín Fierro. Recibiste el mate.");
                break;
            case "Vagabunda":
                tieneMate = false;
                tieneCigarrillo = true;
                Debug.Log("Diste el mate. Recibiste el cigarrillo.");
                break;
        }

        // Avanzar al siguiente dialogo
        SiguienteLinea();
    }

    // ===================== CERRAR DIALOGO =====================
    void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
        fondoDialogos.SetActive(false);
        fondoPrincipal.SetActive(true);
        botonDarObjeto.gameObject.SetActive(false);
        botonSiguiente.gameObject.SetActive(true);
    }
}