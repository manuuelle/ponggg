using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 6f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    void Launch()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(-0.7f, 0.7f);

        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }


    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Raquete"))
        {
            // Identifica em qual lado está a raquete
            float direcaoX;

            if (colisao.transform.position.x < transform.position.x)
                direcaoX = 1f;  // Raquete esquerda: bola vai para a direita
            else
                direcaoX = -1f; // Raquete direita: bola vai para a esquerda

            // Ajusta o ângulo de acordo com o ponto de impacto
            float direcaoY =
                (transform.position.y - colisao.transform.position.y) / 1.5f;

            Vector2 direcao = new Vector2(direcaoX, direcaoY).normalized;

            rb.linearVelocity = direcao * speed;
        }
    }
}
