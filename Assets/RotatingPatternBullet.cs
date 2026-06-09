using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotatingPatternBullet : MonoBehaviour
{

   /*
    * the script that cotrols the movement of a bullet 
    * that shoots bullets in a spiral pattern
    * 
    * Finished commenting!
    */

    //the enemy that this bullet fires
    public GameObject enemyA;

    //self-explanatory
    public float Xspeed;
    public float Yspeed;

    //the bullet this script is attached to
    public GameObject bullet;

    //SPAWNPOINT
    //if two time sees this
    //dont let them get their two tiem
    //juices all over it
    public GameObject spwn1;

    //firing cooldown for this thing, should NOT ever be manually edited
    public float ATKcooldown = 0;

    //the firing speed of this spawner
    public float firingSpeed;

    public Vector2 movement;//dw about this its just like for movement tracking and stuff 

    //the starting angle for firing lulll
    public float angleOfRotation;

    public float shotSpread;


    public float subBulletSpeed;//controls the speed of the subBullets fired from this bullet

    // Update is called once per frame
    void Update()
    {
        //despawn rules:
        //if out of frame bullet is destroyed
        if ((bullet.transform.position.y >= 10) || (bullet.transform.position.y <= -4.12) || (bullet.transform.position.x >= 6) || (bullet.transform.position.x <= -12))
        {
            Destroy(bullet);
        }

        //movement or whatever
        movement = new Vector2(Xspeed * Time.deltaTime, Yspeed * Time.deltaTime);
        bullet.transform.Translate(movement);
        
        ATKcooldown += Time.deltaTime;
        
        //if the attack cooldown is met, create a bullet 
        if (ATKcooldown >= firingSpeed)
        {

            //this thing's bullet firing procedure:

            //create a bullet
            //set it's speed vectors to cosine and sine (respectively, ofc) of the current angle
            //times like, the bullet's speed
            GameObject instance1 = Instantiate(enemyA);
            instance1.transform.position = new Vector2(spwn1.transform.position.x, spwn1.transform.position.y);
            instance1.GetComponent<BasicBulletMovement>().changeSpeeds(subBulletSpeed * Mathf.Cos(angleOfRotation * Mathf.Deg2Rad), subBulletSpeed * Mathf.Sin(angleOfRotation * Mathf.Deg2Rad));//im sorry mrs jackson (oooooooooo)

            //increase the rotation angle by the shotSpread :p
            angleOfRotation -= shotSpread;

            ATKcooldown = 0;
        }

       
    }
}
