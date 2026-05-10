using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FimJogo2 : MonoBehaviour
{
    public void Exibir()
    {
        this.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Esconder()
    {
        this.gameObject.SetActive(false);
    }
    public void TentarNovamente()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Fase 2");

    }
    public void SairJogo()
    {
        SceneManager.LoadScene("Menu");
    }
}