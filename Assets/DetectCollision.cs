using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
/*
 * THis script controls collisions and health for the player
 * Finished commenting!
 */
public class DetectCollision : MonoBehaviour
{
     

    public int health;
    public float graze;
    [SerializeField] EndScript endScript;

    //This is where information  is displayed
    public TextMeshProUGUI scoreText;//score
    public TextMeshProUGUI grazeText;//graze (why am i even telling you this)
    public float radius = 0.5f;
    public GameObject player;//the player
    // Start is called before the first frame update
    private void Start()
    {
        //set up player health
        health = 150;
        
    }
    private void Awake()
    {
        //set endscript up so that we can use it to create panels.
        //there is a null check
        if (endScript == null)
        {
            var go = GameObject.FindWithTag("Finish");
            
            Debug.Log("is go null?");
            Debug.Log(go == null);

            if (go != null)
            {
               
                endScript = go.GetComponent<EndScript>();
            }
            else
            {
                Debug.LogWarning("no end script!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the boss or enemy hits the player, deduct 10 hp
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            if( collision.gameObject.tag == "Enemy" )
            {
                Destroy(collision.gameObject);
            }
            health -= 10;
            
        }
    }

    private void Update()
    {
        
        //GRAZE IMPLEMENTATION:

        //look all the colliders in the radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        
        //for every collider nearby;
        foreach (var hitCollider in hitColliders)
        {
            //if it's an enemy, add 0.05 graze
            if (hitCollider.gameObject.tag != "Player" && hitCollider.gameObject.tag  != "PlayerBullet")
            {          
                graze += 0.05f;
            }
        }

        //DIEING:
        //if health less than or equal to 0, die
        if (health <= 0)
        {
            Destroy(player);
            //set up the death dialog
            endScript.setUp(graze, health);
            

        }
        //display health on the attached textboxes
        scoreText.text = "Health: " + health;
        grazeText.text = "Graze: " + (int)graze;

        //if there's a gameobject of type boss, then set boss's endscript since im unable to set it inside of boss script for some reason
        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            GameObject bossInstance = GameObject.FindGameObjectWithTag("Boss");
            bossInstance.GetComponent<BossScript>().endScript = this.endScript;
        }


    }
    //getter for graze and health
    public float getGraze()
    {
        return graze;
    }

    public int getHealth()
    {
        return health;
    }
   
}
