using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 暂停UI界面
/// </summary>
public class PausePanel : BasePanel
{
    /// <summary>
    /// 实现父类的构造函数
    /// </summary>
    public PausePanel() : base("PausePanel") { }

    /// <summary>
    /// 重写父类进入方法
    /// </summary>
    public override void OnEnter()
    {
        //绑定继续游戏监听事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.AddListener(() => {
            //弹出当前窗口
            panelManager.Pop();
        });
        //绑定设置按钮监听事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.AddListener(() => {
            panelManager.Push(new SettingPanel());
        });
        //绑定退出保存监听事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "QuitAndSaveButton").GetComponent<Button>().onClick.AddListener(() => {
            //先将所有MainUI弹出
            panelManager.AllPop();
            //执行转场景操作
            SceneController.Instance.TransformMainMenuSence("MainMenu");
        });
    }

    /// <summary>
    /// 重写父类退出方法
    /// </summary>
    public override void OnExit()
    {
        //移除所有按钮绑定的监听事件
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "QuitAndSaveButton").GetComponent<Button>().onClick.RemoveAllListeners();

        base.OnExit();
    }
}
