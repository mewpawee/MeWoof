using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VRTK_InteractableObject>().IsTouched())
        {
            if (!Manager.gameOn) {
               Manager.gameStart();
            }
        }
    }
}
