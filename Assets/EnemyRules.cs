using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRules : MonoBehaviour
{
    /*
     * A little script i wrote for the player's bullets 
     * that destroys them if they touch an enemy. I do this to screw with the player so boss fights arent as fast.
     * Finished Commenting!
     * 
     * 
     */
    public GameObject bullet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when the bullet hits an enemy, the player bullet dies.
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(bullet);
        }
    }
}
