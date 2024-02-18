using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsDataManager : MonoBehaviour
{


    public GameObject resultsUI;
    public GameObject welcomeScreen;
    
    public TextMeshProUGUI totalScoreCount;
    public TextMeshProUGUI perfectCount;
    public TextMeshProUGUI goodCount;
    public TextMeshProUGUI okCount;
    public TextMeshProUGUI missCount;
    public TextMeshProUGUI rank;
    public TextMeshProUGUI maxComboCount;



    public int sRankMinScore, aRankMinScore, bRankMinScore, cRankMinScore;


    // Start is called before the first frame update
    void Start()
    {
        ResultsData resultsData = RankingManager.resultsData;
        if(resultsData == null)
        {
            resultsUI.SetActive(false);
            totalScoreCount.text = "";
            perfectCount.text = "";
            goodCount.text = "";
            okCount.text = "";
            missCount.text = "";
            maxComboCount.text = "";
        }
        else
        {
            welcomeScreen.SetActive(false);
            resultsUI.SetActive(true);
            totalScoreCount.text = resultsData.totalScore.ToString();
            perfectCount.text = resultsData.totalPerfect.ToString();
            goodCount.text = resultsData.totalGood.ToString();
            okCount.text = resultsData.totalOk.ToString();
            missCount.text = resultsData.totalMiss.ToString();
            rank.text = GetRank(resultsData.totalScore);
            maxComboCount.text = resultsData.maxComboCount.ToString();
        }
    }

 
    private string GetRank(int totalScore)
    {
        if (totalScore >= sRankMinScore)
        {
            return "S";
        } else if( totalScore >= aRankMinScore)
        {
            return "A";
        } else if (totalScore >= bRankMinScore)
        {
            return "B";
        } else if(totalScore >= cRankMinScore)
        {
            return "C";
        } else
        {
            return "F";
        }
    }
}
