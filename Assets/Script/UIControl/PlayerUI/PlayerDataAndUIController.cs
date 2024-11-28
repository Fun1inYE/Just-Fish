using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责控制玩家数据和UI的控制器类
/// </summary>
public class PlayerDataAndUIController : MonoBehaviour
{
    /// <summary>
    /// 引用玩家数据
    /// </summary>
    public PlayerData playerData;
    /// <summary>
    /// 引用关于玩家数据的UI
    /// </summary>
    public PlayerUI playerUI;
    /// <summary>
    /// 引用游戏存档所需的Player数据
    /// </summary>
    public PlayerGameData playerGameData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //初始化PlayerData
        playerData = new PlayerData();
        //初始化PlayerUI
        playerUI = GetComponent<PlayerUI>();
        //初始化游戏存档所需的数据
        playerGameData = new PlayerGameData();
    }

    /// <summary>
    /// 在Awake结束之后执行
    /// </summary>
    public void Start()
    {
        //将初始化的PlayerUI的值赋给PlayerUI.playerData
        playerUI.SetPlayerData(playerData);
    }

    /// <summary>
    /// 改变玩家名字的方法
    /// </summary>
    /// <param name="name"></param>
    public void ChangePlayerName(string name)
    {
        playerData.NodifyPlayerName(name);
    }

    /// <summary>
    /// 改变金币的方法
    /// </summary>
    public void ChangeCoin(int coinAmount)
    {
        playerData.NodifyCoin(coinAmount);
    }

    /// <summary>
    /// 改变经验的方法
    /// </summary>
    public void ChangeExperience(int experience)
    {
        playerData.NodifyExperience(experience);
    }

    /// <summary>
    /// 改变正在显示卖出的金币数量
    /// </summary>
    /// <param name="sallingCoin"></param>
    public void ChangeSallingCoin(int sallingCoin)
    {
        playerData.NodifySalling(sallingCoin);
    }

    /// <summary>
    /// 将玩家的金币数量归零
    /// </summary>
    public void ChangeSallingCoinToZero()
    {
        playerData.NodifySallingToZero();
    }
    
    /// <summary>
    /// 保存玩家数据
    /// </summary>
    public void SaveData()
    {
        if(playerGameData != null)
        {
            //将玩家的名字储存下来
            playerGameData.playerDataIditentifier.playerNameIdetentifier = playerData.name;
            //将玩家的x轴坐标赋到X轴标识符中
            playerGameData.playerDataIditentifier.coordinate_XIdetentifier = gameObject.transform.position.x;
            //将玩家的y轴坐标复制到Y轴标识符中
            playerGameData.playerDataIditentifier.coordinate_YIdetentifier = gameObject.transform.position.y;
            //将玩家的金币数量进行存储
            playerGameData.playerDataIditentifier.coinIdetentifier = playerData.coin;
            //存储玩家数据
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "playerGameDataIdentifier.sav", playerGameData.playerDataIditentifier);
        }
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public void LoadData()
    {
        if (playerGameData != null)
        {
            //读取存档数据中的玩家标识符号数据
            playerGameData.playerDataIditentifier = SaveManager.Instance.Load<PlayerDataIditentifier>(GameManager.Instance.gameData.archiveSaveName, "playerGameDataIdentifier.sav");
            //读取玩家的名字
            ChangePlayerName(playerGameData.playerDataIditentifier.playerNameIdetentifier);
            //读取玩家坐标并赋值
            gameObject.transform.position = new Vector2((float)playerGameData.playerDataIditentifier.coordinate_XIdetentifier, (float)playerGameData.playerDataIditentifier.coordinate_YIdetentifier);
            //读取玩家的存档的金币数量
            ChangeCoin(playerGameData.playerDataIditentifier.coinIdetentifier);
        }
    }

}
