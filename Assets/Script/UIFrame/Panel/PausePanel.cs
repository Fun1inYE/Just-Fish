using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ͣUI����
/// </summary>
public class PausePanel : BasePanel
{
    /// <summary>
    /// ʵ�ָ���Ĺ��캯��
    /// </summary>
    public PausePanel() : base("PausePanel") { }

    /// <summary>
    /// ��д������뷽��
    /// </summary>
    public override void OnEnter()
    {
        //�󶨼�����Ϸ�����¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.AddListener(() => {
            //������ǰ����
            panelManager.Pop();
        });
        //�����ð�ť�����¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.AddListener(() => {
            panelManager.Push(new SettingPanel());
        });
        //���˳���������¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "QuitAndSaveButton").GetComponent<Button>().onClick.AddListener(() => {
            //�Ƚ�����MainUI����
            panelManager.AllPop();
            //ִ��ת��������
            SceneController.Instance.TransformMainMenuSence("MainMenu");
        });
    }

    /// <summary>
    /// ��д�����˳�����
    /// </summary>
    public override void OnExit()
    {
        //�Ƴ����а�ť�󶨵ļ����¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "QuitAndSaveButton").GetComponent<Button>().onClick.RemoveAllListeners();

        base.OnExit();
    }
}
