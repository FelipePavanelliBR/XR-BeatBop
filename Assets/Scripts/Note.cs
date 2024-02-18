using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    double timeInstantiated;
    public float assignedTime; //time where note is supposed to be hit by the player

    //set up vector to change scale
    Vector3 changeInScale;

    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        changeInScale = new Vector3(0.01f, 0.01f, 0.01f); //Make this vector be self-generated w/ other parameters

    }

    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2)); // Multiply it by 2 bc noteTime accounts for the time between our spawnY and TapY. Here we want time between spawnY and despawnY

        Debug.Log(t);
        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //go from seding notes "Down" to make squares shrink in all sides equally
            transform.localScale = Vector3.Lerp(SongManager.Instance.noteSpawnScale, SongManager.Instance.noteDespawnScale, t);


            //transform.localScale = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);


            //transform.localScale -= changeInScale/50f;
        }
        //
        GetComponent<MeshRenderer>().enabled = true;
    }
}