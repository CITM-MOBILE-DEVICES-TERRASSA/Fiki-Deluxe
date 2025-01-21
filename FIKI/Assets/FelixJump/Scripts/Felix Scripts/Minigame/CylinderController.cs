using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderController : MonoBehaviour
{

    [Header("Cilindro:")]
    public static CylinderController instance;

    public Slider cylinderSlider;
    public Transform cylinder;
    public float rotationSpeed = 10f;


    [Header("Paneles")]
    public RectTransform endPanel;
    public RectTransform lostPanel; 

    private void Awake()
    {
        //Singleton sin persistencia
        if(instance == null)
        {
            instance = this;
        }
        else
        {
           Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cylinder && cylinderSlider)
        {
            cylinder.Rotate(new Vector3(0, -cylinderSlider.value * rotationSpeed * Time.deltaTime, 0));
        }
       
    }
}
