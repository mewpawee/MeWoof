using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class StartButton : MonoBehaviour
{
    private AudioSource AudioClip;
    // Start is called before the first frame update
    void Start()
    {
        AudioClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VRTK_InteractableObject>().IsTouched())
        {
            if (!Manager.gameOn) {
               Manager.gameStart();
               AudioClip.Play();
            }
        }
    }
}
