using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Kevins_StateMachine
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        current_actions = actions.walk;
    }

    // Update is called once per frame
    void Update()
    {

        base.Update();
    }

    ///////////////////////////////////////////////////
    ////////// Function - Overrides
    ///////////////////////////////////////////////////
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

        walk();
    }
}
