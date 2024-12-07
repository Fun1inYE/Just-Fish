using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
/// <summary>
/// ��Ϸ������
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ�Ļ�������
    /// </summary>
    public GameData gameData;
    /// <summary>
    /// ��Ϸ���ݵı�ʶ��
    /// </summary>
    public GameDataIdentifier gameDataIdentifier;

    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// ������״̬��
    /// </summary>
    public TotalMachine totalMachine;

    /// <summary>
    /// ��Ϸ�����Ĺ۲����б�
    /// </summary>
    public List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    /// <summary>
    /// GameManager����
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //��Ϸ���ݵĳ�ʼ��
        gameData = new GameData();
        //��Ϸ���ݱ�ʶ����ʼ��
        gameDataIdentifier = new GameDataIdentifier();

        //������ʼ��
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        RegisterAudioSetting();
    }

    /// <summary>
    /// ��ȡ�û���������
    /// </summary>
    public void RegisterAudioSetting()
    {
        //��ȡ�����������
        AudioManager.Instance.audioSettingManager.LoadAudioSetting();
    }

    /// <summary>
    /// ע�������صĽű����
    /// </summary>
    public void RigisterPlayer()
    {
        //��ȡ��totalController
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //��ȡ��totalMachine
        totalMachine = SetGameObjectToParent.FindFromFirstLayer("StateMachine").GetComponent<TotalMachine>();

        //ע����ң���ʼ�������ؽű�
        totalController.RegisterAboutPlayer();
        //������ҵ�����
        totalController.playerDataAndUIController.ChangePlayerName(gameData.playerName);
        //��ʼ������״̬��
        totalMachine.StartAllMachine();
        //����InventoryManager�е����������ܿ�������ע��
        InventoryManager.Instance.RegisterContainersTotalController();
    }

    /// <summary>
    /// ���������Ϸ�۲����б�
    /// </summary>
    /// <param name="observer"></param>
    public void AddObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    /// <summary>
    /// �˳�������Ϸ�۲����б�
    /// </summary>
    /// <param name="obsevrer"></param>
    public void RemoveObserver(IEndGameObserver obsevrer)
    {
        endGameObservers.Remove(obsevrer);
    }

    /// <summary>
    /// ִ����Ϸ�����۲����б��е�OnEndNotify����
    /// </summary>
    public void NotifyObservers()
    {
        foreach (var observer in endGameObservers)
        {
            observer.OnEndGame();
        }
    }

    /// <summary>
    /// �洢��Ϸ�������
    /// </summary>
    public void SaveData()
    {
        if(gameDataIdentifier != null)
        {
            //�洢����ʱ��
            gameDataIdentifier.createArchiveTimeIdentifier = gameData.createArchiveTime;
            //�ȶ�ȡĿǰϵͳʱ��
            DateTime currentTime = DateTime.Now;
            gameData.reviseArchiveTime = currentTime.ToString();
            //�洢�޸�ʱ��
            gameDataIdentifier.reviseArchiveTimeIdentifier = gameData.reviseArchiveTime;
            //�洢����
            SaveManager.Instance.Save(gameData.archiveSaveName, "GameDataIdentifier.sav", gameDataIdentifier);
        }
        
    }

    /// <summary>
    /// ��ȡ�浵��Ϸ��������
    /// </summary>
    public void LoadData()
    {
        if (gameDataIdentifier != null)
        {
            //��ȡ����
            gameDataIdentifier = SaveManager.Instance.Load<GameDataIdentifier>(gameData.archiveSaveName, "GameDataIdentifier.sav");
            //��ȡ����ʱ��
            gameData.createArchiveTime = gameDataIdentifier.createArchiveTimeIdentifier;
            //��ȡ�޸�ʱ��
            gameData.reviseArchiveTime = gameDataIdentifier.reviseArchiveTimeIdentifier;
        }
    }
    
}
