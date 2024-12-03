using System.Windows.Input;
using UnityEngine;

/// <summary>
/// �������ӿ�
/// </summary>
public interface IController
{
    /// <summary>
    /// �ж�����̳�������ӿڵĿ������Ƿ��������
    /// </summary>
    public bool canRun { get; set; }
}


/// <summary>
/// �������״̬�����ܿ�����ָ����
/// </summary>
public class ConvertController
{
    /// <summary>
    /// �������״̬��������
    /// </summary>
    public string StateMachineName { get; }
    /// <summary>
    /// �������Ҫ�����Ŀ�����
    /// </summary>
    private readonly IController controller;
    /// <summary>
    /// �������ָ��
    /// </summary>
    private readonly bool enable;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="stateMachineName"></param>
    /// <param name="controller"></param>
    /// <param name="enable"></param>
    public ConvertController(string stateMachineName, IController controller, bool enable)
    {
        StateMachineName = stateMachineName;
        this.controller = controller;
        this.enable = enable;
    }

    /// <summary>
    /// ִ��ָ��
    /// </summary>
    public void Execute()
    {
        controller.canRun = enable;
        Debug.Log($"[ConvertController] {StateMachineName} ���� {controller.GetType().Name}.enabled = {enable}");
    }
}