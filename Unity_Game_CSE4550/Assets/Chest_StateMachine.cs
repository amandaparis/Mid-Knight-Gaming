using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_StateMachine : enemy_actions
{
    [HideInInspector] public LayerMask ground;
    [HideInInspector] public BoxCollider2D coll;
    [HideInInspector] public Area_Detector areaDetector;
    private Animator anim;
    public float attack_delay; //Delay time controls frequency of attacks
    public float stun_time;//Stun time that delays transitions when in idle, when hurt, after attacks

    private bool stun_done = false;
    private bool attack_done = false;
    private bool has_attacked = false;

    int hp;
    int current_hp;

    private delegate void state();
    public enum actions { idle, follow, flee, jump, attack, hurt, stun, death }
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
        stateDictionary.Add(actions.follow, follow_state);
        stateDictionary.Add(actions.flee, flee_state);
        stateDictionary.Add(actions.jump, jump_state);
        stateDictionary.Add(actions.attack, attack_state);
        stateDictionary.Add(actions.stun, stun_state);
        stateDictionary.Add(actions.hurt, hurt_state);
        stateDictionary.Add(actions.death, death_state);

        ground = LayerMask.GetMask("ground");
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        //TODO: Refactor this to make it shorter
        Transform colliderActivatorTransform = transform.parent.Find("Collider_Activator");
        GameObject colliderActivator = colliderActivatorTransform.gameObject;
        areaDetector = colliderActivator.GetComponentInParent<Area_Detector>();

        actionscheckHP();
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
        gameObject.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("ground");

        if (trigger_attack())
        {
            gameObject.tag = "Enemy";
            gameObject.layer = LayerMask.NameToLayer("enemy");

            current_actions = actions.attack;
        }
    }
    protected virtual void follow_state()
    {
        stun_done = false;
        on_follow();
        if (trigger_attack())
        {
            current_actions = actions.attack;
        }
        else if (areaDetector.has_left) //TODO: Implement Ivan's method
        {
            current_actions = actions.flee;
        }
    }

    protected virtual void flee_state()
    {
        on_flee();

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
    protected virtual void hurt_state() //already handled by enemy_actions
    {
        on_hurt();

        stun_done = false;
        // attack_done = false;
        current_actions = actions.stun;
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
                // attack_done = false;
                current_actions = actions.attack;
            }
            else
            {
                stun_done = false;
                // attack_done = false;
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

    protected virtual void on_flee()
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

    private IEnumerator attack_timer()
    {
        yield return new WaitForSeconds(attack_delay);
        attack_done = true;
    }
}