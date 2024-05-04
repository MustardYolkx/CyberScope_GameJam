using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dialog_Level1 : MonoBehaviour
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
        dialogueLines.Add("YOU CAN USE THE MOUSE TO AIM, LEFT-CLICK TO SHOOT, AND USE THE SCROLL WHEEL TO ADJUST THE SCOPE, JUST AS YOU HAVE ALWAYS DONE, 573");
        dialogueLines.Add("COOLANT CHECKED, RIFLE VOLTAGE CHECKED, SYSTEMS ALL GREEN");
        dialogueLines.Add("CODE 573 AGENT CYBORG READY FOR FACTORY TESTING");
        dialogueLines.Add("INITIATING COUNTDOWN FOR TESTING...3...2...1 ");


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
