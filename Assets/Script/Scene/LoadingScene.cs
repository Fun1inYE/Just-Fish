using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ҳ�����
/// </summary>
public class LoadingScene : MonoBehaviour
{
    /// <summary>
    /// ��ȡ������ҳ��
    /// </summary>
    public Transform loadingPanel;
    /// <summary>
    /// ��ȡ�������ı�
    /// </summary>
    public Text loadingText;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        loadingPanel = GetComponent<Transform>();
        loadingText = loadingPanel.Find("LoadingText").GetComponent<Text>();
    }
}
