using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;
    void Start()
    {
        hearts = GetComponentsInChildren<UnityEngine.UI.Image>();
    }

    public void UpdateHearts(int health)
    {
        if (hearts == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].enabled = i < health;
            }
        }
    }
}
