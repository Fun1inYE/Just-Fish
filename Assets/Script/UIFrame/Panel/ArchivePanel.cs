using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchivePanel : BasePanel
{
    /// <summary>
    /// ���캯��
    /// </summary>
    public ArchivePanel() : base("ArchivePanel") { }

    //������Ϸ�İ�ť
    public Button goToGameButton;

    /// <summary>
    /// ɾ���浵��ť
    /// </summary>
    public Button deleteArchiveButton;

    /// <summary>
    /// ȡ����ť
    /// </summary>
    public Button cancelButton;

    /// <summary>
    /// �����浵�����б�
    /// </summary>
    List<string> archiveNameList = new List<string>();
    /// <summary>
    /// �����浵����Ϣ�б�
    /// </summary>
    List<GameObject> archiveInfoPanels = new List<GameObject>();

    /// <summary>
    /// ��ǰ�����Ĵ浵��UI
    /// </summary>
    public GameObject currentArchiveInfoPanel;
    /// <summary>
    /// ��ǰ�����Ĵ浵����
    /// </summary>
    public string currentArchiveName;

    //��д��������
    public override void OnEnter()
    {
        //�����ť�пյĻ�����Ҫ���³�ʼ��һ��
        if(goToGameButton == null || deleteArchiveButton == null || cancelButton == null)
        {
            goToGameButton = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "GoToGameButton").GetComponent<Button>();
            //��Ϊ�ճ�ʼ�������Բ��ɽ���
            goToGameButton.interactable = false;

            deleteArchiveButton = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "DeleteArchiveButton").GetComponent<Button>();
            //������ͬ��
            deleteArchiveButton.interactable = false;

            cancelButton = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "CancelButton").GetComponent<Button>();
        }

        //�󶨿�ʼ��Ϸ�¼�
        goToGameButton.onClick.AddListener(() =>
        {
            //��ʼ��Ϸ
            GotoGame();
        });

        deleteArchiveButton.onClick.AddListener(() =>
        {
            //ɾ���浵
            DeleteArchive();
        });

        //�󶨰�ť����
        cancelButton.onClick.AddListener(() =>
        {
            //��������
            panelManager.Pop();
        });


        //��ȡ��Ϸ�浵Ŀ¼�����֣�˳������
        archiveNameList = SaveManager.Instance.ListArchiveName();

        for(int i = 0; i < archiveNameList.Count; i++)
        {
            //��ʾUI˳������UI���뵽archiveInfoPanels�б���
            archiveInfoPanels.Add(UIManager.Instance.GetAndDisPlayPoolUI("ArchiveInfoPanel"));
            //Ϊ�浵��UI����Text
            ReadPlayerGameDataFileAndChangeText(archiveNameList[i], archiveInfoPanels[i]);
            //Ϊ�浵��UI��Ӷ�Ӧ��Button����
            SetArchiveInfoPanelGameButton(archiveNameList[i], archiveInfoPanels[i]);
            //���浵��Ų��ArchiveContent��
            SetGameObjectToParent.SetParentFromFirstLayerParent("MainMenuCanvas", "ArchiveContent", archiveInfoPanels[i]);

        }   

        #region TestCode

        //for (int i = 0; i < archiveInfoPanels.Count; i++)
        //{
        //    Debug.Log(archiveInfoPanels[i]);
        //}

        #endregion

    }

    public override void OnExit()
    {
        //ȡ���󶨰�ť����
        goToGameButton.onClick.RemoveAllListeners();
        deleteArchiveButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        //���archiveInfoPanels�е���Ϣ
        for (int i = 0; i < archiveInfoPanels.Count; i++)
        {
            //�رմ浵��UI
            UIManager.Instance.HideUI(archiveInfoPanels[i]);
            //��UI�Ż�ԭ���ĵط�
            SetGameObjectToParent.SetParent("MainMenuCanvas", archiveInfoPanels[i]);
        }

        //���archiveInfoPanels��GameObject�������´δ洢
        archiveInfoPanels.Clear();

        //�رհ�ť����
        goToGameButton.interactable = false;
        deleteArchiveButton.interactable = false;

        //ִ�и����OnExit����
        base.OnExit();
    }

    /// <summary>
    /// Ϊ�浵�����Text
    /// </summary>
    /// <param name="archiveName">��Ӧ�浵����</param>
    /// <param name="archiveInfoPanel">��Ӧ�浵���UI</param>
    public void ReadPlayerGameDataFileAndChangeText(string archiveName, GameObject archiveInfoPanel)
    {
        //��ȡ��Ӧ�ļ���ʶ��
        PlayerDataIditentifier playerDataIditentifier = SaveManager.Instance.Load<PlayerDataIditentifier>(archiveName, "playerGameDataIdentifier.sav");
        GameDataIdentifier gameDataIdentifier = SaveManager.Instance.Load<GameDataIdentifier>(archiveName, "GameDataIdentifier.sav");
        //�浵����
        archiveInfoPanel.transform.GetChild(0).GetComponent<Text>().text = archiveName;
        //�������
        archiveInfoPanel.transform.GetChild(1).GetComponent<Text>().text = "���֣�" + playerDataIditentifier.playerNameIdetentifier;
        //��ҽ������
        archiveInfoPanel.transform.GetChild(2).GetComponent<Text>().text = "��ң�" + playerDataIditentifier.coinIdetentifier.ToString();
        //TODO:��Ҿ���

        //�浵����ʱ��
        archiveInfoPanel.transform.GetChild(4).GetComponent<Text>().text = "����ʱ�䣺" + gameDataIdentifier.createArchiveTimeIdentifier.ToString();
        //�浵����ʱ��
        archiveInfoPanel.transform.GetChild(5).GetComponent<Text>().text = "�޸�ʱ�䣺" + gameDataIdentifier.reviseArchiveTimeIdentifier.ToString();
    }

    /// <summary>
    /// ����ArchivePanel�е�Button,�Դﵽѡ�иô浵��Ĺ���
    /// </summary>
    public void SetArchiveInfoPanelGameButton(string archiveName, GameObject archiveInfoPanel)
    {
        //��ȡ��Ӧ�ļ���ʶ��
        PlayerDataIditentifier playerDataIditentifier = SaveManager.Instance.Load<PlayerDataIditentifier>(archiveName, "playerGameDataIdentifier.sav");
        GameDataIdentifier gameDataIdentifier = SaveManager.Instance.Load<GameDataIdentifier>(archiveName, "GameDataIdentifier.sav");

        //���浵����ӽ�����Ϸ��ť�Ĺ���
        archiveInfoPanel.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() =>
        {
            //��һ�ε���浵��
            if(currentArchiveInfoPanel == null)
            {
                currentArchiveInfoPanel = archiveInfoPanel;
                //��ѡ�еĴ浵��䰵(��SelectPanel)
                currentArchiveInfoPanel.transform.GetChild(7).gameObject.SetActive(true);

                //����ǰ�����Ĵ浵���ָ��ĳ����ڵĴ浵����
                currentArchiveName = archiveName;

                //����ť������
                goToGameButton.interactable = true;
                deleteArchiveButton.interactable = true;
            }
            //���֮ǰѡ�й��浵��
            else if(currentArchiveInfoPanel != archiveInfoPanel)
            {
                //����һ��ѡ�е�Panel����ԭ������ɫ���ر�SelectPanel��
                currentArchiveInfoPanel.transform.GetChild(7).gameObject.SetActive(false);
                //�����ڵ�Panel���ɰ�ɫ
                currentArchiveInfoPanel = archiveInfoPanel;
                currentArchiveInfoPanel.transform.GetChild(7).gameObject.SetActive(true);

                //����ǰ�����Ĵ浵���ָ��ĳ����ڵĴ浵����
                currentArchiveName = archiveName;

                //����ť������
                goToGameButton.interactable = true;
                deleteArchiveButton.interactable = true;
            }
            
        });
    }

    /// <summary>
    /// ɾ���浵UI�ʹ浵���ݵķ���
    /// </summary>
    public void DeleteArchive()
    {
        //�����ǰ�浵���ֲ�Ϊ�յĻ�,Ҳͬʱ�������ѡ����һ���浵
        if(currentArchiveName != null)
        {
            //����浵UI�б��а�������浵UI
            if(archiveInfoPanels.Contains(currentArchiveInfoPanel))
            {
                //�رմ浵��UI
                UIManager.Instance.HideUI(currentArchiveInfoPanel);
                //��UI�Ż�ԭ���ĵط�
                SetGameObjectToParent.SetParent("MainMenuCanvas", currentArchiveInfoPanel);
            }
            //���б�UI���Ƴ���ǰUI
            archiveInfoPanels.Remove(currentArchiveInfoPanel);
            //�Ӵ浵�����б����Ƴ���ǰ�浵����
            archiveNameList.Remove(currentArchiveName);
            //����ǰ������UI�ÿ�
            currentArchiveInfoPanel = null;
            //ͨ����ǰ�浵����ɾ����Ӧ�浵����
            SaveManager.Instance.Delete(currentArchiveName);
            //����ǰ�����Ĵ浵�����ÿ�
            currentArchiveName = null;
        }

        //ɾ����浵����Ϊû�д浵ѡ�У����԰�ť�޷�����
        goToGameButton.interactable = false;
        deleteArchiveButton.interactable = false;
    }

    /// <summary>
    /// ������Ϸ����UI
    /// </summary>
    public void GotoGame()
    {
        if (currentArchiveName != null)
        {
            //����Ϸ�浵���ָ��ĳ������ѡ�浵����
            GameManager.Instance.gameData.archiveSaveName = currentArchiveName;
            //�������д��ڣ��������ҳ�棬���Ҹ���SenceController����ڶ���
            panelManager.AllPop();
            panelManager.Push(new LoadingPanel(false));
        }
    }
}
