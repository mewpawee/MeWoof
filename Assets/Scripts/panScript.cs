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
        ingredientScript script = obj.GetComponent<ingredientScript>();
            
        script.ing.cookingTime = script.ing.cookingTime + (script.ing.cookFactor * fireLevel * Time.deltaTime);

        if (script.ing.cookingTime > 10 && script.ing.cookState < 2 )
        {
            script.ing.cookState = 2;
            obj.GetComponent<Renderer>().material = script.ing.overcooked;
        }
        else if (script.ing.cookingTime > 5 && script.ing.cookState < 1)
        {
            script.ing.cookState = 1;
            obj.GetComponent<Renderer>().material = script.ing.cooked;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject obj = collision.gameObject;
            obj.GetComponent<ingredientScript>().ing.isCooking = true;
            StartCoroutine(Cooking(obj));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject obj = collision.gameObject;
            obj.GetComponent<ingredientScript>().ing.isCooking = false;
            StartCoroutine(obj.GetComponent<ingredientScript>().CoolDown());
        }
    }

    IEnumerator Cooking(GameObject obj)
    {
        while (obj.GetComponent<ingredientScript>().ing.isCooking == true)
        {
            CheckCooked(obj);
            yield return null;
        }
        yield return null;
    }
}
