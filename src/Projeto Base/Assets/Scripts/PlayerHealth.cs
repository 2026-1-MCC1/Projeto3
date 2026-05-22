using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HealthUI healthUI;

    public int health = 6;
    public Color corDano = Color.red;
    public float duracaoFlash = 0.2f;
    public FimJogo fimJogo;
    private Color corOriginal;
    private bool tomandoDano = false;
    private SkinnedMeshRenderer[] renderers;
    private Color[] coresOriginais;
    private CharacterController controller;
    private Animator animator;
    private Pistola pistola;
    public bool morto = false;
    public AudioSource audioSource;
    public AudioClip somDano;

    void Start()
    {
        pistola = GetComponentInChildren<Pistola>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        coresOriginais = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = new Material(renderers[i].material);
            coresOriginais[i] = renderers[i].material.GetColor("_BaseColor");
        }
        if (healthUI == null)
        {
            healthUI = FindFirstObjectByType<HealthUI>();
        }
        if (healthUI != null)
        {
            healthUI.UpdateHearts(health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (morto) return;
        health -= damage;
        if (healthUI != null)
        {
            healthUI.UpdateHearts(health);
        }
        healthUI?.UpdateHearts(health);

        TocarSomDano();

        if (!tomandoDano)
        {
            StartCoroutine(FlashDano());
        }

        if (health <= 0)
        {
            morto = true;
            StartCoroutine(Die());
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

    IEnumerator Die()
    {
        morto = true;
        Debug.Log("Jogador morreu");
        animator.SetTrigger("Die");
        GetComponent<CharacterController>().enabled = false;
        var arma = GetComponentInChildren<Pistola>();
        yield return null;
        if (arma != null)
        {
            arma.enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(1.5f);
        LancamentoBola.Pontuacao = 0;
        fimJogo.Exibir();
    }
}