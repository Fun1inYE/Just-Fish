using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Panel�Ļ���
/// </summary>
public class BasePanel
{
    /// <summary>
    /// ÿ��Panel������
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ��ǰ��Ծ�����
    /// </summary>
    public GameObject activePanel { get; set; }

    /// <summary>
    /// panel�Ĺ�����
    /// </summary>
    public PanelManager panelManager { get; set; }

    /// <summary>
    /// BasePanel�Ĺ��캯��
    /// </summary>
    /// <param name="Name"></param>
    public BasePanel(string Name)
    {
        this.Name = Name;
    }

    /// <summary>
    /// ����UIҪִ�еķ���
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// ����UI������Ҫִ�еķ���
    /// </summary>
    public virtual void OnUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panelManager.Pop();
        }
    }

    /// <summary>
    /// UI��ͣ��ʱ��Ҫִ�еĴ���
    /// </summary>
    public virtual void OnPasue()
    {
        //��ͣPanel�����߼��
        ComponentFinder.GetOrAddComponent<CanvasGroup>(activePanel).blocksRaycasts = false;
    }

    /// <summary>
    /// UI������ʱ��Ҫִ�еĴ���
    /// </summary>
    public virtual void OnResume()
    {
        //��ʼPanel�����߼��
        ComponentFinder.GetOrAddComponent<CanvasGroup>(activePanel).blocksRaycasts = true;
    }

    /// <summary>
    /// UI�˳���ʱ��Ҫִ�еĴ���
    /// </summary>
    public virtual void OnExit()
    {
        UIManager.Instance.HideUI(activePanel);
    }
}

/// <summary>
/// ���˵�Panel
/// </summary>
public class StartPanel : BasePanel
{
    /// <summary>
    /// ��д���캯��
    /// </summary>
    public StartPanel() : base("MainMenu") { }

    /// <summary>
    /// ���ð�ťPanel
    /// </summary>
    public GameObject buttonPanel;

    /// <summary>
    /// ��дOnEnter()
    /// </summary>
    public override void OnEnter() 
    {
        buttonPanel = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "ButtonPanel").gameObject;

        //�󶨰�ť������
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
    /// ��д���˵���ͣ�ķ���
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
    /// ��дOnUpdate����
    /// </summary>
    public override void OnUpdate()
    {
        
    }

    /// <summary>
    /// ��д��ͣ�ָ�����
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
    /// ��д�˳��ķ���
    /// </summary>
    public override void OnExit()
    {
        //��������������¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "NewGameButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "ContinueButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SettingButton").GetComponent<Button>().onClick.RemoveAllListeners();
        base.OnExit();
    }
}

/// <summary>
/// ����ҳ��Ĵ���
/// </summary>
public class LoadingPanel : BasePanel
{
    public bool isNewArchive;

    /// <summary>
    /// ��д���캯��
    /// </summary>
    public LoadingPanel(bool isNewArchive) : base("LoadingPanel") 
    {
        this.isNewArchive = isNewArchive;
    }

    public override void OnEnter()
    {
        //��SceneController��������Ƿ��ڽ����´浵
        SceneController.Instance.isNewArchive = isNewArchive;
        //��ʼת����
        SceneController.Instance.TransformGameScene("GameSence");
    }
}

