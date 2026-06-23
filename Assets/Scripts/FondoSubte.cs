using UnityEngine;

public class FondoSubte : MonoBehaviour
{
    public SpriteRenderer tunel1;
    public SpriteRenderer tunel2;
    public float velocidadScroll = 2f;

    private float anchuraSprite;

    void Start()
    {
        if (tunel1 != null)
            anchuraSprite = tunel1.bounds.size.x;

        if (tunel1 != null && tunel2 != null)
            tunel2.transform.position = tunel1.transform.position + new Vector3(anchuraSprite, 0, 0);
    }

    void Update()
    {
        Mover(tunel1, tunel2);
        Mover(tunel2, tunel1);
    }

    void Mover(SpriteRenderer sr, SpriteRenderer otro)
    {
        if (sr == null || otro == null) return;
        sr.transform.position += Vector3.left * velocidadScroll * Time.deltaTime;

        float bordeIzq = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        if (sr.transform.position.x + anchuraSprite / 2f < bordeIzq)
        {
            sr.transform.position = new Vector3(
                otro.transform.position.x + anchuraSprite,
                sr.transform.position.y,
                sr.transform.position.z
            );
        }
    }
}
