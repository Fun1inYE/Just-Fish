using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ư����Ϣ����
/// </summary>
public class FloatingPanel : MonoBehaviour
{
    /// <summary>
    /// ��ȡ��Ư�����ӵ�GameObject
    /// </summary>
    public GameObject floatingPanelObj;
    /// <summary>
    /// Ư�����NameUI
    /// </summary>
    private Text nameUI;
    /// <summary>
    /// ������ϢUI
    /// </summary>
    private Text deputyUI;
    /// <summary>
    /// ����UI
    /// </summary>
    private Text descriptionUI;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void InitFloatingPanelObj()
    {
        if (floatingPanelObj != null)
        {
            nameUI = SetGameObjectToParent.FindChildRecursive(floatingPanelObj.transform, "Name").GetComponent<Text>();
            deputyUI = SetGameObjectToParent.FindChildRecursive(floatingPanelObj.transform, "Deputy").GetComponent<Text>();
            descriptionUI = SetGameObjectToParent.FindChildRecursive(floatingPanelObj.transform, "Description").GetComponent<Text>();
        }
        else
        {
            Debug.LogError("floatingPanelObj�ǿյģ���ٲ�Hierarchy����");
        }
    }

    /// <summary>
    /// ��ȡnameUI
    /// </summary>
    /// <returns></returns>
    public Text GetNameUI()
    {
        return nameUI;
    }

    /// <summary>
    /// ��ȡdeputyUI
    /// </summary>
    /// <returns></returns>
    public Text GetDeputyUI()
    {
        return deputyUI;
    }

    /// <summary>
    /// ��ȡdescriptionUI
    /// </summary>
    /// <returns></returns>
    public Text GetDescriptionUI()
    {
        return descriptionUI;
    }
}
