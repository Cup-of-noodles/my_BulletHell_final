using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BulletAttackController : MonoBehaviour {
    /*
     * THe pride and joy of my project. NONE of this script was like enything ive done before.
     * This overarching script sits in the main scene of my game, and controls the overall enemies being spawned, 
     * dialogue being put in, and the overall flaow of the game
     * Finished Commenting!
     */
    //every spawner type 
    public GameObject SpawnerType1;
    public GameObject SpawnerType2;
    public GameObject SpawnerType3;
    public GameObject SpawnerType4;

    //stage boss
    public GameObject boss;

    //the first dialogue box or whatever
    //it is inactive in tghe hieracrchy
    public GameObject StartDialogue;
    ArrayList attackPatterns = new ArrayList();
     


// Start is called before the first frame update
    void Start()
    {
        //pick random attack order 
        for (int i = 0; i < 6; i++)
        {
            //at the first position put dialogue as a "spawner"
            if (i == 0)
            {
                attackPatterns.Add(4);
            }
            //at the last position, put the boss in 
            else if (i == 5)
            {
                attackPatterns.Add(5);
            }
            //otherwise, just put in a normal spawner randomly
            else
            {
                //randomise one of the spawners being put in
                attackPatterns.Add(Random.Range(1, 4));
            }
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
        
        

        //Get how many spawner are currently active.
        GameObject[] spawnersActive;
        spawnersActive = GameObject.FindGameObjectsWithTag("Spawner");
        int spawnerCount = spawnersActive.Length;
         
        //if there are no spawners active
        if (spawnerCount == 0)
        {

            /* create a new spawner based on which is the topmost item in the array
             
                    CURRENT ROSTER OF SPAWNERS:
                    
                    1. spawn in regular bullets in at regular points
                    2. spawn in sprawying bullets in randomly
                    3. spawn in spiraling bullets randomly
                    4. create dialogue
                    5. spawn in stage boss

             */

           
            if (attackPatterns.Count > 0) {
               
                //first attack pattern
                if ((int)attackPatterns[0] == 1)
                {

                    //just put into the proper place
                    GameObject s1instance = Instantiate(SpawnerType1);
                    s1instance.transform.position = new Vector2(0.5267447f, 1.039411f);
                    GameObject s4instance = Instantiate(SpawnerType4);


                }
                //second attack pattern
                else if ((int)attackPatterns[0] == 2)
                {
                    GameObject s2instance = Instantiate(SpawnerType2);
                }
                //third attack pattern
                else if ((int)attackPatterns[0] == 3)
                {
                    GameObject s2instance = Instantiate(SpawnerType3);
                }
                //first dialogue
                else if ((int)attackPatterns[0] == 4)
                {
                    
                    StartDialogue.SetActive(true);
                }
                //create boss
                else if ((int)attackPatterns[0] == 5)
                {
                    Instantiate(boss);
                }
            }

            //update front of list and do a (null check)
            if ( attackPatterns.Count > 0 && attackPatterns != null )
            {
                //when appropriate, remove the frontmost item 
                attackPatterns.RemoveAt(0);
                

            }


                

        }


    }

}
