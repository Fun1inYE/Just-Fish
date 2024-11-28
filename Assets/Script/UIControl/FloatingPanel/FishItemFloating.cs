using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����������������
/// </summary>
public class FishItemFloating : MonoBehaviour
{
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public Text Name { get; set; }
    /// <summary>
    /// ����������ʱ��
    /// </summary>
    public Text FishedTime { get; set; }
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public Text LengthValue { get; set; }
    /// <summary>
    /// ��ʾ����
    /// </summary>
    public Text WeightValue { get; set; }

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        if (Name == null)
            Debug.LogError("Name�ǿյģ�������룡");

        FishedTime = SetGameObjectToParent.FindChildBreadthFirst(Name.gameObject.transform, "fishedTime").GetComponent<Text>();
        if (FishedTime == null)
            Debug.LogError("FishedTime�ǿյģ�������룡");

        LengthValue = SetGameObjectToParent.FindChildBreadthFirst(transform.Find("Length").gameObject.transform, "LengthValue").GetComponent<Text>();
        if (LengthValue == null)
            Debug.LogError("Length�ǿյģ�������룡");

        WeightValue = SetGameObjectToParent.FindChildBreadthFirst(transform.Find("Weight").gameObject.transform, "WeightValue").GetComponent<Text>();
        if (WeightValue == null)
            Debug.LogError("Weight�ǿյģ�������룡");
    }
}
