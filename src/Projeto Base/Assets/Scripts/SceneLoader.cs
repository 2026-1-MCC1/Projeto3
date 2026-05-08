using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


// --- Modificadores de acesso para classes e variáveis ---
public class SceneLoader : MonoBehaviour
{
    public void CarregarJogo1()
    {
        Invoke("Load1", 1f);
    }


    // --- Carrega a cena "Movimentação personagem versão 3" ---
    void Load1()
    {
        SceneManager.LoadScene("Movimentação personagem versão 3");
    }

    public void CarregarJogo2()
    {
        Invoke("Load2", 1f);
    }
    void Load2()
    {
        SceneManager.LoadScene("fase 2");
    }
    public void CarregarJogo3()
    {
        Invoke("Load3", 1f);
    }
    void Load3()
    {
        SceneManager.LoadScene("fase 3");
    }
}