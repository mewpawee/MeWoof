using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using UnityEngine;

public class panScript : MonoBehaviour
{
    private float fireLevel = 1.0f;
    private AudioSource AudioClip;
    public ParticleSystem changeState;
    // Start is called before the first frame update
    int count = 0;

    void Start()
    {
        AudioClip = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (count > 0) AudioClip.loop = true;
        else
        {
            AudioClip.loop = false; 
            AudioClip.Stop();
        }
    }

    private void CheckCooked(GameObject obj)
    {
        ingredientScript script = obj.GetComponent<ingredientScript>();
            
        script.ing.cookingTime = script.ing.cookingTime + (script.ing.cookFactor * fireLevel * Time.deltaTime);

        if (script.ing.cookingTime > 10 && script.ing.cookState < 2 )
        {
            script.ing.cookState = 2;
            obj.GetComponent<Renderer>().material = script.ing.overcooked;
            changeState.Stop();
            if (changeState.isStopped)
            {
                changeState.Play();
            }
        }
        else if (script.ing.cookingTime > 5 && script.ing.cookState < 1)
        {
            script.ing.cookState = 1;
            obj.GetComponent<Renderer>().material = script.ing.cooked;
            changeState.Stop();
            if (changeState.isStopped)
            {
                changeState.Play();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            count++;
            GameObject obj = collision.gameObject;
            obj.GetComponent<ingredientScript>().ing.isCooking = true;
            StartCoroutine(Cooking(obj));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            count--;
            GameObject obj = collision.gameObject;
            obj.GetComponent<ingredientScript>().ing.isCooking = false;
            StartCoroutine(obj.GetComponent<ingredientScript>().CoolDown());
        }
    }

    IEnumerator Cooking(GameObject obj)
    {
        AudioClip.Play();
        while (obj.GetComponent<ingredientScript>().ing.isCooking == true)
        {
            CheckCooked(obj);
            yield return null;
        }
        yield return null;
    }
}
