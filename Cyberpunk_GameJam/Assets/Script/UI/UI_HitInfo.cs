using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HitInfo : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    public int scoreNumber;

    public Vector3 pos;

    public bool isHuman;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        transform.position = pos;
        PopOutEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHuman)
        {
            scoreText.text = scoreNumber.ToString();
        }
        
        
        //transform.position = pos;
    }

    public void InputCombo(int score)
    {
        scoreNumber = score;
    }

    public void GetMousePos(Vector2 mousePos)
    {
        pos = mousePos;
    }

    public void PopOutEffect()
    {
        gameObject.LeanScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
        StartCoroutine(DisappearDelay());
    }

    IEnumerator DisappearDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        Disappear();
    }
    public void Disappear()
    {
        gameObject.LeanScale(new Vector3(0, 0, 0), 0.2f);
        Destroy(gameObject, 0.2f);
    }
}
