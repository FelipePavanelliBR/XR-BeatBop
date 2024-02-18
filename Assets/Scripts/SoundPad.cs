using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class SoundPad : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    //[SerializeField] Animator animator;
    public bool soundPadPressed = false;

    public GameObject notePrefab;
    List<Note> notes = new List<Note>(); //to keep track of notes that were instantiated
    public List<double> timeStamps = new List<double>(); //all the time stamps that player needs to tap on the notes


    int spawnIndex = 0; //to keep track what timeStamp needs to be spawn
    int inputIndex = 0; //to keep track what timeStamp needs to be detected


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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //handle spawning notes
        if (spawnIndex < timeStamps.Count) //acts like a while loop
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab,
                    transform.position,
                    transform.rotation); //creating the note prefab

                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }


        //Handling player input

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliiSeconds / 1000.0);


            //note hit
            if (soundPadPressed)
            {
                
                double hitPerformance = Math.Abs(audioTime - timeStamp);
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit(hitPerformance);
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;

                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Debug.Log("Calling MISS NOW");
                print($" Missed {inputIndex} note");
                inputIndex++;

            }

        }
    }
    public void TapInSoundPad()
    {
        soundPadPressed = true;
    }

    public void TapOutSoundPad()
    {
        soundPadPressed = false;
    }


    private void Hit(double hitPerformance)
    {
        ScoreManager.Hit(hitPerformance);
    }


    private void Miss()
    {
        ScoreManager.Miss();
    }


    //public void PressButtonAnimate()
    //{
    //    animator.SetBool("Pressed", true);
    //}

    //public void ReturnButtonAnimate()
    //{
    //    animator.SetBool("Pressed", false);
    //}
}
