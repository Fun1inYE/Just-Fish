using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

/// <summary>
/// ����ģʽ�ж�������Ľӿ�
/// </summary>
public interface IMoveCommand
{
    /// <summary>
    /// ִ�������õķ���
    /// </summary>
    void Execute();
}

/// <summary>
/// �����ƶ��ķ���
/// </summary>
public class MoveCommand : IMoveCommand
{
    /// <summary>
    /// ��ҵ��ƶ��ٶ�
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// ��ҵ��ƶ�����
    /// </summary>
    public Vector3 direction;
    /// <summary>
    /// ��Ҫ�ٿص�Rigidbody
    /// </summary>
    public Rigidbody2D rb;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="moveSpeed">��ҵ��ƶ��ٶ�</param>
    /// <param name="direction">��ҵ��ƶ�����</param>
    /// <param name="rb">��Ҫ�ٿص�Rigidbody</param>
    public MoveCommand(float moveSpeed, Vector3 direction, Rigidbody2D rb)
    {
        this.moveSpeed = moveSpeed;
        this.direction = direction; 
        this.rb = rb;
    }
    /// <summary>
    /// ִ������ķ���
    /// </summary>
    public void Execute()
    {
        Vector2 force = new Vector2(direction.x * moveSpeed, 0f);
        rb.AddForce(force);
    }
}

/// <summary>
/// ��Ծ�ķ���
/// </summary>
public class JumpCommand : IMoveCommand
{
    /// <summary>
    /// ��Ծ������
    /// </summary>
    public float jumpForce;
    /// <summary>
    /// ��Ҫ�ٿص�Rigidbody
    /// </summary>
    public Rigidbody2D rb;
    
    /// <summary>
    /// ���캯��
    /// </summary>
    public JumpCommand(float jumpForce, Rigidbody2D rb)
    {
        this.rb=rb;
        this.jumpForce = jumpForce;
    }

    /// <summary>
    /// ִ������
    /// </summary>
    public void Execute()
    {
        Vector2 directForce = Vector2.up * jumpForce;
        //��Player��y���ٶ�С��0.01f��ʱ���������
        if(rb.velocity.y < 0.01f)
        {
            //���һ��˲�����ϵ���
            rb.AddForce(directForce, ForceMode2D.Impulse);
        }
    }
}