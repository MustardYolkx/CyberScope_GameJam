using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Dialog_Level2Instruction : MonoBehaviour
{

    public TMP_Text currentDialogueText; // 当前对话的Text组件
    public List<string> dialogueLines = new List<string>(); // 存储所有对话行
    private int currentLine = 0; // 跟踪当前显示的对话行
    private bool isComplete = true; // 是否显示完整对话
    private float typingSpeed = 0.05f; // 字符显示的速度
    //private string currentText = ""; // 当前逐字显示的文本
    public bool allDialoguesComplete = false;
    public string sceneName;
    public Image dialogueImage; // 对话期间显示的图片
    public Sprite specificImage; // 特定对话要显示的图片
    


    void Start()
    {
        dialogueImage.gameObject.SetActive(false);
        // 添加对话内容
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
                StopAllCoroutines(); // 停止当前的逐字显示
                currentDialogueText.text = dialogueLines[currentLine]; // 显示完整文本
                isComplete = true; // 标记为完整显示
            }
            else if (currentLine < dialogueLines.Count - 1)
            {
                currentLine++;
                StartCoroutine(TypeLine());
            }
            else if (currentLine == dialogueLines.Count - 1) // 检查是否是最后一行对话
            {
                allDialoguesComplete = true; // 所有对话已完成
            }
        }

        if (allDialoguesComplete)
        {
            SceneManager.LoadScene(sceneName);
        }

    }

    IEnumerator TypeLine()
    {
        isComplete = false; // 开始逐字显示时标记为未完整显示
        string currentText = dialogueLines[currentLine];
        currentDialogueText.text = ""; // 清空文本准备显示

        // 在逐字显示前设置图片显示逻辑
        if (currentLine == 1) // 对第三句进行判断
        {
            dialogueImage.sprite = specificImage;
            dialogueImage.gameObject.SetActive(true); // 显示图片
        }
        

        foreach (char c in currentText)
        {
            currentDialogueText.text += c; // 逐字添加到文本组件
            yield return new WaitForSeconds(typingSpeed); // 等待设定的时间后显示下一个字符
        }

        isComplete = true; // 全部显示完成
        if (currentLine == dialogueLines.Count - 1) // 同样在这里检查是否为最后一行
        {
            allDialoguesComplete = true; // 所有对话已完成
        }
    }
}
