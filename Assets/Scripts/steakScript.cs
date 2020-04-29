using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steakScript : MonoBehaviour
{
    public int cookState = 0;
    public float cookingTime = 0.0f;
    public bool isCooking = false; 
    public float cookFactor = 1.1f;
    public float volume = 100f;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cookingTime);
        Debug.Log(cookState);
        Debug.Log(cookingTime);
    }

    public IEnumerator CoolDown()
    {
        yield return null;
        while (!isCooking && cookingTime > 0)
        {
            cookingTime = cookingTime - Time.deltaTime;
            yield return null;
        }
        yield return null;
    }  
}