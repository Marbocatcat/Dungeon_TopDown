using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltController : MonoBehaviour
{
   

    public float speed; // the speed of the bolt.
    public Rigidbody2D boltBody2D;

    GameObject target; // the wizard

    void Start()
    {
        boltBody2D = GetComponent<Rigidbody2D>(); // get a reference to the rigidbody2d of the bolt.
        target = GameObject.FindGameObjectWithTag("Wizzard");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
       
        boltBody2D.velocity = new Vector2(moveDir.x, moveDir.y); // based on physics it shoots the wizzard.
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // if the bolt collides with Wizzard, run the isHit() funciton on the wizzard and destroy the bolt.
        if (collision.tag == "Wizzard")
        {
            collision.gameObject.GetComponent<wizzard>().isHit();
            Destroy(this.gameObject);
        }
        // if the bolt hits walls , destroy the bolt object.
        else if (collision.tag == "walls")
        {
            Destroy(this.gameObject);
        }
        else
        {
            // if the bolt doesnt hit anything start the coroutine to fade the bolt away.
            StartCoroutine("fadeBolt");
        }
        
    }

    IEnumerator fadeBolt()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
