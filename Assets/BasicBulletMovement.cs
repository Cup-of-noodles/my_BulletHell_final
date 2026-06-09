using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * This is the first script ever written in this project after the player's movement. 
 * It is used to move bullets with a basic velocity, nothing too fancy
 * Finished commenting!
 */
public class BasicBulletMovement : MonoBehaviour
{

    public float Xspeed;//x-component of this bullet velocity
    public float Yspeed;//y-component of the velocity
    public GameObject bullet;//whichever bullet this is to be attached to
    public Vector2 movement;//the vecotr that is used to translate this bullet across the screen

    // Start is called before the first frame update

    //Set it disbled by default. it is allowed to move only when i want it to.
   void Start()
    {
        //set this bullet to be active
        bullet.SetActive(true);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //despawn rules:
        //if out of frame bullet is destroyed
        //i used some specific coordinates to declare this
        if ((bullet.transform.position.y >= 9)|| (bullet.transform.position.y <= - 4.12) || (bullet.transform.position.x >= 6) || (bullet.transform.position.x <= -11.119))
        {
            Destroy(bullet);
        }

        //move this thang
        //the position is changed with speed * deltatime
        movement = new Vector2(Xspeed * Time.deltaTime, Yspeed * Time.deltaTime);
        bullet.transform.Translate(movement);

        

    }

    //used to change the speed of the attached bullet, usually upon initialization. 
    //i have a bunch of other bullets that work like this, actually
    public void changeSpeeds(float newXspeed, float newYspeed)
    {

        Xspeed = newXspeed;
        Yspeed = newYspeed;
    }
}
