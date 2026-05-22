using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 120f;
    [SerializeField] FimJogo fimJogo;
    [SerializeField] AudioClip sirene;
    bool gameOver = false;
    bool sireneTocouu = false;
    AudioSource audioSource;

    void Start()
    {
        remainingTime = 120f;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (gameOver) return;

        if (LancamentoBola.Pontuacao >= 47)
        {
            gameOver = true;
            return;
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Toca sirene quando faltar 30 segundos
            if (remainingTime <= 30f && !sireneTocouu)
            {
                sireneTocouu = true;
                if (sirene != null && audioSource != null)
                    audioSource.PlayOneShot(sirene);
            }
        }
        else
        {
            remainingTime = 0;
            gameOver = true;

            if (fimJogo != null)
                fimJogo.Exibir();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}