using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public SoundPad[] pads;
    [SerializeField] EnemyStages enemyStages;


    public float songDelayInSeconds;
    public double marginOfError; //in seconds

    public int inputDelayInMilliiSeconds;


    public string fileLocation =   "test2.mid"; // name of MIDI file in StreamingAssets folder with .mid in the end
    public float noteTime; //Player reaction time; Time that the note appears on screen

    public Vector3 noteSpawnScale;
    public Vector3 noteTapScale;
    public Vector3 noteDespawnScale
    {
        get{
            return noteTapScale - (noteSpawnScale - noteTapScale);
            }
    }

    //change it to scale units and make vectors
    public float noteSpawnY;
    public float noteTapY;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY); //adapt to vector
        }
    }

    public static MidiFile midiFile;

    void Start()
    {
        Instance = this;
        ReadFromFile();

    }

    private void ReadFromFile()
    {
        string filePath = GetStreamingAssetsPath();

#if UNITY_ANDROID && !UNITY_EDITOR
        StartCoroutine(ReadFileUsingWWW(filePath));
#else
        midiFile = MidiFile.Read(filePath);
        GetDataFromMidi();
#endif
    }

    private string GetStreamingAssetsPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return "jar:file://" + Application.dataPath + "!/assets/" + fileLocation;
#else
        return Application.streamingAssetsPath + "/" + fileLocation;
#endif
    }

    private IEnumerator ReadFileUsingWWW(string path)
    {
        using (WWW www = new WWW(path))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                // File is successfully loaded
                byte[] fileData = www.bytes;
                MemoryStream stream = new MemoryStream(fileData);
                midiFile = MidiFile.Read(stream);
                GetDataFromMidi();
            }
            else
            {
                Debug.LogError("Failed to load MIDI file: " + www.error);
            }
        }
    }




    private void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes(); //
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        //Calling each Event's SetTimeStamps with array
        foreach (var pad in pads) pad.SetTimeStamps(array); //for all types of pads
        enemyStages.SetTimeStamps(array);

        //foreach(var state in 

        Invoke(nameof(StartSong), songDelayInSeconds);
    }


    public void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
