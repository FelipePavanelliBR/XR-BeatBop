using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsData 
{

    public int totalScore, totalPerfect, totalGood, totalOk, totalMiss, maxComboCount;


    public ResultsData(int totalScore, int totalPerfect, int totalGood, int totalOk, int totalMiss, int maxComboCount)
    {
        this.totalScore = totalScore;
        this.totalPerfect = totalPerfect;
        this.totalGood = totalGood;
        this.totalOk = totalOk;
        this.totalMiss = totalMiss;
        this.maxComboCount = maxComboCount;
    }
}
