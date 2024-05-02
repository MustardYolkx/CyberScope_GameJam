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
        JustTarget,
    }

    public BodyParts bodyPart;

    public float score;
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
            }
        }
            
        else if (bodyPart == BodyParts.Body )
        {
            if (animator != null)
            {
                animator.SetTrigger("ShootBody");
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
    }

    public void StopMovement()
    {

    }
    
}
