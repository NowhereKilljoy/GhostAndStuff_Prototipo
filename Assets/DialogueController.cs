using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed;
    public AudioSource audioSource;
    public Animator DialogueAnimator;
    private bool StartDialogue = true;

    // Start is called before the first frame update
   

    // Update is called once per frame
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
        if (Index <= Sentences.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            DialogueText.text = "";
            DialogueAnimator.SetTrigger("Exit");
            Index= 0;
            StartDialogue = true;
        }

        IEnumerator WriteSentence()
        {
            foreach (char Character in Sentences[Index].ToCharArray())
            {
                DialogueText.text += Character;
                yield return new WaitForSeconds(DialogueSpeed);
            }
            Index++;
        }
    }
}