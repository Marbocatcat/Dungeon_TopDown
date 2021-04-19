using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chort : MonoBehaviour
{
    [SerializeField]
    private Transform wizzard;

    bool running;

    public float speed;
    public float lineOfSite;

    Rigidbody2D rigidBody2D;
    Animator animator;
    Vector3 baseScale;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // if the wizzard is within line of site , he will run towards him.
        if(isSpotted())
        {
            chasing();
            flipTowardsPlayer();
        }
        else
        {
            running = false;
        }

        // this is when chort is idle
        handleAnimations(running);

    }

    void chasing()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, wizzard.position, speed * Time.deltaTime); // move towards the player
        running = true;
    }

    bool isSpotted()
    {
        float distanceFromPlayer = Vector2.Distance(wizzard.position, transform.position); // returns the distance from player position to the enemy position

        if (distanceFromPlayer < lineOfSite)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }

    void handleAnimations(bool running)
    {
        if(running)
        {
            animator.Play("chort_run_anim");
        }
        else
        {
            animator.Play("chort_idle");
        }
    }
    void flipTowardsPlayer()
    {
        Vector3 newScale = baseScale;
        float playerDirection = wizzard.position.x - transform.position.x; // records where the player is relative to chort.
        
        if(playerDirection < -0.1)
        {
            newScale.x = -baseScale.x;
        }
        else if(playerDirection > 0.1)
        {
            newScale.x = baseScale.x;
        }
        transform.localScale = newScale;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }


}
