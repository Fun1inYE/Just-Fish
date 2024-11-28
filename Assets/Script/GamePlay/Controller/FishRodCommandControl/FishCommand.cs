using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 执行钓鱼动作的命令
/// </summary>
public interface IFishCommand
{
    /// <summary>
    /// 执行命令的方法
    /// </summary>
    public void Execute();
}

/// <summary>
/// 拿出鱼竿的类
/// </summary>
public class TakeFishRodCommand : IFishCommand
{
    /// <summary>
    /// 鱼竿的transform
    /// </summary>
    public Transform fishRodPointTransform;
    /// <summary>
    /// 判断是否拿出了鱼竿
    /// </summary>
    public bool takeFishRode;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TakeFishRodCommand(Transform fishRodPointTransform, bool takeFishRode)
    {
        this.fishRodPointTransform = fishRodPointTransform;
        this.takeFishRode = takeFishRode;
    }

    /// <summary>
    /// 切换空手与鱼竿状态
    /// </summary>
    public void Execute()
    {
        fishRodPointTransform.gameObject.SetActive(takeFishRode);
    }
}

/// <summary>
/// 抛竿的类
/// </summary>
public class CastingRodCommand : IFishCommand
{
    /// <summary>
    /// 鱼漂的Transform
    /// </summary>
    public Transform driftTransform;
    /// <summary>
    /// 鱼漂的RigidBody
    /// </summary>
    public Rigidbody2D driftRb;
    /// <summary>
    /// 鱼线的Transform
    /// </summary>
    public Transform rodWireTransform;
    /// <summary>
    /// 抛竿的力量（默认为3）
    /// </summary>
    public float castingRodForce = 50f;

    /// <summary>
    /// 构造函数
    /// </summary>
    public CastingRodCommand(Transform driftTransform, Rigidbody2D driftRb, Transform rodWireTransform, float castingRodForce)
    {
        this.driftTransform = driftTransform;
        this.driftRb = driftRb;
        this.rodWireTransform = rodWireTransform;
        this.castingRodForce = castingRodForce;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    public void Execute()
    {
        //先获取到鼠标点击的屏幕位置
        Vector3 mouseClickPoint = Input.mousePosition;
        //确保鼠标点击的位置的z坐标是相机正切面的最近处
        mouseClickPoint.z = Camera.main.nearClipPlane;
        //将鼠标点击的位置转换成世界坐标
        Vector3 mouseClickWroldPoint = Camera.main.ScreenToWorldPoint(mouseClickPoint);
        //将鼠标点击的位置的z轴转换成0
        mouseClickWroldPoint.z = 0;
        // 以中心点为参考，计算相对坐标
        Vector3 relativePosition = (mouseClickWroldPoint - rodWireTransform.position).normalized;

        //先将鱼漂gameObject启动
        driftTransform.gameObject.SetActive(true);
        //给一个向鼠标的方向的力
        driftRb.AddForce(relativePosition * castingRodForce, ForceMode2D.Impulse);
    }
}

/// <summary>
/// 收鱼竿的类
/// </summary>
public class ReelInRodCommand : IFishCommand
{
    /// <summary>
    /// 鱼线的Transform
    /// </summary>
    public Transform rodWireTransform;
    /// <summary>
    /// 鱼漂的RigidBody
    /// </summary>
    public Rigidbody2D driftRb;
    /// <summary>
    /// 鱼漂的Transform
    /// </summary>
    public Transform driftTransform;
    /// <summary>
    /// 收杆子的力量
    /// </summary>
    public float roolInRodForce = 5f;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ReelInRodCommand(Transform rodWireTransform, Transform driftTransform, Rigidbody2D driftRb)
    {
        this.rodWireTransform = rodWireTransform;
        this.driftTransform = driftTransform;
        this.driftRb = driftRb;
    }

    /// <summar>
    /// 执行命令
    /// </summary>
    public void Execute()
    {
        //RigidBody不为空
        if(driftRb != null && rodWireTransform != null)
        {
            //收杆之前，先把速度关闭，确保收杆的时候只有向鱼竿头的力
            driftRb.velocity = new Vector2(0, 0);
            //将鱼漂的重力关闭
            driftRb.gravityScale = 0f;
            //对鱼漂施加一个力
            driftRb.AddForce(Vector2.up * roolInRodForce, ForceMode2D.Impulse);
        }

    }
}
