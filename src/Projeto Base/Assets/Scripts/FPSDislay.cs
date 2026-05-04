using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TMP_Text fpsText;

    private float timer;
    private int frameCount;

    void Update()
    {
        frameCount++;
        timer += Time.unscaledDeltaTime;

        if (timer >= 0.5f) // atualiza a cada meio segundo
        {
            int fps = Mathf.RoundToInt(frameCount / timer);

            frameCount = 0;
            timer = 0f;
            fpsText.text = $"FPS: {Application.targetFrameRate}";
        }
    }
}