using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2TargetMovement : MonoBehaviour
{
    public float moveSpeed;
    public float leftPos;
    public float rightPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(leftPos, transform.position.y);
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Movement()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        gameObject.LeanMoveLocalX(rightPos,moveSpeed);       
        yield return new WaitForSeconds(moveSpeed);
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        gameObject.LeanMoveLocalX(leftPos,moveSpeed);
        yield return new WaitForSeconds(moveSpeed);
        StartCoroutine(Movement());
    }
}
