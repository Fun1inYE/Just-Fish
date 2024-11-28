using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

/// <summary>
/// 命令模式行动命令类的接口
/// </summary>
public interface IMoveCommand
{
    /// <summary>
    /// 执行命令用的方法
    /// </summary>
    void Execute();
}

/// <summary>
/// 左右移动的方法
/// </summary>
public class MoveCommand : IMoveCommand
{
    /// <summary>
    /// 玩家的移动速度
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 玩家的移动方向
    /// </summary>
    public Vector3 direction;
    /// <summary>
    /// 需要操控的Rigidbody
    /// </summary>
    public Rigidbody2D rb;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="moveSpeed">玩家的移动速度</param>
    /// <param name="direction">玩家的移动方向</param>
    /// <param name="rb">需要操控的Rigidbody</param>
    public MoveCommand(float moveSpeed, Vector3 direction, Rigidbody2D rb)
    {
        this.moveSpeed = moveSpeed;
        this.direction = direction; 
        this.rb = rb;
    }
    /// <summary>
    /// 执行命令的方法
    /// </summary>
    public void Execute()
    {
        Vector2 force = new Vector2(direction.x * moveSpeed, 0f);
        rb.AddForce(force);
    }
}

/// <summary>
/// 跳跃的方法
/// </summary>
public class JumpCommand : IMoveCommand
{
    /// <summary>
    /// 跳跃的力量
    /// </summary>
    public float jumpForce;
    /// <summary>
    /// 需要操控的Rigidbody
    /// </summary>
    public Rigidbody2D rb;
    
    /// <summary>
    /// 构造函数
    /// </summary>
    public JumpCommand(float jumpForce, Rigidbody2D rb)
    {
        this.rb=rb;
        this.jumpForce = jumpForce;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    public void Execute()
    {
        Vector2 directForce = Vector2.up * jumpForce;
        //当Player的y轴速度小于0.01f的时候才能起跳
        if(rb.velocity.y < 0.01f)
        {
            //添加一个瞬间向上的力
            rb.AddForce(directForce, ForceMode2D.Impulse);
        }
    }
}