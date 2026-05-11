using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPause : MonoBehaviour
{
    public GameObject painelPause;

    public MonoBehaviour PlayerController;

    private bool pausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
            {
                Retornar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        painelPause.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerController.enabled = false;
        pausado = true;
    }

    public void Retornar()
    {
        painelPause.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerController.enabled = true;
        pausado = false;
    }

    public void IrMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}