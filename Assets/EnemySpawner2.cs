using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     * The general idea for this kind of spawner is to spawn bullets at random places at the top of the screen
     * used in 2 (two) spawner prefabs, the falling star and the spiral ones
     * FInished Commenting!
     * 
     */
public class EnemySpawner2 : MonoBehaviour{
     //one of these keeps track of how long each attack lasts, one of these track of the frequency of the attack.
    private float elapsedTime = 0;//used to track the lifetime
    private float ATKcooldown = 0;//used to track the attack cooldown

    public float fireRate;//rate this spawner fires at
    public float SPAWNER_LIFE;//ifetime of this spawner
    //the spawner this script was attached to.
    public GameObject thisSpawner;//this spawner object
    public GameObject enemyA;//enemy this spawner fires

    
    // Update is called once per frame
    void Update()
    {
        //increment the time passed since last attack
        elapsedTime += Time.deltaTime;
        ATKcooldown += Time.deltaTime;

        //create an enemy at the top of the screen at a random x-coord
        if (ATKcooldown >= fireRate)
        {

            //pick a random number between -11.119 and 11.119
            //the y-value will always be 7
            float randX = Random.Range(-10f, 10f);
            //instatiate there
            GameObject instance = Instantiate(enemyA);
            instance.transform.position = new Vector2(randX, 7);
            //reset cooldown
            ATKcooldown = 0;
            
        }
        GameObject dieDialog = GameObject.FindGameObjectWithTag("Finish");

        //is it's outlived it's lifespan, or end dialog is up; destroy this object 
        if (elapsedTime >= SPAWNER_LIFE || dieDialog != null && dieDialog.activeInHierarchy)
        {
            Destroy(thisSpawner);
        }


    }
}
