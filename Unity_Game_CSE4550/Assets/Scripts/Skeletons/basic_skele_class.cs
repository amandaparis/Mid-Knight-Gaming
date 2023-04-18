using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_skele_class : MonoBehaviour
{
    public Rigidbody2D Enemy;
    int enemyHp; 
    int CurrentHp; 
    private BoxCollider2D coll;
    public SpriteRenderer sprite_filp;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        CurrentHp = GetComponent<enemy_class>().CurrentHp(); 
        Enemy = GetComponent<Rigidbody2D>();
        sprite_filp = GetComponent<SpriteRenderer>() ;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHp = GetComponent<enemy_class>().CurrentHp(); 

        if(enemyHp <= 0)
        {
            coll.isTrigger = true; 
            //Enemy.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;  
        }
        else if(enemyHp != CurrentHp)
        {
            CurrentHp = enemyHp;  
        }  
    }

    public LayerMask playerlayers; 


    public Transform viewTrans; 
    public float v_range_y ;  //1 
    public float v_range_x ;  //1


//*///////////////////////////test the saiz of the hit box //////////////////////////////////
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(viewTrans.position, new Vector3(v_range_x, v_range_y,1));
    
    }


    public bool skel_awake()
    {
            Debug.Log("skeleton will awaken ");
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(viewTrans.position, new Vector2(v_range_x, v_range_y),0, playerlayers );         
        
        if(hitplayer.Length > 0)
            return true; 
        else
            return false; 
    }

    public Transform attTrans; 
    public float att_range_y ;  //1 
    public float att_rangex_ ;  //1


    public bool  trigger_attack() 
    {      
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, playerlayers );         
            if(hitplayer.Length > 0)
            {
                return true; 
            }
            else
                return false;
    }

    public int enemy_daamge = 1 ;

    public Rigidbody2D player_RB;

    public void damage_player()
    {
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, playerlayers );         

           if(hitplayer.Length > 0)//for(int i = 0; i < hitplayer.Length; i++)
            {
                hitplayer[0].GetComponent<Player_Heath>().player_takeDamage(enemy_daamge);
               // player_RB.AddForce(transform.right * pushback_force, ForceMode2D.Impulse);
            }
    }

}