using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class reginaldDialogue : MonoBehaviour
{

    //I used a youtube video to help me with this. I had never made a dialogue system before: https://youtu.be/8oTYabhj248?si=TjvFxH83amt8mYuc
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int lineIndex;
    public bool isTyping = false;
    public GameObject nextLinePrompt;
    public GameObject player;

    private void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        if (isTyping)
        {
            StopAllCoroutines();
            textComponent.text = lines[lineIndex];
            isTyping = false;

            if (nextLinePrompt != null)
            {
                nextLinePrompt.SetActive(true);
            }
        }
        else
        {
            if (lineIndex == lines.Length - 1)
            {
                if (nextLinePrompt != null)
                {
                    nextLinePrompt.SetActive(false);
                }

                player.GetComponent<gridMovement>().keyPickedUp = true;
                gameObject.SetActive(false);
            }
            else
            {
                NextLine();
            }
        }
    }
}    private void StartDialogue()
    {
        lineIndex = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
{
    isTyping = true;
    textComponent.text = "";

    if (nextLinePrompt != null)
    {
        nextLinePrompt.SetActive(false);
    }

    foreach (char c in lines[lineIndex].ToCharArray())
    {
        textComponent.text += c;
        yield return new WaitForSeconds(textSpeed);
    }

    isTyping = false;

    if (nextLinePrompt != null)
    {
        nextLinePrompt.SetActive(true);
    }
}

    private void NextLine()
    {
    if (lineIndex < lines.Length - 1)
    {
        lineIndex++;
        textComponent.text = string.Empty;

        if (nextLinePrompt != null)
        {
            nextLinePrompt.SetActive(false);
        }

        StartCoroutine(TypeLine());
    }

    }
}
