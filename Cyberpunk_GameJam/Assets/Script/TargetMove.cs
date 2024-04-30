using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    private Transform trans;
    public float distance;

    public Vector3 originPos;
    public float movingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        originPos= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveDown()
    {
        gameObject.LeanMoveLocalY(distance, movingTime).setEaseInOutBack();
    }

    public void MoveUp()
    {
        gameObject.LeanMoveLocalY(originPos.y, movingTime).setEaseInOutBack();
    }
}
