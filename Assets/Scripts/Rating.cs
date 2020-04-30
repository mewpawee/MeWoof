using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour
{    
    private float steakRating;
    private float vegRating;
    public static float countSteak = 0;
    public static float countVeg = 0;
    private float steakScore = 0;
    private float vegScore = 0;
    private float cookingScore;
    int cookingSteakState;
    private float questCountveg;
    private float questCountSteak;

    float steakScoreTemp;
    float vegScoreTemp;

    private int isSteakEnter = 1;
    private int isVegEnter = 1;

    private float steakThreshold = 40f;
    private float vegThreshold = 20f;

    void Start()
    {
        //collect random quest
        questCountveg = Manager.orderDetails["onion"];
        questCountSteak = Manager.orderDetails["meat"];
    }

    private void OnTriggerEnter(Collider other)
    {        

        if (other.gameObject.tag == "meat" & isSteakEnter == 1)
        {
            float steakVolume = other.gameObject.GetComponent<ingredientScript>().ing.volume;
            cookingSteakState = other.gameObject.GetComponent<ingredientScript>().ing.cookState;
            if (steakVolume > 35f & steakVolume < 45f)
            {
                float tempTreshold = steakThreshold;
                steakScoreTemp = validateScore(steakVolume, steakThreshold, tempTreshold);
            }
            else
            {
                float tempTreshold = steakThreshold * 1.25f;
                steakScoreTemp = validateScore(steakVolume, steakThreshold, tempTreshold);
            }
            steakScore = steakScore + steakScoreTemp;
            countSteak++;
            isSteakEnter = 0;
            cookRating();
            steakRating = ratingDish(steakScore, countSteak, steakThreshold, questCountSteak);
        }
        //do more at here
        if (other.gameObject.tag == "onion" & isVegEnter == 1)
        {
            //fill if any
            float vegVolume = other.gameObject.GetComponent<ingredientScript>().ing.volume;
            if(vegVolume > 35f & vegVolume < 45f)
            {
                float tempTreshold = vegThreshold;
                vegScoreTemp = validateScore(vegVolume, steakThreshold, tempTreshold);
            }
            else if (vegVolume > 45f)
            {
                float tempTreshold = vegThreshold * 1.25f;
                vegScoreTemp = validateScore(vegVolume, steakThreshold, tempTreshold);
            }
            countVeg++;
            vegScore = vegScore + vegScoreTemp;
            isVegEnter = 0;
            vegRating = ratingDish(vegScore, countVeg, vegThreshold, questCountveg);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "meat" & isSteakEnter == 0) { isSteakEnter = 1; }
        if (other.gameObject.tag == "onion" & isVegEnter == 0) { isVegEnter = 1; }

    }
    private float validateScore(float volume ,float threshold,float tempTreshold)
    {
        float ScoreTemp = (threshold - Math.Abs(volume - tempTreshold)) / threshold;
        return ScoreTemp;
    }
    private float ratingDish(float score, float count, float treshold, float questCount)
    {
        float rating;
        float countScore;
        float rI;
        rI = score / count;
        if (count == questCount) { countScore = 1; }
        else { countScore = 0; }
        rating = 0.5f * countScore + 0.5f * rI / treshold;
        return rating;
    }
    private void cookRating()
    {
        if (cookingSteakState == 1) { cookingScore = 1; }
        else { cookingScore = 0; }
    }
    private void calculateRating()
    {
        //ratio volumeSteak:texture:volumeVeg = 4:5:1
        //able to change
        Manager.score = 0.4f * steakRating  + 0.5f * cookingScore + 0.1f * vegRating;

    }
    // Update is called once per frame
    void Update()
    {
        calculateRating();
    }
}
