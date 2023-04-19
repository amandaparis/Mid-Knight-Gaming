using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Area_Detector : MonoBehaviour
{
    public bool has_left;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            has_left = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            has_left = false;
        }
    }

}
