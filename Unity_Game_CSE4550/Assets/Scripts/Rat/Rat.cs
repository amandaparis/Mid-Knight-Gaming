using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Kevins_StateMachine
{
    private Animator anim;
    float random_number;
    private bool hasPlayedDeathSound = false;
    [SerializeField] private AudioSource deathSoundEffect;

    // Start is called before the first frame update
    void Start()
    {

        base.Start();
        current_actions = actions.walk;
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        // attack_delay = 2;

        // if (current_actions == actions.hurt)
        // {
        //     deathSoundEffect.Play();
        // }
    }

    ///////////////////////////////////////////////////
    ////////// State - Overrides
    ///////////////////////////////////////////////////

    //Just to make it jump when it attacks
    protected override void attack_state()
    {
        if (!attack_done && !has_attacked)
        {
            random_number = Random.Range(1, 3);
            if (random_number == 2)
            {
                current_actions = actions.jump;
            }
            else
            {
                on_attack();
            }
            has_attacked = true;
            StartCoroutine(attack_timer());
        }
        else if (attack_done)
        {
            has_attacked = false;
            attack_done = false;
            current_actions = actions.stun;
        }

    }
    /////////////////////////////////////////////////
    ////////// Function - Overrides
    ///////////////////////////////////////////////////

    protected override void on_idle()
    {
        anim.SetInteger("state", (int)actions.idle);
    }

    protected override void on_walk()
    {

        anim.SetInteger("state", (int)actions.walk);
        if (Enemy.position.x >= max_x)
        {
            sprite_filp.flipX = true;
        }
        else if (Enemy.position.x <= min_x)
        {
            sprite_filp.flipX = false;
        }

        walk();
    }

    protected override void on_attack()
    {
        anim.SetInteger("state", (int)actions.attack);
    }

    protected override void on_jump()
    {
        //Check if sprite is flipped to see which direction should the rat charge
        switch (sprite_filp.flipX)
        {
            case true:
                Enemy.velocity = new Vector2(Enemy.velocity.x - 1.5f, 3f);
                break;
            case false:
                Enemy.velocity = new Vector2(Enemy.velocity.x + 1.5f, 3f);
                break;
            default:
                Enemy.velocity = new Vector2(Enemy.velocity.x, Enemy.velocity.y);
                break;
        }
    }
    protected override void on_stun()
    {
        anim.SetInteger("state", (int)actions.idle);
    }
    protected override void on_hurt()
    {
        //deathSoundEffect.Play();
    }

    protected override void on_death()
    {

        if (!hasPlayedDeathSound)
        {
            deathSoundEffect.Play();
            hasPlayedDeathSound = true;
        }

        //Create an infinitely long ray where the player is and check if it hits ground layermask
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, ground);

        //If the ray the ray hits
        if ((hit.collider != null))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.20f, transform.position.z);

            Enemy.bodyType = RigidbodyType2D.Static;
            coll.isTrigger = true;
            coll.enabled = false;
        }

    }


}
