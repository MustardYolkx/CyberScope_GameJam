using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi_Movement : MonoBehaviour
{

    public GameObject startPos;
    public GameObject endPos;

    public GameObject movableObj;
    public float speed;
    public float time;

    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        movableObj.transform.position = startPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        time += Time.deltaTime;
    }

    public void Movement()
    {
        if (Vector2.Distance(movableObj.transform.position, endPos.transform.position) < 0.1f)
        {
            time = 0;
            movableObj.transform.position = startPos.transform.position;
        }
        else
        {
            movableObj.transform.position= Vector2.MoveTowards(movableObj.transform.position, endPos.transform.position, time * speed);
        }

    }
}
