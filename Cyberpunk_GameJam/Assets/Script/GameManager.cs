using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float waitSecond;

    public TargetMove target1;
    public TargetMove target2;
    public TargetMove target3;

    public float currentTime;
    public float targetChangeWaitingTime;



    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        StartCoroutine(FirstTarget());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FirstTarget()
    {
        currentTime = 0;
        target1.MoveDown();
        yield return new WaitForSeconds(target1.movingTime);
        while (currentTime < waitSecond)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitForSeconds(targetChangeWaitingTime);
                target1.MoveUp();
                //time = waitSecond;
                
                break;
            }
            yield return null;
            currentTime += Time.deltaTime;

            //Debug.Log(currentTime);
        }
        target1.MoveUp();
        yield return new WaitForSeconds(target2.movingTime);
        currentTime = 0;
        target2.MoveDown();
        yield return new WaitForSeconds(target1.movingTime);

        while (currentTime < waitSecond)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitForSeconds(targetChangeWaitingTime);
                target2.MoveUp();
                //time = waitSecond;

                break;
            }
            yield return null;
            currentTime += Time.deltaTime;

            //Debug.Log(currentTime);
        }
        target2.MoveUp();
        yield return new WaitForSeconds(target2.movingTime);
        currentTime = 0;
        target3.MoveDown();
        yield return new WaitForSeconds(target1.movingTime);

        while (currentTime < waitSecond)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitForSeconds(targetChangeWaitingTime);
                target3.MoveUp();
                //time = waitSecond;

                break;
            }
            yield return null;
            currentTime += Time.deltaTime;
            //Debug.Log(currentTime);
        }
        target3.MoveUp();
    }

}
