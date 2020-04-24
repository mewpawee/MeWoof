using System.Collections;
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
        Component script = obj.GetComponent<steakScript>();
            
        obj.GetComponent<steakScript>().cookingTime = obj.GetComponent<steakScript>().cookingTime + (obj.GetComponent<steakScript>().cookFactor * fireLevel * Time.deltaTime);

        if (obj.GetComponent<steakScript>().cookingTime > 10 && obj.GetComponent<steakScript>().cookState < 2 )
        {
            obj.GetComponent<steakScript>().cookState = 2;
            obj.GetComponent<Renderer>().material = Manager.overCooked;
        }
        else if (obj.GetComponent<steakScript>().cookingTime > 5 && obj.GetComponent<steakScript>().cookState < 1)
        {
            obj.GetComponent<steakScript>().cookState = 1;
            obj.GetComponent<Renderer>().material = Manager.cooked;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "steak")
        {
            GameObject obj = collision.gameObject;
            obj.GetComponent<steakScript>().isCooking = true;
            StartCoroutine(Cooking(obj));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "steak")
        {
            GameObject obj = collision.gameObject;
            obj.GetComponent<steakScript>().isCooking = false;
            StartCoroutine(obj.GetComponent<steakScript>().CoolDown());
        }
    }

    IEnumerator Cooking(GameObject obj)
    {
        while (obj.GetComponent<steakScript>().isCooking == true)
        {
            CheckCooked(obj);
            yield return null;
        }
        yield return null;
    }
}
