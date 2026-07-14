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

    [Header("Camino")]
    public bool esCaminoMate = false;
    public bool esCaminoCelular = false;

    private int indiceDialogo = 0;
    private string[][] dialogoActual;
    private string personajeActual = "";

    // ── CAMINO PELOTA ────────────────────────────────────────────────────────

    private Dictionary<string, string> objetoCorrectoPelota = new Dictionary<string, string>
    {
        { "Messi",     "pelota" },
        { "Duki",      "copa" },
        { "Milagros",  "autografo" },
        { "Wanda",     "screenshots" },
        { "Susana",    "cartera" },
        { "Francella", "martinFierro" },
        { "Vagabundo", "mate" },
    };

    private string[][] dialogoMessiPelota = new string[][]
    {
        new string[] { "Messi",   "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi",   "Yo miro mucho, eh. La gente no se da cuenta pero yo veo todo. Y hay alguien acá que está re tranquilo. Todos asustados y este tipo como si nada, tranqui, sabiéndola. Me da mala espina." },
        new string[] { "Jugador", "[Dar pelota]" },
        new string[] { "Messi",   "Mira que bueno, nunca hubiera conseguido uno, Anto se va a volver loca. Dale, te cuento." },
        new string[] { "Messi",   "Cuando se cerraron las puertas, vi a alguien soltar el aire. No aliviado: contento. Como cuando ya sabés que ganaste. Encima cantaba bajito. Para mí ya se siente ganador, como si llegar a Catedral lo tuviera asegurado." },
        new string[] { "Messi",   "Cuidámela eh." },
        new string[] { "Messi",   "Ya te dije todo lo que sé. ¿Tenés algo para tomar?" },
    };

    private string[][] dialogoDukiPelota = new string[][]
    {
        new string[] { "Duki",    "¿Qué onda? Todo esto es una locura, bro. Venía a bancar a De Paul y ahora estoy huyendo de un T-Rex. Si esto no es una letra de trap, no sé qué es." },
        new string[] { "Jugador", "¿Escuchaste algo raro?" },
        new string[] { "Duki",    "Mirá, tenía un auri puesto y el otro libre. Escuché a alguien decir 'Catedral' re tranquilo, bro. Con todo el quilombo que había, hablar así de piola no es normal. Y dijo algo como 'ya está todo listo', bajito, para adentro." },
        new string[] { "Jugador", "[Dar Copa del Mundo]" },
        new string[] { "Duki",    "¡La copa del mundo, bro! Venía a bancar a De Paul y ahora la tengo en las manos. Okay, esto no se lo cuento a nadie. Dale, te cuento algo más." },
        new string[] { "Duki",    "La voz que escuché era de una mina. Hablaba en voz muy baja pero era claramente una mujer. Dijo algo raro: 'me voy a hacer viral'. En medio de un ataque de dinosaurios. Alguien que piensa en las redes ahora... me parece algo tétrico." },
        new string[] { "Duki",    "Tomá. No hago favores pero acá tenés. Mi autógrafo." },
        new string[] { "Duki",    "Ya está, bro. No sé nada más. Estoy inspirándome para componer un nuevo tema. No me interrumpas." },
    };

    private string[][] dialogoMilagrosPelota = new string[][]
    {
        new string[] { "Milagros Pilar", "¡Chicos, no se imaginan esto! ¡Un dinosaurio de verdad! Ay, hola. ¿Me seguís? No importa. ¿Tenés buena luz acá? Es una tragedia, obvio. Pero el contenido... el contenido es increíble." },
        new string[] { "Jugador",        "¿Viste algo sospechoso?" },
        new string[] { "Milagros Pilar", "Yo capturo todo, es casi un reflejo. Vi a alguien que subió al subte sin correr. Todos corrían y esta persona caminaba. Caminaba como si supiera que el subte la iba a esperar." },
        new string[] { "Jugador",        "[Dar autógrafo de Duki]" },
        new string[] { "Milagros Pilar", "¡DUKI! ¡Ay, me muero literalmente! ¿Cómo lo conseguiste? ¿Me lo das? ¿Lo puedo postear? Okay okay." },
        new string[] { "Milagros Pilar", "Te cuento algo que no le dije a nadie. Antes de subir, en el caos, vi a alguien con el control en la mano. Lo guardó cuando empezaron a mirar. Era una chica. Con cadenas, ropa oscura." },
        new string[] { "Milagros Pilar", "Mirá estas capturas. Son mi chisme mejor guardado." },
        new string[] { "Milagros Pilar", "Ya te conté todo, gordi. Ahora dejame que tengo que editar el reel de los dinosaurios. ¿Sabés el alcance que va a tener?" },
    };

    private string[][] dialogoWandaPelota = new string[][]
    {
        new string[] { "Wanda",   "Esto es un desastre absoluto. No puedo correr con estos tacos. ¿Vos quién sos? No te conozco ¿Trabajas para mí?" },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Wanda",   "Miro todo. Es un don. Vi a alguien que cuando apareció el control, no puso cara de sorpresa. Todo el mundo abrió los ojos. Pero esta persona, cara de nada. Yo sé lo que es actuar sorpresa. Lo hice en televisión por años." },
        new string[] { "Jugador", "[Dar screenshots]" },
        new string[] { "Wanda",   "Ay, ay, ay. Me muero. ¿Sabes todo lo que puedo conseguir con esta captura de pantalla? Lo hundo como el Titanic en dos segundos si quiero." },
        new string[] { "Wanda",   "No te voy a decir el nombre porque algo me tengo que guardar yo, querida. Pero esta persona estaba parada como arriba de un escenario, ¿entendés? Con esa pose. Eso lo tenés cuando estás hace años en el ambiente, no te sale de la nada." },
        new string[] { "Wanda",   "Tomá, se le cayó a alguien en el caos y ya tengo esta edición." },
        new string[] { "Wanda",   "Ya te dije un montón, no me hables más." },
    };

    private string[][] dialogoSusanaPelota = new string[][]
    {
        new string[] { "Susana",  "¡Esto es una pesadilla! ¡Yo tenía grabación mañana! ¿Y vos quién sos, querido? En esta situación todos somos iguales. Bueno, casi." },
        new string[] { "Jugador", "¿Vio algo sospechoso?" },
        new string[] { "Susana",  "Mirá, yo tengo ojo clínico. Años de entrevistar gente. Hay alguien acá que tiene un aura rara. Alguien que parece que no se sorprendió cuando vió los dinosaurios." },
        new string[] { "Jugador", "[Dar cartera Louis Vuitton]" },
        new string[] { "Susana",  "¡MI CARTERA! ¡Ay, pensé que la había perdido para siempre! Las fotos de Pacha... el rouge... todo está. Vos sos un ángel." },
        new string[] { "Susana",  "Vi la cara del infiltrado, querido. Cuando encontraron el control, todos miraron el objeto, pero esta persona miró a los demás. Era un chico joven, del ambiente artístico. Quería pasar desapercibido." },
        new string[] { "Susana",  "Merecés un premio por toda esta investigación. Tomá." },
        new string[] { "Susana",  "Ya te dije todo, querido. Ahora necesito sentarme. Este vagón está muy frío." },
    };

    private string[][] dialogoFrancellaPelota = new string[][]
    {
        new string[] { "Francella", "¡Eh! ¡Qué cosa, no! ¡Quién lo iba a decir! Dinosaurios. En Buenos Aires. Esto parece de película." },
        new string[] { "Jugador",   "¿Notaste algo raro?" },
        new string[] { "Francella", "Uno nota cosas cuando es actor, viste. Y hay alguien acá que está fingiendo el miedo. Se le nota, no le sale natural. En la respiración, en los ojos. Después de tantos años arriba del escenario uno se da cuenta de esas cosas." },
        new string[] { "Jugador",   "[Dar Martín Fierro]" },
        new string[] { "Francella", "¡No te puedo creer! ¡Es la verdad, que locura! Ahora yo también sé cuanto pesa la copa del mundo, esta pesadita eh. Está bien. Te cuento todo." },
        new string[] { "Francella", "Vi a alguien acariciar el control con el pulgar. Así, suave. Como quien tiene algo que le da seguridad. Tenía anillos, grandes. Lo vi claramente." },
        new string[] { "Francella", "No tengo nada para vos, disculpá, salí corriendo y no agarré nada. Pero te puedo ofrecer un mate calentito." },
        new string[] { "Francella", "Ya te dije todo lo que podía decirte. El resto lo tenés que resolver vos. Apurate. Tick tock." },
    };

    private string[][] dialogoVagabundoPelota = new string[][]
    {
        new string[] { "Vagabundo", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador",   "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabundo", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Jugador",   "[Dar mate]" },
        new string[] { "Vagabundo", "Mmm que rico, está un poco lavado pero por lo menos me calienta el cuerpo. Ya que estamos girando el mate, te cuento." },
        new string[] { "Vagabundo", "Vi a alguien que cuando encontraron el control, miró a los demás antes de mirarlo. No sé el nombre, tenía tatuajes creo." },
        new string[] { "Vagabundo", "Esto era lo que necesitaba. Gracias, pibe." },
        new string[] { "Vagabundo", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." },
    };

    // ── CAMINO MATE ──────────────────────────────────────────────────────────

    private Dictionary<string, string> objetoCorrectoMate = new Dictionary<string, string>
    {
        { "Vagabundo", "mate" },
        { "Duki",      "cigarrillo" },
        { "Milagros",  "autografo" },
        { "Wanda",     "screenshots" },
        { "Susana",    "cartera" },
        { "Messi",     "martinFierro" },
        { "Francella", "copa" },
    };

    private Dictionary<string, string> mensajeEquivocadoMate = new Dictionary<string, string>
    {
        { "Vagabundo", "¿No tenés algo calentito para darme? No sabés el frío que paso acá." },
        { "Duki",      "Bro, ¿qué es esto? No me sirve. Guardatelo." },
        { "Milagros",  "Ay, que esperabas, ¿un canje? ¿Tenés algo más interesante?" },
        { "Wanda",     "¿Esto me lo traés a mí? No, querido. Guardátelo." },
        { "Susana",    "¿Y que hago yo con esto? Traéme algo de valor y hablamos." },
        { "Messi",     "No, esto no es para mí. Pero gracias igual." },
        { "Francella", "Ay, no. Esto no lo necesito. Pero aprecio el gesto." },
    };

    private string[][] dialogoVagabundoMate = new string[][]
    {
        new string[] { "Vagabundo", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador",   "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabundo", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe. ¿No tenés algo para darme?" },
        new string[] { "Jugador",   "[Dar mate]" },
        new string[] { "Vagabundo", "Mmm que rico, está un poco lavado pero por lo menos me calienta el cuerpo. Ya que estamos girando el mate, te cuento; Vi a alguien que cuando encontraron el control, miró a los demás antes de mirarlo. No sé el nombre, tenía tatuajes creo." },
        new string[] { "Vagabundo", "Tomá igual, en este quilombo no soy la única que necesita uno." },
        new string[] { "Vagabundo", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." },
    };

    private string[][] dialogoDukiMate = new string[][]
    {
        new string[] { "Duki",    "¿Qué onda? Todo esto es una locura, bro. Venía a bancar a De Paul y ahora estoy huyendo de un T-Rex. Si esto no es una letra de trap, no sé qué es." },
        new string[] { "Jugador", "¿Escuchaste algo raro?" },
        new string[] { "Duki",    "Mirá, tenía un auricular puesto pero el otro libre. Escuché una voz decir 'Catedral' con demasiada calma. En medio del caos, eso no es normal, bro. Y escuché algo como 'ya está todo listo'. Dicho bajito. Como para sí mismo." },
        new string[] { "Jugador", "[Dar cigarrillo]" },
        new string[] { "Duki",    "¿Naaa, como sabías que necesitaba uno? Dale, te cuento algo más. La voz que escuché era de una mina. Dijo algo raro: 'me voy a hacer viral'. En medio de un ataque de dinosaurios. Alguien que piensa en las redes ahora... me parece algo tétrico." },
        new string[] { "Duki",    "Tomá. No hago favores pero acá tenés. Mi autógrafo." },
        new string[] { "Duki",    "Ya está, bro. No sé nada más. Estoy inspirándome para componer un nuevo tema. No me interrumpas." },
    };

    private string[][] dialogoMilagrosMate = new string[][]
    {
        new string[] { "Milagros Pilar", "¡Chicos, no se imaginan esto! ¡Un dinosaurio de verdad! Ay, hola. ¿Me seguís? No importa. ¿Tenés buena luz acá? Es una tragedia, obvio. Pero el contenido... el contenido es increíble." },
        new string[] { "Jugador",        "¿Viste algo sospechoso?" },
        new string[] { "Milagros Pilar", "Yo capturo todo, es casi un reflejo. En el video se ve como alguien subió al subte sin correr. Todos corrían y esta persona caminaba. Caminaba como si supiera que el subte la iba a esperar." },
        new string[] { "Jugador",        "[Dar autógrafo]" },
        new string[] { "Milagros Pilar", "¡DUKI! ¡Ay, me muero literal! ¿Cómo lo conseguiste? Te cuento algo que no le dije a nadie. Antes de subir, en el caos, vi a alguien con el control en la mano. Lo guardó cuando empezaron a mirar. Era un chico. Con cadenas, ropa oscura." },
        new string[] { "Milagros Pilar", "Mirá estas capturas. Son mi chisme mejor guardado." },
        new string[] { "Milagros Pilar", "Ya te conté todo, gordi. Ahora dejáme que tengo que editar el reel de los dinosaurios. ¿Sabés el alcance que va a tener?" },
    };

    private string[][] dialogoWandaMate = new string[][]
    {
        new string[] { "Wanda",   "Esto es un desastre absoluto. No puedo correr con estos tacos. ¿Vos quién sos? No te conozco ¿Trabajas para mí?" },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Wanda",   "Miro todo. Es un don. Vi a alguien que cuando apareció el control, no puso cara de sorpresa. Todo el mundo abrió los ojos. Pero esta persona, cara de nada. Yo sé lo que es actuar sorprendida. Lo hice en televisión por años." },
        new string[] { "Jugador", "[Dar screenshots]" },
        new string[] { "Wanda",   "¿Sabes todo lo que puedo conseguir con esta captura de pantalla? No te voy a decir el nombre porque me tengo que guardar algo siempre para mí, por las dudas. Pero esta persona estaba parada como si estuviera en el escenario." },
        new string[] { "Wanda",   "Tomá, se le cayó a alguien en el caos y ya tengo esta edición." },
        new string[] { "Wanda",   "Ya te dije un montón, no me hables más." },
    };

    private string[][] dialogoSusanaMate = new string[][]
    {
        new string[] { "Susana",  "¡Esto es una pesadilla! ¡Yo tenía un día muy ocupado! ¿Y vos quién sos, querido? En esta situación todos somos iguales. Bueno, casi." },
        new string[] { "Jugador", "¿Vio algo sospechoso?" },
        new string[] { "Susana",  "Mirá, yo tengo ojo clínico. Años de entrevistar gente. Hay alguien acá que tiene un aura rara. Alguien que parece que no se sorprendió cuando vió los dinosaurios." },
        new string[] { "Jugador", "[Dar cartera]" },
        new string[] { "Susana",  "¡MI CARTERA! Las fotos de Pacha... el rouge... está todo. Vos sos un angel. Cuando encontraron el control, todos miraron al control. Esta persona miró a los demás. Era alguien joven. Del ambiente artístico, me parece. Tiene esa presencia escénica. " },
        new string[] { "Susana",  "Merecés un premio por toda esta investigación. Tomá." },
        new string[] { "Susana",  "Ya te dije todo, querido. Ahora necesito sentarme. Este vagón está muy frío." },
    };

    private string[][] dialogoMessiMate = new string[][]
    {
        new string[] { "Messi",   "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi",   "Miro mucho. La gente no se da cuenta, pero analizo todo. Como en la cancha. Hay alguien acá que se mueve distinto. No nervioso. Calculador. Como un jugador que ya sabe cuál va a ser la jugada antes de que empiece." },
        new string[] { "Jugador", "[Dar Martín Fierro]" },
        new string[] { "Messi",   "Naa, nunca hubiera conseguido uno. Dale, te cuento. Vi a alguien exhalar cuando cerraron las puertas. No de alivio. De satisfacción. Yo conozco esa cara. Es la cara de un jugador cuando el partido ya está ganado. Estaba cantando algo." },
        new string[] { "Messi",   "Cuidámela eh." },
        new string[] { "Messi",   "Ya te dije todo lo que sé. ¿Tenés algo para tomar?" },
    };

    private string[][] dialogoFrancellaMate = new string[][]
    {
        new string[] { "Francella", "¡Eh! ¡Qué cosa, no! ¡Quién lo iba a decir! Dinosaurios. En Buenos Aires. Esto parece de película." },
        new string[] { "Jugador",   "¿Notaste algo raro?" },
        new string[] { "Francella", "Notás cosas cuando sos actor. Cosas que otros no ven. Hay alguien acá que está actuando el miedo. Y el miedo actuado tiene un ritmo distinto al miedo real. Lo noto en la respiración, en los ojos. Años de oficio." },
        new string[] { "Jugador",   "[Dar Copa del Mundo]" },
        new string[] { "Francella", "¡No te puedo creer! ¡Es la real, qué locura! Ahora yo también sé cuanto pesa la copa del mundo. Vi a alguien acariciar el control con el pulgar. Así, suave. Como quien tiene algo que le da seguridad. Tenía anillos, grandes. Lo vi claramente." },
        new string[] { "Francella", "No tengo nada para vos, disculpá, salí corriendo y no agarré nada cuando llegaron los dinosaurios, espero que te sirva lo que vi." },
        new string[] { "Francella", "Ya te dije todo lo que podía decirte. El resto lo tenés que resolver vos. Apurate." },
    };

    // ── CAMINO CELULAR ───────────────────────────────────────────────────────

    private Dictionary<string, string> objetoCorrectoCelular = new Dictionary<string, string>
    {
        { "Milagros",  "celular" },
        { "Wanda",     "screenshots" },
        { "Susana",    "cartera" },
        { "Francella", "martinFierro" },
        { "Vagabundo", "mate" },
        { "Duki",      "cigarrillo" },
        { "Messi",     "autografo" },
    };

    private Dictionary<string, string> mensajeEquivocadoCelular = new Dictionary<string, string>
    {
        { "Milagros",  "Ay, que esperabas, ¿un canje? ¿Tenés algo más interesante?" },
        { "Wanda",     "¿Esto me lo traés a mí? No, querida. Guardátelo." },
        { "Susana",    "¿Y qué hago yo con esto? Traéme algo de valor y hablamos." },
        { "Francella", "Ay, no. Esto no lo necesito. Pero aprecio el gesto." },
        { "Vagabundo", "No, gracias, pero no puedo hacer nada con eso. ¿No tenés algo calentito para darme?" },
        { "Duki",      "Bro, ¿qué es esto? No me sirve. Guardatelo." },
        { "Messi",     "No, esto no es para mí. Pero gracias igual." },
    };

    private string[][] dialogoMilagrosCelular = new string[][]
    {
        new string[] { "Milagros Pilar", "¡Me quedé sin batería, esto es una tragedia! Tengo que avisarles a mis followers ¿Cómo hago ahora? Este contenido hay que aprovecharlo, me tengo que hacer viral." },
        new string[] { "Jugador",        "¿Viste algo sospechoso?" },
        new string[] { "Milagros Pilar", "Yo capturo todo, es casi un reflejo. En el video se ve como alguien subió al subte sin correr. Todos corrían y esta persona caminaba. Caminaba como si supiera que el subte la iba a esperar." },
        new string[] { "Jugador",        "[Dar celular]" },
        new string[] { "Milagros Pilar", "¡Ay, me muero literalmente! ¿Cómo lo conseguiste? ¿Me lo das? ¿Tiene señal? Necesito subir una story YA. Okay okay. Te cuento algo que no le dije a nadie. Antes de subir, en el caos, vi a alguien con el control en la mano. Lo guardó cuando empezaron a mirar. Era una señora, muy coqueta, bien vestida." },
        new string[] { "Milagros Pilar", "Mirá estas capturas. Son mi chisme mejor guardado." },
        new string[] { "Milagros Pilar", "Ya te conté todo, gordi. Ahora dejáme que tengo que editar el reel de los dinosaurios. ¿Sabés el alcance que va a tener?" },
    };

    private string[][] dialogoWandaCelular = new string[][]
    {
        new string[] { "Wanda",   "Esto es un desastre absoluto. No puedo correr con estos tacos. ¿Vos quién sos? No te conozco ¿Trabajas para mí?" },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Wanda",   "Miro todo. Es un don. Vi a alguien que cuando apareció el control, no puso cara de sorpresa. Todo el mundo abrió los ojos. Pero esta persona, cara plana. Yo sé lo que es actuar sorpresa. Lo hice en televisión por años." },
        new string[] { "Jugador", "[Dar screenshots]" },
        new string[] { "Wanda",   "Ay, ay, ay. Me muero. ¿Sabes todo lo que puedo conseguir con esta captura de pantalla? Lo hundo como el Titanic en dos segundos si quiero. No te voy a decir el nombre porque me tengo que guardar algo siempre para mí, por las dudas. Pero esta persona estaba parada como si estuviera en el escenario. La gente del espectáculo con experiencia se para así, no es para novatos." },
        new string[] { "Wanda",   "Tomá, se le cayó a alguien en el caos y ya tengo esta edición." },
        new string[] { "Wanda",   "Ya te dije un montón, no me hables más." },
    };

    private string[][] dialogoSusanaCelular = new string[][]
    {
        new string[] { "Susana",  "¡Esto es una pesadilla! ¡Yo tenía un día muy ocupado! ¿Y vos quién sos, querido? En esta situación todos somos iguales. Bueno, casi." },
        new string[] { "Jugador", "¿Vió algo sospechoso?" },
        new string[] { "Susana",  "Mirá, yo tengo ojo clínico. Años de entrevistar gente. Hay alguien acá que tiene un aura rara. Alguien que parece que no se sorprendió cuando vió los dinosaurios." },
        new string[] { "Jugador", "[Dar cartera Louis Vuitton]" },
        new string[] { "Susana",  "¡MI CARTERA! ¡Ay, pensé que la había perdido para siempre! Las fotos de Pacha... el rouge... todo está. Vos sos un ángel. Te cuento todo lo que vi. Cuando encontraron el control, todos miraron al control. Esta persona miró a los demás. Era un chico joven. Del ambiente artístico, me parece. Tiene esa presencia escénica. Pero en ese momento no quería que lo vieran. Eso me llamó la atención." },
        new string[] { "Susana",  "Merecés un premio por toda esta investigación. Tomá." },
        new string[] { "Susana",  "Ya te dije todo, querido. Ahora necesito sentarme. Este vagón está muy frío." },
    };

    private string[][] dialogoFrancellaCelular = new string[][]
    {
        new string[] { "Francella", "¡Eh! ¡Qué cosa, no! ¡Quién lo iba a decir! Dinosaurios. En Buenos Aires. Esto parece de película." },
        new string[] { "Jugador",   "¿Notaste algo raro?" },
        new string[] { "Francella", "Notás cosas cuando sos actor. Cosas que otros no ven. Hay alguien acá que está actuando el miedo. Y el miedo actuado tiene un ritmo distinto al miedo real. Lo noto en la respiración, en los ojos. Años de oficio." },
        new string[] { "Jugador",   "[Dar Martín Fierro]" },
        new string[] { "Francella", "Ay. Este es mío, ¿sabés? Bueno, era mío. Me lo sacó Susana en una apuesta hace años. Está bien. Te cuento todo. Vi a alguien acariciar el control con el pulgar. Así, suave. Como quien tiene algo que le da seguridad. Tenía brillos en la ropa, no reconocí quién era pero brillaba, brillaba. Lo vi claramente." },
        new string[] { "Francella", "No tengo nada para vos, disculpá, salí corriendo con lo que tenía en la mano, te puedo ofrecer un mate calentito." },
        new string[] { "Francella", "Ya te dije todo lo que podía decirte, espero que te sea de ayuda." },
    };

    private string[][] dialogoVagabundoCelular = new string[][]
    {
        new string[] { "Vagabundo", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en Once. ¿Qué querés?" },
        new string[] { "Jugador",   "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabundo", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Jugador",   "[Dar mate]" },
        new string[] { "Vagabundo", "Mmm que rico, está un poco lavado pero por lo menos me calienta el cuerpo. Ya que estamos girando el mate, te cuento; Vi a alguien que cuando encontraron el control, miró a los demás antes de mirarlo. No sé el nombre, tenía tatuajes creo." },
        new string[] { "Vagabundo", "Tomá igual, en este quilombo no soy la única que necesita uno." },
        new string[] { "Vagabundo", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." },
    };

    private string[][] dialogoDukiCelular = new string[][]
    {
        new string[] { "Duki",    "¿Qué onda? Todo esto es una locura, bro. Venía a bancar a De Paul y ahora estoy huyendo de un T-Rex. Si esto no es una letra de trap, no sé qué es." },
        new string[] { "Jugador", "¿Escuchaste algo raro?" },
        new string[] { "Duki",    "Mirá, tenía un auri puesto y el otro libre. Escuché a alguien decir 'Catedral' re tranquilo, bro. Con todo el quilombo que había, hablar así de piola no es normal. Y dijo algo como 'ya está todo listo', bajito, para adentro." },
        new string[] { "Jugador", "[Dar cigarrillo]" },
        new string[] { "Duki",    "¿Naaa, cómo sabías que necesitaba uno? En este contexto no hago preguntas. Dale, te cuento algo más. La voz que escuché era de una mina. Hablaba bajito pero era claramente una mujer. Dijo algo raro: 'todos me van a mirar'. En medio de un ataque de dinosaurios. Alguien que piensa en eso ahora me llamó la atención." },
        new string[] { "Duki",    "Tomá. No hago favores pero acá tenés. Mi autógrafo." },
        new string[] { "Duki",    "Ya está, bro. No sé nada más. Estoy inspirándome para componer un nuevo tema. No me interrumpas." },
    };

    private string[][] dialogoMessiCelular = new string[][]
    {
        new string[] { "Messi",   "Hola. Sí, soy yo. Tranquilo, no pasa nada. Igual esto es un despelote, eh. Y mirá que yo vi cosas raras." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi",   "Mirá, yo no hablo mucho pero escucho. Y acá hay alguien que no está actuando como los demás. Todos gritando, corriendo, y esta persona ni se inmutó. Como si ya supiera lo que iba a pasar." },
        new string[] { "Jugador", "[Dar autógrafo]" },
        new string[] { "Messi",   "Ah, mirá vos. Se lo llevo a los chicos, se van a poner contentos. Dale, te cuento lo que vi. Cuando arrancó el subte, había una persona con una sonrisita. No de nerviosa. De que le salió bien. Yo esa cara la conozco, la vi en vestuarios. Es la cara del que ya sabe que ganó." },
        new string[] { "Messi",   "No tengo nada para darte, salí con lo puesto. Pero te dije todo lo que vi." },
        new string[] { "Messi",   "Listo, eso es todo lo que sé. Ojalá te sirva." },
    };

    // ── INICIO ───────────────────────────────────────────────────────────────

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
            if (pd.gameObject.scene.isLoaded) { personajeDialogo = pd; break; }
        }

        Button[] todosButtons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button b in todosButtons)
        {
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BotonSiguiente")      botonSiguiente = b;
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BotonDarObjeto")      botonDarObjeto = b;
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BotonCerrar")         botonCerrar = b;
            if (b.gameObject.scene.isLoaded && b.gameObject.name == "BtnCerrarInventario") botonCerrarInventario = b;
        }

        TextMeshProUGUI[] todosTextos = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in todosTextos)
        {
            if (t.gameObject.scene.isLoaded && t.gameObject.name == "TextNombre")  textNombre = t;
            if (t.gameObject.scene.isLoaded && t.gameObject.name == "TextDialogo") textDialogo = t;
        }

        GameObject[] todosGO = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in todosGO)
        {
            if (go.scene.isLoaded && go.name == "PanelDialogo")   panelDialogo = go;
            if (go.scene.isLoaded && go.name == "HUD")             hud = go;
            if (go.scene.isLoaded && go.name == "PERSONAJES")      personajesGrupo = go;
            if (go.scene.isLoaded && go.name == "PanelInventario") panelInventario = go;
        }

        if (botonSiguiente != null)        botonSiguiente.onClick.AddListener(SiguienteLinea);
        if (botonDarObjeto != null)        botonDarObjeto.onClick.AddListener(AbrirInventarioParaElegir);
        if (botonCerrar != null)           botonCerrar.onClick.AddListener(CerrarDialogo);
        if (botonCerrarInventario != null) botonCerrarInventario.onClick.AddListener(CancelarSeleccionInventario);
        if (botonDarObjeto != null)        botonDarObjeto.gameObject.SetActive(false);

        Debug.Log("botonSiguiente: " + botonSiguiente);
        Debug.Log("panelDialogo: " + panelDialogo);
    }

    // ── INICIAR DIÁLOGOS ─────────────────────────────────────────────────────

    public void IniciarDialogoMessi()
    {
        personajeActual = "Messi";
        if (esCaminoCelular)   dialogoActual = dialogoMessiCelular;
        else if (esCaminoMate) dialogoActual = dialogoMessiMate;
        else                   dialogoActual = dialogoMessiPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteMessiCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoDuki()
    {
        personajeActual = "Duki";
        if (esCaminoCelular)   dialogoActual = dialogoDukiCelular;
        else if (esCaminoMate) dialogoActual = dialogoDukiMate;
        else                   dialogoActual = dialogoDukiPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteDukiCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoMilagros()
    {
        personajeActual = "Milagros";
        if (esCaminoCelular)   dialogoActual = dialogoMilagrosCelular;
        else if (esCaminoMate) dialogoActual = dialogoMilagrosMate;
        else                   dialogoActual = dialogoMilagrosPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteMilagrosCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoWanda()
    {
        personajeActual = "Wanda";
        if (esCaminoCelular)   dialogoActual = dialogoWandaCelular;
        else if (esCaminoMate) dialogoActual = dialogoWandaMate;
        else                   dialogoActual = dialogoWandaPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteWandaCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoSusana()
    {
        personajeActual = "Susana";
        if (esCaminoCelular)   dialogoActual = dialogoSusanaCelular;
        else if (esCaminoMate) dialogoActual = dialogoSusanaMate;
        else                   dialogoActual = dialogoSusanaPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteSusanaCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoFrancella()
    {
        personajeActual = "Francella";
        if (esCaminoCelular)   dialogoActual = dialogoFrancellaCelular;
        else if (esCaminoMate) dialogoActual = dialogoFrancellaMate;
        else                   dialogoActual = dialogoFrancellaPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteFrancellaCharla);
        IniciarDialogo();
    }

    public void IniciarDialogoVagabundo()
    {
        personajeActual = "Vagabundo";
        if (esCaminoCelular)   dialogoActual = dialogoVagabundoCelular;
        else if (esCaminoMate) dialogoActual = dialogoVagabundoMate;
        else                   dialogoActual = dialogoVagabundoPelota;
        if (personajeDialogo != null) personajeDialogo.MostrarPersonaje(spriteVagabundoCharla);
        IniciarDialogo();
    }

    // ── LÓGICA DE DIÁLOGO ────────────────────────────────────────────────────

    void IniciarDialogo()
    {
        if (hud != null)             hud.SetActive(false);
        if (panelDialogo != null)    panelDialogo.SetActive(true);
        if (fondoDialogos != null)   fondoDialogos.SetActive(true);
        if (fondoPrincipal != null)  fondoPrincipal.SetActive(false);
        if (personajesGrupo != null) personajesGrupo.SetActive(false);
        indiceDialogo = 0;
        MostrarLineaActual();
    }

    void MostrarLineaActual()
    {
        if (textNombre == null || textDialogo == null) return;

        string nombre = dialogoActual[indiceDialogo][0];
        string texto  = dialogoActual[indiceDialogo][1];

        if (nombre == "Jugador" && texto.StartsWith("[Dar"))
        {
            textNombre.text  = "";
            textDialogo.text = "Tal vez pueda ofrecerle algo a cambio.";
            if (botonSiguiente != null) botonSiguiente.gameObject.SetActive(false);
            if (botonDarObjeto != null) botonDarObjeto.gameObject.SetActive(true);
        }
        else
        {
            textNombre.text  = nombre;
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
        if (panelInventario != null) panelInventario.SetActive(true);
        if (panelDialogo != null)    panelDialogo.SetActive(false);
        InventarioManager.instancia.modoSeleccion    = true;
        InventarioManager.instancia.alClickearObjeto = ObjetoElegido;
    }

    void ObjetoElegido(string idObjeto)
    {
        InventarioManager.instancia.modoSeleccion    = false;
        InventarioManager.instancia.alClickearObjeto = null;
        if (panelInventario != null) panelInventario.SetActive(false);
        if (panelDialogo != null)    panelDialogo.SetActive(true);

        Dictionary<string, string> correcto;
        if (esCaminoCelular)   correcto = objetoCorrectoCelular;
        else if (esCaminoMate) correcto = objetoCorrectoMate;
        else                   correcto = objetoCorrectoPelota;

        bool acerto = correcto.ContainsKey(personajeActual) && correcto[personajeActual] == idObjeto;

        if (acerto)
        {
            DarObjeto();
        }
        else
        {
            string msg = "No, esto no me sirve. ¿Tenés otra cosa?";
            if (esCaminoCelular && mensajeEquivocadoCelular.ContainsKey(personajeActual))
                msg = mensajeEquivocadoCelular[personajeActual];
            else if (esCaminoMate && mensajeEquivocadoMate.ContainsKey(personajeActual))
                msg = mensajeEquivocadoMate[personajeActual];
            textNombre.text  = personajeActual;
            textDialogo.text = msg;
        }
    }

    void DarObjeto()
    {
        Debug.Log("DarObjeto: celular=" + esCaminoCelular + " mate=" + esCaminoMate + " personaje=" + personajeActual);
        if (esCaminoCelular)   DarObjetoCelular();
        else if (esCaminoMate) DarObjetoMate();
        else                   DarObjetoPelota();
        SiguienteLinea();
    }

    void DarObjetoPelota()
    {
        switch (personajeActual)
        {
            case "Messi":
                if (personajeDialogo != null && spriteMessiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMessiContento);
                InventarioManager.instancia?.QuitarObjeto("pelota");
                InventarioManager.instancia?.AgregarObjeto("copa");
                Anotador.AgregarPista("Messi", "Exhaló con satisfacción cuando cerraron las puertas. Estaba cantando. Cree que llegar a Catedral está asegurado.");
                break;
            case "Duki":
                if (personajeDialogo != null && spriteDukiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteDukiContento);
                InventarioManager.instancia?.QuitarObjeto("copa");
                InventarioManager.instancia?.AgregarObjeto("autografo");
                Anotador.AgregarPista("Duki", "Era una mujer. Dijo 'me voy a hacer viral' en medio del caos.");
                break;
            case "Milagros":
                if (personajeDialogo != null && spriteMilagrosContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMilagrosContento);
                InventarioManager.instancia?.QuitarObjeto("autografo");
                InventarioManager.instancia?.AgregarObjeto("screenshots");
                Anotador.AgregarPista("Milagros", "Vi a alguien con el control antes de subir. Lo guardó al ser observada. Chica con cadenas y ropa oscura.");
                break;
            case "Wanda":
                if (personajeDialogo != null && spriteWandaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteWandaContento);
                InventarioManager.instancia?.QuitarObjeto("screenshots");
                InventarioManager.instancia?.AgregarObjeto("cartera");
                Anotador.AgregarPista("Wanda", "Se para como en un escenario. Alguien del espectáculo con experiencia, no es novata.");
                break;
            case "Susana":
                if (personajeDialogo != null && spriteSusanaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteSusanaContento);
                InventarioManager.instancia?.QuitarObjeto("cartera");
                InventarioManager.instancia?.AgregarObjeto("martinFierro");
                Anotador.AgregarPista("Susana", "Cuando encontraron el control, miró a los demás en vez de mirarlo. Joven, del ambiente artístico.");
                break;
            case "Francella":
                if (personajeDialogo != null && spriteFrancellaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteFrancellaContento);
                InventarioManager.instancia?.QuitarObjeto("martinFierro");
                InventarioManager.instancia?.AgregarObjeto("mate");
                Anotador.AgregarPista("Francella", "Acariciaba el control con el pulgar. Tenía anillos grandes.");
                break;
            case "Vagabundo":
                if (personajeDialogo != null && spriteVagabundoContento != null)
                    personajeDialogo.MostrarPersonaje(spriteVagabundoContento);
                InventarioManager.instancia?.QuitarObjeto("mate");
                Anotador.AgregarPista("Vagabundo", "Miró a los demás antes de mirar el control. Tenía tatuajes.");
                break;
        }
    }

    void DarObjetoMate()
    {
        switch (personajeActual)
        {
            case "Vagabundo":
                if (personajeDialogo != null && spriteVagabundoContento != null)
                    personajeDialogo.MostrarPersonaje(spriteVagabundoContento);
                InventarioManager.instancia?.QuitarObjeto("mate");
                InventarioManager.instancia?.AgregarObjeto("cigarrillo");
                Anotador.AgregarPista("Vagabundo", "Miró a los demás antes de mirar el control. Tenía tatuajes.");
                break;
            case "Duki":
                if (personajeDialogo != null && spriteDukiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteDukiContento);
                InventarioManager.instancia?.QuitarObjeto("cigarrillo");
                InventarioManager.instancia?.AgregarObjeto("autografo");
                Anotador.AgregarPista("Duki", "Era una mujer. Dijo 'me voy a hacer viral' en medio del caos.");
                break;
            case "Milagros":
                if (personajeDialogo != null && spriteMilagrosContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMilagrosContento);
                InventarioManager.instancia?.QuitarObjeto("autografo");
                InventarioManager.instancia?.AgregarObjeto("screenshots");
                Anotador.AgregarPista("Milagros", "Vi a alguien con el control antes de subir. Lo guardó al ser observado. Un chico con cadenas y ropa oscura.");
                break;
            case "Wanda":
                if (personajeDialogo != null && spriteWandaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteWandaContento);
                InventarioManager.instancia?.QuitarObjeto("screenshots");
                InventarioManager.instancia?.AgregarObjeto("cartera");
                Anotador.AgregarPista("Wanda", "Se para como en un escenario. Alguien del espectáculo con experiencia.");
                break;
            case "Susana":
                if (personajeDialogo != null && spriteSusanaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteSusanaContento);
                InventarioManager.instancia?.QuitarObjeto("cartera");
                InventarioManager.instancia?.AgregarObjeto("martinFierro");
                Anotador.AgregarPista("Susana", "Cuando encontraron el control, miró a los demás en vez de mirarlo. Joven, del ambiente artístico.");
                break;
            case "Messi":
                if (personajeDialogo != null && spriteMessiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMessiContento);
                InventarioManager.instancia?.QuitarObjeto("martinFierro");
                InventarioManager.instancia?.AgregarObjeto("copa");
                Anotador.AgregarPista("Messi", "Exhaló con satisfacción cuando cerraron las puertas. Estaba cantando. Cree que llegar a Catedral está asegurado.");
                break;
            case "Francella":
                if (personajeDialogo != null && spriteFrancellaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteFrancellaContento);
                InventarioManager.instancia?.QuitarObjeto("copa");
                Anotador.AgregarPista("Francella", "Acariciaba el control con el pulgar. Tenía anillos grandes.");
                break;
        }
    }

    void DarObjetoCelular()
    {
        switch (personajeActual)
        {
            case "Milagros":
                if (personajeDialogo != null && spriteMilagrosContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMilagrosContento);
                InventarioManager.instancia?.QuitarObjeto("celular");
                InventarioManager.instancia?.AgregarObjeto("screenshots");
                Anotador.AgregarPista("Milagros", "Vi a alguien con el control antes de subir. Lo guardó al ser observada. Una señora coqueta, bien vestida.");
                break;
            case "Wanda":
                if (personajeDialogo != null && spriteWandaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteWandaContento);
                InventarioManager.instancia?.QuitarObjeto("screenshots");
                InventarioManager.instancia?.AgregarObjeto("cartera");
                Anotador.AgregarPista("Wanda", "Se para como en un escenario. Alguien del espectáculo con experiencia, no es novata.");
                break;
            case "Susana":
                if (personajeDialogo != null && spriteSusanaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteSusanaContento);
                InventarioManager.instancia?.QuitarObjeto("cartera");
                InventarioManager.instancia?.AgregarObjeto("martinFierro");
                Anotador.AgregarPista("Susana", "Cuando encontraron el control, miró a los demás en vez de mirarlo. Joven, del ambiente artístico.");
                break;
            case "Francella":
                if (personajeDialogo != null && spriteFrancellaContento != null)
                    personajeDialogo.MostrarPersonaje(spriteFrancellaContento);
                InventarioManager.instancia?.QuitarObjeto("martinFierro");
                InventarioManager.instancia?.AgregarObjeto("mate");
                Anotador.AgregarPista("Francella", "Acariciaba el control con el pulgar. Tenía brillos en la ropa.");
                break;
            case "Vagabundo":
                if (personajeDialogo != null && spriteVagabundoContento != null)
                    personajeDialogo.MostrarPersonaje(spriteVagabundoContento);
                InventarioManager.instancia?.QuitarObjeto("mate");
                InventarioManager.instancia?.AgregarObjeto("cigarrillo");
                Anotador.AgregarPista("Vagabundo", "Miró a los demás antes de mirar el control. Tenía tatuajes.");
                break;
            case "Duki":
                if (personajeDialogo != null && spriteDukiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteDukiContento);
                InventarioManager.instancia?.QuitarObjeto("cigarrillo");
                InventarioManager.instancia?.AgregarObjeto("autografo");
                Anotador.AgregarPista("Duki", "Era una mujer. Dijo 'todos me van a mirar' en medio del caos.");
                break;
            case "Messi":
                if (personajeDialogo != null && spriteMessiContento != null)
                    personajeDialogo.MostrarPersonaje(spriteMessiContento);
                InventarioManager.instancia?.QuitarObjeto("autografo");
                Anotador.AgregarPista("Messi", "Tenía una sonrisita cuando arrancó el subte. La cara del que ya sabe que ganó.");
                break;
        }
    }

    void CerrarDialogo()
    {
        Debug.Log("CerrarDialogo ejecutado");
        if (panelInventario != null) panelInventario.SetActive(false);
        if (InventarioManager.instancia != null)
        {
            InventarioManager.instancia.modoSeleccion    = false;
            InventarioManager.instancia.alClickearObjeto = null;
        }
        if (hud != null)              { hud.SetActive(true); Debug.Log("HUD activado"); }
        if (personajeDialogo != null) { personajeDialogo.OcultarPersonaje(); Debug.Log("Personaje ocultado"); }
        if (panelDialogo != null)     panelDialogo.SetActive(false);
        if (fondoDialogos != null)    { fondoDialogos.SetActive(false); Debug.Log("FondoDialogos desactivado"); }
        if (fondoPrincipal != null)   { fondoPrincipal.SetActive(true); Debug.Log("FondoPrincipal activado"); }
        if (botonDarObjeto != null)   botonDarObjeto.gameObject.SetActive(false);
        if (botonSiguiente != null)   botonSiguiente.gameObject.SetActive(true);
        if (personajesGrupo != null)  { personajesGrupo.SetActive(true); Debug.Log("PersonajesGrupo activado"); }
    }

    public void CancelarSeleccionInventario()
    {
        if (InventarioManager.instancia != null && InventarioManager.instancia.modoSeleccion)
        {
            InventarioManager.instancia.modoSeleccion    = false;
            InventarioManager.instancia.alClickearObjeto = null;
            if (panelInventario != null) panelInventario.SetActive(false);
            if (panelDialogo != null)    panelDialogo.SetActive(true);
        }
    }

    public void IrAAcusacion()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Acusacion");
    }
}