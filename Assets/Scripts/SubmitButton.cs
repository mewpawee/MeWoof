using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class SubmitButton : MonoBehaviour
{
    bool submitState = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<VRTK_InteractableObject>().IsTouched()) {
            submitState = false;
        }
        else if (GetComponent<VRTK_InteractableObject>().IsTouched() && !submitState)
        {
            submitState = true;
            if (Manager.gameOn)
            {
                Manager.submitDish();
            }
        }
    }
}
