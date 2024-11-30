using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新游戏窗口
/// </summary>
public class NewGamePanel : BasePanel
{
    /// <summary>
    /// 重写构造函数
    /// </summary>
    public NewGamePanel() : base("NewGamePanel") { }
    /// <summary>
    /// 输入存档名字的InputField
    /// </summary>
    public InputField inputArchiveName;
    /// <summary>
    /// 输入玩家名字的InputField
    /// </summary>
    public InputField inputPlayerName;

    /// <summary>
    /// 存档名字警告Text
    /// </summary>
    public Text archiveNameWarning;
    /// <summary>
    /// 玩家名字警告Text
    /// </summary>
    public Text playerNameWarning;

    /// <summary>
    /// 重写OnEnter()
    /// </summary>
    public override void OnEnter()
    {
        //获取到对应的Panel面板
        Transform mainMenuCanvasGameObject = SetGameObjectToParent.FindFromFirstLayer("MainMenuCanvas").GetComponent<Transform>();
        //初始化两个输入框
        inputArchiveName = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "InputArchiveName").GetComponent<InputField>();
        inputPlayerName = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "InputPlayerName").GetComponent<InputField>();
        //初始化两个警告Text
        archiveNameWarning = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "ArchiveNameWarning").GetComponent<Text>();
        playerNameWarning = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "PlayerNameWarning").GetComponent<Text>();

        //默认关闭警告
        archiveNameWarning.gameObject.SetActive(false);
        //默认关闭警告
        playerNameWarning.gameObject.SetActive(false);

        //绑定进入游戏事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndStartGameButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            string archiveName = CheckArchiveName(inputArchiveName.text);
            string playerName = CheckPlayerName(inputPlayerName.text);

            //只有两点审查完毕之后才可以创建新存档
            if (archiveName == inputArchiveName.text && playerName == inputPlayerName.text)
            {
                //将玩家输入的存档名字传入GameManager中的gameData中的存档名字
                GameManager.Instance.gameData.archiveSaveName = archiveName;
                //将玩家输入的存放名字传入GameManager中的gameData总的存档名字
                GameManager.Instance.gameData.playerName = playerName;
                //获取当前系统时间
                DateTime currentTime = DateTime.Now;
                //将时间赋给gameData
                GameManager.Instance.gameData.createArchiveTime = currentTime.ToString();

                //弹出所有窗口
                panelManager.AllPop();
                //然后压入加载窗口，并且传达SenceController玩家在建立新存档
                panelManager.Push(new LoadingPanel(true));
            }

            //以下是判断玩家输入的字符串是否有问题，如果检测过了就关闭警告Text
            if(archiveName == "Repeat")
            {
                archiveNameWarning.text = "存档名字重复了";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else if(archiveName == "Null")
            {
                archiveNameWarning.text = "存档名字是空的";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else if (archiveName == "InvalidChar")
            {
                archiveNameWarning.text = "存档名字包含非法字符：< > : \" / \\ | ? *";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else if(archiveName == "More200")
            {
                archiveNameWarning.text = "存档名字长度超过200";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else
            {
                archiveNameWarning.gameObject.SetActive(false);
            }

            if(playerName == "More30")
            {
                playerNameWarning.text = "玩家名字超过30个字符了";
                playerNameWarning.gameObject.SetActive(true);
            }
            else if(playerName == "Null")
            {
                playerNameWarning.text = "玩家名字是空的!";
                playerNameWarning.gameObject.SetActive(true);
            }
            else
            {
                playerNameWarning.gameObject.SetActive(false);
            }

        });

        //绑定退出创建新存档事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            //调用panelManager退出当前活跃窗口
            panelManager.Pop();
        });
    }

    /// <summary>
    /// 重写OnExit
    /// </summary>
    public override void OnExit()
    {
        //关闭警告UI
        archiveNameWarning.gameObject.SetActive(false);
        playerNameWarning.gameObject.SetActive(false);

        //清空输入框
        inputArchiveName.text = "";
        inputPlayerName.text = "";

        // 先移除之前可能存在的监听事件，防止重复添加
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndStartGameButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.RemoveAllListeners();
        //执行基类的退出方法
        base.OnExit();
    }

    /// <summary>
    /// 检查存档名字并返回名字
    /// </summary>
    public string CheckArchiveName(string archiveName)
    {
        //获取非法字符列表
        char[] invalidchars = Path.GetInvalidFileNameChars();
        
        foreach(char c in archiveName)
        {
            //检查非法字符是否与archiveName中的字符串相同
            if (Array.Exists(invalidchars, invalidchars => invalidchars == c))
            {
                return "InvalidChar";
            }
        }

        //如果存档中有相同的名字出现的话
        if (SaveManager.Instance.ListArchiveName().Contains(archiveName))
        {
            return "Repeat";
        }

        //检查是否有空格
        else if(string.IsNullOrWhiteSpace(archiveName))
        {
            return "Null";
        }
        //检查长度是否超过200
        else if(archiveName.Length > 200)
        {
            return "More200";
        }
        else
        {
            return archiveName;
        }
    }

    /// <summary>
    /// 检查玩家名字
    /// </summary>
    public string CheckPlayerName(string playerName)
    {
        //规定玩家起的名字不允许超过30个
        if(playerName.Length >= 30)
        {
            return "More30";
        }
        else if (string.IsNullOrWhiteSpace(playerName))
        {
            return "Null";
        }
        else
        {
            return playerName;
        }
    }
}
