using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Runtime.Remoting;
using UnityEngine.SocialPlatforms.Impl;
using System;
using System.Collections;
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
    //Materials
    public Material meatCooked;
    public Material meatOverCooked;
    public Material onionCooked;
    public Material onionOverCooked;
    public Material chickenCooked;
    public Material chickenOverCooked;
    public Material salmonCooked;
    public Material salmonOverCooked;
    public Material asparagusCooked;
    public Material asparagusOverCooked;
    public Material carrotCooked;
    public Material carrotOverCooked;


    //Texts
    public TMP_Text scoreGUI;
    public TMP_Text countDownGUI;
    public TMP_Text scoreBoard;
    public TMP_Text orderGUI;
    public TMP_Text scoreGUITemp;

    //Variables
    static int menuCount = 1;
    public static float scoreTemp = 0;
    public static float score = 0;
    private float countDown = 304;
    public float highScore = 0;
    public static bool gameOn = false;
    string highScoreKey = "HighScore";
    public static Queue<Dictionary<string, int>> ordersQueue = new Queue<Dictionary<string, int>>();
   
    private void Awake()
    {
        PlayerPrefs.SetFloat(highScoreKey, 0);
        PlayerPrefs.Save();
    }
    void Start()
    {
        highScore = PlayerPrefs.GetFloat(highScoreKey, 0);
        scoreBoard.text = "HighScore: " + highScore.ToString();
        scoreGUI.text = "score: " + score.ToString();
        scoreGUITemp.text = "current score: " + scoreTemp.ToString();
        countDownGUI.text = "time: " + countDown.ToString("F2");
    }

    private void Update()
    {
        highScore = PlayerPrefs.GetFloat(highScoreKey, 0);
        scoreBoard.text = "HighScore: " + highScore.ToString();
        if (gameOn)
        {
            countDown = countDown - Time.deltaTime;
            updateOrderDetails();
        }

        scoreGUI.text = "score: " + score.ToString();
        scoreGUITemp.text = "current score: " + scoreTemp.ToString();

        if (countDown > 0)
        {
            countDownGUI.text = "time: " + countDown.ToString("F2");
        }
        else {
            countDownGUI.text = "Timeup";
            checkHighScore();
            countDown = 300;
            gameOn = false;
        }
    }
    public static void gameStart() {
        Manager.menuCount = 1;
        ordersQueue.Clear();
        ordersQueue.Enqueue(genOrderDetails());
        ordersQueue.Enqueue(genOrderDetails());
        gameOn = true;
        Ratingv2.ordered = new Dictionary<string, int>();
        foreach (string key in ordersQueue.Peek().Keys)
        {
            Ratingv2.ordered.Add(key, 0);
        }
    }

    public static void submitDish()
    {
        ordersQueue.Dequeue();
        score += scoreTemp;
        menuCount++;
        Ratingv2.ordered = new Dictionary<string, int>();
        foreach (string key in ordersQueue.Peek().Keys)
        {
            Ratingv2.ordered.Add(key, 0);
        }
        foreach (GameObject obj in Ratingv2.ingredients) 
        {
            Destroy(obj);
        }
        ordersQueue.Enqueue(  genOrderDetails());
        scoreTemp = 0;
    }

    void checkHighScore()
    {
        //If our scoree is greter than highscore, set new higscore and save.
        if (score > highScore)
        {
            PlayerPrefs.SetFloat(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }
    private static Dictionary<string,int> genOrderDetails() {
         Dictionary<string, int> order = new Dictionary<string, int>();
         List<string> meats = new List<string> {"meat","salmon","chicken"};
         List<string> vegetables = new List<string> {"onion","carrot","asparagus"};
         order.Add(meats[UnityEngine.Random.Range(0, meats.Count)], 3);
         order.Add(vegetables[UnityEngine.Random.Range(0, vegetables.Count)], 5);
         return order;
    }

    private void updateOrderDetails() {
        orderGUI.text = "Menu: " + menuCount + "\n";
        foreach (string key in ordersQueue.Peek().Keys)
        {
            orderGUI.text = orderGUI.text + key + ": " + Ratingv2.ordered[key] + "/" + ordersQueue.Peek()[key] + "\n";
        }
    }

    public static ingredient newIngrediant(string name) {
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        if (name == "meat")
            return new ingredient(1.1f, 200f, manager.meatCooked, manager.meatOverCooked);
        else if (name == "salmon")
        {
            return new ingredient(2.0f, 50f, manager.salmonCooked, manager.salmonOverCooked);
        }
        else if (name == "chicken")
        {
            return new ingredient(2.0f, 50f, manager.chickenCooked, manager.chickenOverCooked);
        }
        else if (name == "carrot")
        {
            return new ingredient(2.0f, 50f, manager.carrotCooked, manager.carrotOverCooked);
        }
        else if (name == "onion")
        {
            return new ingredient(2.0f, 50f, manager.onionCooked, manager.onionOverCooked);
        }
        else if (name == "asparagus")
        {
            return new ingredient(2.0f, 50f, manager.asparagusCooked, manager.asparagusOverCooked);
        }
        else
            return null;
    }
}
