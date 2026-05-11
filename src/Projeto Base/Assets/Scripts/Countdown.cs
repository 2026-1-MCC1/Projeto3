using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    // --- Texto da contagem regressiva na UI ---
    public TextMeshProUGUI countdownText;

    IEnumerator Start()
    {
        // --- Congela todo o jogo ---
        Time.timeScale = 0f;

        // --- Ativa o texto da contagem ---
        countdownText.gameObject.SetActive(true);

        // --- Mostra o número 3 ---
        countdownText.text = "3";
        yield return StartCoroutine(RealTimeWait(1f));

        // --- Mostra o número 2 ---
        countdownText.text = "2";
        yield return StartCoroutine(RealTimeWait(1f));

        // --- Mostra o número 1 ---
        countdownText.text = "1";
        yield return StartCoroutine(RealTimeWait(1f));

        // --- Mostra mensagem de início ---
        countdownText.text = "GO!";
        yield return StartCoroutine(RealTimeWait(0.5f));

        // --- Esconde o texto da tela ---
        countdownText.gameObject.SetActive(false);

        // --- Descongela o jogo ---
        Time.timeScale = 1f;
    }

    // --- Coroutine que espera usando tempo real ---
    // --- Funciona mesmo com Time.timeScale = 0 ---
    IEnumerator RealTimeWait(float time)
    {
        float timer = 0f;   // --- Variável que controla o tempo passado ---

        // --- Repete até atingir o tempo desejado ---
        while (timer < time)
        {
            timer += Time.unscaledDeltaTime;    // --- Soma tempo real independente do jogo estar pausado ---
            yield return null;  // --- Espera o próximo frame ---
        }
    }
}