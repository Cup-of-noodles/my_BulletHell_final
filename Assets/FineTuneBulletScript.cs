using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FineTuneBulletScript : MonoBehaviour
{

    /*
     * Finished commenting!
     * This script if for bullets which must pause and rotate towards the player before starting to move
     */

    //x and y speed components
    public float Xspeed;
    public float Yspeed;
    //the bullet this is attached to
    public GameObject bullet;
    //x and y acceleration components
    public float xAccel;
    public float yAccel;
    //the player this is going to target
    [SerializeField] GameObject Player;
    //whether or not this vullet has rotated toawrdss the player yet
    public bool rotated;

    //tracks elapsed time so that the tine it takes to start
    ///moving can be tracked
    private float elapsedTime = 0;
    //vector that controls movement
    public Vector2 movement;
    //how much time it takes after spawning to start moving
    public float phase1Cooldown;

    // Start is called before the first frame update
    void Start()
    {
        //locate player 
        Player = GameObject.FindGameObjectWithTag("Player");
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
         
        
        //destroy if out of bounds
        if ((bullet.transform.position.y >= 9) || (bullet.transform.position.y <= -4.12) || (bullet.transform.position.x >= 6) || (bullet.transform.position.x <= -11.119))
        {
            Destroy(bullet);
        }
        //if it hasnt been rotated
        if (!(rotated))
        {
            //rotate towards enemy, mark as rotated 
            rotated = true;
            rotateToEnemy();
        }
        //if cooldown is over
        if (elapsedTime >= phase1Cooldown )
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

   //setter methods for speed and acceleration fields
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

    //causes this bullet to rotate the the player's position at this point of time.
    public void rotateToEnemy()
    {
        //if there's a player
        if (Player != null) { 

        //get the vector pointing from the bullet to the player
        Vector2 newDir = (Vector2)Player.transform.position - (Vector2)bullet.transform.position;

         //use trig ratios and the x and y distance of the previous vector
         ////to determine the angle to rotate... yeah...
        float deg = Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, deg + 90));

        }


    }

}
