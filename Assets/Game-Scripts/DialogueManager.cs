using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class DialogueManager : MonoBehaviour
{
    public Text nameTxt;
    public Text dialogueTxt;

    private ThirdPersonMoving6thFloor tpm;

    public Animator anim; 
    
    private Queue<string> sentences; 


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
    }


    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

        anim.SetBool("isOpen", true);

        nameTxt.text = dialogue.name; 

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Cursor.lockState = CursorLockMode.None;
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            dialogueTxt.text = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTxt.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTxt.text += letter;
            yield return null; 
        }
    }


    public void EndDialogue()
    {
        //Debug.Log("End of Conversation"); 
        anim.SetBool("isOpen", false);
    }


    private void Update()
    {
        DisplayNextSentence(); 
    }

}
