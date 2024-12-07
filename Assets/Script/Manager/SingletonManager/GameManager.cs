using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
/// <summary>
/// 游戏管理器
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 游戏的基础数据
    /// </summary>
    public GameData gameData;
    /// <summary>
    /// 游戏数据的标识符
    /// </summary>
    public GameDataIdentifier gameDataIdentifier;

    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 引用总状态机
    /// </summary>
    public TotalMachine totalMachine;

    /// <summary>
    /// 游戏结束的观察者列表
    /// </summary>
    public List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    /// <summary>
    /// GameManager单例
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //游戏数据的初始化
        gameData = new GameData();
        //游戏数据标识符初始化
        gameDataIdentifier = new GameDataIdentifier();

        //单例初始化
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
    /// 读取用户声音设置
    /// </summary>
    public void RegisterAudioSetting()
    {
        //读取玩家声音设置
        AudioManager.Instance.audioSettingManager.LoadAudioSetting();
    }

    /// <summary>
    /// 注册玩家相关的脚本组件
    /// </summary>
    public void RigisterPlayer()
    {
        //获取到totalController
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //获取到totalMachine
        totalMachine = SetGameObjectToParent.FindFromFirstLayer("StateMachine").GetComponent<TotalMachine>();

        //注册玩家，初始化玩家相关脚本
        totalController.RegisterAboutPlayer();
        //更改玩家的名字
        totalController.playerDataAndUIController.ChangePlayerName(gameData.playerName);
        //初始化所有状态机
        totalMachine.StartAllMachine();
        //对着InventoryManager中的容器进行总控制器的注册
        InventoryManager.Instance.RegisterContainersTotalController();
    }

    /// <summary>
    /// 加入结束游戏观察者列表
    /// </summary>
    /// <param name="observer"></param>
    public void AddObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    /// <summary>
    /// 退出结束游戏观察者列表
    /// </summary>
    /// <param name="obsevrer"></param>
    public void RemoveObserver(IEndGameObserver obsevrer)
    {
        endGameObservers.Remove(obsevrer);
    }

    /// <summary>
    /// 执行游戏结束观察者列表中的OnEndNotify方法
    /// </summary>
    public void NotifyObservers()
    {
        foreach (var observer in endGameObservers)
        {
            observer.OnEndGame();
        }
    }

    /// <summary>
    /// 存储游戏相关数据
    /// </summary>
    public void SaveData()
    {
        if(gameDataIdentifier != null)
        {
            //存储建档时间
            gameDataIdentifier.createArchiveTimeIdentifier = gameData.createArchiveTime;
            //先读取目前系统时间
            DateTime currentTime = DateTime.Now;
            gameData.reviseArchiveTime = currentTime.ToString();
            //存储修改时间
            gameDataIdentifier.reviseArchiveTimeIdentifier = gameData.reviseArchiveTime;
            //存储数据
            SaveManager.Instance.Save(gameData.archiveSaveName, "GameDataIdentifier.sav", gameDataIdentifier);
        }
        
    }

    /// <summary>
    /// 读取存档游戏基础数据
    /// </summary>
    public void LoadData()
    {
        if (gameDataIdentifier != null)
        {
            //读取数据
            gameDataIdentifier = SaveManager.Instance.Load<GameDataIdentifier>(gameData.archiveSaveName, "GameDataIdentifier.sav");
            //读取建档时间
            gameData.createArchiveTime = gameDataIdentifier.createArchiveTimeIdentifier;
            //读取修改时间
            gameData.reviseArchiveTime = gameDataIdentifier.reviseArchiveTimeIdentifier;
        }
    }
    
}
