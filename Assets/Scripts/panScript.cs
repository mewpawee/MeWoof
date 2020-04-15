using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panScript : MonoBehaviour
{
    public bool panOn = true;
    private float panTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cooking();
        CheckPan();
    }

    void Cooking() {
        if (steakScript.isCooking == true)
        {
            panTime = panTime + Time.deltaTime;
        }
        else if (steakScript.isCooking == false && panTime >= 0) {
            panTime = panTime + Time.deltaTime;
        }
    }

    void CheckPan() {

        if (panTime > 10)
        {
            steakScript.state = 2;
        }
        else if (panTime > 5)
        {
            steakScript.state = 1;
        }
        
    }
}
