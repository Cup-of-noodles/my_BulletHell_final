using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is attatched to the sheild of the player.
 * it's purpose is to ensure that when the shield is active, it destroys any enemies that touches it.
 * Commenting done!
 */

public class BlockingScript : MonoBehaviour
{
   //while this object is active in the hierarchy, destory anything with an enemy tag that touch it.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            

        }
    }
}
