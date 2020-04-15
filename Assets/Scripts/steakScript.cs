using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steakScript : MonoBehaviour
{
    public static int state = 0;
    public static bool isCooking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateLog();
    }

    void StateLog() {

        if (state == 1)
        {
            Debug.Log("cooked");
        }
        else if (state == 2) {
            Debug.Log("overcooked");
        }
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "pan") {
           isCooking = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "pan")
        {

            isCooking = false;
        }
    }
}
