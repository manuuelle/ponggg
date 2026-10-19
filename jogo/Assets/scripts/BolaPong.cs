
using UnityEngine;

public class BolaPong : MonoBehaviour
{
    public float velocidade = 6f;
    public float limiteX = 8f;

    private Rigidbody2D rb;
    private PongGameManager gerente;
    private bool pontoContado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gerente = FindFirstObjectByType<PongGameManager>();

        if (rb == null || gerente == null)
        {
            Debug.LogError("Verifique o Rigidbody2D e o GameManager.");
            enabled = false;
            return;
        }

        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        LancarBola();
    }

    void LancarBola()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(-0.5f, 0.5f);

        Vector2 direcao = new Vector2(x, y).normalized;
        rb.linearVelocity = direcao * velocidade;
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Quicar nas raquetes
        if (colisao.gameObject.CompareTag("Raquete"))
        {
            float direcaoX = transform.position.x -
                             colisao.transform.position.x;

            float direcaoY = (transform.position.y -
                              colisao.transform.position.y) / 2f;

            if (direcaoX == 0f)
                direcaoX = rb.linearVelocity.x >= 0f ? 1f : -1f;

            Vector2 direcao = new Vector2(direcaoX, direcaoY).normalized;

            // Garante que a bola siga para o lado oposto
            direcao.x = Mathf.Sign(direcaoX) *
                        Mathf.Abs(direcao.x);

            rb.linearVelocity = direcao * velocidade;
        }

        // As paredes superior e inferior quicam automaticamente
        // pelo Collider2D e Rigidbody2D.
    }

    void Update()
    {
        if (pontoContado || gerente == null)
            return;

        if (transform.position.x >= limiteX)
        {
            pontoContado = true;
            gerente.MarcarPonto(1);
        }
        else if (transform.position.x <= -limiteX)
        {
            pontoContado = true;
            gerente.MarcarPonto(2);
        }
    }
}
