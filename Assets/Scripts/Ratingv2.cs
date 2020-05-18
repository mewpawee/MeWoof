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


    private void Start()
    {
        foreach (string key in Manager.orderDetails.Keys)
        {
            ordered.Add(key, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            float volume = other.gameObject.GetComponent<ingredientScript>().ing.volume;
            cookingState = other.gameObject.GetComponent<ingredientScript>().ing.cookState;
            float tempMean = Manager.newIngrediant(other.gameObject.tag).volume / 4f;
            upperBound = tempMean + threshold;
            lowerBound = tempMean - threshold;
            if (ordered.ContainsKey(other.gameObject.tag))
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
                scoreCount = quantity(scoreVolume, ordered[other.gameObject.tag], tempMean, Manager.orderDetails[other.gameObject.tag]);
                //Manager.score = 0.4f * (0.8f * steakRating + 0.2f * vegRating) + 0.4f * cookingScore + 0.2f * questRecipe(ordered[other.gameObject.tag]);
                Manager.score = 0.4f * scoreCount + 0.4f * scoreState + 0.2f * questRecipe(ordered[other.gameObject.tag]);
                Manager.score = (float)Math.Round(Manager.score,2);
            }
            else
            {
                ordered.Add(other.gameObject.tag, 1);
            }
        }       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            if (ordered.ContainsKey(other.gameObject.tag))
            {
                Manager.score = Manager.score - (0.4f * scoreCount + 0.4f * scoreState + 0.2f * questRecipe(ordered[other.gameObject.tag]));
                Manager.score = (float)Math.Round(Manager.score,2);
                ordered[other.gameObject.tag]--;
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
        foreach (KeyValuePair<string, int> kvp in ordered)
        {
            Debug.Log(kvp.Key);
            Debug.Log(kvp.Value);
        }
    }
}
