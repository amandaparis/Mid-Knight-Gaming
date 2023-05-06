using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Chest_StateMachine
{
    private Animator anim;
    float random_number;
    private SpriteRenderer spriteRenderer;
    private bool flipped;
    public float speed = 1.5f; // adjust this to control the speed of the enemy
    private Transform player; // reference to the player's transform
    private Vector3 newPosition;
    private Transform enemy_position;

    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        current_actions = actions.idle;

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Find the player game object and get its transform
        player = GameObject.FindWithTag("Player").transform;

        //TODO: Make it so it gets the start position, without needing a gameobject
        enemy_position = transform.parent.Find("Orginal_Position").transform;
        flipped = spriteRenderer.flipX;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    ///////////////////////////////////////////////////
    ////////// On States to override 
    ///////////////////////////////////////////////////

    ///////////////////////////////////////////////////
    ////////// Function - Overrides
    ///////////////////////////////////////////////////
    protected override void on_idle()
    {
        switch (flipped)
        {
            case true:
                attTrans.transform.localPosition = new Vector3(transX, transY, 0);
                spriteRenderer.flipX = true;
                break;
            case false:
                attTrans.transform.localPosition = new Vector3(-transX, transY, 0);
                spriteRenderer.flipX = false;
                break;
        }
        anim.SetInteger("state", (int)actions.idle);
    }

    protected override void on_follow()
    {
        anim.SetInteger("state", (int)actions.follow);
        flip_sprite(transform, player);
        FollowObject(player);
    }

    protected override void on_flee()
    {
        FollowObject(enemy_position);
        flip_sprite(transform, enemy_position);
        //TODO: Refactor this, to make it less
        if ((Mathf.Abs(transform.position.x - enemy_position.position.x) < 0.1f))// Check if reached the position, Include floating point errors
        {
            Debug.Log("GOING TO IDLE");
            current_actions = actions.idle;
        }
    }


    protected override void on_attack()
    {
        //damage_player(); Activated on attack animation
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
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 2.5f);
        }
    }

    void flip_sprite(Transform object1, Transform object2)
    {
        switch (flipped)
        {
            case true:
                if (object1.position.x >= object2.position.x)
                {
                    attTrans.transform.localPosition = new Vector3(-transX, transY, 0);
                    spriteRenderer.flipX = !flipped;
                }
                else if (object1.position.x <= object2.position.x)
                {
                    attTrans.transform.localPosition = new Vector3(transX, transY, 0);
                    spriteRenderer.flipX = flipped;
                }
            break;

            case false:
                if (object1.position.x >= object2.position.x)
                {
                    attTrans.transform.localPosition = new Vector3(transX, transY, 0);
                    spriteRenderer.flipX = flipped;
                }
                else if (object1.position.x <= object2.position.x)
                {
                    attTrans.transform.localPosition = new Vector3(-transX, transY, 0);
                    spriteRenderer.flipX = !flipped;
                }
            break;
            default:
        }

    }

}

