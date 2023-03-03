using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

    // Start is called before the first frame update
    private Rigidbody2D Enemy;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer rend;
    PlayerActions player;

    public GameObject head;
    public GameObject deadParticle;

    public int maxHP = 200;
    int currentHp;

    public string currentState;
    ///STATES: REB WAL ATTA  HURT  DEATH
    //ACTIONS:  0   1   2     3      4        


    public float distance;      //distance from skeleton to player
    public float attackRange;   //range that the player needs to be for enemy to attack
    [SerializeField]
    float walkSpeed;            //walk speed of the enemy

    //Skeleton can only attack once at a time. 
    public float timeBetweenAttacks;    //time that has to happen for enemy to attack again
    public float attackCools;           //time that it takes for attacking to continue

    public bool hurt;                   //if enemy is hurt, for hurt animation
    public bool attacking = false;      //if enemy is attacking, for attacking animation



    private void Start()
    {
        Enemy = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        currentHp = maxHP;
        //hurt = false;
        player = FindObjectOfType<PlayerActions>();      //makes the player's functions avail to this script
        currentState = "WALK";
        attackCools = timeBetweenAttacks;
    }

    // Update is called once per frame
    private void Update()
    {
        //walking function
        if (isFacingRight())
        {
            Enemy.velocity = new Vector2(walkSpeed, 0f);
        }
        else
        {
            Enemy.velocity = new Vector2(-walkSpeed, 0f);
        }

        //distance from enemy to player
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (attackCools > 0) attackCools -= Time.deltaTime;
        /////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        //STATES
        switch (currentState)
        {
            case "REBORN":
                anim.SetInteger("state", 0);
                break;

            case "WALK":
                anim.SetInteger("state", 1);
                Walk();
                if(distance <= attackRange)
                {
                    currentState = "ATTACK";
                }
                //*******************
                //WILL ADD WHEN SKELETON IS HURT
                break;

            case "ATTACK":
                //anim.SetInteger("state", 2);
                Attack();
                break;

            case "HURT":
                anim.SetInteger("state", 3);
                Hurt();
                break;

            case "DEATH":
                anim.SetInteger("state", 4);
                break;
        }
        
        if (attackCools > 0) attackCools -= Time.deltaTime;
    }

    //is enemy facing right?
    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    //function for enemy to ignore the player object & not get stuck on it
    //will add more if statements if there are more objects*********
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Sword Trap")
            && !collision.gameObject.CompareTag("Enemy"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(Enemy.velocity.x)), transform.localScale.y);
        }


    }

    /// <summary>
    /// These two functions will be completed with player's hurt functions
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attacking == true)
            {

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attacking == true)
            {

            }
        }
    }

    /// <summary>
    /// End of TODO
    /// </summary>
    /// <param name="damage"></param>
    /// 

    //Take damage function that will be used by player scripts***********
    public void takedamage(int damage)
    {
        currentHp = currentHp - damage;

        //anim.SetTrigger("hit");


        Debug.Log("Enemy HP : " + damage);
        if (currentHp <= 0)
        {
            animations_update();
        }

    }

    public void Hurt()
    {
        anim.SetBool("hurt", true);
        Invoke("ResetHurt", 0.1f);
    }

    void ResetHurt()
    {
        anim.SetBool("hurt", false);
        hurt = false;
    }

    //Activates on keyframe
    private void Death()
    {

        //enable the head, turn off physics, and turn off collisions
        head.SetActive(true);
        Enemy.bodyType = RigidbodyType2D.Static;
        coll.isTrigger = true;

    }

    //Activates on keyframe
    private void activeParticles()
    {
        deadParticle.SetActive(true);
    }


    private void animations_update()
    {
        anim.SetTrigger("Skeleton_death");

    }

    public void Attack()
    {
        if (attackCools < 0)
        {
            anim.SetInteger("state", 2);
            Invoke("ResetAttack", 0.1f);
            attackCools = timeBetweenAttacks;
        }
        else
            currentState = "WALK";
    }

    public void Walk()
    {

    }

    void ResetAttack()
    {
        anim.SetBool("attack", false);
    }

}
