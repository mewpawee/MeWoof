using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class StartButton : MonoBehaviour
{
    private AudioSource AudioClip;
    public static bool buttonState = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VRTK_InteractableObject>().IsTouched() && Manager.gameOn == false)
        {
            if (buttonState == false)
            {
                AudioClip.Play();
                buttonState = true;
                Manager.gameStart();
                //StartCoroutine(startGame());
            }
        }
    }
}
