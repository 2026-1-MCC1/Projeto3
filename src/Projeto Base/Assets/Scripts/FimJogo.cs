using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FimJogo : MonoBehaviour
{
    public GameObject painelDerrota;
    public void Exibir()
    {
        painelDerrota.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Esconder()
    {
        painelDerrota.gameObject.SetActive(false);
    }
    public void TentarNovamente()
    {
        Time.timeScale = 1f;

        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }
    public void irMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
