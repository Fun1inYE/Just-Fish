using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������������ݺ�UI�Ŀ�������
/// </summary>
public class PlayerDataAndUIController : MonoBehaviour
{
    /// <summary>
    /// �����������
    /// </summary>
    public PlayerData playerData;
    /// <summary>
    /// ���ù���������ݵ�UI
    /// </summary>
    public PlayerUI playerUI;
    /// <summary>
    /// ������Ϸ�浵�����Player����
    /// </summary>
    public PlayerGameData playerGameData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //��ʼ��PlayerData
        playerData = new PlayerData();
        //��ʼ��PlayerUI
        playerUI = GetComponent<PlayerUI>();
        //��ʼ����Ϸ�浵���������
        playerGameData = new PlayerGameData();
    }

    /// <summary>
    /// ��Awake����֮��ִ��
    /// </summary>
    public void Start()
    {
        //����ʼ����PlayerUI��ֵ����PlayerUI.playerData
        playerUI.SetPlayerData(playerData);
    }

    /// <summary>
    /// �ı�������ֵķ���
    /// </summary>
    /// <param name="name"></param>
    public void ChangePlayerName(string name)
    {
        playerData.NodifyPlayerName(name);
    }

    /// <summary>
    /// �ı��ҵķ���
    /// </summary>
    public void ChangeCoin(int coinAmount)
    {
        playerData.NodifyCoin(coinAmount);
    }

    /// <summary>
    /// �ı侭��ķ���
    /// </summary>
    public void ChangeExperience(int experience)
    {
        playerData.NodifyExperience(experience);
    }

    /// <summary>
    /// �ı�������ʾ�����Ľ������
    /// </summary>
    /// <param name="sallingCoin"></param>
    public void ChangeSallingCoin(int sallingCoin)
    {
        playerData.NodifySalling(sallingCoin);
    }

    /// <summary>
    /// ����ҵĽ����������
    /// </summary>
    public void ChangeSallingCoinToZero()
    {
        playerData.NodifySallingToZero();
    }
    
    /// <summary>
    /// �����������
    /// </summary>
    public void SaveData()
    {
        if(playerGameData != null)
        {
            //����ҵ����ִ�������
            playerGameData.playerDataIditentifier.playerNameIdetentifier = playerData.name;
            //����ҵ�x�����긳��X���ʶ����
            playerGameData.playerDataIditentifier.coordinate_XIdetentifier = gameObject.transform.position.x;
            //����ҵ�y�����긴�Ƶ�Y���ʶ����
            playerGameData.playerDataIditentifier.coordinate_YIdetentifier = gameObject.transform.position.y;
            //����ҵĽ���������д洢
            playerGameData.playerDataIditentifier.coinIdetentifier = playerData.coin;
            //�洢�������
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "playerGameDataIdentifier.sav", playerGameData.playerDataIditentifier);
        }
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    public void LoadData()
    {
        if (playerGameData != null)
        {
            //��ȡ�浵�����е���ұ�ʶ��������
            playerGameData.playerDataIditentifier = SaveManager.Instance.Load<PlayerDataIditentifier>(GameManager.Instance.gameData.archiveSaveName, "playerGameDataIdentifier.sav");
            //��ȡ��ҵ�����
            ChangePlayerName(playerGameData.playerDataIditentifier.playerNameIdetentifier);
            //��ȡ������겢��ֵ
            gameObject.transform.position = new Vector2((float)playerGameData.playerDataIditentifier.coordinate_XIdetentifier, (float)playerGameData.playerDataIditentifier.coordinate_YIdetentifier);
            //��ȡ��ҵĴ浵�Ľ������
            ChangeCoin(playerGameData.playerDataIditentifier.coinIdetentifier);
        }
    }

}
