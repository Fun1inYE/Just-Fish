using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家的UI显示类
/// </summary>
public class PlayerUI : MonoBehaviour
{
    /// <summary>
    /// 玩家自己起的名字
    /// </summary>
    public Text playerName;
    /// <summary>
    /// 玩家所持有的金币数量
    /// </summary>
    public Text coinAmount;
    /// <summary>
    /// 玩家的经验值
    /// </summary>
    public Text experienceAmount;
    /// <summary>
    /// 正在被卖预定显示的金币
    /// </summary>
    public Text sallingCoin;

    /// <summary>
    /// 获取到玩家的数据
    /// </summary>
    public PlayerData playerData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        GameObject InventoryCanvas = SetGameObjectToParent.FindFromFirstLayer("InventoryCanvas");
        playerName = ComponentFinder.GetChildComponent<Text>(InventoryCanvas, "PlayerName");
        coinAmount = ComponentFinder.GetChildComponent<Text>(InventoryCanvas, "Property");
        experienceAmount = ComponentFinder.GetChildComponent<Text>(InventoryCanvas, "Experience");
        sallingCoin = ComponentFinder.GetChildComponent<Text>(InventoryCanvas, "SaleCoin");
    }

    /// <summary>
    /// 这里要通过控制器来对playerData进行赋值
    /// </summary>
    /// <param name="data"></param>
    public void SetPlayerData(PlayerData data)
    {
        //对playerData进行赋值
        playerData = data;
        //初始化或更新PlayerUI
        UpdateUI();
        //对玩家操作进行注册
        playerData.OnPlayerDataChange += UpdateUI;
    }

    #region TestCode
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Z))
    //        playerData.CheckOnPlayerDataChangeList();
    //}
    #endregion

    /// <summary>
    /// 更新UI
    /// </summary>
    public void UpdateUI()
    {
        playerName.text = "名字：" + playerData.name;
        coinAmount.text = "金币：" + playerData.coin;
        experienceAmount.text = "经验：" + playerData.experience;
        sallingCoin.text = "金币：" + playerData.sallingCoin;
    }

    /// <summary>
    /// 如果对象被销毁
    /// </summary>
    public void OnDestroy()
    {
        //防止内存泄露
        playerData.OnPlayerDataChange -= UpdateUI;
    }
}
