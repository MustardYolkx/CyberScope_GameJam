using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3TargetMovement : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public GameObject leftPos;
    public GameObject rightPos;
    public GameObject targetPos;
    public GameObject movableObj;
    public float localScaleX;
    public float time;

    public float stayTime;

    public bool isAlive;

    public bool turnRight = false;
    public bool isTarget1;
    public bool isTarget2;

    public string sceneNameNextLevel;
    public string sceneNameGameOver;
    public enum State
    {
        Idle,
        TurnRight,
        TurnLeft,
        Run,
    }
    public State currentState;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        movableObj.transform.position = leftPos.transform.position;
        localScaleX = movableObj.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public void StopMovement()
    {

        isAlive = false;
        //gameObject.LeanPause();

    }

       public void Movement()
    {
        if (isAlive)
        {
            if(currentState!=State.Run)
            {
                if (Vector2.Distance(movableObj.transform.position, leftPos.transform.position) < 0.1f)
                {
                    currentState = State.Idle;
                    if (time > stayTime)
                    {
                        currentState = State.TurnRight;
                        movableObj.transform.localScale = new Vector2(-localScaleX, movableObj.transform.localScale.y);
                    }
                    time += Time.deltaTime;
                }
                else if (Vector2.Distance(movableObj.transform.position, rightPos.transform.position) < 0.1f)
                {
                    currentState = State.Idle;
                    if (time > stayTime)
                    {
                        currentState = State.TurnLeft;
                        movableObj.transform.localScale = new Vector2(localScaleX, movableObj.transform.localScale.y);
                    }
                    time += Time.deltaTime;

                }
                else
                {
                    time = 0;
                }
                if (currentState == State.TurnRight)
                {
                    anim.SetBool("isWalk", true);
                    movableObj.transform.position = Vector2.MoveTowards(movableObj.transform.position, rightPos.transform.position, Time.deltaTime * moveSpeed);
                }
                else if (currentState == State.TurnLeft)
                {
                    anim.SetBool("isWalk", true);
                    movableObj.transform.position = Vector2.MoveTowards(movableObj.transform.position, leftPos.transform.position, Time.deltaTime * moveSpeed);

                }
                else if (currentState == State.Idle)
                {
                    anim.SetBool("isWalk", false);
                }
            }
            else if (currentState == State.Run)
            {
                movableObj.transform.localScale = new Vector2(-localScaleX, movableObj.transform.localScale.y);
                anim.SetBool("isRun", true);
                movableObj.transform.position = Vector2.MoveTowards(movableObj.transform.position, targetPos.transform.position, Time.deltaTime * moveSpeed * 2);
                if(Vector2.Distance(movableObj.transform.position, targetPos.transform.position) < 0.1f)
                {
                    SceneManager.LoadScene(sceneNameGameOver);
                }
            }
        }
        else
        {
            if(!isTarget2&&!isTarget1)
            {
                SceneManager.LoadScene(sceneNameGameOver);
            }
        }
           


    }
}
