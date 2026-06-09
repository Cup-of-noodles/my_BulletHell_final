using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner4 : MonoBehaviour 
{
    /*
     * this spawner creates auto-firing bullets that come from the sides of the screen
     * Finished commenting!
     * 
     */
    //one of these keeps track of how long each attack lasts, one of these track of the frequency of the attack.
    private float elapsedTime = 0;//do not edit this i swear to god
    private float ATKcooldown = 0;

    public float fireRate;//the rate at which this spawner fires
    public float SPAWNER_LIFE;//the life this spawer exists for
    //the spawner this script was attached to.
    public GameObject thisSpawner;
    public GameObject enemyA;//the enemy this spawner creates

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        ATKcooldown += Time.deltaTime;

        //when firing
        if (ATKcooldown >= fireRate)
        {
            ATKcooldown = 0;

            // create bullets in a pattern:
        
           
            //3 created at the left

            GameObject instance11 = Instantiate(enemyA);
            instance11.transform.position = new Vector2(-6, 0);
            instance11.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            GameObject instance12 = Instantiate(enemyA);
            instance12.transform.position = new Vector2(-6, 2);
            instance12.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            GameObject instance13 = Instantiate(enemyA);
            instance13.transform.position = new Vector2(-6, 4);
            instance13.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            //3 created at the right
            GameObject instance14 = Instantiate(enemyA);
            instance14.transform.position = new Vector2(4, 0);
            instance14.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            GameObject instance15 = Instantiate(enemyA);
            instance15.transform.position = new Vector2(4, 2);
            instance15.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            GameObject instance16 = Instantiate(enemyA);
            instance16.transform.position = new Vector2(4, 4);
            instance16.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));




            ATKcooldown = 0;
        }
        //used to check if the end dialogue is there
        GameObject dieDialog = GameObject.FindGameObjectWithTag("Finish");

        //is it's outlived it's lifespan, or end dialog is up; destroy this object 
        if (elapsedTime >= SPAWNER_LIFE || dieDialog != null && dieDialog.activeInHierarchy)
        {
            Destroy(thisSpawner);
        }
    }
}
