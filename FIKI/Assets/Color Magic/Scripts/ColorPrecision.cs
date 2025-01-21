using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPrecision : MonoBehaviour
{
    private ColorSliders colorSliders;
    private ColorGenerator colorTarget;

    private Color selectedColor;
    private Color colorToMatch;

    private float precision;

    private void Start()
    {
        colorSliders = GameObject.FindObjectOfType<ColorSliders>();
        colorTarget = GameObject.FindObjectOfType<ColorGenerator>();
    }

    public void Update()
    {
        precision = GetPrecision();
    }

    public float GetPrecision()
    {
        selectedColor = colorSliders.GetColor();
        colorToMatch = colorTarget.GetColor();

        float percentageR = GetPercentage(selectedColor.r, colorToMatch.r);
        float percentageG = GetPercentage(selectedColor.g, colorToMatch.g);
        float percentageB = GetPercentage(selectedColor.b, colorToMatch.b);
    

        return (percentageR + percentageG + percentageB) / 3; 
    }

    private float GetPercentage(float color1, float color2)
    {
        float difference = Mathf.Abs(color1 - color2);
        return 100f - Mathf.Min(difference * 100f, 100f);
    }
}