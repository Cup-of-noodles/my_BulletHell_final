
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStarMovement : MonoBehaviour
{
    /*
     * this script is supposed to control the movement of bullets that push 
     * out all of the bullets in 4 directions and it was originally supposed to
     * look like a falling star so that is why it is named like that
     * 
     * Finished Commenting!
     */
    //the bullet it fires
    public GameObject enemyA;
    //components of velocity
    public float Xspeed;
    public float Yspeed;
   
    //this object that is attached to
    public GameObject bullet;
    //life of this bullet(the reason inly this bullet gets a life is because this script also acts for spawners that spawn bullets outwards)
    public float life;
    public float lifeCounter;

    //firing cooldown for this thing, should NOT ever be manually edited
    private float ATKcooldown = 0;

    //the firing speed of this spawner
    public float firingSpeed;

    private Vector2 movement;//dw about this its just like for movement tracking and stuff 


    public float subBulletSpeed;//controls the speed of the subBullets fired from this bullet

    // Update is called once per frame
    void Update()
    {
        //despawn rules:
        //if out of frame bullet is destroyed
        if ((bullet.transform.position.y >= 9) || (bullet.transform.position.y <= -4.12) || (bullet.transform.position.x >= 6) || (bullet.transform.position.x <= -11.119))
        {
            Destroy(bullet);
        }

        //movement or whatever
        movement = new Vector2(Xspeed * Time.deltaTime, Yspeed * Time.deltaTime);
        bullet.transform.Translate(movement);

        //increment time counter variables
        ATKcooldown += Time.deltaTime;
        lifeCounter += Time.deltaTime;

        //at a regular interval, spawn the bullets from their proper place.
        if (ATKcooldown >= firingSpeed && (enemyA != null) )
        {

            //create a new instance of enemyA at this bullet
            GameObject instance1 = Instantiate(enemyA);
            instance1.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
            instance1.GetComponent<BasicBulletMovement>().changeSpeeds(-subBulletSpeed, -subBulletSpeed);


            GameObject instance2 = Instantiate(enemyA);
            instance2.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
            instance2.GetComponent<BasicBulletMovement>().changeSpeeds(subBulletSpeed, subBulletSpeed);


            GameObject instance3 = Instantiate(enemyA);
            instance3.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
            instance3.GetComponent<BasicBulletMovement>().changeSpeeds(-subBulletSpeed, subBulletSpeed);

            GameObject instance4 = Instantiate(enemyA);
            instance4.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y);
            instance4.GetComponent<BasicBulletMovement>().changeSpeeds(subBulletSpeed, -subBulletSpeed);

            ATKcooldown = 0;
        }

        //if the life runs out, destroy this bullet
        if (lifeCounter >= life)
        {
            
            Destroy(bullet);
        }


    }
}

