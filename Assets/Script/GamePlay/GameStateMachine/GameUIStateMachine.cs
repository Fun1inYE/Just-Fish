using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ϷUI״̬��
/// </summary>
public class GameUIStateMachine : MonoBehaviour
{
    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// ������Ϸ״̬
    /// </summary>
    public GameUIState gameState;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        //�ű���ʼ��ʱĬ�ϲ���ִ��Update�ȷ���
        enabled = false;

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// ����״̬���ķ���
    /// </summary>
    public void StartMachine()
    {
        //Ĭ�Ͻ������״̬
        gameState = new isGaming();
        //��״̬����״̬��
        SetGameState(gameState);
        enabled = true;
    }

    /// <summary>
    /// ֹͣ״̬��
    /// </summary>
    public void StopMachine()
    {
        //�Ƚ�״̬ת�ص��������״̬
        SetGameState(new isGaming());
        //��ͣ״̬��������
        enabled = false;
    }

    /// <summary>
    /// ת��״̬�ķ���
    /// </summary>
    /// <param name="gameState"></param>
    public void SetGameState(GameUIState gameState)
    {
        this.gameState?.OnExit(this);
        this.gameState = gameState;
        this.gameState?.OnEnter(this);
    }

    /// <summary>
    /// �ṩ�ӿڣ����ⲿ��ѯ��ǰ״̬
    /// </summary>
    /// <returns>���ص�ǰ�����״̬</returns>
    public string CheckStatus()
    {
        return gameState.GetType().Name;
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void Update()
    {
        this.gameState?.OnUpdate(this);
    }

    public void LateUpdate()
    {
        this.gameState?.OnLateUpdate(this);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(CheckStatus());
        }
    }
}
