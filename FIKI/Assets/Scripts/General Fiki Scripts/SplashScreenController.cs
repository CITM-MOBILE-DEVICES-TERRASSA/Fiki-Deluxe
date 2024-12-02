using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
    float contador = 0;
    bool transicion = false;
    // Start is called before the first frame update
    void Start()
    {
        //LevelTransitionController.instance.StartTransition(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;

        if(contador > 3 && !transicion)
        {
            transicion = true;
            LevelTransitionController.instance.StartTransition(1, 2);
        }
    }


    private IEnumerator Transition(float time)
    {
        yield return new WaitForSeconds(time);
    }

}
