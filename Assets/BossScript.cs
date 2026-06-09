using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    /*
     * this script controls the boss's attack patterns. at diffrent
     * point of health, the attack will change, and this is controlled by thr sign variable. 
     * 
     * Finished COmmenting!
     * 
     */

    [SerializeField] GameObject thisBoss;//attached boss
    [SerializeField] GameObject player;// the player to target
    public int bossHealth;//health of the boss
    public TextMeshProUGUI infoText;//used to display the boss's health
    public EndScript endScript; //originally serialize field.
                                 //it is public so that it can be set by the player

    //which attack sign is playing right now?
    public int sign;
    //firing cooldown for this thing, should NOT ever be manually edited with the editor
    public float ATKcooldown = 0;
    //the firing speed of this spawner
    public float firingSpeed;

    //Variables for specific signs:

    /*SIGN 1:
     */

    // the two firing angles from which bullets are shot from
    public float firingAngle1;
    public float firingAngle2;
    //idk how to describe this but uhhhh its like the angle change ig?
    public float difference;
    public GameObject enemyA;
    public GameObject enemyA2;
    public GameObject enemyB;
    public float sign1BulletSpeed;
    //counts how long this sign's timer has been running...
    public float signTimer1;


    //Sign2-specific stuff
    
    //the below firing angles are incremented and decremented by these amounts respectively to create the pattern
    public int difference2;
    public int difference22;
    //the two firing angles from where bullet are shot from to create a "death flower"
    public float firingAngle3;
    public float firingAngle33;
    //the enemy that the flower is comprised of
    public GameObject enemyC;
    //the speed at which enemyC is spawned with
    public float sign2BulletSpeed;
    //the decceleration it is spawned with
    public float bulletDecel2;
    //the maximum absolute value of rotation the firing angle can go before changing directions
    public float angleRetakeThreshHold;
    //tracks phases of this attack sign
    public float atk2Timer;
    //checks the refresh rate of the bullet bursts in the second part of this attack
    public float atk2Phase2Timer;
    //this going to be the thing that causes the bullets to randomly spawn 
    public GameObject bulletSpawnPoint;



    /*
    * Sign 3-Specific stuff
    */
    ////used to tracks phases of this attack sign
    public float atk3Timer;
    //determines when the second phase of this attack starts.
    public float atk32ndPhaseThreshold;
    //these variables represent the range of which accelerations bullets can be spawned with
    public float minAcceleration;
    public float maxAcceleration;
    //these variables represent the range of which velocities bullets can be spawned with
    public float minVelocity;
    public float maxVelocity;
    //these variables represent the range of which rotations bullets can be spawned with
    public float minRot;
    public float maxRot;
   //generic bullet that is "pushed" out randomly at part 1
    public GameObject enemyD;
    //homing bullet used for second phase of this attack
    public GameObject enemyE;



    // Start is called before the first frame update
    void Start()
    {
        //set this boss's health and the player it will be fighting
        bossHealth = 300;
        player = GameObject.FindGameObjectWithTag("Player");

        //setting the info-text so that it can properly display 
        GameObject textObj = GameObject.Find("info-board");
        infoText = textObj.GetComponent<TextMeshProUGUI>();
    }

   

    // Update is called once per frame
    void Update()
    {

        //display the boss's health
        infoText.text = "boss health: " + bossHealth;

        // count the cooldown
        ATKcooldown += Time.deltaTime;
        //pick out an attack based on health
        chooseAttack();
       

        if (ATKcooldown >= firingSpeed)
        {
            ATKcooldown = 0;

            //pick out attack 
            //chooseAttack();

            if (sign == 1)
            {
                
                //create 6 bullets
                //set it's speed vectors to cosine and sine (respectively, ofc) of the current angle
                //times like, the bullet's speed
                //aslo set it's rotation
                signTimer1 += Time.deltaTime;

                for (int i = 0; i < 7; i++)
                {
                    GameObject instance = Instantiate(enemyA);
                    instance.transform.position = new Vector2(thisBoss.transform.position.x, thisBoss.transform.position.y);
                    instance.GetComponent<BossBullet1>().changespeedsAndAcceleration(-1f, -1f, sign1BulletSpeed * Mathf.Cos((firingAngle1 + (20 * i)) * Mathf.Deg2Rad), sign1BulletSpeed * Mathf.Sin((firingAngle1 + (20 * i)) * Mathf.Deg2Rad));
                }

                //every 0.2 scconds, careate a circle of bullets around the player, have them have a random acc and velocity and firing angle in the ranges specified
                if (signTimer1 >= 0.2)
                {
                    signTimer1 = 0f;
                    for (int i = 0; i < 360; i += 30)
                    {
                        //set speed and acceleration randomly (although these variables are usualy used in the third sign)
                        float acceleration = Random.Range(minAcceleration, maxAcceleration);
                        float speed = Random.Range(minVelocity, maxVelocity);

                        //this is so that whatever way the bullet is rotated, it will point away from the player
                        float rotation = Random.Range(i - 90,i + 90);
                        GameObject instance = Instantiate(enemyA2);
                        //the radius of the bullet circle is now 2
                        instance.transform.position = new Vector2(player.transform.position.x + 2f * Mathf.Cos(i), player.transform.position.y + 2f * Mathf.Sin(i));
                        //set the bullet's components
                        instance.GetComponent<BossBullet2>().changespeedsAndAccelerationandDeg(0, acceleration, 0, speed, rotation );
                        

                    }
                }
                


                //if sign1 bullet angle is greter than 360, then set it to 0 and fire the rays towards the player
                if (firingAngle1 >= 360 && player != null)
                {
                    firingAngle1 = 0;
                    float deg = 0;
                    //determine angle to fire 
                    //if there's a player
                    if (player != null)
                    {
                        //get the vector pointing from the bullet to the player
                        Vector2 newDir = (Vector2)player.transform.position - (Vector2)thisBoss.transform.position;

                        //use trig ratios and the x and y distance of the previous vector
                        ////to determine th angle to rotate... yeah...
                        deg = Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg;
                    }

                    //create necessary bullets (reworked it into a for loop so that it's much less uglier)
                    //it makes bullets 45 deg adjacent to the one directly fired towards the player
                    for (float i = deg + 45; i <= deg + 135; i += 45 ) {
                        GameObject instance67 = Instantiate(enemyB);
                        instance67.transform.position = new Vector2(thisBoss.transform.position.x, thisBoss.transform.position.y);
                        instance67.GetComponent<BasicBulletMovement>().changeSpeeds(0, -10);
                        instance67.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
                    }


                }
                else
                {
                    //update firing angles
                    firingAngle1 += difference;
                }


            }
            /*
            * Sign 2
            */
            else if (sign == 2)
            {
                atk2Timer += Time.deltaTime;

                //for 10 seconds, do the bullet flower thing
                if (atk2Timer <= 2.05f && atk2Timer >= 0)
                {
                    //create bullets in a circle... sort of


                    //create enemy with a direction at one of the 8 45-degree intervals
                    for (int i = 0; i < 8; i++)
                    {
                        GameObject instance = Instantiate(enemyC);
                        instance.transform.position = new Vector2(thisBoss.transform.position.x, thisBoss.transform.position.y);
                        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, firingAngle3 + (45 * i)));
                        instance.GetComponent<BossBullet1>().changespeedsAndAcceleration(0, sign2BulletSpeed, 0, -bulletDecel2);

                        GameObject instance2 = Instantiate(enemyC);
                        instance2.transform.position = new Vector2(thisBoss.transform.position.x, thisBoss.transform.position.y);
                        instance2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, firingAngle33 + (45 * i)));
                        instance2.GetComponent<BossBullet1>().changespeedsAndAcceleration(0, sign2BulletSpeed, 0, -bulletDecel2);
                    }

                    
                    //increment angles (or decrement based on sign but whatever)
                    firingAngle33 += difference22;
                    firingAngle3 += difference2;

                    // when angle change limit reached
                    if (firingAngle3 >= angleRetakeThreshHold || firingAngle3 <= -angleRetakeThreshHold)
                    {
                        //reset firing angle
                        firingAngle3 = 0;
                        //change direction of bullet spiral on both sides
                        difference2 *= -1;
                        difference22 *= -1;

                    }

                }
                //there will be a 0.9 second gap where it is easy to 
                //if attack has been going longer than 10.9 seconds
                //during this time, those rapid-bullet-spawner thingies will be created at random spots throughout the map. you have been warned!

                else if (atk2Timer >= 2.05f && atk2Timer <= 2.45f)
                {
                    atk2Phase2Timer += Time.deltaTime;
                    //every 0.15 seconds, create a burst of bullets at a random point
                    if (atk2Phase2Timer > 0.15)
                    {
                        atk2Phase2Timer = 0;
                        GameObject instance = Instantiate(bulletSpawnPoint);
                        Vector2 spawnpoint = 
                            new Vector2
                            (Random.Range(thisBoss.transform.position.x - 4, thisBoss.transform.position.x + 4), Random.Range(thisBoss.transform.position.y - 6, thisBoss.transform.position.y + 4));

                        instance.transform.position = spawnpoint;
                    }
                }
                //at 2.7 seconds, restart the attack's cycle once more
                else if (atk2Timer >= 2.7f)
                {
                    atk2Timer = 0;
                    atk2Phase2Timer = 0;
                        
                }
                

                

            }
            //sign 3
            else if (sign == 3)
            {
                //update attack timer
                atk3Timer += Time.deltaTime;
                
                //if 2nd phase threshold hasnt been met,
                if (atk3Timer <= atk32ndPhaseThreshold)
                {

                    //10 times, create a bullet per frame
                    for (int i = 0; i <= 10; i++)
                    {
                        GameObject instance = Instantiate(enemyD);
                        //randomly define acceleration,speed, rotation, abd spawnpoint
                        float acceleration = Random.Range(minAcceleration, maxAcceleration);
                        float speed = Random.Range(minVelocity, maxVelocity);
                        float rotation = Random.Range(minRot, maxRot);
                        Vector2 spawnpoint = new Vector2(thisBoss.transform.position.x, thisBoss.transform.position.y);


                        instance.transform.position = spawnpoint;

                        instance.GetComponent<BossBullet1>().changespeedsAndAcceleration(-acceleration, -acceleration, speed * Mathf.Cos(rotation), speed * Mathf.Sin(rotation));
                    }
                }
                
                //if enough time has passed for the threshold 
                else if (atk3Timer >= atk32ndPhaseThreshold && atk3Timer <= atk32ndPhaseThreshold + 0.5) {

                        //create a circle of homing bullets 
                        for (int i = 0; i < 360; i += 20)
                        {
                            GameObject instance8n = Instantiate(enemyE);
                            instance8n.transform.position = new Vector2(thisBoss.transform.position.x + Mathf.Cos(i), thisBoss.transform.position.y + Mathf.Sin(i));
                            instance8n.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i + 90));
                        }

                        

                    
                }
                //for  0.7 sec period of time, this boss will be open to attack
                else if (atk3Timer >= atk32ndPhaseThreshold + 0.7)
                {
                    //after  that, reset the timer for this attack sign
                    atk3Timer = 0;
                }


            }


            //if player is not active in hierarchy or health is 0, kill yourself (this gameobject)
            if (bossHealth <= 0 || player == null || !player.activeInHierarchy)
            {
                //if this boss has been properly defeated, then bring up the winning screen
                if (bossHealth <= 0 )
                {
                    //if the player is not null and we are able to acess the player's information
                    if (player != null)
                    {
                        //get player's script and the nfetch it's variables
                        DetectCollision playerScriptinstance = player.GetComponent<DetectCollision>();
                        //set up the panel for when you beat the level

                        endScript.setUpWin(playerScriptinstance.getGraze(), playerScriptinstance.getHealth());
                    }
                    
                }
                //and of course, destory the boss
                Destroy(thisBoss);

            }





        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        // a bullet fired by the player will deplete you of 5/1000 hp

        if (collision.gameObject.tag == "PlayerBullet")
        {


            bossHealth -= 1;
            Destroy(collision.gameObject);
        }
    }

    //pick an attack based on health and set the attack sign to that
    public void chooseAttack()
    {
        //every "checkpoint",  choose one of your 3 attack signs according to the health key below
        /*
         * 
         * 
         * Checkpoint 1: 999 hp
         * attack sign name:
         * [path of the wind: great shot of zephryos]
         * 
         * Checkpoint 2: 666 hp
         * attack sign name:
         * [lotus sign: fire flower]
         * 
         * Checkpoint 3: 333 hp
         * attack sign name:
         * [taxevader's distaste: indefinite chase]
         */

        if (bossHealth > 200)
        {
            sign = 1;
            //set firing to the proper thibng for this sign
            firingSpeed = 0.1f;            


        }
        else if (bossHealth > 100)
        {
            firingSpeed = 0.1f;
            sign = 2;
           
        }
        else if (bossHealth > 0)
        {
            sign = 3;
            
        }


    }



}
