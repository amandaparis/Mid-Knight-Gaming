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




}
