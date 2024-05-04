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

    public List<Level3Target> level3_Targets;
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
            level3_Targets[index].isTarget= true;
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
    IEnumerator Level3()
    {
        yield return new WaitForSeconds(1);
        currentTime = 0;
        
        while (currentTime < waitSecond_Level2)
        {
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
