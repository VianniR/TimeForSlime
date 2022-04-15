using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Queue<string> sentences;
    
    public GameObject currentConvoNumber;
    public GameObject nextConvoNumber;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.Play("Dialogue_Open");

        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        
        sentences.Clear();


        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log("Next Sentence");

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return 100;
        }
    }

    void EndDialogue()
    {
        animator.Play("Dialogue_Close");
        Debug.Log("End of conversation");
        currentConvoNumber.SetActive(false);
        nextConvoNumber.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
