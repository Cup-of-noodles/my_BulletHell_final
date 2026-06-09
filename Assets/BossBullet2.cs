using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet2 : MonoBehaviour
{
    /*
    * Finished commenting!
    * This script if for bullets which must pause and rotate towards a cerian direction before starting to move
    */

    //x and y speed components
    public float Xspeed;
    public float Yspeed;
    //the bullet this is attached to
    public GameObject bullet;
    //x and y acceleration components
    public float xAccel;
    public float yAccel;
   
    //whether or not this vullet has rotated toawrdss the player yet
    public bool rotated;

    //tracks elapsed time so that the tine it takes to start
    ///moving can be tracked
    private float elapsedTime = 0;
    //vector that controls movement
    public Vector2 movement;
    //how much time it takes after spawning to start moving
    public float phase1Cooldown;
    //direction to rotate in 
    public float deg;
    

    // Start is called before the first frame update
    void Start()
    {
        //because it hasnt rotated yet, sillys!
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {//increment the elapsed time
        elapsedTime += Time.deltaTime;


        //destroy if out of bounds
        if ((bullet.transform.position.y >= 9) || (bullet.transform.position.y <= -4.12) || (bullet.transform.position.x >= 6) || (bullet.transform.position.x <= -11.119))
        {
            Destroy(bullet);
        }
        //if it hasnt been rotated
        if (!(rotated))
        {
            //rotate towards proper direction, mark as rotated 
            rotated = true;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, deg + 90));
        }
        //if cooldown is over
        if (elapsedTime >= phase1Cooldown)
        {
            //move this bullet with a kinematic equation
            movement = new Vector2(Time.deltaTime * Time.deltaTime * 0.5f * xAccel + Xspeed * Time.deltaTime, (Time.deltaTime * Time.deltaTime * 0.5f * yAccel) + Yspeed * Time.deltaTime);
        }
        // otherwise, during the span the bullet is supposed to stay still, movement should be zero
        else
        {

            movement = Vector2.zero;
        }
        //translate this bullet with the movement vecotr we set earlier.
        bullet.transform.Translate(movement);

    }

    //setter methods for speed and acceleration fields. also deg field.
    public void changeSpeeds(float newXspeed, float newYspeed)
    {
        Xspeed = newXspeed;
        Yspeed = newYspeed;
    }

    public void changespeedsAndAccelerationandDeg(float newAccX, float newAccY, float newXspeed, float newYspeed,  float newDeg)
    {
        xAccel = newAccX;
        yAccel = newAccY;
        Xspeed = newXspeed;
        Yspeed = newYspeed;
        deg = newDeg;

    }

    public void changeDeg(float newDeg)
    {
        deg = newDeg;
    }


}
