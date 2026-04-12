using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CarregarJogo()
    {
        Invoke("Load", 1f);
    }

    void Load()
    {
        SceneManager.LoadScene("Movimentação personagem versão 3");
    }
}