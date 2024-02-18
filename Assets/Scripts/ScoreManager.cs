using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;


    public double perfectMargin;
    public double goodMargin;
    public double okMargin;

    public GameObject scoreUI;
    public GameObject comboUI;
    public GameObject multiplierUI;
    public GameObject feedbackUI;

    public Animator judgeAnimator;
    public Animator multiplierAnimator;


    private TMPro.TextMeshProUGUI scoreText;
    private TMPro.TextMeshProUGUI comboText;
    private TMPro.TextMeshProUGUI multiplierText;
    private TMPro.TextMeshProUGUI feedbackText;


    public static int[] multipliers = { 1, 2, 4, 8, 10 };
    public static int basePoint = 5; //gets different basePoint as a paramenter of hit function -> Hit:[PERFECT, GOOD, OK, MISS]
    

    public static int comboScore;
    public static int totalScore;
    public static int currMultiplier;
    public static int prevMultiplier;


    public static int maxComboCount = 0;

    public static int totalPerfect, totalGood, totalOk, totalMiss;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        comboScore = 0;
        totalScore = 0;
        ExtractTexts();
        currMultiplier = multipliers[0];
        prevMultiplier = multipliers[0];


        totalPerfect = 0;
        totalGood = 0;
        totalOk = 0;
        totalMiss = 0;
    }

    private void ExtractTexts()
    {
        scoreText = scoreUI.GetComponent<TMPro.TextMeshProUGUI>();
        comboText = comboUI.GetComponent<TMPro.TextMeshProUGUI>();
        multiplierText = multiplierUI.GetComponent<TMPro.TextMeshProUGUI>();
        feedbackText = feedbackUI.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public static void Hit(double hitPerformance)
    {
        Instance.hitSFX.Play(); //maybe queue different SFX dependending on performance?
        //trigger material emission

        JudgePerformance(hitPerformance);
        Instance.judgeAnimator.SetTrigger("judgeNow");


        //trigger Feedback animation after defining 






        totalScore = totalScore + basePoint * currMultiplier;

    }

    public static void JudgePerformance(double hitPerformance)
    {
       double perfectMargin = Instance.perfectMargin;
       double goodMargin = Instance.goodMargin;
       double okMargin = Instance.okMargin;
       TMPro.TextMeshProUGUI feedbackText = Instance.feedbackText;
        hitPerformance = hitPerformance - 0.1;
        if (hitPerformance <= perfectMargin) //Perfect hit
        {
            totalPerfect++;
            comboScore += 1;
            basePoint = 15;
            Instance.feedbackText.text = "PERFECT";
        }
        else if (hitPerformance <= goodMargin) //Good hit
        {
            totalGood++;
            //comboScore += 1;
            comboScore = 0;
            basePoint = 10;
            Instance.feedbackText.text = "GOOD"; 

        }
        else if (hitPerformance <= okMargin) //Ok hit
        {
            totalOk++;
            //comboScore += 1;
            comboScore = 0;
            basePoint = 5;
            Instance.feedbackText.text = "OK";
        }

    }

    public static void Miss()
    {
        totalMiss++;
        Instance.feedbackText.text = "MISS";
        Instance.judgeAnimator.SetTrigger("judgeNow");
        Instance.missSFX.Play();
        comboScore = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        AdjustMaxComboCount();
        AdjustMultiplier();

        // score point should start showing 0
        // Combo count should start at 2
        // multiplier should start at 2x


      
        comboText.text = comboScore.ToString();
        

        scoreText.text = totalScore.ToString();


        if (currMultiplier > multipliers[0])
        {
            multiplierText.text = "x" + currMultiplier.ToString();
        } else if (currMultiplier == multipliers[0])
        {
            multiplierText.text = "";
        }


        if (currMultiplier != prevMultiplier) //activating animation only when multiplier value changes from what it was
        {
            multiplierAnimator.SetTrigger("MultiplyNow");
            prevMultiplier = currMultiplier;
        }
    }

    private void AdjustMultiplier()
    {
        if(comboScore < 6)
        {
            currMultiplier = multipliers[0];
        }
        else if(comboScore < 11)
        {
            currMultiplier = multipliers[1];
        }
        else if (comboScore < 16)
        {
            currMultiplier = multipliers[2];
        }
        else if (comboScore < 21)
        {
            currMultiplier = multipliers[3];
        }
        else if (comboScore >=21)
        {
            currMultiplier = multipliers[4];
        }
    }


    private static void AdjustMaxComboCount()
    {
        if (comboScore > maxComboCount)
        {
            maxComboCount = comboScore;
        }
    }
}
