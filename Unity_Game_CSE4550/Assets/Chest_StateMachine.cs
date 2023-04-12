using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_StateMachine : enemy_actions
{
    [HideInInspector] public LayerMask ground; 
    [HideInInspector] public BoxCollider2D coll;
    public float attack_delay;
    public float stun_delay;
    private float stun_time; 
    int hp;
    int current_hp;

    private delegate void state();
    public enum actions { idle, follow, jump, attack, hurt, stun ,death }
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
        stateDictionary.Add(actions.follow,follow_state);
        stateDictionary.Add(actions.jump, jump_state);
        stateDictionary.Add(actions.attack, attack_state);
        stateDictionary.Add(actions.stun , stun_state);
        stateDictionary.Add(actions.hurt, hurt_state);
        stateDictionary.Add(actions.death, death_state);

        ground = LayerMask.GetMask("ground");
        coll = GetComponent<BoxCollider2D>();

        actionscheckHP();
    }
    protected virtual void Update()
    {
        actionscheckHP();
        
        stateDictionary[current_actions].Invoke();

        if (current_actions != actions.hurt && current_actions != actions.stun)
        {
            stun_time = Time.time + stun_delay;
        }
    }

    ///////////////////////////////////////////////////
    ////////// States
    ///////////////////////////////////////////////////

    protected virtual void idle_state()
    {
        on_idle();
        if (trigger_attack())
        {
            attack_delay = Time.time + 1f/2;
            current_actions = actions.attack;
        }

    }
    protected virtual void follow_state()
    {
        on_follow();
        if (trigger_attack())
        {
            attack_delay = Time.time + 1f/2;
            current_actions = actions.attack;
        }
    }
    protected virtual void jump_state()
    {
        if (is_ground())
        {
            on_jump();
            current_actions = actions.stun; 
        }
    }
    protected virtual void attack_state()
    {
        on_attack();
        if (attack_delay <= Time.time)
        {
            damage_player();
            current_actions = actions.stun;
        }
    }
    protected virtual void hurt_state() //already handled by enemy_actions
    {
        on_hurt();
        if (stun_time <= Time.time)
        {
            stun_time = Time.time + stun_delay;
            current_actions = actions.stun;
        }
    }
    protected virtual void stun_state()
    {
        if (stun_time <= Time.time)
        {
            on_stun();
            if (trigger_attack())
            {
                attack_delay = Time.time + 1f / 2;
                current_actions = actions.attack;
            }
            else
            {
                current_actions = actions.follow;
            }
        }

    }
    protected virtual void death_state()
    {
        on_death();
    }

    ///////////////////////////////////////////////////
    ////////// On States to override 
    ///////////////////////////////////////////////////

    protected virtual void on_idle()
    {

    }

    protected virtual void on_follow()
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

    protected virtual void on_stun()
    {

    }


    protected virtual void on_death()
    {

    }

    ///////////////////////////////////////////////////
    ////////// Misc
    ///////////////////////////////////////////////////

    //Convert from strings to actions
    private void actionscheckHP()
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

    private bool is_ground()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}