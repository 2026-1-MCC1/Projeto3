using UnityEngine;
using UnityEngine.SceneManagement;


// --- Modificadores de acesso para classes e variáveis ---
public class SceneLoader : MonoBehaviour
{
    public void CarregarJogo()
    {
        Invoke("Load", 1f);
    }


    // --- Carrega a cena "Movimentação personagem versão 3" ---
    void Load()
    {
        SceneManager.LoadScene("Movimentação personagem versão 3");
    }
}