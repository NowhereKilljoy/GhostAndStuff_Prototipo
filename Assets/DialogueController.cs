using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueElement
{
    [TextArea]
    public string Sentence;
    public GameObject[] ObjectsToActivate;
    public GameObject[] ObjectsToDeactivate;
}

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public DialogueElement[] Dialogues;
    private int Index = 0;
    public float DialogueSpeed;
    public AudioSource audioSource;
    public Animator DialogueAnimator;
    private bool StartDialogue = true;

    [Header("Objeto que se desactivará al final del diálogo")]
    public GameObject objectToDisable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (StartDialogue)
            {
                DialogueAnimator.SetTrigger("Enter");
                StartDialogue = false;
            }
            else
            {
                PlaySound();
                NextSentence();
            }
        }
    }

    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void NextSentence()
    {
        if (Index < Dialogues.Length)
        {
            DialogueText.text = "";
            HandleObjectActivation(Dialogues[Index]);
            StartCoroutine(WriteSentence(Dialogues[Index].Sentence));
        }
        else
        {
            DialogueText.text = "";
            DialogueAnimator.SetTrigger("Exit");

            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            Index = 0;
            StartDialogue = true;
        }
    }

    IEnumerator WriteSentence(string sentence)
    {
        foreach (char Character in sentence.ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
    }

    void HandleObjectActivation(DialogueElement element)
    {
        if (element.ObjectsToActivate != null)
        {
            foreach (var obj in element.ObjectsToActivate)
            {
                if (obj != null) obj.SetActive(true);
            }
        }

        if (element.ObjectsToDeactivate != null)
        {
            foreach (var obj in element.ObjectsToDeactivate)
            {
                if (obj != null) obj.SetActive(false);
            }
        }
    }
}
