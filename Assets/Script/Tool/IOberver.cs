using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �۲���ģʽ�ı��۲��ߵĽӿ�
/// </summary>
public interface IUIObserver
{
    /// <summary>
    /// ��UIʱҪ���õķ���
    /// </summary>
    public void OnOpenUI();
    /// <summary>
    /// �ر�UiҪ���õķ���
    /// </summary>
    public void OnCloseUI();
}

/// <summary>
/// ������صĿ���ʱ�ķ���
/// </summary>
public interface IInventoyControllerObserver : IUIObserver
{
    
}

/// <summary>
/// �̵�ҳ����صĿ��Ƶķ���
/// </summary>
public interface IStoreControllerObserver : IUIObserver
{
    
}

/// <summary>
/// ��Ϸ�������Ľ�����Ϸ�۲���ģʽ�Ľӿ�
/// </summary>
public interface IEndGameObserver
{
    /// <summary>
    /// ������Ϸ֮��Ҫ�㲥�ķ���
    /// </summary>
    public void OnEndGame();
}