using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D bulletBody2D;

    // Update is called once per frame
  
    private void FixedUpdate()
    {
        bulletBody2D.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if bullets collides with an enemy , destroy the enemy , otherwise start the bullet fade coroutine.
        if(collision.gameObject.name == "Chort")
        {
            collision.gameObject.GetComponent<chort>().isHit(); // if you hit chort , it runs a function that -health;
            Destroy(gameObject);
        }
        else if(collision.gameObject.name == "Necromancer")
        {
            collision.gameObject.GetComponent<NecromancerScript>().isHit(); // if you hit chort , it runs a function that -health;
            Destroy(gameObject);
        }
        else if(collision.gameObject.name == "Shaman")
        {
            collision.gameObject.GetComponent<ShamanScript>().isHit();
            Destroy(gameObject);
        }
        else if(collision.tag == "walls") // if I collide with walls , destroy the bullet instantly
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine("bulletFade");
        }
    }

    // this gives it a delay of 2 before the bullet fades away.
    IEnumerator bulletFade()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
