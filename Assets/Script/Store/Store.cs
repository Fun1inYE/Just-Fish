using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店的对应类
/// </summary>
public class Store : MonoBehaviour
{
    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 商店的collider
    /// </summary>
    public Collider2D storeCol;
    /// <summary>
    /// 商店是否被进入了（默认是false）
    /// </summary>
    public bool wasEnterStore = false;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        storeCol = GetComponent<Collider2D>();
        if (storeCol == null)
        {
            Debug.LogError("storeCol是空的，请检查Hierarchy窗口! ");
        }
    }

    /// <summary>
    /// 进入碰撞体内触发的方法
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //玩家进入商店范围了
            wasEnterStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //玩家退出商店范围了
            wasEnterStore = false;
        }
    }
}
