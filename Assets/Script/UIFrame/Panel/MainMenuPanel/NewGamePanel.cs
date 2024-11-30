using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����Ϸ����
/// </summary>
public class NewGamePanel : BasePanel
{
    /// <summary>
    /// ��д���캯��
    /// </summary>
    public NewGamePanel() : base("NewGamePanel") { }
    /// <summary>
    /// ����浵���ֵ�InputField
    /// </summary>
    public InputField inputArchiveName;
    /// <summary>
    /// ����������ֵ�InputField
    /// </summary>
    public InputField inputPlayerName;

    /// <summary>
    /// �浵���־���Text
    /// </summary>
    public Text archiveNameWarning;
    /// <summary>
    /// ������־���Text
    /// </summary>
    public Text playerNameWarning;

    /// <summary>
    /// ��дOnEnter()
    /// </summary>
    public override void OnEnter()
    {
        //��ȡ����Ӧ��Panel���
        Transform mainMenuCanvasGameObject = SetGameObjectToParent.FindFromFirstLayer("MainMenuCanvas").GetComponent<Transform>();
        //��ʼ�����������
        inputArchiveName = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "InputArchiveName").GetComponent<InputField>();
        inputPlayerName = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "InputPlayerName").GetComponent<InputField>();
        //��ʼ����������Text
        archiveNameWarning = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "ArchiveNameWarning").GetComponent<Text>();
        playerNameWarning = SetGameObjectToParent.FindChildRecursive(mainMenuCanvasGameObject, "PlayerNameWarning").GetComponent<Text>();

        //Ĭ�Ϲرվ���
        archiveNameWarning.gameObject.SetActive(false);
        //Ĭ�Ϲرվ���
        playerNameWarning.gameObject.SetActive(false);

        //�󶨽�����Ϸ�¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndStartGameButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            string archiveName = CheckArchiveName(inputArchiveName.text);
            string playerName = CheckPlayerName(inputPlayerName.text);

            //ֻ������������֮��ſ��Դ����´浵
            if (archiveName == inputArchiveName.text && playerName == inputPlayerName.text)
            {
                //���������Ĵ浵���ִ���GameManager�е�gameData�еĴ浵����
                GameManager.Instance.gameData.archiveSaveName = archiveName;
                //���������Ĵ�����ִ���GameManager�е�gameData�ܵĴ浵����
                GameManager.Instance.gameData.playerName = playerName;
                //��ȡ��ǰϵͳʱ��
                DateTime currentTime = DateTime.Now;
                //��ʱ�丳��gameData
                GameManager.Instance.gameData.createArchiveTime = currentTime.ToString();

                //�������д���
                panelManager.AllPop();
                //Ȼ��ѹ����ش��ڣ����Ҵ���SenceController����ڽ����´浵
                panelManager.Push(new LoadingPanel(true));
            }

            //�������ж����������ַ����Ƿ������⣬��������˾͹رվ���Text
            if(archiveName == "Repeat")
            {
                archiveNameWarning.text = "�浵�����ظ���";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else if(archiveName == "Null")
            {
                archiveNameWarning.text = "�浵�����ǿյ�";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else if (archiveName == "InvalidChar")
            {
                archiveNameWarning.text = "�浵���ְ����Ƿ��ַ���< > : \" / \\ | ? *";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else if(archiveName == "More200")
            {
                archiveNameWarning.text = "�浵���ֳ��ȳ���200";
                archiveNameWarning.gameObject.SetActive(true);
            }
            else
            {
                archiveNameWarning.gameObject.SetActive(false);
            }

            if(playerName == "More30")
            {
                playerNameWarning.text = "������ֳ���30���ַ���";
                playerNameWarning.gameObject.SetActive(true);
            }
            else if(playerName == "Null")
            {
                playerNameWarning.text = "��������ǿյ�!";
                playerNameWarning.gameObject.SetActive(true);
            }
            else
            {
                playerNameWarning.gameObject.SetActive(false);
            }

        });

        //���˳������´浵�¼�
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            //����panelManager�˳���ǰ��Ծ����
            panelManager.Pop();
        });
    }

    /// <summary>
    /// ��дOnExit
    /// </summary>
    public override void OnExit()
    {
        //�رվ���UI
        archiveNameWarning.gameObject.SetActive(false);
        playerNameWarning.gameObject.SetActive(false);

        //��������
        inputArchiveName.text = "";
        inputPlayerName.text = "";

        // ���Ƴ�֮ǰ���ܴ��ڵļ����¼�����ֹ�ظ����
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndStartGameButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.RemoveAllListeners();
        //ִ�л�����˳�����
        base.OnExit();
    }

    /// <summary>
    /// ���浵���ֲ���������
    /// </summary>
    public string CheckArchiveName(string archiveName)
    {
        //��ȡ�Ƿ��ַ��б�
        char[] invalidchars = Path.GetInvalidFileNameChars();
        
        foreach(char c in archiveName)
        {
            //���Ƿ��ַ��Ƿ���archiveName�е��ַ�����ͬ
            if (Array.Exists(invalidchars, invalidchars => invalidchars == c))
            {
                return "InvalidChar";
            }
        }

        //����浵������ͬ�����ֳ��ֵĻ�
        if (SaveManager.Instance.ListArchiveName().Contains(archiveName))
        {
            return "Repeat";
        }

        //����Ƿ��пո�
        else if(string.IsNullOrWhiteSpace(archiveName))
        {
            return "Null";
        }
        //��鳤���Ƿ񳬹�200
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
    /// ����������
    /// </summary>
    public string CheckPlayerName(string playerName)
    {
        //�涨���������ֲ�������30��
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
