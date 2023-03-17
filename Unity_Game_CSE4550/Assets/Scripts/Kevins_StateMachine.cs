using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kevins_StateMachine : enemy_actions
{
    [HideInInspector] public BoxCollider2D coll;
    
    public float attack_delay;
    public float stun_delay;
    private float stun_time = 0; 
    int hp;
    int current_hp;

    private delegate void state();
    public enum actions { idle, walk, jump, attack, hurt, death }
    public actions current_actions;
    private Dictionary<actions, state> stateDictionary = new Dictionary<actions, state>()
    {
    };

    // *Start and updates might cause problem
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
        actionscheckHP();
        
        if(current_actions != actions.hurt && current_actions != actions.idle )
        {
            stun_time = Time.time + stun_delay; 
        }

        stateDictionary[current_actions].Invoke();
    }

    ///////////////////////////////////////////////////
    ////////// States
    ///////////////////////////////////////////////////

    protected virtual void idle_state()
    {
        on_idle();
        if (stun_time <= Time.time)
        {
            if (trigger_attack())
            {
                attack_delay = Time.time + 1f / 2;
                current_actions = actions.attack;
            }
            else
            {
                current_actions = actions.walk;
            }
        }

    }
    protected virtual void walk_state()
    {
        on_walk();
        Debug.Log("walk state");
        if (trigger_attack())
        {
            Debug.Log("trigger attack");
            attack_delay = Time.time + 1f / 2;
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

        if (attack_delay <= Time.time)
        {
            damage_player();
            current_actions = actions.idle;

        }
    }
    protected virtual void hurt_state()
    {
        Debug.Log("hurt state");
        on_hurt();

        if (stun_time <= Time.time)
        {
            stun_time = Time.time + stun_delay;
            current_actions = actions.idle;
        }
    }
    protected virtual void death_state()
    {
        Debug.Log("death state");
        on_death();

        coll.isTrigger = true;
        Enemy.bodyType = RigidbodyType2D.Static;
        coll.enabled = false;  
    }


    ///////////////////////////////////////////////////
    ////////// On States to override 
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

    ///////////////////////////////////////////////////
    ////////// Misc
    ///////////////////////////////////////////////////

    //Convert from strings to actions
    public void actionscheckHP()
    {
        current_hp = hp;
        hp = GetComponent<enemy_class>().CurrentHp();

        switch (checkHP(""))
        {
            case "DEATH":
                current_actions = actions.death;
                break;
            case "HURT":
                current_actions = actions.hurt;
                break;
            default:
                break;
        }
    }
}