
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PongGameManager : MonoBehaviour
{
    public int pontuacaoMaxima = 5;
    public int pontosJogador1 = 0;
    public int pontosJogador2 = 0;

    public TMP_Text textoPontos;
    public GameObject telaGameOver;
    public TMP_Text textoGameOver;
    public GameObject prefabBola;

    private GameObject bolaAtual;
    private bool jogoFinalizado = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (telaGameOver != null)
            telaGameOver.SetActive(false);

        AtualizarPlacar();
        CriarBola();
    }

    public void MarcarPonto(int jogador)
    {
        if (jogoFinalizado)
            return;

        if (jogador == 1)
            pontosJogador1++;
        else if (jogador == 2)
            pontosJogador2++;
        else
            return;

        AtualizarPlacar();

        if (pontosJogador1 >= pontuacaoMaxima)
        {
            FinalizarJogo("Jogador 1 venceu!");
        }
        else if (pontosJogador2 >= pontuacaoMaxima)
        {
            FinalizarJogo("Jogador 2 venceu!");
        }
        else
        {
            CriarBola();
        }
    }

    void AtualizarPlacar()
    {
        if (textoPontos != null)
        {
            textoPontos.text =
                pontosJogador1 + " x " + pontosJogador2;
        }
    }

    void CriarBola()
    {
        if (bolaAtual != null)
            Destroy(bolaAtual);

        if (prefabBola == null)
        {
            Debug.LogError("Prefab Bola não configurado!");
            return;
        }

        bolaAtual = Instantiate(
            prefabBola,
            Vector3.zero,
            Quaternion.identity
        );
    }

    void FinalizarJogo(string mensagem)
    {
        jogoFinalizado = true;

        if (bolaAtual != null)
            Destroy(bolaAtual);

        if (telaGameOver != null)
            telaGameOver.SetActive(true);

        if (textoGameOver != null)
            textoGameOver.text = mensagem;

        Time.timeScale = 0f;
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
