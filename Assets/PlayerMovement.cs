using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
 /*
  * this class controls the player's movement, and also controls whether the player is allowed to use 
  * shield ability, or shoot in the precsence of a boss
  * 
  * Finished Commenting!
  */
{
    //FIELDS:

    public GameObject player;//the player this script moves
    public GameObject Shield;// the attched shield
    public GameObject hitboxVisual;//the gameobject that serves as a visual for the hitbox
    private Vector2 movement;//vector that controls movement 
    public float speed = 1f;//sped of the player
    private float timeElapsed = 0f;
    
    //vaeiable meant for tuning the player's bullet firing spped in the presence of a boss
    public float bulletSpeed;
    public GameObject playerBullet;//the bullet prefab fired by the player
    public float bulletCoolDown;//cooldown for firing bullets 
    private float bulletTimeCounter;//counter updates with deltatime for checking if bullet can be fired

    public float sheildActivationTime;//hpw long the sheild can be active for
    public float shieldCooldown;//counter for whther sheild can be activated
    //use this to detect whether a boss is active in hierarchy
    public bool bossActive;


    


    // Start is called before the first frame update
    //by default, boss is not active, shield is set to inactive, hitbox is set to inactive
    void Start()
    {
        
        Shield.SetActive(false);
        hitboxVisual.SetActive(false);
        bossActive = false;
    }

    // Update is called once per frame
    //update is updating irregularly
    void Update()
    {
        //
        timeElapsed += Time.deltaTime;
        bulletTimeCounter += Time.deltaTime;

        //check if there is objectof tag  boss in hierarchy. if one is present, mark bossactive
        GameObject[] aBoss;
        aBoss = UnityEngine.GameObject.FindGameObjectsWithTag("Boss"); 
        
        if (aBoss.Length >= 1)
        {
            bossActive = true;
        }

        //movement: move player with WASDor arrow keys depending on input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement = new Vector2(0, speed * Time.deltaTime);
            player.transform.Translate(movement);
        }
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement = new Vector2(-speed * Time.deltaTime, 0);
            player.transform.Translate(movement);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement = new Vector2(0, -speed * Time.deltaTime);
            player.transform.Translate(movement);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement = new Vector2(speed * Time.deltaTime, 0);
            player.transform.Translate(movement);
        }
        //otherwise, if there is no input, set movement to 0
        movement = Vector2.zero;


        /*
         * This code is such that you can't shield for the first three seconds. 
         *///this is the night mail crossing the border letters for the
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //show hitbox, regardless of the time elapsed
            hitboxVisual.SetActive(true);

            //if more than 3 seconds have elapsed and shield isnt already active, then enable shield 
            if (timeElapsed >= shieldCooldown && !Shield.activeInHierarchy && !bossActive)
            {
                //set timeElapsed to zero again
                timeElapsed = 0;
                Shield.SetActive(true);

            }
        }

        //if z key held down and a boss is active, then fire a bullet, but only if the cooldown is finished
        if (Input.GetKey(KeyCode.Z) && bossActive)
        {
            if (bulletTimeCounter >= bulletCoolDown) {
                //resert time ocunter
                bulletTimeCounter = 0f;
                
                //fire off bullets
                //
                /*
                GameObject instance1 = Instantiate(playerBullet);
                instance1.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
                instance1.GetComponent<BasicBullet_movement>().changeSpeeds(0, bulletSpeed);
                instance1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                GameObject instance2 = Instantiate(playerBullet);
                instance2.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
                instance2.GetComponent<BasicBullet_movement>().changeSpeeds(0, bulletSpeed);
                instance2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -10));

                GameObject instance3 = Instantiate(playerBullet);
                instance3.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
                instance3.GetComponent<BasicBullet_movement>().changeSpeeds(0, bulletSpeed );
                instance3.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 10));
                */

                for (int i = -10; i <= 10; i+= 10)
                {
                    GameObject instance = Instantiate(playerBullet);
                    instance.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
                    instance.GetComponent<BasicBulletMovement>().changeSpeeds(0, bulletSpeed);
                    instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
                }




            }

        }



        //if 1 second have elapsed and shield is already active, then disable shield 
        if (timeElapsed >= sheildActivationTime && Shield.activeInHierarchy)
        {
            //set shield inactive and reset timeElasped
            timeElapsed = 0;
            Shield.SetActive(false);
        }
        

        


    }
}
