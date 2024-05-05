using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogLevel2Lose : MonoBehaviour
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
        dialogueLines.Add("MISSION FAILED, 573. KOHEN HAS INITIATED AN INVESTIGATION INTO HIS OWN ASSASSINATION ATTEMPT.");
        dialogueLines.Add("TO ENSURE THE ORGANIZATION'S INFORMATION REMAINS SECURE, WE WILL ACTIVATE YOUR SELF-DESTRUCT PROTOCOL.");
        dialogueLines.Add("THANK YOU FOR YOUR SERVICE, 573.");
        dialogueLines.Add("Self-destruct program initiated: Countdown...3...2...1");


        dialogueLines.Add("G A M E  O V E R");


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
