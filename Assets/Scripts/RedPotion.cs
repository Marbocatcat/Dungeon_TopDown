using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    // once it collides to a player
    // add health to the player
    // destroy this game object

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wizzard")
        {
            Destroy(this.gameObject); // destroy the potion
            collision.gameObject.GetComponent<wizzard>().addHealth(); // run the add health function on the wizzard if it enters the collision.
            
        }
      
    }
}
