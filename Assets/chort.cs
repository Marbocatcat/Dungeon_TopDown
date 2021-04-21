using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chort : MonoBehaviour
{
    [SerializeField]
    private Transform wizzard;

    bool running;
    bool isFacingLeft;
    bool justHit;
    bool hit;

    public float speed;
    public float health;
    public float lineOfSite;
    public GameObject damageCounter;

    private Material matRed;
    private Material matDefault;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody2D;
    Animator animator;
    Vector3 baseScale;

    private void Start()
    {
        hit = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
        matRed = Resources.Load("matRed", typeof(Material)) as Material;
        matDefault = spriteRenderer.material;
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

        if(health <= 0)
        {
            isDead();
        }

        // this is when chort is idle
        handleAnimations(running);
    
    }


    void handleDamageCounter()
    {
        Instantiate(damageCounter, transform.position, transform.rotation);
    }
    void isDead()
    {
        Destroy(gameObject);
    }
    public void isHit() // runs when you hit the wizzard.
    {


        hit = true;
        health--;
        spriteRenderer.material = matRed;
        handleDamageCounter();

        if(health <=0 )
        {
            isDead();
        }
        else
        {
            StartCoroutine("resetMaterial");
        }
 
    }

    IEnumerator resetMaterial()
    {
        yield return new WaitForSeconds(.1f);
        spriteRenderer.material = matDefault;
        hit = false;
    }
    IEnumerator resetJusthit()
    {
        yield return new WaitForSeconds(.1f);
        justHit = false;
    }

    void chasing()
    {

        // if im hit stop chasing, otherwise chase the wizzard
        if(hit == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, wizzard.position, speed * Time.deltaTime); // move towards the player
            running = true;
        }
        else if(hit == true)
        {
            running = false;
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        
        
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
        if(running && !hit)
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
            isFacingLeft = true;
        }
        else if(playerDirection > 0.1)
        {
            newScale.x = baseScale.x;
            isFacingLeft = false;
        }
        transform.localScale = newScale;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wizzard" && justHit == false)
        {
            collision.gameObject.GetComponent<wizzard>().isHit();
            justHit = true;
        }
        else
        {
            StartCoroutine("resetJusthit");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }


}
