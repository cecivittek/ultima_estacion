using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoManager : MonoBehaviour
{
    // OBJETOS DEL JUGADOR
    private bool tienePelota = false;
    private bool tieneCigarrillo = false;
    private bool tieneScreenshots = false;
    private bool tieneCartera = false;
    private bool tieneMartinFierro = false;
    private bool tieneMate = false;
    private bool tieneAutografo = false;
    private bool tieneCopa = false;

    private int indiceDialogo = 0;
    private bool dialogoActivo = false;
    private string[][] dialogoActual;

    // ===================== MESSI =====================
    private string[][] dialogoMessiSinObjeto = new string[][]
    {
        new string[] { "Messi", "Hola. Sí, soy yo. No hace falta foto. Todo esto es una locura. Pero bueno. Ya vi cosas raras en Qatar." },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Messi", "Miro mucho. La gente no se da cuenta, pero analizo todo. Como en la cancha. Hay alguien acá que se mueve distinto. No nervioso. Calculado. Como un jugador que ya sabe cuál va a ser la jugada antes de que empiece." },
    };

    private string[][] dialogoMessiConObjeto = new string[][]
    {
        new string[] { "Messi", "Mira que bueno, nunca hubiera conseguido uno, Anto se va a volver loca. Dale, te cuento." },
        new string[] { "Messi", "Vi a alguien exhalar cuando cerraron las puertas. No de alivio. De satisfacción. Yo conozco esa cara. Es la cara de un jugador cuando el partido ya está ganado. Estaba sola pero le hablaba a 'su gente', me pareció raro. Esta persona cree que ya ganó. Que llegar a Catedral está asegurado." },
        new string[] { "Messi", "Cuidamela eh" },
        new string[] { "Messi", "Ya te dije todo lo que sé. ¿Tenés algo para comer?" },
    };

    // ===================== DUKI =====================
    private string[][] dialogoDukiSinObjeto = new string[][]
    {
        new string[] { "Duki", "¿Qué onda? Todo esto es una locura, bro. Venía a bancar a De Paul y ahora estoy huyendo de un T-Rex. Si esto no es una letra de trap, no sé qué es." },
        new string[] { "Jugador", "¿Escuchaste algo raro?" },
        new string[] { "Duki", "Mirá, tenía un auricular puesto pero el otro libre. Escuché una voz decir 'Catedral' con demasiada calma. En medio del caos, eso no es normal, bro. Y escuché algo como 'ya está todo listo'. Dicho bajito. Como para sí mismo." },
        new string[] { "Duki", "Bro, ¿qué es esto? No me sirve. Guardatelo." },
    };

    private string[][] dialogoDukiConObjeto = new string[][]
    {
        new string[] { "Duki", "¡No te puedo creer! ¡Es la verdad, que locura! Dale, te cuento algo más." },
        new string[] { "Duki", "La voz que escuché era de una mina. Hablaba en voz muy baja pero era claramente una mujer. Dijo algo raro: 'me voy a hacer viral'. En medio de un ataque de dinosaurios. Alguien que piensa en las redes ahora... me parece algo tétrico." },
        new string[] { "Duki", "Tomá. No hago favores pero acá tenés. Mi autógrafo." },
        new string[] { "Duki", "Ya está, bro. No se nada más. Estoy componiendo un nuevo tema. No me interrumpas." },
    };

    // ===================== MILAGROS PILAR =====================
    private string[][] dialogoMilagrosSinObjeto = new string[][]
    {
        new string[] { "Milagros Pilar", "¡Chicos, no se imaginan esto! ¡Un dinosaurio de verdad! Ay, hola. ¿Me seguís? No importa. ¿Tenés buena luz acá? Es una tragedia, obvio. Pero el contenido... el contenido es increíble." },
        new string[] { "Jugador", "¿Viste algo sospechoso?" },
        new string[] { "Milagros Pilar", "Yo capturo todo, es casi un reflejo. Vi a alguien que subió al subte sin correr. Todos corrían y esta persona caminaba. Caminaba como si supiera que el subte la iba a esperar." },
        new string[] { "Milagros Pilar", "Ay, que esperabas, ¿un canje?. ¿Tenés algo más interesante?" },
    };

    private string[][] dialogoMilagrosConObjeto = new string[][]
    {
        new string[] { "Milagros Pilar", "¡DUKI! ¡Ay, me muero literalmente! ¿Cómo lo conseguiste? ¿Me lo das? ¿Lo puedo postear? Okay okay." },
        new string[] { "Milagros Pilar", "Te cuento algo que no le dije a nadie. Antes de subir, en el caos, vi a alguien con el control en la mano. Lo guardó cuando empezaron a mirar. Era una señora, muy coqueta, bien vestida." },
        new string[] { "Milagros Pilar", "Mirá estas capturas. Son mi chisme mejor guardado." },
        new string[] { "Milagros Pilar", "Ya te conté todo, amigo. Ahora dejame que tengo que editar el reel de los dinosaurios. ¿Sabés el alcance que va a tener?" },
    };

    // ===================== WANDA =====================
    private string[][] dialogoWandaSinObjeto = new string[][]
    {
        new string[] { "Wanda", "Esto es un desastre absoluto. No puedo correr con estos tacos. ¿Vos quién sos? No te conozco ¿Trabajas para mi?" },
        new string[] { "Jugador", "¿Escuchaste algo raro en el vagón?" },
        new string[] { "Wanda", "Miro todo. Es un don. Vi a alguien que cuando apareció el control, no puso cara de sorpresa. Todo el mundo abrió los ojos. Pero esta persona, cara plana. Yo sé lo que es actuar sorpresa. Lo hice en televisión por años." },
        new string[] { "Wanda", "¿Esto me lo traés a mí? No, querida. Guardatelo." },
    };

    private string[][] dialogoWandaConObjeto = new string[][]
    {
        new string[] { "Wanda", "Ay, ay, ay. Me muero. ¿Sabes todo lo que puedo conseguir con esta captura de pantalla? Lo hundo como el Titanic en dos segundos si quiero." },
        new string[] { "Wanda", "No te voy a decir el nombre porque me tengo que guardar algo siempre para mi. Pero esta persona estaba parada como si fuera la reina del mundo y no la conoce nadie. Típico de las generaciones de ahora, hay que bajarlos del pony." },
        new string[] { "Wanda", "Tomá, se le calló a alguien en el caos y ya tengo esta edición." },
        new string[] { "Wanda", "Ya te dije un montón, no me hables más." },
    };

    // ===================== SUSANA =====================
    private string[][] dialogoSusanaSinObjeto = new string[][]
    {
        new string[] { "Susana", "¡Esto es una pesadilla! ¡Yo tenía grabación mañana! ¿Y vos quién sos, querido? En esta situación todos somos iguales. Bueno, casi." },
        new string[] { "Jugador", "¿Vio algo sospechoso?" },
        new string[] { "Susana", "Mirá, yo tengo ojo clínico. Años de entrevistar gente. Hay alguien acá que tiene un aura rara. Alguien que parece que no se sorprendió cuando vió los dinosaurios." },
        new string[] { "Susana", "¿Y que hago yo con esto? Traeme algo de valor y hablamos." },
    };

    private string[][] dialogoSusanaConObjeto = new string[][]
    {
        new string[] { "Susana", "¡MI CARTERA! ¡Ay, pensé que la había perdido para siempre! Las fotos de Pacha... el rouge... todo está. Vos sos un ángel." },
        new string[] { "Susana", "Vi la cara del infiltrado, querido. Cuando encontraron el control, todos miraron al control. Esta persona miró a los demás. Era una chica joven que buscaba llamar la atención, como siempre." },
        new string[] { "Susana", "Mereces un premio por toda esta investigación. Tomá." },
        new string[] { "Susana", "Ya te dije todo, querido. Ahora necesito sentarme. Este vagón está muy frío ¿Alguien tiene un Valium?" },
    };

    // ===================== FRANCELLA =====================
    private string[][] dialogoFrancellaSinObjeto = new string[][]
    {
        new string[] { "Francella", "¡Eh! ¡Qué cosa, no! ¡Quién lo iba a decir! Dinosaurios. En Buenos Aires. Esto parece de película." },
        new string[] { "Jugador", "¿Notaste algo raro?" },
        new string[] { "Francella", "Notás cosas cuando sos actor. Hay alguien acá que está actuando el miedo. Y el miedo actuado tiene un ritmo distinto al miedo real. Lo noto en la respiración, en los ojos. Años de oficio." },
        new string[] { "Francella", "Ay, no. Esto no lo necesito. Pero aprecio el gesto." },
    };

    private string[][] dialogoFrancellaConObjeto = new string[][]
    {
        new string[] { "Francella", "¡No te puedo creer! ¡Es la verdad, que locura! Ahora yo también se cuanto pesa la copa del mundo, esta pesadita eh. Está bien. Te cuento todo." },
        new string[] { "Francella", "Vi a alguien acariciar el control con el pulgar. Así, suave. Como quien tiene algo que le da seguridad. Tenia anillos, grandes. Lo vi claramente." },
        new string[] { "Francella", "No tengo nada para vos, disculpá, salí corriendo con lo que tenía en la mano, te puedo ofrecer un mate calentito." },
        new string[] { "Francella", "Ya te dije todo lo que podía decirte, espero que te sea de ayuda." },
    };

    // ===================== VAGABUNDA =====================
    private string[][] dialogoVagabundaSinObjeto = new string[][]
    {
        new string[] { "Vagabunda", "Ey. No me pises las cosas. Dinosaurios... dinosaurios. Vi cosas peores en el Once. ¿Qué querés?" },
        new string[] { "Jugador", "¿Viste algo raro en el vagón?" },
        new string[] { "Vagabunda", "Todo me parece raro. Pero hay alguien acá que no está asustado. Cuando hay dinosaurios afuera, la gente se asusta. Yo vivo en la calle, aprendo a leer a la gente. El que no tiene miedo... algo sabe." },
        new string[] { "Vagabunda", "No, gracias, pero no puedo hacer nada con eso. ¿No tenés algo calentito para darme?" },
    };

    private string[][] dialogoVagabundaConObjeto = new string[][]
    {
        new string[] { "Vagabunda", "Mmm que rico, está un poco lavado pero por lo menos me calienta el cuerpo." },
        new string[] { "Vagabunda", "Ya que estamos girando el mate, te cuento: Vi a alguien que cuando encontraron el control, miró a los demás antes de mirarlo. No se el nombre, tenía tatuajes creo." },
        new string[] { "Vagabunda", "Ya te dije todo lo que sé. Déjame tranquilo que el subte me da sueño." },
    };

    void Update()
    {if (Input.GetKeyDown(KeyCode.Alpha1)) IniciarDialogoMessi();
if (Input.GetKeyDown(KeyCode.Alpha2)) IniciarDialogoDuki();
if (Input.GetKeyDown(KeyCode.Alpha3)) IniciarDialogoMilagros();
if (Input.GetKeyDown(KeyCode.Alpha4)) IniciarDialogoWanda();
if (Input.GetKeyDown(KeyCode.Alpha5)) IniciarDialogoSusana();
if (Input.GetKeyDown(KeyCode.Alpha6)) IniciarDialogoFrancella();
if (Input.GetKeyDown(KeyCode.Alpha7)) IniciarDialogoVagabunda();
    }

    // ===================== INICIAR DIALOGOS =====================
    public void IniciarDialogoMessi()
    {
        dialogoActual = tienePelota ? dialogoMessiConObjeto : dialogoMessiSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoDuki()
    {
        dialogoActual = tieneCigarrillo ? dialogoDukiConObjeto : dialogoDukiSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoMilagros()
    {
        dialogoActual = tieneAutografo ? dialogoMilagrosConObjeto : dialogoMilagrosSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoWanda()
    {
        dialogoActual = tieneScreenshots ? dialogoWandaConObjeto : dialogoWandaSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoSusana()
    {
        dialogoActual = tieneCartera ? dialogoSusanaConObjeto : dialogoSusanaSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoFrancella()
    {
        dialogoActual = tieneMartinFierro ? dialogoFrancellaConObjeto : dialogoFrancellaSinObjeto;
        IniciarDialogo();
    }

    public void IniciarDialogoVagabunda()
    {
        dialogoActual = tieneMate ? dialogoVagabundaConObjeto : dialogoVagabundaSinObjeto;
        IniciarDialogo();
    }

    void IniciarDialogo()
    {
        indiceDialogo = 0;
        dialogoActivo = true;
        Debug.Log(dialogoActual[indiceDialogo][0] + ": " + dialogoActual[indiceDialogo][1]);
    }

    void SiguienteLinea()
    {
        indiceDialogo++;
        if (indiceDialogo >= dialogoActual.Length)
        {
            dialogoActivo = false;
            Debug.Log("--- Fin del dialogo ---");
            return;
        }
        Debug.Log(dialogoActual[indiceDialogo][0] + ": " + dialogoActual[indiceDialogo][1]);
    }

    // ===================== RECOGER OBJETOS =====================
    public void RecogerPelota() { tienePelota = true; Debug.Log("Recogiste la pelota"); }
    public void RecogerCigarrillo() { tieneCigarrillo = true; Debug.Log("Recogiste el cigarrillo"); }
    public void RecogerScreenshots() { tieneScreenshots = true; Debug.Log("Recogiste los screenshots"); }
    public void RecogerCartera() { tieneCartera = true; Debug.Log("Recogiste la cartera"); }
    public void RecogerMartinFierro() { tieneMartinFierro = true; Debug.Log("Recogiste el Martin Fierro"); }
    public void RecogerMate() { tieneMate = true; Debug.Log("Recogiste el mate"); }
    public void RecogerAutografo() { tieneAutografo = true; Debug.Log("Recogiste el autografo"); }
}