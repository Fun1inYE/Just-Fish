using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ҵ�������
/// </summary>
public class PlayerData
{
    /// <summary>
    /// ��ҵ��Լ��������(Ĭ������ΪdefaultName)
    /// </summary>
    public string name = "defaultName";
    /// <summary>
    /// ��������е�Ӳ��(Ĭ��Ϊ0)
    /// </summary>
    public int coin = 0;
    /// <summary>
    /// ��ҵľ��飨Ĭ��Ϊ0��
    /// </summary>
    public int experience = 0;
    /// <summary>
    /// ���Ԥ�����Ľ��
    /// </summary>
    public int sallingCoin = 0;

    /// <summary>
    /// ί���¼�������ϵͳ�ø������ݻ���UI��
    /// </summary>
    public event Action OnPlayerDataChange;

    #region TestCode
    public void CheckOnPlayerDataChangeList()
    {
        // ��ȡ�¼���ί�ж���
        var invocationList = OnPlayerDataChange?.GetInvocationList();

        // �������������
        Debug.Log($"ע��ķ�������: {invocationList.Length}");
    }
    #endregion


    /// <summary>
    /// ���ѹ۲���Ҫ������ҵ�������
    /// </summary>
    /// <param name="name"></param>
    public void NodifyPlayerName(string name)
    {
        this.name = name;
        //���ѹ۲���Ҫ�������������
        OnPlayerDataChange?.Invoke();
    }

    /// <summary>
    /// ���ѹ۲��߸���Coin��
    /// </summary>
    /// <param name="coin"></param>
    public void NodifyCoin(int coin)
    {
        this.coin += coin;
        //���ѹ۲��߸ø���������
        OnPlayerDataChange?.Invoke();         
    }

    /// <summary>
    /// ���ѹ۲��߸���experience
    /// </summary>
    /// <param name="experience"></param>
    public void NodifyExperience(int experience)
    {
        this.experience += experience;
        OnPlayerDataChange?.Invoke();
    }

    /// <summary>
    /// ���ѹ۲���Ӧ�ø���sallingCoin
    /// </summary>
    /// <param name="sallingCoin"></param>
    public void NodifySalling(int sallingCoin)
    {
        this.sallingCoin += sallingCoin;
        OnPlayerDataChange?.Invoke();
    }

    /// <summary>
    /// ���ѹ۲���Ӧ�ø���sallingCoin
    /// </summary>
    /// <param name="sallingCoin"></param>
    public void NodifySallingToZero()
    {
        this.sallingCoin = 0;
        OnPlayerDataChange?.Invoke();
    }
}
