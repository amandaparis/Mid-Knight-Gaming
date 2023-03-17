using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Kevins_StateMachine
{
    private Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    ///////////////////////////////////////////////////
    ////////// Function - Overrides
    ///////////////////////////////////////////////////

    protected virtual void on_idle()
    {
        anim.SetInteger("state", (int)actions.idle);
    }

    protected override void on_walk()
    {

        if (Enemy.position.x > max_x)
        {
            sprite_filp.flipX = true;
        }
        else if (Enemy.position.x < min_x)
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

    protected override void on_hurt()
    {
        anim.SetInteger("state", (int)actions.hurt);
    }

    protected override void death_state()
    {
        anim.SetInteger("state", (int)actions.death);
    }


}
