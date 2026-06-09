using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    /*this one-method class is called upon a button press, and this script is attached to the camera.
     * it is used to go to the game
     *  idea from here: https://www.youtube.com/watch?v=DX7HyN7oJjE
     *  
     *  FInished Commenting!
     *
     */
    public void PlayGame()
    {
        //out of thje scenes ordered in the build file, load the one at index 1
        SceneManager.LoadSceneAsync(1);
    }
}
