using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


// --- Modificadores de acesso para classes e variáveis ---
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 120f;


    bool gameOver = false;
    void Start()
    {
        remainingTime = 450f;
    }


    // --- Atualiza o cronômetro, reduz o tempo e exibe na tela em minutos e segundos ---
    void Update()
    {
        if (gameOver) return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime =0;
            gameOver = true;

            //Exibir tela de fim de jogo
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("TelaDerrota");

        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}



