using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public TextMeshProUGUI textNombre;
    public TextMeshProUGUI textDialogo;
    public Button botonSiguiente;
    public Button botonDarObjeto;
    public Button botonCerrar;
    public GameObject panelDialogo;
    public GameObject fondoDialogos;
    public GameObject fondoPrincipal;
    public GameObject panelInventario;
    public Button botonCerrarInventario;
    public GameObject hud;
    public GameObject personajesGrupo;
    public PersonajeDialogo personajeDialogo;
    public Sprite spriteMessiCharla;
    public Sprite spriteMessiContento;
    public Sprite spriteDukiContento;
    public Sprite spriteMilagrosContento;
    public Sprite spriteWandaContento;
    public Sprite spriteSusanaContento;
    public Sprite spriteFrancellaContento;
    public Sprite spriteVagabundoContento;
    public Sprite spriteDukiCharla;
    public Sprite spriteMilagrosCharla;
    public Sprite spriteWandaCharla;
    public Sprite spriteSusanaCharla;
    public Sprite spriteFrancellaCharla;
    public Sprite spriteVagabundoCharla;

    private bool tienePelota = true;
    private bool tieneCigarrillo = false;
    private bool tieneScreenshots = false;
    private bool tieneCartera = false;
    private bool tieneMartinFierro = false;
    private bool tieneMate = false;
    private bool tieneAutografo = false;
    private bool tieneCopa = false;

    private int indiceDialogo = 0;
    private string[][] dialogoActual;
    private string personajeActual = "";
    // Que objeto es el correcto para cada personaje
    private Dictionary<string, string> objetoCorrecto = new Dictionary<string, string>
    {
        { "Messi",     "pelota" },
        { "Duki",      "copa" },
        { "Milagros",  "autografo" },
        { "Wanda",     "screenshots" },
        { "Susana",    "cartera" },
        { "Francella", "martinFierro" },
        { "Vagabundo", "mate" },
    };

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
        new string[] { "Jugador", "[Dar pelota]" },
        new string[] { "Messi", "Mira que bueno, nunca hubiera conseguido uno, Anto se va a volver loca. Dale, te cuento." },
        new string[] { "Messi", "Vi a alguien exhalar cuando cerraron las puertas. No de alivio. De satisfacción. Es la cara de un jugador cuando el partido ya está ganado. Estaba cantando algo. Esta persona cree que ya ganó. Que llegar a Catedral está asegurado." },
        new string[] { "Messi", "Cuidámela eh." },
        new string[] { "Messi", "Ya te dije todo lo que sé. ¿Tenés algo para tomar?" },
    };

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
        new string[] { "Jugador", "[Dar Copa del Mundo]" },
        new string[] { "Duki", "¡La copa del mundo, bro! Venía a bancar a De Paul y ahora la tengo en las manos. Okay, esto no se lo cuento a nadie. Dale, te cuento algo más." },
        new string[] { "Duki", "La voz que escuché era de una mina. Hablaba en voz muy baja pero era claramente una mujer. Dijo algo raro: 'me voy a hacer viral'. En medio de un ataque de dinosaurios. Alguien que piensa en las redes ahora... me parece algo tétrico." },
        new string[] { "Duki", "Tomá. No hago favores pero acá tenés. Mi autógrafo." },
        new string[] { "Duki", "Ya está, bro. No sé nada más. Estoy inspirándome para componer un nuevo tema. No me interrumpas." },
    };

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
        new string[] { "Jugador", "[Dar autógrafo de Duki]" },
        new string[] { "Milagros Pilar", "¡DUKI! ¡Ay, me muero literalmente! ¿Cómo lo conseguiste? ¿Me lo das? ¿Lo puedo postear? Okay okay." },
        new string[] { "Milagros Pilar", "Te cuento algo que no le dije a nadie. Antes de subir, en el caos, vi a alguien con el control en la mano. Lo guardó cuando empezaron a mirar. Era una chica. Con cadenas, ropa oscura." },
        new string[] { "Milagros Pilar", "Mirá estas capturas. Son mi chisme mejor guardado." },
        new string[] { "Milagros Pilar", "Ya te conté todo, gordi. Ahora dejame que tengo que editar el reel de los dinosaurios. ¿Sabés el alcance que va a tener?" },
    };

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
        new string[] { "Jugador", "[Dar screenshots]" },
        new string[] { "Wanda", "Ay, ay, ay. Me muero. ¿Sabes todo lo que puedo conseguir con esta captura de pantalla? Lo hundo como el Titanic en dos segundos si quiero." },
        new string[] { "Wanda", "No te voy a decir el nombre porque me tengo que guardar algo siempre para mí. Pero esta persona estaba parada como si estuviera en el escenario. La gente del espectáculo con experiencia se para así, no es para novatos." },
        new string[] { "Wanda", "Tomá, se le cayó a alguien en el caos y ya tengo esta edición." },
        new string[] { "Wanda", "Ya te dije un montón, no me hables más." },
    };

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
        new string[] { "Jugador", "[Dar cartera Louis Vuitton]" },
        new string[] { "Susana", "¡MI CARTERA! ¡Ay, pensé que la había perdido para siempre! Las fotos de Pacha... el rouge... todo está. Vos sos un ángel." },
        new string[] { "Susana", "Vi la cara del infiltrado, querido. Cuando encontraron el control, todos miraron al control. Esta persona miró a los demás. Era un chico joven. Del ambiente artístico, me parece. Tiene esa presencia escénica. Pero en ese momento no quería que lo vieran. Eso me llamó la atención." },
        new string[] { "Susana", "Merecés un premio por toda esta investigación. Tomá." },
        new string[] { "Susana", "Ya te dije todo, querido. Ahora necesito sentarme. Este vagón está muy frío." },
    };

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
        new string[] { "Jugador", "[Dar Martín Fierro]" },
        new string[] { "Francella", "¡No te puedo creer! ¡Es la verdad, que locura! Ahora yo también sé cuanto pesa la copa del mundo, esta pesadita eh. Está bien. Te cuento todo." },
        new string[] { "Francella", "Vi a alguien acariciar el control con el pulgar. Así, suave. Como quien tiene algo que le da seguridad. Tenía anillos, grandes. Lo vi claramente." },
        new string[] { "Francella", "No tengo nada para vos, disculpá, salí corriendo y no agarré nada. Pero te puedo ofrecer un mate calentito." },
        new string[] { "Francella", "Ya te dije todo lo que podía decirte. El resto lo tenés que resolver vos. Apurate. Tick tock." },
    };

    private string[][] dialogoVagabundoSinObjeto = new string[][]
    {
        new string[] { "Vagabundo", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador", "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabundo", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Vagabundo", "¿No tenés algo calentito para darme? No sabés el frío que paso acá." },
    };

    private string[][] dialogoVagabundoConObjeto = new string[][]
    {
        new string[] { "Vagabundo", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador", "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabundo", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Jugador", "[Dar mate]" },
        new string[] { "Vagabundo", "Mmm que rico, está un poco lavado pero por lo menos me calienta el cuerpo. Ya que estamos girando el mate, te cuento." },
        new string[] { "Vagabundo", "Vi a alguien que cuando encontraron el control, miró a los demás antes de mirarlo. No sé el nombre, tenía tatuajes creo." },
        new string[] { "Vagabundo", "Esto era lo que necesitaba. Gracias, pibe." },
        new string[] { "Vagabundo", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." },
    };

    void Start()
    {
        fondoPrincipal = GameObject.Find("fondo_principal");

        SpriteRenderer[] todosSprites = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
        foreach (SpriteRenderer sr in todosSprites)
        {
            if (sr.gameObject.scene.isLoaded && sr.gameObject.name == "fondo_dialogos")
                fondoDialogos = sr.gameObject;
        }

        PersonajeDialogo[] pds = Resources.FindObjectsOfTypeAll<PersonajeDialogo>();
        foreach (PersonajeDialogo pd in pds)
        {
            if (pd.gameObject.scene.isLoaded)
            {
                personajeDialogo = pd;
                break;
            }
        }

        Button[] todosButtons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button b in todosButtons)
        {
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BotonSiguiente")
                botonSiguiente = b;
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BotonDarObjeto")
                botonDarObjeto = b;
        }

        TextMeshProUGUI[] todosTextos = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in todosTextos)
        {
            if (t.gameObject.scene.isLoaded && t.gameObject.name == "TextNombre")
                textNombre = t;
            if (t.gameObject.scene.isLoaded && t.gameObject.name == "TextDialogo")
                textDialogo = t;
        }

      GameObject[] todosGO = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in todosGO)
        {
            if (go.scene.isLoaded && go.name == "PanelDialogo")
                panelDialogo = go;
            if (go.scene.isLoaded && go.name == "HUD")
                hud = go;
                if (go.scene.isLoaded && go.name == "PERSONAJES")
                personajesGrupo = go;
                if (go.scene.isLoaded && go.name == "PanelInventario")
                panelInventario = go;
        }

        Button[] botonesCerrar = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button b in botonesCerrar)
        {
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BotonCerrar")
                botonCerrar = b;
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BtnCerrarInventario")
                botonCerrarInventario = b;
        }

        if (botonSiguiente != null) botonSiguiente.onClick.AddListener(SiguienteLinea);
        if (botonDarObjeto != null) botonDarObjeto.onClick.AddListener(AbrirInventarioParaElegir);
        if (botonCerrar != null) botonCerrar.onClick.AddListener(CerrarDialogo);
        if (botonCerrarInventario != null) botonCerrarInventario.onClick.AddListener(CancelarSeleccionInventario);
        if (botonDarObjeto != null) botonDarObjeto.gameObject.SetActive(false);


        Debug.Log("botonSiguiente: " + botonSiguiente);
        Debug.Log("panelDialogo: " + panelDialogo);
    }

    public void IniciarDialogoMessi()
    {
        personajeActual = "Messi";
        dialogoActual = dialogoMessiConObjeto;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteMessiCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoDuki()
    {
        personajeActual = "Duki";
        dialogoActual = dialogoDukiConObjeto;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteDukiCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoMilagros()
    {
        personajeActual = "Milagros";
        dialogoActual = dialogoMilagrosConObjeto;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteMilagrosCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoWanda()
    {
        personajeActual = "Wanda";
        dialogoActual = dialogoWandaConObjeto;
    
    if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteWandaCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoSusana()
    {
        personajeActual = "Susana";
        dialogoActual = dialogoSusanaConObjeto;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteSusanaCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoFrancella()
    {
        personajeActual = "Francella";
        dialogoActual = dialogoFrancellaConObjeto;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteFrancellaCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoVagabundo()
    {
        personajeActual = "Vagabundo";
        dialogoActual = dialogoVagabundoConObjeto;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteVagabundoCharla);
        IniciarDialogo();
    }

    void IniciarDialogo()
    {
        if (hud != null) hud.SetActive(false);
        indiceDialogo = 0;
        MostrarLineaActual();
    }

    void MostrarLineaActual()
    {
        if (textNombre == null || textDialogo == null) return;

        string nombre = dialogoActual[indiceDialogo][0];
        string texto = dialogoActual[indiceDialogo][1];

       if (nombre == "Jugador" && texto.StartsWith("[Dar"))
        {
            textNombre.text = "";
            textDialogo.text = "Tal vez pueda ofrecerle algo a cambio.";
            if (botonSiguiente != null) botonSiguiente.gameObject.SetActive(false);
            if (botonDarObjeto != null) botonDarObjeto.gameObject.SetActive(true);
        }
        else
        {
            textNombre.text = nombre;
            textDialogo.text = texto;
            if (botonSiguiente != null) botonSiguiente.gameObject.SetActive(true);
            if (botonDarObjeto != null) botonDarObjeto.gameObject.SetActive(false);
        }
    }

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
void AbrirInventarioParaElegir()
    {
        if (InventarioManager.instancia == null) return;

        // Mostrar el inventario
        if (panelInventario != null) panelInventario.SetActive(true);
        if (panelDialogo != null) panelDialogo.SetActive(false);
        InventarioManager.instancia.modoSeleccion = true;
        InventarioManager.instancia.alClickearObjeto = ObjetoElegido;
    }

    void ObjetoElegido(string idObjeto)
    {
        // Apagar modo selección para que no se dispare de nuevo
        InventarioManager.instancia.modoSeleccion = false;
        InventarioManager.instancia.alClickearObjeto = null;
        if (panelInventario != null) panelInventario.SetActive(false);
        if (panelDialogo != null) panelDialogo.SetActive(true);

        // ¿Es el objeto correcto para este personaje?
        bool acerto = objetoCorrecto.ContainsKey(personajeActual)
                      && objetoCorrecto[personajeActual] == idObjeto;

        if (acerto)
        {
            DarObjeto(); // sigue tu lógica de siempre (premio + pista + diálogo bueno)
        }
        else
        {
            textDialogo.text = "No, esto no me sirve. ¿Tenés otra cosa?";
            // dejamos el botón de inventario por si quiere reintentar
        }
    }
    void DarObjeto()
    {
        switch (personajeActual)
        {
            case "Messi":
            if (personajeDialogo != null && spriteMessiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMessiContento);
                tienePelota = false;
                tieneCopa = true;
                InventarioManager.instancia?.QuitarObjeto("pelota");
                InventarioManager.instancia?.AgregarObjeto("copa");
                Anotador.AgregarPista("Messi", "Exhaló con satisfacción cuando cerraron las puertas. Estaba cantando. Cree que llegar a Catedral está asegurado.");
                break;
            case "Duki":
                if (personajeDialogo != null && spriteDukiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteDukiContento);
                tieneCopa = false;
                tieneCopa = false;
                tieneAutografo = true;
                InventarioManager.instancia?.QuitarObjeto("copa");
                InventarioManager.instancia?.AgregarObjeto("autografo");
                Anotador.AgregarPista("Duki", "Era una mujer. Dijo 'me voy a hacer viral' en medio del caos.");
                break;
            case "Milagros":
                if (personajeDialogo != null && spriteMilagrosContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMilagrosContento);
                tieneAutografo = false;
                tieneAutografo = false;
                tieneScreenshots = true;
                InventarioManager.instancia?.QuitarObjeto("autografo");
                InventarioManager.instancia?.AgregarObjeto("screenshots");
                Anotador.AgregarPista("Milagros", "Vi a alguien con el control antes de subir. Lo guardó al ser observada. Chica con cadenas y ropa oscura.");
                break;
           case "Wanda":
                if (personajeDialogo != null && spriteWandaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteWandaContento);
                tieneScreenshots = false;
                tieneScreenshots = false;
                tieneCartera = true;
                InventarioManager.instancia?.QuitarObjeto("screenshots");
                InventarioManager.instancia?.AgregarObjeto("cartera");
                Anotador.AgregarPista("Wanda", "Se para como en un escenario. Alguien del espectáculo con experiencia, no es novata.");
                break;
           case "Susana":
                if (personajeDialogo != null && spriteSusanaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteSusanaContento);
                tieneCartera = false;
                tieneCartera = false;
                tieneMartinFierro = true;
                InventarioManager.instancia?.QuitarObjeto("cartera");
                InventarioManager.instancia?.AgregarObjeto("martinFierro");
                Anotador.AgregarPista("Susana", "Cuando encontraron el control, miró a los demás en vez de mirarlo. Joven, del ambiente artístico.");
                break;
            case "Francella":
                if (personajeDialogo != null && spriteFrancellaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteFrancellaContento);
                tieneMartinFierro = false;
                tieneMartinFierro = false;
                tieneMate = true;
                InventarioManager.instancia?.QuitarObjeto("martinFierro");
                InventarioManager.instancia?.AgregarObjeto("mate");
                Anotador.AgregarPista("Francella", "Acariciaba el control con el pulgar. Tenía anillos grandes.");
                break;
            case "Vagabundo":
                if (personajeDialogo != null && spriteVagabundoContento != null)
                    personajeDialogo.MostrarPersonaje(spriteVagabundoContento);
                tieneMate = false;
                tieneMate = false;
                InventarioManager.instancia?.QuitarObjeto("mate");
                Anotador.AgregarPista("Vagabundo", "Miró a los demás antes de mirar el control. Tenía tatuajes.");
                break;
        }
        SiguienteLinea();
    }

   void CerrarDialogo()
    {
        // Limpiar el inventario por si quedó abierto
        if (panelInventario != null) panelInventario.SetActive(false);
        if (InventarioManager.instancia != null)
        {
            InventarioManager.instancia.modoSeleccion = false;
            InventarioManager.instancia.alClickearObjeto = null;
        }

        if (hud != null) hud.SetActive(true);
        if (personajeDialogo != null) personajeDialogo.OcultarPersonaje();
        if (panelDialogo != null) panelDialogo.SetActive(false);
        if (fondoDialogos != null) fondoDialogos.SetActive(false);
        if (fondoPrincipal != null) fondoPrincipal.SetActive(true);
        if (botonDarObjeto != null) botonDarObjeto.gameObject.SetActive(false);
        if (botonSiguiente != null) botonSiguiente.gameObject.SetActive(true);
        if (personajesGrupo != null) personajesGrupo.SetActive(true);
    }
    void CancelarSeleccionInventario()
    {
        // Solo actúa si estábamos eligiendo objeto para un personaje
        if (InventarioManager.instancia != null && InventarioManager.instancia.modoSeleccion)
        {
            InventarioManager.instancia.modoSeleccion = false;
            InventarioManager.instancia.alClickearObjeto = null;
            if (panelInventario != null) panelInventario.SetActive(false);
            // Volver al diálogo
            if (panelDialogo != null) panelDialogo.SetActive(true);
        }
    }
}