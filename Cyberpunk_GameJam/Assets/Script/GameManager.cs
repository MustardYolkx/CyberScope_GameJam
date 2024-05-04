using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Canvas canvas;
    public float waitSecond_Level1;
    public float waitSecond_Level2;
    public float waitSecond_Level3;
    public string sceneNameNextLevel;
    public string sceneNameGameOver;



    public TargetMove target1;
    public TargetMove target2;
    public TargetMove target3;

    public float currentTime;
    public float targetChangeWaitingTime;

    public float thisLevelScore;
    public enum Level
    {
        Level1,
        Level2, 
        Level3,

    }

    public Level currentLevel;

    //Generate hit info panel
    public GameObject scorePanel;
    public GameObject bodyPartPanel;

    public List<GameObject> level3_Targets;
    public Level3TargetMovement target1_Level3;
    public Level3TargetMovement currentRandomTarget;
    public int level3_TargetCount;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        Cursor.visible = false;
        if(currentLevel== Level.Level1)
        {
            StartCoroutine(FirstTarget());
        }
        if(currentLevel == Level.Level3)
        {
            int index = Random.Range(0,level3_Targets.Count);
            level3_Targets[index].GetComponentInChildren<Level3Target>().isTarget= true;
            level3_Targets[index].GetComponentInChildren<Level3TargetMovement>().isTarget = true;
            currentRandomTarget = level3_Targets[index].GetComponentInChildren<Level3TargetMovement>();
            StartCoroutine(Level3());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
    }

    IEnumerator FirstTarget()
    {
        yield return new WaitForSeconds(1); 
        currentTime = 0;
        target1.MoveDown();
        yield return new WaitForSeconds(target1.movingTime);
        while (currentTime < waitSecond_Level1)
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

        while (currentTime < waitSecond_Level1)
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

        while (currentTime < waitSecond_Level1)
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
        yield return new WaitForSeconds(target1.movingTime);
        if (thisLevelScore > 14)
        {
            SceneManager.LoadScene(sceneNameNextLevel);
        }
        if (thisLevelScore <15)
        {
            SceneManager.LoadScene(sceneNameGameOver);
        }
    }

    IEnumerator Level2()
    {

        yield return null;
    }

    public void TargetCount_Level3()
    {
        level3_TargetCount++;
    }
    public void TriggerTarget2_Level3()
    {
        currentRandomTarget.currentState = Level3TargetMovement.State.Run;
    }
    IEnumerator Level3()
    {
        yield return new WaitForSeconds(1);
        currentTime = 0;
        
        while (currentTime < waitSecond_Level3)
        {
            if(!currentRandomTarget.isAlive&&!target1_Level3.isAlive)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(sceneNameNextLevel);
            }
            //TriggerTarget2_Level3();
            if (level3_TargetCount==2)
            {
                              
                break;
            }
            yield return null; 
            currentTime += Time.deltaTime;

            //Debug.Log(currentTime);
        }
        yield return new WaitForSeconds(1);

        //GameOver
    }
    public void GenerateHitInfoPanel(Vector3 pos,int score,Target_Info.BodyParts bodyPart)
    {
        if(bodyPart == Target_Info.BodyParts.JustTarget)
        {
            GameObject hitInfoPanel = Instantiate(scorePanel, canvas.transform);
            hitInfoPanel.GetComponentInParent<UI_HitInfo>().pos = pos;
            hitInfoPanel.GetComponentInParent<UI_HitInfo>().scoreNumber = score;
        }
        else if(bodyPart == Target_Info.BodyParts.Head)
        {
            GameObject hitInfoPanel = Instantiate(bodyPartPanel, canvas.transform);
            hitInfoPanel.GetComponentInParent<UI_HitInfo>().pos = pos;
        }
    }
}
