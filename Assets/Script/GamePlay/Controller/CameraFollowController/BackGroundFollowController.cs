using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景跟随效果管理器
/// </summary>
public class BackGroundFollowController : MonoBehaviour
{
    /// <summary>
    /// 获取到摄像机的Transform
    /// </summary>
    public Transform cameraTransform;

    /// <summary>
    /// 获取到游戏内GameObject的支点
    /// </summary>
    public GameObject prop;
    /// <summary>
    /// 获取到BackGround
    /// </summary>
    public GameObject background;
    /// <summary>
    /// 获取到Sun
    /// </summary>
    public GameObject sun;
    /// <summary>
    /// 获取到cloud
    /// </summary>
    public GameObject cloud;
    /// <summary>
    /// 云的移动速度
    /// </summary>
    public float cloudMoveSpeed = 5f;
    /// <summary>
    /// 引用总控制器用于获取到玩家的位置
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        prop = SetGameObjectToParent.FindFromFirstLayer("Prop");
        cameraTransform = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<Transform>();
        background = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "BackGround").gameObject;
        sun = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "Sun").gameObject;
        cloud = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "Cloud").gameObject;

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        //默认不开始执行生命周期
        enabled = false;
    }

    /// <summary>
    /// 注册初始化背景跟随脚本
    /// </summary>
    public void RegisterBackGroundFollowController()
    {
        enabled = true;
    }

    public void Update()
    {
        CloudFollow();
    }

    public void LateUpdate()
    {
        //背景跟随摄像机
        BackGroundFollow();

        SunFollow();
    }

    /// <summary>
    /// 背景跟随相机的方法
    /// </summary>
    public void BackGroundFollow()
    {
        //获取到摄像机的x轴位置
        Vector2 cameraXposition = new Vector2(cameraTransform.position.x, background.transform.position.y);
        //给背景位置进行赋值
        background.transform.position = cameraXposition;
    }

    /// <summary>
    /// 太阳跟随相机的方法
    /// </summary>
    public void SunFollow()
    {
        //获取到摄像机的x轴位置
        Vector2 cameraXposition = new Vector2(cameraTransform.position.x, background.transform.position.y);
        //给太阳位置进行赋值
        sun.transform.position = cameraXposition;
    }

    /// <summary>
    /// 云彩飘动和跟随背景的效果
    /// </summary>
    public void CloudFollow()
    {   
        //获取到背景和云彩的x轴的距离
        float distance = Vector2.Distance(new Vector2(background.transform.position.x, 0f), new Vector2(cloud.transform.position.x, 0f));
        
        //如果距离超过原始距离就回到原始位置
        if(distance >= background.GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            //判断玩家与云彩的相对位置，可以判断出玩家是走到了左边边缘，或者右边边缘
            if(totalController.movePlayerController.playerTransform.position.x < cloud.transform.position.x)
            {
                //云彩应该回到的位置
                cloud.transform.position = new Vector2(background.transform.position.x - background.GetComponent<SpriteRenderer>().bounds.size.x / 2, cloud.transform.position.y);
            }
            else
            {
                //云彩应该回到的位置
                cloud.transform.position = new Vector2(background.transform.position.x + background.GetComponent<SpriteRenderer>().bounds.size.x / 2, cloud.transform.position.y);
            }
        }

        //云彩飘动的效果
        cloud.transform.Translate(Vector2.left * Time.deltaTime * cloudMoveSpeed);
    }
}
