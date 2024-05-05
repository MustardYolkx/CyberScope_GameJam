using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Dialog_Level3 : MonoBehaviour
{

    public TMP_Text currentDialogueText; // ��ǰ�Ի���Text���
    public List<string> dialogueLines = new List<string>(); // �洢���жԻ���
    private int currentLine = 0; // ���ٵ�ǰ��ʾ�ĶԻ���
    private bool isComplete = true; // �Ƿ���ʾ�����Ի�
    private float typingSpeed = 0.05f; // �ַ���ʾ���ٶ�
    //private string currentText = ""; // ��ǰ������ʾ���ı�
    public bool allDialoguesComplete = false;
    public string sceneName;
    public Image dialogueImage; // �Ի��ڼ���ʾ��ͼƬ
    public Sprite specificImage; // �ض��Ի�Ҫ��ʾ��ͼƬ



    void Start()
    {
        dialogueImage.gameObject.SetActive(false);
        // ��ӶԻ�����
        dialogueLines.Add("GOOD WORK, 573. YOU WILL UNDERGO MINOR ADJUSTMENTS BEFORE PROCEEDING TO YOUR NEXT MISSION.");
        dialogueLines.Add("ON MAY 4TH, TWO YEARS AGO, A CYBER-TERRORIST ATTACK RESULTED IN THE CRASH OF THE NEAR-EARTH SATELLITE \"EXCALIBUR.\" THIS ACT OF TERRORISM CAUSED THE DEATHS OF NEARLY 7,000 PEOPLE IN NEW PARIS, AND THE MASTERMIND BEHIND IT REMAINS UNIDENTIFIED.");
        dialogueLines.Add("RECENTLY, WE DETECTED ENCRYPTED COMMUNICATIONS MATCHING THE WAVEFORM USED IN THAT ATTACK, REVEALING THAT TERRORISTS ARE USING THIS SECURE METHOD TO TRADE HUMAN BRAINS AND CONSCIOUSNESS ONLINE.");
        dialogueLines.Add("AFTER RAIDING THEIR BASE, TWO UNIDENTIFIED HACKERS MANAGED TO ESCAPE.");
        dialogueLines.Add("OUR INVESTIGATION HAS DETERMINED THAT AT LEAST ONE CRIMINAL IS HIDING AT THIS LOCATION. ");
        dialogueLines.Add("573, YOU ARE TASKED TO ELIMINATE THIS CRIMINAL AND ANY NEARBY ACCOMPLICES BEFORE THEY CAN DISAPPEAR.");


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

        // ��������ʾǰ����ͼƬ��ʾ�߼�
        if (currentLine == 4) // �Ե���������ж�
        {
            dialogueImage.sprite = specificImage;
            dialogueImage.gameObject.SetActive(true); // ��ʾͼƬ
        }
        

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