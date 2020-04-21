using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panScript : MonoBehaviour
{
    private float fireLevel = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckCooked(GameObject obj)
    {
        steakScript.cookingTime = steakScript.cookingTime + (steakScript.cookFactor * fireLevel * Time.deltaTime);

        if (steakScript.cookingTime > 10 && steakScript.cookState < 2 )
        {
            steakScript.cookState = 2;
            obj.GetComponent<Renderer>().material = Manager.overCooked;
        }
        else if (steakScript.cookingTime > 5 && steakScript.cookState < 1)
        {
            steakScript.cookState = 1;
            obj.GetComponent<Renderer>().material = Manager.cooked;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "steak")
        {
            steakScript.isCooking = true;
            StartCoroutine(Cooking(collision.gameObject));
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

    IEnumerator Cooking(GameObject obj)
    {
        while (steakScript.isCooking == true)
        {
            CheckCooked(obj);
            yield return null;
        }
        yield return null;
    }
}
