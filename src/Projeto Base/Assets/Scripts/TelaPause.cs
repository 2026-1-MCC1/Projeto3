using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelaPause : MonoBehaviour
{
    public void Ativar()
    {
        this.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Desativar()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("Movimentação personagem versão 3");
    }
    public void SairJogo()
    {
        SceneManager.LoadScene("Menu");
    }
}
