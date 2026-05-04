using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyManager : MonoBehaviour
{
    public float skySpeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }
}
