using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Kevins_StateMachine
{
    private Animator anim;
    float random_number;
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
        attack_delay = 2;
    }

    ///////////////////////////////////////////////////
    ////////// State - Overrides
    ///////////////////////////////////////////////////

    //Just to make it jump when it attacks
    protected override void attack_state()
    {

        if (attack_delay <= Time.time)
        {
            random_number = Random.Range(1, 3);
            if (random_number == 2)
            {
                current_actions = actions.jump;
            }
            else
            {   //Damage player function is activated by itself as a keyframe on "Death" animation
                on_attack();
                current_actions = actions.idle;
            }
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

        if (Enemy.position.x >= max_x)
        {
            sprite_filp.flipX = true;
        }
        else if (Enemy.position.x <= min_x)
        {
            sprite_filp.flipX = false;
        }

        anim.SetInteger("state", (int)actions.walk);
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
                Enemy.velocity = new Vector2(Enemy.velocity.x,Enemy.velocity.y);
             break;
        }
    }
    protected override void on_death()
    {;
        Enemy.bodyType = RigidbodyType2D.Static;
        coll.isTrigger = true;
        coll.enabled = false;
    }


}
