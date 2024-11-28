using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景跟随效果管理器
/// </summary>
public class BackGroundFollowController_MainSence : MonoBehaviour
{
    /// <summary>
    /// 获取到摄像机的位置
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
    /// 获取到cloud
    /// </summary>
    public GameObject cloud;
    /// <summary>
    /// 云的移动速度
    /// </summary>
    public float cloudMoveSpeed = 5f;

    /// <summary>
    /// 获取玩家位置的组件
    /// </summary>
    public Transform playerTransform;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        cameraTransform = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<Transform>();
        playerTransform = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<Transform>();
        prop = SetGameObjectToParent.FindFromFirstLayer("Prop");
        cloud = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "Cloud").gameObject;
        background = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "BackGround").gameObject;
    }

    public void Update()
    {
        CloudFollow();
    }

    /// <summary>
    /// 云彩飘动和跟随背景的效果
    /// </summary>
    public void CloudFollow()
    {
        //获取到背景和云彩的x轴的距离
        float distance = Vector2.Distance(new Vector2(background.transform.position.x, 0f), new Vector2(cloud.transform.position.x, 0f));

        //如果距离超过原始距离就回到原始位置
        if (distance >= background.GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            //判断玩家与云彩的相对位置，可以判断出玩家是走到了左边边缘，或者右边边缘
            if (playerTransform.position.x < cloud.transform.position.x)
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
