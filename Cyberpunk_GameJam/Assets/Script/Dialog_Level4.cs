using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Dialog_Level4 : MonoBehaviour
{

    public TMP_Text currentDialogueText; // ��ǰ�Ի���Text���
    public List<string> dialogueLines = new List<string>(); // �洢���жԻ���
    private int currentLine = 0; // ���ٵ�ǰ��ʾ�ĶԻ���
    private bool isComplete = true; // �Ƿ���ʾ�����Ի�
    private float typingSpeed = 0.05f; // �ַ���ʾ���ٶ�
    //private string currentText = ""; // ��ǰ������ʾ���ı�
    public bool allDialoguesComplete = false;
    public string sceneName;



    void Start()
    {
        // ��ӶԻ�����
        dialogueLines.Add("THERE'S NO TIME FOR CONGRATULATIONS, 573. AN URGENT MISSION AWAITS.");
        dialogueLines.Add("DURING A FIREFIGHT WITH TERRORISTS, ONE OF THE TERRORISTS STAYED BEHIND TO COVER THE RETREAT OF HIS COMRADES. HE HAS TAKEN ONE OF OUR HUMAN OFFICERS HOSTAGE TO BUY TIME.");
        dialogueLines.Add("573, YOU ARE TASKED WITH DISARMING THE TERRORIST WHILE ENSURING THE SAFETY OF THE HOSTAGE.");
        dialogueLines.Add("SINCE THE TERRORIST'S BRAIN MAY CONTAIN ADDITIONAL INTELLIGENCE ABOUT THEIR BASE, DO NOT KILL THE TERRORIST!");
        dialogueLines.Add("HIS ACTIVE BRAIN WILL SERVE AS OUR BEST SOURCE OF INTELLIGENCE. ");
        dialogueLines.Add("PLEASE BE ADVISED, PRIORITIZE THE SAFETY OF THE HUMAN OFFICER BEING HELD HOSTAGE. HUMAN LIFE IS ALWAYS MORE IMPORTANT THAN OURS.");


        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isComplete)
            {
                StopAllCoroutines(); // ֹͣ��ǰ��������ʾ
                currentDialogueText.text = dialogueLines[currentLine]; // ��ʾ�����ı�
                isComplete = true; // ���Ϊ������ʾ
            }
            else if (currentLine < dialogueLines.Count - 1)
            {
                currentLine++;
                StartCoroutine(TypeLine());
            }
            else if (currentLine == dialogueLines.Count - 1) // ����Ƿ������һ�жԻ�
            {
                allDialoguesComplete = true; // ���жԻ������
            }
        }

        if (allDialoguesComplete)
        {
            SceneManager.LoadScene(sceneName);
        }

    }

    IEnumerator TypeLine()
    {
        isComplete = false; // ��ʼ������ʾʱ���Ϊδ������ʾ
        string currentText = dialogueLines[currentLine];
        currentDialogueText.text = ""; // ����ı�׼����ʾ
        

        foreach (char c in currentText)
        {
            currentDialogueText.text += c; // ������ӵ��ı����
            yield return new WaitForSeconds(typingSpeed); // �ȴ��趨��ʱ�����ʾ��һ���ַ�
        }

        isComplete = true; // ȫ����ʾ���
        if (currentLine == dialogueLines.Count - 1) // ͬ�����������Ƿ�Ϊ���һ��
        {
            allDialoguesComplete = true; // ���жԻ������
        }
    }
}