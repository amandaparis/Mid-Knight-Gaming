using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Chest_StateMachine
{
    private Animator anim;
    float random_number;
    private SpriteRenderer spriteRenderer;
    public float speed = 1.5f; // adjust this to control the speed of the enemy
    private Transform player; // reference to the player's transform
    private Vector3 newPosition;
    private Transform newPosition_enemy; //TODO: Change name to something different because it might get confusing

    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        current_actions = actions.idle;

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Find the player game object and get its transform
        player = GameObject.FindWithTag("Player").transform;
        newPosition_enemy = GameObject.Find("Orginal_Position").transform;


    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }
    /*
        1. If the player touches the attack box, while chest is idle it will go attack
            Idle -> Attack
        2. It will follow the player while attacking
            Attack -> walk
        3. If the player exits the collider, it will return back to its original spot
            walk and player exits -> Flee -> Idle
    */

    ///////////////////////////////////////////////////
    ////////// On States to override 
    ///////////////////////////////////////////////////

    ///////////////////////////////////////////////////
    ////////// Function - Overrides
    ///////////////////////////////////////////////////
    protected override void on_idle()
    {
        anim.SetInteger("state", (int)actions.idle);
    }

    protected override void on_follow()
    {
        //TODO: Allow for sprites to be flipped, any direction. Currently one direction
        if (transform.position.x >= player.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x <= player.position.x)
        {
            spriteRenderer.flipX = true;
        }
        anim.SetInteger("state", (int)actions.follow);
        FollowObject(player);
    }

    protected override void on_flee()
    {
        //TODO: Allow for sprites to be flipped, any direction. Currently one direction
        if (transform.position.x >= newPosition_enemy.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x <= newPosition_enemy.position.x)
        {
            spriteRenderer.flipX = true;
        }

        FollowObject(newPosition_enemy);

        //TODO: Refactor this, to make it less
        if( (Mathf.Abs(transform.position.x - newPosition_enemy.position.x) < 0.1f) )// Include floating point errors
        {
            spriteRenderer.flipX = true;
            current_actions = actions.idle;
        }
    }


    protected override void on_attack()
    {
        anim.SetInteger("state", (int)actions.attack);
    }

    protected override void on_death()
    {
        Enemy.bodyType = RigidbodyType2D.Static;
        coll.isTrigger = true;
        coll.enabled = false;
    }

    protected override void on_stun()
    {
        anim.SetInteger("state", (int)actions.idle);
    }


    /////////////////////////////////////////////////
    //////// Misc
    /////////////////////////////////////////////////
    public void FollowObject(Transform Object)
    {
        //Calculate the difference, to get the distance
        Vector3 distance = new Vector3((Object.position.x - transform.position.x), 0f, 0f);

        //Used to conserve the direction
        distance.Normalize();

        newPosition = transform.position + distance * speed * Time.deltaTime;

        /* 
        Used to check if the distance falls within the value so, the enemy
        constantly does not overshoot due to floating precision errors 
        (There really small but they add up)
        */
        if (Mathf.Abs(Object.position.x - transform.position.x) > 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 2.5f);
    }

}

