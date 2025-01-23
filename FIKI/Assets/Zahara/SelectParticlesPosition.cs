using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectParticlesPosition : MonoBehaviour
{

    public Slider sliderR;
    public Slider sliderG;
    public Slider sliderB;

    private bool isDraggingSliderR = false;
    private bool isDraggingSliderG = false;
    private bool isDraggingSliderB = false;

    public AudioSource slideFx;
    void Start()
    {
        // Asignar eventos usando los métodos del inspector
        sliderR.onValueChanged.AddListener(value => OnSliderValueChanged(1, value));
        sliderG.onValueChanged.AddListener(value => OnSliderValueChanged(2, value));

        sliderB.onValueChanged.AddListener(value => OnSliderValueChanged(3, value));

        this.GetComponentInChildren<ParticleSystem>().enableEmission = false;


    }

    private void Update()
    {
        if (!sliderR.IsActive())
        {
            this.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        }
    }

    public void OnSliderBeginDrag(int sliderId)
    {
        slideFx.Play();
        if (sliderId == 1) isDraggingSliderR = true;
        else if (sliderId == 2) isDraggingSliderG = true;
        else if (sliderId == 3) isDraggingSliderB = true;

        Debug.Log($"Comenzó a arrastrar el slider {sliderId}");
    }

    public void OnSliderEndDrag(int sliderId)
    {
        if (sliderId == 1) { isDraggingSliderR = false; }
        else if (sliderId == 2) { isDraggingSliderG = false; }
        else if (sliderId == 3) { isDraggingSliderB = false; }
        this.GetComponentInChildren<ParticleSystem>().enableEmission = false;

        Debug.Log($"Terminó de arrastrar el slider {sliderId}");
    }

    public void OnSliderValueChanged(int sliderId, float value)
    {

        Slider targetSlider;

        if (sliderId == 1 && isDraggingSliderR)
        {
            targetSlider = sliderR;

        }
        else if (sliderId == 2 && isDraggingSliderG)
        {
            targetSlider = sliderG;
           
        }
        else if (sliderId == 3 && isDraggingSliderB)
        {
            targetSlider = sliderB;
        }
        else
        {
            targetSlider = sliderR;
        }

        this.GetComponent<RectTransform>().position = targetSlider.handleRect.position;
        this.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        this.GetComponentInChildren<ParticleSystem>().startColor = targetSlider.colors.normalColor;



    }
}
