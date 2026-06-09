using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    /*most of the code here originated from like a video bcos the bullet patterns are really more my speed when it comes to coding
     * anyways this is a script meant to be attached to a dialogue box to have it function.
     * vid link here: https://www.youtube.com/watch?v=8oTYabhj248
     * 
     * Finished Commenting!
    //*/
    //textbox to show dialogue in
    public TextMeshProUGUI dialogueText;
    //array of dialogues
    public string[] dialogues;
    //the speed at which new characters reveal themself
    public float textSpeed;
    //the current dialogue out of the above array which is being shown
    public int dialogueIndex;
    //the panel this is supposed to be attached to
    public GameObject thisPanel;

    // Start is called before the first frame update
    void Start()
    {
        //empty out the text box, start the coroutine
        dialogueText.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
       
        //when dialogue button clicked
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            //if previous text is finished,
            if (dialogueText.text == dialogues[dialogueIndex])
            {
                //dialogueIndex = 0;
                advanceDialogue();
            }
            else
            {
                //then advancetext
                StopAllCoroutines();
                dialogueText.text = dialogues[dialogueIndex];

            }
     
        }
    }

    void startDialogue()
    {
        //set some variables, start the coroutine
        dialogueIndex = 0;
        StartCoroutine(typeLine());
    }

    //types out a line
    IEnumerator typeLine()
    {
        //every time this coroutine runs, wait the set time before adding in the next character in this word
        foreach (char c in dialogues[dialogueIndex].ToCharArray())
        {
            //this creates an efftect of the words being "spoken"
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //advances dialogue and ends this dialogue before 
    void advanceDialogue()
    {
        //if the dialogue index is within range, increase the index, rest the dialogue box, and start "typing" the new line into the box
        if (dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine(typeLine());
        }
        //comes to this case assuming we are out of new words to say. then destroy this gameobject.
        else
        {
            Destroy(thisPanel); 
        }
        
    }
}
