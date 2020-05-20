using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratingv2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float scoreCount;
    private float scoreState;
    int cookingState;
    float scoreVolume;

    private float threshold = 10f;
    private float upperBound;
    private float lowerBound;
    public static Dictionary<string, int> ordered = new Dictionary<string, int>();
    public static List<GameObject> ingredients = new List<GameObject>();

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 && Manager.gameOn)
        {
            float volume = other.gameObject.GetComponent<ingredientScript>().ing.volume;
            cookingState = other.gameObject.GetComponent<ingredientScript>().ing.cookState;
            float tempMean = Manager.newIngrediant(other.gameObject.tag).volume / 4f;
            upperBound = tempMean + threshold;
            lowerBound = tempMean - threshold;
            ingredients.Add(other.gameObject);
            if (ordered.ContainsKey(other.gameObject.tag) )
            {
                if (volume > lowerBound & volume < upperBound)
                {
                    scoreVolume = validateScore(volume, Manager.newIngrediant(other.gameObject.tag).volume / 4f, tempMean);
                }
                else
                {
                    scoreVolume = validateScore(volume, Manager.newIngrediant(other.gameObject.tag).volume / 4f, tempMean * 1.25f);
                }
                ordered[other.gameObject.tag]++;
                cookRating(cookingState);
                scoreCount = quantity(scoreVolume, ordered[other.gameObject.tag], tempMean, Manager.ordersQueue.Peek()[other.gameObject.tag]);
                //Manager.score = 0.4f * (0.8f * steakRating + 0.2f * vegRating) + 0.4f * cookingScore + 0.2f * questRecipe(ordered[other.gameObject.tag]);
                Manager.scoreTemp = 0.4f * scoreCount + 0.4f * scoreState + 0.2f * questRecipe(ordered[other.gameObject.tag]);
                Manager.scoreTemp = (float)Math.Round(Manager.scoreTemp,2);
            }
        }       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 9 && Manager.gameOn)
        {
            if (ordered.ContainsKey(other.gameObject.tag))
            {
                Manager.scoreTemp = Manager.scoreTemp - (0.4f * scoreCount + 0.4f * scoreState + 0.2f * questRecipe(ordered[other.gameObject.tag]));
                Manager.scoreTemp = (float)Math.Round(Manager.scoreTemp,2);
                ordered[other.gameObject.tag]--;
                ingredients.Remove(other.gameObject);
            }
        }
    }
    private void cookRating(int cookState)
    { 
        if (cookState == 1) { scoreState = 1f; }
        else { scoreState = 0f; }
    }
    private float quantity(float score, float count, float threshold, float questCount)
    {
        float rating;
        float countScore;
        float rI;
        rI = score / count;
        if (count == questCount) { countScore = 1f; }
        else { countScore = 0f; }
        rating = 0.5f * countScore + 0.5f * rI / threshold;
        return rating;
    }
    private float validateScore(float volume, float threshold, float tempTreshold)
    {
        float ScoreTemp = (threshold - Math.Abs(volume - tempTreshold)) / threshold;
        return ScoreTemp;
    }
    private float questRecipe(int count)
    {
        float score = 0f;
        if (count > 0) { score = score + 0.5f; }
        return score;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject list in ingredients) {
            Debug.Log(list);
        }
    }
}
