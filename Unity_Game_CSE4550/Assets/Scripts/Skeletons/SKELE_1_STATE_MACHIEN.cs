using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKELE_1_STATE_MACHIEN : basic_skele_class
{
    public Animator anim;

    string CurrentState ;

    public bool awaking = true;   

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
