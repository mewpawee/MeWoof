using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Runtime.Remoting;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class ingredient {
    public int cookState = 0;
    public float cookingTime = 0.0f;
    public bool isCooking = false;
    public float cookFactor;
    public float volume;
    public Material cooked;
    public Material overcooked;
    public ingredient(float thisCookFactor, float thisVolume, Material thisCooked, Material thisOverCooked) {
        cookFactor = thisCookFactor;
        volume = thisVolume;
        cooked = thisCooked;
        overcooked = thisOverCooked;
    }
}

public class Manager : MonoBehaviour
{
    public Material meatCooked;
    public Material meatOverCooked;
    public Material onionCooked;
    public Material onionOverCooked;
    public TMP_Text scoreGUI;
    public TMP_Text countDownGUI;
    public TMP_Text scoreBoard;
    public static float score = 0;
    private float countDown = 10;
    public float highScore = 0;
    public bool gameOn = false;
    string highScoreKey = "HighScore";
    public static Dictionary<string, int> orderDetails = new Dictionary<string, int>();
    private void Awake()
    {
        orderDetails.Add("meat", 3);
        orderDetails.Add("onion", 5);
        PlayerPrefs.SetFloat(highScoreKey, 0);
        PlayerPrefs.Save();
    }
    void Start()
    {
        highScore = PlayerPrefs.GetFloat(highScoreKey, 0);
        scoreBoard.text = "HighScore: " + highScore.ToString();
        scoreGUI.text = "score: " + score.ToString();
        countDownGUI.text = "time: " + countDown.ToString("F2");
    }

    private void Update()
    {
        highScore = PlayerPrefs.GetFloat(highScoreKey, 0);
        scoreBoard.text = "HighScore: " + highScore.ToString();
        if (gameOn)
        {
            countDown = countDown - Time.deltaTime;
        }

        //scoreGUI.text = "score: " + score.ToString() +"\n steakCount: " + Rating.countSteak + "/" + orderDetails["meat"] + "\n vegCount:" +Rating.countVeg + "/" + orderDetails["onion"];
        scoreGUI.text = "score: " + score.ToString() + "\n steakCount: " + Ratingv2.ordered["meat"] + "/" + orderDetails["meat"] + "\n vegCount:" + Ratingv2.ordered["onion"] + "/" + orderDetails["onion"];
        if (countDown > 0)
        {
            countDownGUI.text = "time: " + countDown.ToString("F2");
        }
        else {
            countDownGUI.text = "Timeup";
            checkScore();
            countDown = 10;
            gameOn = false;
        }
    }
    public void gameStart() {
        orderDetails["meat"] = UnityEngine.Random.Range(3,5);
        orderDetails["onion"] = UnityEngine.Random.Range(3, 5);
        GetComponent<Manager>().gameOn = true;
    }
    void checkScore()
    {
        //If our scoree is greter than highscore, set new higscore and save.
        if (score > highScore)
        {
            PlayerPrefs.SetFloat(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }

    public static ingredient newIngrediant(string name) {
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        if (name == "meat")
            return new ingredient(1.1f, 200f, manager.meatCooked, manager.meatOverCooked);
        else if (name == "onion") {
            return new ingredient(2.0f, 50f, manager.onionCooked, manager.onionOverCooked);
        }
        else
            return null;
    }
}
