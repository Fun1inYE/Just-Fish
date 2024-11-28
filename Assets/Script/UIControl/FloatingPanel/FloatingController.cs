using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���������ܿ�����
/// </summary>
public class FloatingController : MonoBehaviour
{
    /// <summary>
    /// ���������������Ľű�
    /// </summary>
    public FishItemFloating fishItemFloatingScript;
    /// <summary>
    /// ���ù����������Ľű�
    /// </summary>
    public ToolItemFloating toolItemFloatingScript;
    /// <summary>
    /// ���õ����������Ľű�
    /// </summary>
    public PropItemFloating propItemFloatingScript;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void InitializeFloatingPanel()
    {
        fishItemFloatingScript = transform.Find("FishItemFloatingPanel").GetComponent<FishItemFloating>();
        toolItemFloatingScript = transform.Find("ToolItemFloatingPanel").GetComponent<ToolItemFloating>();
        //��ʼ��toolItemFloatingScript
        toolItemFloatingScript.InitializeToolItemFloating();
        propItemFloatingScript = transform.Find("PropItemFloatingPanel").GetComponent<PropItemFloating>();
        propItemFloatingScript.InitializePropItemFloating();

    }

    /// <summary>
    /// �ر������������
    /// </summary>
    public void CloseAllFloating()
    {
        fishItemFloatingScript.gameObject.SetActive(false);
        toolItemFloatingScript.gameObject.SetActive(false);
        propItemFloatingScript.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// ����ĳһ�������壨�����Ļ�Ҳ�����ö��б�����
    /// </summary>
    public void OpenFloating(bool isFishItemFloatingOpen, bool isToolItemFloatingOpen, bool isPropItemFloatingOpen)
    {
        fishItemFloatingScript.gameObject.SetActive(isFishItemFloatingOpen);
        toolItemFloatingScript.gameObject.SetActive(isToolItemFloatingOpen);
        propItemFloatingScript.gameObject.SetActive(isPropItemFloatingOpen);
    }
}
