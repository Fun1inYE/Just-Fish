using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��͵���
/// </summary>
public class FishRod : MonoBehaviour
{
    /// <summary>
    /// ��ȡ����͵�Transform
    /// </summary>
    public Transform fishRodTransform;
    /// <summary>
    /// �ߵ�Transform
    /// </summary>
    public Transform wireTransform;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        fishRodTransform = GetComponent<Transform>();
        wireTransform = transform.GetChild(0).GetComponent<Transform>();
    }
}
