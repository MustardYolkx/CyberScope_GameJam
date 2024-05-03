using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2TargetMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject leftPos;
    public GameObject rightPos;
    public GameObject movableObj;
    public float localScaleX;
    public float time;
    public bool isAlive;

    public bool turnRight = false;
    // Start is called before the first frame update
    void Start()
    {
        movableObj. transform.position = leftPos.transform.position;
        localScaleX = movableObj.transform.localScale.x;
        //StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    public void StopMovement()
    {
        
        isAlive= false;
        //gameObject.LeanPause();

    }

    public void Movement()
    {
        if (isAlive)
        {
            if (Vector2.Distance(movableObj.transform.position, leftPos.transform.position) < 0.1f)
            {               
                movableObj.transform.localScale= new Vector2(-localScaleX, movableObj.transform.localScale.y);
                turnRight = true;
            }
            else if(Vector2.Distance(movableObj.transform.position, rightPos.transform.position) < 0.1f)
            {                
                movableObj.transform.localScale = new Vector2(localScaleX, movableObj.transform.localScale.y);
                turnRight = false;
            }
            
            if (turnRight)
            {
                movableObj.transform.position = Vector2.MoveTowards(movableObj.transform.position, rightPos.transform.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                movableObj.transform.position = Vector2.MoveTowards(movableObj.transform.position, leftPos.transform.position, Time.deltaTime * moveSpeed);
             
            }
        }
        
        
    }
    //IEnumerator Movement()
    //{
    //    while (isAlive)
    //    {
    //        if(isAlive)
    //        {
    //            transform.localScale = new Vector2(-localScaleX, transform.localScale.y);
    //            gameObject.LeanMoveLocalX(rightPos, moveTime);
    //        }          
    //        yield return new WaitForSeconds(moveTime);
    //        if (isAlive)
    //        {
    //            transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    //            gameObject.LeanMoveLocalX(leftPos, moveTime);
    //        }
    //        yield return new WaitForSeconds(moveTime);
    //        if (isAlive)
    //        {
    //            StartCoroutine(Movement());
    //        }
    //    }
       
    //}
}
