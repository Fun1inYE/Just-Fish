using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��һ����������GameObject����
/// </summary>
public class DrawLine : MonoBehaviour
{
    /// <summary>
    /// ����ߵľ�ͷ��
    /// </summary>
    public Transform wirePointTransform;
    /// <summary>
    /// ��Ư�����ߵĵ�
    /// </summary>
    public Transform driftPointTransform;
    /// <summary>
    /// ���ߵı���
    /// </summary>
    public LineRenderer lineRenderer { get; set; }
    /// <summary>
    /// �ж��Ƿ���Ի��ߣ�Ĭ��Ϊfalse��
    /// </summary>
    public bool canDrawing = false;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("lineRenderer�ǿյģ�����Hierarchy���ڣ�");
        }
    }

    /// <summary>
    /// ��ʼ�����ߵķ���
    /// </summary>
    public void InitLineRenderer()
    {
        GameObject player = SetGameObjectToParent.FindFromFirstLayer("Player");
        //��ȡ������λ
        wirePointTransform = ComponentFinder.GetChildComponent<Transform>(player, "FishRodPoint").transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        driftPointTransform = ComponentFinder.GetChildComponent<Transform>(player, "DriftPoint").transform.GetChild(0).GetComponent<Transform>();
    }

    /// <summary>
    /// ���ߵķ���
    /// </summary>
    /// <param name="canDrawing">�Ƿ���Ի���</param>
    public void DrawFishingLine()
    {
        //��ʼ���� 
        if (wirePointTransform != null && driftPointTransform != null && canDrawing == true)
        {
            // ����LineRenderer�Ķ�����
            lineRenderer.positionCount = 2;
            // �����߿�
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;

            lineRenderer.SetPosition(0, wirePointTransform.position);
            lineRenderer.SetPosition(1, driftPointTransform.position);
        }
        //ֹͣ����
        if(canDrawing == false)
        {
            lineRenderer.positionCount = 0;
        }

    }
}
