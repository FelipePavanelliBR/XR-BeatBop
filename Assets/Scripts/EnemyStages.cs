using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStages : MonoBehaviour //Singleton that constrols appearence and dialogue of main enemy
{

    public static EnemyStages instance = null;
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public List<double> timeStamps = new List<double>(); //list of times when the Boss will advance to next stage

    public static int stageIndex ;
    public Animator animator1;
    public Animator animator2;

    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;
    public GameObject dialogue4;
    public GameObject dialogue5;


    private bool[] dialogueTriggered = new bool[5];

    public TextMeshProUGUI debugText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
          
    }
    
    // Start is called before the first frame update
    void Start()
    {
        stageIndex = 0;
}

    // Update is called once per frame
    void Update()
    {
        if(stageIndex < timeStamps.Count)
        {
            switch (stageIndex)
            {
                case 0:


                    break;


                case 1:
                    debugText.text = "First cinematic without boss";
                    Debug.Log("First cinematic"); //8s First visuals without boss. Context Dialogue
                   
                    if (!dialogueTriggered[0])
                    {
                        TriggerDialogue(dialogue1);
                        dialogueTriggered[0] = true;


                    }

                    break;


                case 2:
                    debugText.text = "Boss coming in";
                    Debug.Log("Entering enemy stage 1"); // 16s; BOSS coming in, dialogue 1, no notes

                    animator1.SetTrigger("BossComeIn");
                    if (!dialogueTriggered[1])
                    {
                        TriggerDialogue(dialogue2);
                        dialogueTriggered[1] = true;


                    }
                    break;


                case 3:
                    debugText.text = "Screen turns on for Boss State 1";
                    Debug.Log("BOSS State 1"); // 30s; Prep for notes spawning. no Dialogue
                                               //screen animation SET 1

                    break;


                case 4:
                    debugText.text = "(REDCUBE) BOSS State 2";
                    Debug.Log("Enemy Stage 2"); // 50s Dialogue for Stage 2.


                    if (!dialogueTriggered[2])
                    {
                        TriggerDialogue(dialogue3);
                        dialogueTriggered[2] = true;
                    }

                    break;


                case 5:
                    debugText.text = "BOSS 'ackboledgement' state";
                    Debug.Log("Entering stage 3 part 1: acknoledgement"); // 1:30s; Dialogue in
                    if (!dialogueTriggered[3])
                    {
                        TriggerDialogue(dialogue4);
                        dialogueTriggered[3] = true;


                    }
                    break;


                case 6:
                    debugText.text = "BOSS State 3 ANGRY";
                    Debug.Log("Entering stage 3 part 2: Evil break"); // 1:47s; Other dialogue
                    if (!dialogueTriggered[4])
                    {
                        TriggerDialogue(dialogue5);
                        dialogueTriggered[4] = true;


                    }
                    break;


                case 7:
                    debugText.text = "Showing either win or lose aniamtion";
                    Debug.Log("This will be change depending on scores"); // 2:20s; Conditional stag
                    animator2.SetTrigger("BossExplode");
                   
                    break;
            }
            if (SongManager.GetAudioSourceTime() >= timeStamps[stageIndex])
            {
                //Go to next stage
                stageIndex++;
            }
        }
    }

    private void TriggerDialogue(GameObject dialogueObj)
    {
        dialogueObj.SetActive(true);

        dialogueObj.GetComponent<Dialogue>().StartDialogue();

        Debug.Log("DEbug");
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
}
