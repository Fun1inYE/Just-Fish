using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��״̬����
/// </summary>
public class TotalMachine : MonoBehaviour
{
    /// <summary>
    /// ���õ���״̬��
    /// </summary>
    public FishingStateMachine fishingStateMachine;
    /// <summary>
    /// ������Ϸ��UI��״̬��
    /// </summary>
    public GameUIStateMachine gameUIStateMachine;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishingStateMachine = GetComponent<FishingStateMachine>();
        gameUIStateMachine = GetComponent<GameUIStateMachine>();
    }

    /// <summary>
    /// ��������״̬��
    /// </summary>
    public void StartAllMachine()
    {
        fishingStateMachine.StartMachine();
        gameUIStateMachine.StartMachine();
    }

    /// <summary>
    /// ֹͣ����״̬��
    /// </summary>
    public void StopAllMachine()
    {
        fishingStateMachine.StopMachine();
        gameUIStateMachine.StopMachine();
    }
}
