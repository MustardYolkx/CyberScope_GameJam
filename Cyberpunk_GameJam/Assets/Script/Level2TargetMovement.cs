using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2TargetMovement : MonoBehaviour
{
    public float moveTime;
    public float leftPos;
    public float rightPos;

    public float localScaleX;
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(leftPos, transform.position.y,transform.position.z);
        localScaleX= transform.localScale.x;
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMovement()
    {
        
        isAlive= false;
        gameObject.LeanPause();

    }
    IEnumerator Movement()
    {
        while (isAlive)
        {
            if(isAlive)
            {
                transform.localScale = new Vector2(-localScaleX, transform.localScale.y);
                gameObject.LeanMoveLocalX(rightPos, moveTime);
            }          
            yield return new WaitForSeconds(moveTime);
            if (isAlive)
            {
                transform.localScale = new Vector2(localScaleX, transform.localScale.y);
                gameObject.LeanMoveLocalX(leftPos, moveTime);
            }
            yield return new WaitForSeconds(moveTime);
            if (isAlive)
            {
                StartCoroutine(Movement());
            }
        }
       
    }
}
