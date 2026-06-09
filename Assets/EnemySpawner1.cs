using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
//using UnityEditor.Search;
using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    /*
     * omigosh this script is so old, it 
     * seriously makes me feel nostaligc...
     * 
     * anyhow, this script spawns basic bullets at the top of the screen in a 
     * line like this, and also controsl the life of the spawner.
     *      .      .    .
     *      .      .    .
     *      .      .    .
     *      .      .    .
     *      .      .    .
     *      .      .    .
     *      
     *      Finished Commenting!
     */

    /*Every Enemy type this spwner uses:
     */
    public GameObject enemyA;
    
    //individual spawners
    public GameObject spawnerA;
    public GameObject spawnerB;
    public GameObject spawnerC;
    public GameObject spawnerD;
    public GameObject spawnerE;

    //all the spawners as a collective
    public GameObject collectiveSpawners;

    //one of these keeps track of how long each attack lasts, one of these track of the frequency of the attack.
    
    private float elapsedTime = 0;//used to track cooldowns
    private float ATKcooldown = 0;//how often bullets are fired
    public float SPAWNER_LIFE;// the life this spawner exists for

    //it's a queue tracking different spawning positions!! You're gonna be so pround of me Mrs. Jackson!! :D
    Queue<int> spawningPositions = new Queue<int>();

    //the firing speed of this spawner
    public float firingSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //start off by setting up the queue in this order of spawning positions. 
        //1 means outer most spawning positions A, D
        //2 means 2nd outer most positions B, C
        //3 means innermost position E
        spawningPositions.Enqueue(1);
        spawningPositions.Enqueue(2);
        spawningPositions.Enqueue(3);
    }

    // Update is called once per frame
    void Update()
    {
        //increment the time passed since last attack
       elapsedTime += Time.deltaTime; 
       ATKcooldown += Time.deltaTime;

        if (ATKcooldown >= firingSpeed)
       {
            //if fromt of queue is one 
            if (spawningPositions.Peek() == 1)
            {
                //spawn bullet at A and D
                GameObject eAinstance = Instantiate(enemyA);
                eAinstance.transform.position = new Vector2(spawnerA.transform.position.x, spawnerA.transform.position.y);

                GameObject eAinstance2 = Instantiate(enemyA);
                eAinstance2.transform.position = new Vector2(spawnerD.transform.position.x, spawnerD.transform.position.y);

                
            }

            // if fronf of queue is two 
            else if(spawningPositions.Peek() == 2)
            {
                //spawn bullet at B and C
                GameObject eAinstance = Instantiate(enemyA);
                eAinstance.transform.position = new Vector2(spawnerB.transform.position.x, spawnerB.transform.position.y);

                GameObject eAinstance2 = Instantiate(enemyA);
                eAinstance2.transform.position = new Vector2(spawnerC.transform.position.x, spawnerC.transform.position.y);
                
            }
            else if (spawningPositions.Peek() == 3)
            {
                //spawn bullet at E and reset cooldown
                GameObject eAinstance = Instantiate(enemyA);
                eAinstance.transform.position = new Vector2(spawnerE.transform.position.x, spawnerE.transform.position.y);
            }
            ATKcooldown = 0;
            //go to next item in the queue
            int item = spawningPositions.Dequeue();
            spawningPositions.Enqueue(item);
        }
        GameObject dieDialog = GameObject.FindGameObjectWithTag("Finish");

        //is it's outlived it's lifespan, or end dialog is up; destroy the collective spawners 
        if (elapsedTime >= SPAWNER_LIFE || dieDialog != null && dieDialog.activeInHierarchy)
        {
            Destroy(collectiveSpawners);
        }


    }


   
}
