using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndScript : MonoBehaviour

{
    /*
     * the script sitting inside the stage end panel,
     * it triggers the stage end panel when the end cinditions have been met so that
     * you can navigate back to the main menu
     * 
     * i was inspired by a yt video for this, but idk if i should credit it, bc i didnt really copy it word for word... maybe
     * ok well here it is..
     * https://www.youtube.com/watch?v=K4uOjb5p3Io&t=237s
     * 
     * Finished COmmenting!
     */
    //where we display final graze and health
    public TextMeshProUGUI finalGrazeText;
    public TextMeshProUGUI finalHealthText;
    //where the final message is 
    public TextMeshProUGUI finalMessage;
   
    //lets set up the attached gameobject and display the proper statistics
    public void setUp(float graze, int health)
    {

        gameObject.SetActive(true);
        finalHealthText.text = "Health: " + health;
        finalGrazeText.text = "Graze: " + ((int)graze);
    }
    //like the previous message, but this dialog is for when you win, so it has a different message
    public void setUpWin(float graze, int health)
    {
        gameObject.SetActive(true);
        finalHealthText.text = "Health: " + health;
        finalGrazeText.text = "Graze: " + ((int)graze);
        
        
        //change end message based on health for the "ranks"
        if (health == 150){
            finalMessage.text = "i really like you for that, but please touch grass (rank 0)";
            return;
        }
        else if (health >= 100 && health < 150)
        {
            
            finalMessage.text = "you did better than i expected you to (rank 1)";
            return;
        }
        else if (health > 80 && health < 100)
        {
            finalMessage.text = "lock in, that was average, that aint good (rank 2)";
            return;
        }
        else
        {
            finalMessage.text = "are you even trying? really? (rank 3)";
            return;
        }
    }


}
