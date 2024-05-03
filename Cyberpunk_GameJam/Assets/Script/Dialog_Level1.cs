using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dialog : MonoBehaviour
{

    public TMP_Text currentDialogueText; // 当前对话的Text组件
    public List<string> dialogueLines = new List<string>(); // 存储所有对话行
    private int currentLine = 0; // 跟踪当前显示的对话行
    private bool isComplete = true; // 是否显示完整对话
    private float typingSpeed = 0.05f; // 字符显示的速度
    private string currentText = ""; // 当前逐字显示的文本
    public bool allDialoguesComplete = false;
    public string sceneName;

    void Start()
    {
        // 添加对话内容
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
