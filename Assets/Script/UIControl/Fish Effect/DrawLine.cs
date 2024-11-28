using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画一条链接两个GameObject的线
/// </summary>
public class DrawLine : MonoBehaviour
{
    /// <summary>
    /// 鱼竿线的尽头点
    /// </summary>
    public Transform wirePointTransform;
    /// <summary>
    /// 鱼漂链接线的点
    /// </summary>
    public Transform driftPointTransform;
    /// <summary>
    /// 画线的变量
    /// </summary>
    public LineRenderer lineRenderer { get; set; }
    /// <summary>
    /// 判断是否可以画线（默认为false）
    /// </summary>
    public bool canDrawing = false;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("lineRenderer是空的，请检查Hierarchy窗口！");
        }
    }

    /// <summary>
    /// 初始化画线的方法
    /// </summary>
    public void InitLineRenderer()
    {
        GameObject player = SetGameObjectToParent.FindFromFirstLayer("Player");
        //获取两个点位
        wirePointTransform = ComponentFinder.GetChildComponent<Transform>(player, "FishRodPoint").transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        driftPointTransform = ComponentFinder.GetChildComponent<Transform>(player, "DriftPoint").transform.GetChild(0).GetComponent<Transform>();
    }

    /// <summary>
    /// 画线的方法
    /// </summary>
    /// <param name="canDrawing">是否可以画线</param>
    public void DrawFishingLine()
    {
        //开始画线 
        if (wirePointTransform != null && driftPointTransform != null && canDrawing == true)
        {
            // 设置LineRenderer的顶点数
            lineRenderer.positionCount = 2;
            // 设置线宽
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;

            lineRenderer.SetPosition(0, wirePointTransform.position);
            lineRenderer.SetPosition(1, driftPointTransform.position);
        }
        //停止画线
        if(canDrawing == false)
        {
            lineRenderer.positionCount = 0;
        }

    }
}
