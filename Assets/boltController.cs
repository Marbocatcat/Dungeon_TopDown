using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltController : MonoBehaviour
{
    [SerializeField]
    public Transform wizzard;

    public float speed;
    public Rigidbody2D boltBody2D;

    // Update is called once per frame


    private void Start()
    {
        shootWizzard();
    }

    void shootWizzard()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, wizzard.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if bullets collides with an enemy , destroy the enemy , otherwise start the bullet fade coroutine.
        if (collision.tag == "Wizzard")
        {
            // hit script on the wizzard goes here
        }
        else if (collision.tag == "walls") // if I collide with walls , destroy the bullet instantly
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
