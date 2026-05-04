using UnityEngine;
using System.Collections;


// --- Modificadores de acesso para classes e variáveis ---
public class PlayerHealth : MonoBehaviour
{
    public int health = 6;
    // VISUAL
    public Renderer playerRenderer;
    public Color corDano = Color.red;
    public float duracaoFlash = 0.2f;

    private Color corOriginal;
    private bool tomandoDano = false;
    private SkinnedMeshRenderer[] renderers;
    private Color[] coresOriginais;

    // SOM
    public AudioSource audioSource;
    public AudioClip somDano;

    void Start()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        coresOriginais = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = new Material(renderers[i].material);

            // pega cor original
            coresOriginais[i] = renderers[i].material.GetColor("_BaseColor");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (!tomandoDano)
        {
            StartCoroutine(FlashDano());
        }

        TocarSomDano();
        if (health <= 0)
        {
            Die();
        }
    }
    IEnumerator FlashDano()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetColor("_BaseColor", Color.red);
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetColor("_BaseColor", coresOriginais[i]);
        }
    }

    void TocarSomDano()
    {
        if (audioSource != null && somDano != null)
        {
            audioSource.PlayOneShot(somDano);
        }
    }

    // --- Faz com que o jogador morra ---
    void Die()
    {
        Debug.Log("Jogador morreu");

        Destroy(gameObject);
    }
}