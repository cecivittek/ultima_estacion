using UnityEngine;
using UnityEngine.UI;
 
public class BotonPersonaje : MonoBehaviour
{
    [SerializeField] private AcusacionManager manager;
    [SerializeField] private string nombrePersonaje;
    public Image imagenFondo;
    public SpriteRenderer spritePersonaje;
 
    public void AlClickear()
    {
        manager.SeleccionarPersonaje(nombrePersonaje, this);
    }
}
 