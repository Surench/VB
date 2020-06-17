using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    [SerializeField] public Material[] BallMaterials;
    [SerializeField] public Material[] SplashMaterials;
    [SerializeField] public Color[] SplashColors;
    [SerializeField] public Material[] BlockMaterials;
    [SerializeField] public Material[] RedBlockMaterials;
    [SerializeField] public Material[] Tube1Materials;
    [SerializeField] public Material[] Tube2Materials;
    [SerializeField] public Color[] cameraColors;

    [SerializeField] Camera cam;

    public static int CurrentColorIndex = 0;

    public void InitColor ()
    {
        int currentLevel = LevelManager.currentLevel;
        int maxColorIndex = BallMaterials.Length;

        if (currentLevel < maxColorIndex)
        {
            CurrentColorIndex = currentLevel;
        }
        else
        {
            float division = (float)currentLevel * 1f / maxColorIndex * 1f;
            int totalMax = (int)Mathf.Floor(division) * (maxColorIndex);

            CurrentColorIndex = currentLevel - (totalMax);
        }

        cam.backgroundColor = cameraColors[CurrentColorIndex];
    }
}
