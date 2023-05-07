using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kevins_StateMachine : enemy_actions
{
    [HideInInspector] public LayerMask ground;
    [HideInInspector] public BoxCollider2D coll;

    public float attack_delay; //Delay time controls frequency of attacks
    public float stun_time;//Stun time that delays transitions when in idle, when hurt, after attacks

    [HideInInspector] public bool stun_done = false;
    [HideInInspector] public bool attack_done = false;
    [HideInInspector] public bool has_attacked = false;

    int hp;
    int current_hp;

    private delegate void state();
    public enum actions { idle, walk, jump, attack, hurt, stun, death }
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
        stateDictionary.Add(actions.stun, stun_state);
        stateDictionary.Add(actions.hurt, hurt_state);
        stateDictionary.Add(actions.death, death_state);

        ground = LayerMask.GetMask("ground");
        coll = GetComponent<BoxCollider2D>();
    }
    protected virtual void Update()
    {
        actionscheckHP();

        stateDictionary[current_actions].Invoke();

    }

    ///////////////////////////////////////////////////
    ////////// States
    ///////////////////////////////////////////////////

    protected virtual void idle_state()
    {
        on_idle();
        if (trigger_attack())
        {

            current_actions = actions.attack;
        }
        else
        {
            current_actions = actions.walk;
        }
    }
    protected virtual void walk_state()
    {
        stun_done = false;
        on_walk();
        if (trigger_attack())
        {
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
        if (!attack_done && !has_attacked)
        {
            on_attack();
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
    protected virtual void stun_state()
    {
        on_stun();
        StartCoroutine(stun_timer());
        if (stun_done)
        {
            if (trigger_attack())
            {
                stun_done = false;
                current_actions = actions.attack;
            }
            else
            {
                stun_done = false;
                current_actions = actions.walk;
            }
        }

    }
    protected virtual void hurt_state() //already handled by enemy_actions
    {
        on_hurt();

        stun_done = false;
        // attack_done = false;
        current_actions = actions.stun;
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

    private IEnumerator stun_timer()
    {
        yield return new WaitForSeconds(stun_time);
        stun_done = true;
    }

    public IEnumerator attack_timer()
    {
        yield return new WaitForSeconds(attack_delay);
        attack_done = true;
    }
}