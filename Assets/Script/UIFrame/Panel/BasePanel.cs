using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Panel的基类
/// </summary>
public class BasePanel
{
    /// <summary>
    /// 每个Panel的名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 当前活跃的面板
    /// </summary>
    public GameObject activePanel { get; set; }

    /// <summary>
    /// panel的管理器
    /// </summary>
    public PanelManager panelManager { get; set; }

    /// <summary>
    /// BasePanel的构造函数
    /// </summary>
    /// <param name="Name"></param>
    public BasePanel(string Name)
    {
        this.Name = Name;
    }

    /// <summary>
    /// 进入UI要执行的方法
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 正在UI界面中要执行的方法
    /// </summary>
    public virtual void OnUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panelManager.Pop();
        }
    }

    /// <summary>
    /// UI暂停的时候要执行的代码
    /// </summary>
    public virtual void OnPasue()
    {
        //暂停Panel的射线检测
        ComponentFinder.GetOrAddComponent<CanvasGroup>(activePanel).blocksRaycasts = false;
    }

    /// <summary>
    /// UI继续的时候要执行的代码
    /// </summary>
    public virtual void OnResume()
    {
        //开始Panel的射线检测
        ComponentFinder.GetOrAddComponent<CanvasGroup>(activePanel).blocksRaycasts = true;
    }

    /// <summary>
    /// UI退出的时候要执行的代码
    /// </summary>
    public virtual void OnExit()
    {
        UIManager.Instance.HideUI(activePanel);
    }
}

/// <summary>
/// 主菜单Panel
/// </summary>
public class StartPanel : BasePanel
{
    /// <summary>
    /// 重写构造函数
    /// </summary>
    public StartPanel() : base("MainMenu") { }

    /// <summary>
    /// 引用按钮Panel
    /// </summary>
    public GameObject buttonPanel;

    /// <summary>
    /// 重写OnEnter()
    /// </summary>
    public override void OnEnter() 
    {
        buttonPanel = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "ButtonPanel").gameObject;

        //绑定按钮监听器
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "NewGameButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            panelManager.Push(new NewGamePanel());
        });

        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            panelManager.Push(new ArchivePanel());
        });

        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            panelManager.Push(new SettingPanel());
        });

        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "QuitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    /// <summary>
    /// 重写主菜单暂停的方法
    /// </summary>
    public override void OnPasue()
    {
        if(buttonPanel != null)
        {
            buttonPanel.SetActive(false);
        }
        base.OnPasue();
    }

    /// <summary>
    /// 重写OnUpdate方法
    /// </summary>
    public override void OnUpdate()
    {
        
    }

    /// <summary>
    /// 重写暂停恢复方法
    /// </summary>
    public override void OnResume()
    {
        if (buttonPanel != null)
        {
            buttonPanel.SetActive(true);
        }
        base.OnResume();
    }

    /// <summary>
    /// 重写退出的方法
    /// </summary>
    public override void OnExit()
    {
        //解除按键监听器事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "NewGameButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.RemoveAllListeners();
        base.OnExit();
    }
}

/// <summary>
/// 加载页面的窗口
/// </summary>
public class LoadingPanel : BasePanel
{
    public bool isNewArchive;

    /// <summary>
    /// 重写构造函数
    /// </summary>
    public LoadingPanel(bool isNewArchive) : base("LoadingPanel") 
    {
        this.isNewArchive = isNewArchive;
    }

    public override void OnEnter()
    {
        //给SceneController传达玩家是否在建立新存档
        SceneController.Instance.isNewArchive = isNewArchive;
        //开始转场景
        SceneController.Instance.TransformGameScene("GameSence");
    }
}

