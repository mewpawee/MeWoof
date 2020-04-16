using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steakScript : MonoBehaviour
{
    public static int cookState = 0;
    public static float cookingTime = 0.0f;
    public static bool isCooking = false;
    public static float cookFactor = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoolDown();
        //Debug.Log(cookingTime);
        Debug.Log(cookState);
    }

    private void CoolDown() { 
            if (!isCooking && cookingTime > 0) {
                cookingTime = cookingTime - Time.deltaTime;
            }
    }
  
}
