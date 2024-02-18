using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTransition : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float duration;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart) DissolveOut();
    }


    public void DissolveIn()
    {
        Dissolve(1, -1);
    }


    public void DissolveOut()
    {
        Dissolve(-1, 1);
    }


    public void Dissolve(float dissolveIn, float dissolveOut)
    {
        StartCoroutine(DissolveRoutine(dissolveIn, dissolveOut));
    }


    public IEnumerator DissolveRoutine(float dissolveIn, float dissolveOut)
    {
        float timer = 0;
        while(timer < duration)
        {
            float currDissolveStrength = Mathf.Lerp(dissolveIn, dissolveOut, timer / duration);
            rend.material.SetFloat("_DissolveStrength", currDissolveStrength);

            timer += Time.deltaTime;
            yield return null;
        }

        float finalDissolve = dissolveOut;
        rend.material.SetFloat("_DissolveStrength", finalDissolve);
    }
}
