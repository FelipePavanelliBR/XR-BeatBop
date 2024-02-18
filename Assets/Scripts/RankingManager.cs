using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{        
    public static RankingManager Instance;

    public static ResultsData resultsData;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (EnemyStages.stageIndex <= 7)
        {
            PopulateResults();

        }
    }


    public void PopulateResults()
    {
        resultsData = new ResultsData(ScoreManager.totalScore, ScoreManager.totalPerfect, ScoreManager.totalGood, ScoreManager.totalOk, ScoreManager.totalMiss, ScoreManager.maxComboCount);
    }
}
