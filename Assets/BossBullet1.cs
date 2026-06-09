using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBullet1 : MonoBehaviour
{
    /*
     * despite the name of this script, this script functions a lot like the basic bullet script, in the sense that 
     * it can be used to translate any bullet, but this one is special and can cause a accelaerating effect. 
     * its usualy only used for boss bullets though.
     * Finished commenting!
     */


    public float Xspeed;//x-component of speed
    public float Yspeed;//y-component 
    public GameObject bullet;//bullet that this script moves
    public float xAccel;//x-component of accleration
    public float yAccel;//y-component of acceleration

    public Vector2 movement;//vector used to translate this bullet


    // Update is called once per frame
    void Update()
    {
        //destroy if out of bounds using the cooridinates set
        if ((bullet.transform.position.y >= 9) || (bullet.transform.position.y <= -4.12) || (bullet.transform.position.x >= 6) || (bullet.transform.position.x <= -11.119))
        {
            Destroy(bullet);
        }
      
        
        //i like to move it move it
        //yeah just move it with the physics formula:
        // delta x = 1/2 a*t^2  + vi * t
        movement = new Vector2(Time.deltaTime * Time.deltaTime * 0.5f * xAccel + Xspeed * Time.deltaTime, (Time.deltaTime * Time.deltaTime * 0.5f * yAccel) + Yspeed * Time.deltaTime);
        bullet.transform.Translate(movement);
    }


    //setter methods for speed and acceleration. to make sure the bullet moves upon initialization
    public void changeSpeeds(float newXspeed, float newYspeed)
    {
        Xspeed = newXspeed;
        Yspeed = newYspeed;
    }

    public void changespeedsAndAcceleration(float newAccX, float newAccY, float newXspeed, float newYspeed)
    {
        xAccel = newAccX;
        yAccel = newAccY;
        yAccel = newXspeed;
        Xspeed = newXspeed;
        Yspeed = newYspeed;

    }
}
