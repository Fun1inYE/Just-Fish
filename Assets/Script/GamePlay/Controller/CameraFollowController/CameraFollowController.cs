using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

/// <summary>
/// 相机跟随的脚本
/// </summary>
public class CameraFollowController : MonoBehaviour
{
    /// <summary>
    /// 获取到总控制器，主要是为了获取到总控制器中的玩家位置
    /// </summary>
    TotalController totalController;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        enabled = false;
    }

    /// <summary>
    /// 注册相机跟随的方法
    /// </summary>
    public void RegisterCameraFollowController()
    {
        enabled = true;
    }

    /// <summary>
    /// 摄像机跟随放在LateUpdate
    /// </summary>
    public void LateUpdate()
    {
        //只获取玩家的x轴的二维坐标
        Vector3 newPosition = new Vector3(totalController.movePlayerController.playerRb.position.x, transform.position.y, transform.position.z);
        //将位置赋予给摄像机
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5f);
    }
}
