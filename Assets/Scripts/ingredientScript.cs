using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ingredientScript : MonoBehaviour
{
    public ingredient ing;
    private void Awake()
    {
        ing = Manager.newIngrediant(this.gameObject.tag);
    }
    public IEnumerator CoolDown()
    {
        yield return null;
        while (!ing.isCooking && ing.cookingTime > 0)
        {
            ing.cookingTime = ing.cookingTime - Time.deltaTime;
            yield return null;
        }
        yield return null;
    }  
}