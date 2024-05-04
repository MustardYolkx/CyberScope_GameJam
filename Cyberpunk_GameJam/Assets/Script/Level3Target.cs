using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Target : MonoBehaviour
{
    public GameManager manager;
    public bool isAlive;
    public bool isTarget;

    public bool isTarget1;
    // Start is called before the first frame update
    void Start()
    {
        manager= FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTarget2()
    {
        if (isTarget1)
        {
            manager.TriggerTarget2_Level3();
        }
    }
}
