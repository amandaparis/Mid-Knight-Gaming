using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kevins_StateMachine : enemy_actions
{
    private delegate void state();
    public actions current_actions;
    public enum actions
    { idle, walk, jump, attack, hurt, death }
    private Dictionary<actions, state> stateDictionary = new Dictionary<actions, state>()
    {
    };

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //FSM
        //Contains the actions associated with it's states
        stateDictionary.Add(actions.idle, idle_state);
        stateDictionary.Add(actions.walk, walk_state);
        stateDictionary.Add(actions.jump, jump_state);
        stateDictionary.Add(actions.attack, attack_state);
        stateDictionary.Add(actions.hurt, hurt_state);
        stateDictionary.Add(actions.death, death_state);
    }

    protected virtual void Update()
    {
        // /* check if the current actions has changed from the previous frame, to -
        // prevent duplicates */
        // if (current_actions != previous_actions)
        // {

        //     Debug.Log(current_actions);
        //     previous_actions = current_actions;
        //     stateDictionary[current_actions].Invoke();
        //     //Activate states via the key (current_actions)
            
        // }

        stateDictionary[current_actions].Invoke();

    }


    ///////////////////////////////////////////////////
    ////////// States
    ///////////////////////////////////////////////////

    protected virtual void idle_state()
    {
        on_idle();
        // if (trigger_attack())
        // {
        //     current_actions = actions.attack;
        // }
        // else
        // {
        //     current_actions = actions.walk;
        // }


    }
    protected virtual void walk_state()
    {
        on_walk();
        Debug.Log("walk state");
        
        if (trigger_attack())
        {
             Debug.Log("trigger attack");
            current_actions = actions.attack;
        }
    }
    protected virtual void jump_state()
    {
        Debug.Log("jump state");
        on_jump();
    }
    protected virtual void attack_state()
    {
        Debug.Log("attack state");
        on_attack();
    }
    protected virtual void hurt_state()
    {
        Debug.Log("hurt state");
        on_hurt();
        current_actions = actions.idle;
    }
    protected virtual void death_state()
    {
        Debug.Log("death state");
        on_death();
    }


    ///////////////////////////////////////////////////
    ////////// On States to overide 
    ///////////////////////////////////////////////////

    protected virtual void on_idle()
    {

    }

    protected virtual void on_walk()
    {

    }

    protected virtual void on_jump()
    {

    }
    protected virtual void on_attack()
    {

    }

    protected virtual void on_hurt()
    {

    }

    protected virtual void on_death()
    {

    }


    //     /* 
    //     Constructor: constructs the class with states values
    //         must be at the bottom because you can't initialize the dictionary with
    //         states when first creating it, only after its created
    //     */
    //     public Enemy_class()
    //     {
    //         stateDictionary = new Dictionary<actions, state>()
    //        {
    //            //The states for the FSM
    //            {actions.idle,idle_state},
    //            {actions.walk,walk_state},
    //            {actions.jump,jump_state},
    //            {actions.attack,attack_state},
    //            {actions.hurt,hurt_state},
    //            {actions.death,death_state},
    //        };
    //     }
}