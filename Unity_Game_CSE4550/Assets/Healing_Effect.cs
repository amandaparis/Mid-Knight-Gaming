using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_Effect : MonoBehaviour
{
    public float cleanup_time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position);
        gameObject.transform.localPosition = new Vector3(0, -0.05f, 0);
        gameObject.transform.localScale = new Vector3(1,1,1);
        StartCoroutine(cleanup());
    }

    private IEnumerator cleanup()
    {
        yield return new WaitForSeconds(cleanup_time);
        Destroy(gameObject);
    }
}
