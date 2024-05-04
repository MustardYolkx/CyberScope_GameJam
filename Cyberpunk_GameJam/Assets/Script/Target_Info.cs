using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Info : MonoBehaviour
{ 
    public Animator animator;
    private GameManager gameManager;
    public enum BodyParts
    {
        Head,
        Body,
        Arm,
        Leg,
        MetalArm,
        Gun,
        JustTarget,
        Block,
    }

    public BodyParts bodyPart;

    public enum Level
    {
        Level1,
        Level2,
        Level3,
        Level4,
    }

    public Level currentLevel;


    public int score;
    private int count;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<Animator>())
        {
            animator = GetComponentInParent<Animator>();
        }
        
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
            if(animator!=null)
            {
                animator.SetTrigger("ShootHead");
                if (currentLevel == Level.Level4)
                {
                    gameObject.GetComponentInParent<Level4Target>().isAlive = false;
                }
            }
        }
            
        else if (bodyPart == BodyParts.Body )
        {
            if (animator != null)
            {
                animator.SetTrigger("ShootBody");
                if (currentLevel == Level.Level4)
                {
                    gameObject.GetComponentInParent<Level4Target>().isAlive = false;
                }
            }
        }
        else if (bodyPart == BodyParts.Leg)
        {
            if (animator != null)
            {
                animator.SetTrigger("ShootLeg");
            }
        }
        else if(bodyPart == BodyParts.JustTarget)
        {
            if(count==0)
            {
                gameManager.thisLevelScore += score;
                count++;
            }
            
        }
        else if (bodyPart == BodyParts.MetalArm)
        {
            if (animator != null)
            {
                animator.SetTrigger("ShootMetalArm");
            }
        }
        else if (bodyPart == BodyParts.Gun)
        {
            if (animator != null)
            {
                animator.SetTrigger("ShootGun");
            }
        }
        else if (bodyPart == BodyParts.Arm)
        {
            if (animator != null)
            {
                animator.SetTrigger("ShootArm");
                if(currentLevel == Level.Level4)
                {
                    gameObject.GetComponentInParent<Level4Target>().isAlive = false;
                }
            }
        }
    }

    public void StopMovement()
    {

    }
    
}
