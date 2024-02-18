using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float textPermanenceSeconds;

    private bool startedAlready = false;
    private int index;


    // Start is called before the first frame update
    void Awake()
    {
        textComponent.text = string.Empty;
        
    }       

    // Update is called once per frame
    void Update()
    {
        if(textComponent.text == lines[index])
        {
            Debug.Log("aisnaldnasndasd");
            NextLine();
        }
    }


    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = 0;

        if (startedAlready == false)
        {
            StartCoroutine(TypeLine());
            startedAlready = true;
        }
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(KillDialogue());
        }
    }

    IEnumerator KillDialogue()
    {
        yield return new WaitForSeconds(textPermanenceSeconds);

        gameObject.SetActive(false);
    }
}
