using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Dialog_Level2Instruction : MonoBehaviour
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
        dialogueLines.Add("FACTORY TESTING PASSED, 573. YOU ARE NOW ASSIGNED YOUR FIRST MISSION.");
        dialogueLines.Add("ALBERT KOHEN, THE HEAD OF KOHEN GROUP.");
        dialogueLines.Add("HE HAS AMASSED A SUBSTANTIAL FORTUNE AND INFLUENCE BY INVESTING IN WARS OVERSEAS, AND HAS GRADUALLY INFILTRATED THE POLITICAL, EDUCATIONAL, AND EVEN MEDICAL SECTORS.");
        dialogueLines.Add("HOWEVER, HE ACTUALLY ACQUIRES A LARGE NUMBER OF NATIVE HUMAN ORGANS THROUGH WARTIME ACTIVITIES AND SELLS THEM ON THE BLACK MARKET FOR HUGE PROFITS. ");
        dialogueLines.Add("YOUR MISSION, 573, IS TO ELIMINATE ALBERT KOHEN. BE ADVISED, IF THE MISSION FAILS, YOU WILL BE SUBJECTED TO A SELF-DESTRUCT PROTOCOL.");


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
        if (currentLine == 1) // �Ե���������ж�
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
