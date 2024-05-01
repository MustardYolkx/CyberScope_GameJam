using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Info : MonoBehaviour
{
    private GameManager gameManager;
    public enum BodyParts
    {
        Head,
        Body,
        Arm,
        Leg,
        JustTarget,
    }

    public BodyParts bodyPart;

    public float score;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if(bodyPart == BodyParts.Head)
        {

        }
        else if (bodyPart == BodyParts.Body )
        {

        }
        else if(bodyPart == BodyParts.JustTarget)
        {
            gameManager.thisLevelScore += score;
        }
    }

    
}
