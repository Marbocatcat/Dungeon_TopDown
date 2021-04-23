using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowBolt : MonoBehaviour
{
    public float speed; // the speed of the bolt.
 

    Transform target; // the wizard


    private void Start()
    {
       
        target = GameObject.FindGameObjectWithTag("Wizzard").transform;
    }

    void Update()
    {
        
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);


        StartCoroutine("fadeBolt");
        
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
     

    }

    IEnumerator fadeBolt()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
