using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panScript : MonoBehaviour
{
    private float fireLevel = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cooking();
    }



    void Cooking() {
        if (steakScript.isCooking == true)
        {
            //cooking time = cookingtime + (cook factor * fire level * delta time)
            CheckCooked();
        }
    }


    private void CheckCooked()
    {
        steakScript.cookingTime = steakScript.cookingTime + (steakScript.cookFactor * fireLevel * Time.deltaTime);

        if (steakScript.cookingTime > 10 && steakScript.cookState < 2 )
        {
            steakScript.cookState = 2;
        }
        else if (steakScript.cookingTime > 5 && steakScript.cookState < 1)
        {
            steakScript.cookState = 1;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "steak")
        {
            steakScript.isCooking = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "steak")
        {
            steakScript.isCooking = false;
            StartCoroutine(steakScript.CoolDown());
        }
    }
}
