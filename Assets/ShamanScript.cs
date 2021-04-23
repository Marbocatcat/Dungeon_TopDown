using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanScript : MonoBehaviour
{
    [SerializeField]
    private Transform wizzard;

    bool running;
    bool justHit;
    bool hit;

    public float speed;
    public float health;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate;
    private float nextFireTime;

    public Transform firePoint;
    public GameObject bulletToFire;
    public GameObject damageCounter;

    private Material matRed;
    private Material matDefault;
    private Transform staffTransform;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody2D;
    Animator animator;
    Vector3 baseScale;


    private void Awake()
    {
        staffTransform = transform.Find("baton"); // returns the child transform of the object red_staff
    }

    private void Start()
    {
        hit = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale; // this determines where the necromancer is facing
        matRed = Resources.Load("matRed", typeof(Material)) as Material; // preloads the sprite material red for when it gets hit.
        matDefault = spriteRenderer.material; // default material when hes not hit.
    }

    // Update is called once per frame
    void Update()
    {
        whereIstheWizzard();



        if (health <= 0)
        {
            isDead();
        }

        // this is when chort is idle
        handleAnimations(running);
    }
    void whereIstheWizzard()
    {
        float distanceFromPlayer = Vector2.Distance(wizzard.position, transform.position); // returns the distance between the wizzards position to the my position.

        // only returns true if the necromancer's distance from the wizzard is less than the line of site but greater than the shooting range.
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            chasing();
            flipTowardsPlayer();
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time) // if the distance from player is less than the shooting range start firing.
        {
            attack();
            flipTowardsPlayer();
        }
        else
        {
            running = false;
        }

    }
    void attack() // instantiate the bolt and fires it.
    {
        Instantiate(bulletToFire, firePoint.position, Quaternion.identity);
        nextFireTime = Time.time + fireRate; // fire rate is reset to 1 sec.

    }

    void handleDamageCounter() // this is the graphic damage counter that pops up in his head when he is hit.
    {
        GameObject counter = (GameObject)Instantiate(damageCounter, transform.position, transform.rotation);

        Destroy(counter, .5f);
    }
    void isDead()
    {
        Destroy(gameObject);
    }
    public void isHit() // runs when you hit the wizzard.
    {
        hit = true;
        health--;
        spriteRenderer.material = matRed; // sets the sprite material to red.
        handleDamageCounter();

        if (health <= 0)
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
        // waits for .1 sec before resetting hit to false and the material back to default.
        yield return new WaitForSeconds(.1f);
        spriteRenderer.material = matDefault;
        hit = false;
    }

    void chasing()
    {

        // if im hit stop chasing, otherwise chase the wizzard
        if (hit == false)
        {
            moveTowards();
            running = true;
        }
        else if (hit == true)
        {
            running = false;
            transform.position = new Vector2(transform.position.x, transform.position.y); // this staggers the necromancer when hes hit.
        }
    }
    void moveTowards()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, wizzard.position, speed * Time.deltaTime); // move towards the player
    }



    void handleAnimations(bool running)
    {
        // if you are running and and not hit, play the idle anim, otherwise play the run animation.

        if (running && hit == false)
        {
            animator.Play("orc_shaman_run_anim");
        }
        else
        {
            animator.Play("shaman_idle_anim");
        }
    }
    void flipTowardsPlayer()
    {
        Vector3 newScale = baseScale;
        float playerDirection = wizzard.position.x - transform.position.x; // records where the player is relative to chort.

        if (playerDirection < -0.1) // if the player is on my left , turn left, otherwise turn right.
        {
            newScale.x = -baseScale.x;
        }
        else if (playerDirection > 0.1)
        {
            newScale.x = baseScale.x;
        }
        transform.localScale = newScale;
    }


    // shows the line of site and range of site.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
