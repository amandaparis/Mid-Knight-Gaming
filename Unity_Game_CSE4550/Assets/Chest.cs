using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Kevins_StateMachine
{
    private Animator anim;
    float random_number;
    private SpriteRenderer spriteRenderer;
    public float speed; // adjust this to control the speed of the enemy
    private Transform player; // reference to the player's transform
    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        current_actions = actions.walk;

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Find the player game object and get its transform
        player = GameObject.FindWithTag("Player").transform;


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
    protected override void on_walk()
    {
        if (transform.position.x >= player.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x <= player.position.x)
        {
            spriteRenderer.flipX = false;
        }
        anim.SetInteger("state", (int)actions.walk);
        FollowPlayer();
    }


    /////////////////////////////////////////////////
    //////// Misc
    /////////////////////////////////////////////////
    public void FollowPlayer()
    {
        //Calculate the difference, to get the distance
        Vector3 distance = new Vector3((player.position.x - transform.position.x), 0f, 0f);

        //Used to conserve the direction
        distance.Normalize();

        newPosition = transform.position + distance * speed * Time.deltaTime;

        /* 
        Used to check if the distance falls within the value so, the enemy
        constantly does not overshoot due to floating precision errors 
        (There really small but they add up)
        */
        if (Mathf.Abs(player.position.x - transform.position.x) > 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 2.5f);
        }

    }

