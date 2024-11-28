using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 挂载到鱼漂的脚本
/// </summary>
public class Drift : MonoBehaviour
{
    /// <summary>
    /// 鱼漂的位置
    /// </summary>
    public Transform driftTransform;
    /// <summary>
    /// 鱼漂的碰撞体
    /// </summary>
    public Collider2D driftCollider;
    /// <summary>
    /// 鱼漂的RigidBody
    /// </summary>
    public Rigidbody2D driftRb;
    /// <summary>
    /// 要检测的水Layer层
    /// </summary>
    public LayerMask waterLayer;
    /// <summary>
    /// 判断鱼漂是否碰到水(默认为false)
    /// </summary>
    public bool isTouchWater = false;
    /// <summary>
    /// 判断鱼漂是否可以吸附（一个是状态机里能更改，还有一个位置就是钓鱼控制器）
    /// </summary>
    public bool canAdsorb = false;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        driftTransform = GetComponent<Transform>();
        driftCollider = GetComponent<Collider2D>();
        driftRb = GetComponent<Rigidbody2D>();
        waterLayer = LayerMask.GetMask("Water");
    }

    public void Update()
    {
        //鱼漂的射线检测
        RayCastHitWithWater();
    }

    ///// <summary>
    ///// 鱼漂吸附渔竿头的方法
    ///// </summary>
    //public void MoveToTarget()
    //{
    //    //driftRb.velocity = Vector3.zero;
    //    driftTransform.position = Vector2.MoveTowards(driftTransform.position, fishRodScript.wireTransform.position, speed * Time.deltaTime);
    //}

    /// <summary>
    /// 基于射线检测的鱼漂是否接触到水了
    /// </summary>
    public void RayCastHitWithWater()
    {
        //让射线只检测waterLayer层
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.09f, waterLayer);
        if (hit.collider != null && hit.collider.CompareTag("Water"))
        {
            if(!isTouchWater)
            {
                Debug.Log("鱼漂接触到水了");
                isTouchWater = true;
            }
        }
        else
        {
            if (isTouchWater)
            {
                Debug.Log("鱼漂离开水了");
                isTouchWater = false;
            }
        }
    }

    //在场景视图中绘制 Gizmos 来可视化射线
    //private void OnDrawGizmos()
    //{
    //     设置 Gizmos 的颜色
    //    Gizmos.color = Color.red;

    //     从物体的当前位置向下绘制射线（仅在场景视图中显示）
    //    Gizmos.DrawRay(transform.position, Vector2.down * 0.09f);
    //}

    /// <summary>
    /// 碰撞检测
    /// </summary>
    /// <param name = "collision" ></ param >
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Water"))
    //    {
    //        Debug.Log("鱼漂接触到水了");
    //        isTouchWater = true;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Water"))
    //    {
    //        Debug.Log("鱼漂离开水了");
    //        isTouchWater = false;
    //    }
    //}
}
