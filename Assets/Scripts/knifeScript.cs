using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeScript : MonoBehaviour
{
    private static int CountColli = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCutting();
        Debug.Log(steakScript.cutState);
    }


private void CheckCutting()
    {
        if (CountColli == 1)
        {
            steakScript.cutState = 1;
        }
        else if (CountColli >= 2)
        {
            steakScript.cutState = 2;
        }
    }


private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "steak")
        {

            CountColli = CountColli + 1; 
        }
    }
}


