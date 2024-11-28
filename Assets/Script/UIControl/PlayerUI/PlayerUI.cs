using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ҵ�UI��ʾ��
/// </summary>
public class PlayerUI : MonoBehaviour
{
    /// <summary>
    /// ����Լ��������
    /// </summary>
    public Text playerName;
    /// <summary>
    /// ��������еĽ������
    /// </summary>
    public Text coinAmount;
    /// <summary>
    /// ��ҵľ���ֵ
    /// </summary>
    public Text experienceAmount;
    /// <summary>
    /// ���ڱ���Ԥ����ʾ�Ľ��
    /// </summary>
    public Text sallingCoin;

    /// <summary>
    /// ��ȡ����ҵ�����
    /// </summary>
    public PlayerData playerData;

    /// <summary>
    /// �ű���ʼ��
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
    /// ����Ҫͨ������������playerData���и�ֵ
    /// </summary>
    /// <param name="data"></param>
    public void SetPlayerData(PlayerData data)
    {
        //��playerData���и�ֵ
        playerData = data;
        //��ʼ�������PlayerUI
        UpdateUI();
        //����Ҳ�������ע��
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
    /// ����UI
    /// </summary>
    public void UpdateUI()
    {
        playerName.text = "���֣�" + playerData.name;
        coinAmount.text = "��ң�" + playerData.coin;
        experienceAmount.text = "���飺" + playerData.experience;
        sallingCoin.text = "��ң�" + playerData.sallingCoin;
    }

    /// <summary>
    /// �����������
    /// </summary>
    public void OnDestroy()
    {
        //��ֹ�ڴ�й¶
        playerData.OnPlayerDataChange -= UpdateUI;
    }
}
