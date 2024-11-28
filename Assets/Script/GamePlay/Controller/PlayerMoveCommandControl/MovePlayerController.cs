using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlayerController : MonoBehaviour, IController
{
    /// <summary>
    /// 获取到玩家的Transform
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// 获取到玩家的RigidBody
    /// </summary>
    public Rigidbody2D playerRb;
    /// <summary>
    /// 获取到玩家的动画控制器
    /// </summary>
    public Animator playerAnimator;

    /// <summary>
    /// 玩家UI支撑点
    /// </summary>
    public Transform propUIPoint;
    /// <summary>
    /// 钓鱼指示器支撑点
    /// </summary>
    public Transform fishIndicatorPoint;
    /// <summary>
    /// 文字提示UI的支撑点
    /// </summary>
    public Transform TextUIPoint;

    /// <summary>
    /// 玩家的移动速度
    /// </summary>
    public float playerSpeed = 2f;
    /// <summary>
    /// 玩家的跳跃力量
    /// </summary>
    public float jumpForce = 3f;
    /// <summary>
    /// 检测地面的点
    /// </summary>
    public Transform checkGroundPoint;
    /// <summary>
    /// 检测射线的长度(默认0.1)
    /// </summary>
    public float radius = 0.01f;
    /// <summary>
    /// 储存指令的列表
    /// </summary>
    public List<IMoveCommand> commandList;
    /// <summary>
    /// player是否面朝右面
    /// </summary>
    bool isFaceToRight = true;

    /// <summary>
    /// 来自于ICommand接口，判断该控制器是否能运行
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    //左岸边传送点
    public Transform transformPoint_Left;

    //右岸边传送点
    public Transform transformPoint_Right;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        //获取到player的GameObject
        playerTransform = GetComponent<Transform>();
        if(playerTransform == null)
        {
            Debug.LogError("playerTransform是空的，请检查代码！");
        }

        propUIPoint = ComponentFinder.GetChildComponent<Transform>(gameObject, "PropUIPoint");
        fishIndicatorPoint = ComponentFinder.GetChildComponent<Transform>(gameObject, "FishIndicatorPoint");
        TextUIPoint = ComponentFinder.GetChildComponent<Transform>(gameObject, "TextUIPoint");

        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            Debug.LogError("playerRb是空的，请检查代码！");
        }

        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("playerAnimator是空的，请检查代码！");
        }

        //获取检测地面的点
        checkGroundPoint = transform.Find("CheckGroundPoint");
        if (checkGroundPoint == null)
        {
            Debug.LogError("checkGroundPoint是空的，请检查代码！");
        }

        //获取岸边传送点
        transformPoint_Left = SetGameObjectToParent.FindChildRecursive(SetGameObjectToParent.FindFromFirstLayer("Prop").transform, "TransformPoint_Left").GetComponent<Transform>();
        transformPoint_Right = SetGameObjectToParent.FindChildRecursive(SetGameObjectToParent.FindFromFirstLayer("Prop").transform, "TransformPoint_Right").GetComponent<Transform>();

        commandList = new List<IMoveCommand>();

    }

    private void FixedUpdate()
    {

        //调用移动玩家的方法
        MovePlayer();
        //调用移动玩家的方法
        Jump();
        //检测玩家是否掉入水中
        CheckPlayerInWater();

    }

    /// <summary>
    /// 移动玩家的方法
    /// </summary>
    public void MovePlayer()
    {
        //-1为负方向，1为正方向
        float moveInput = 0;
        if (!canRun)
        {
            moveInput = 0;
        }
        
        else if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }
        playerRb.velocity = new Vector2(moveInput * playerSpeed, playerRb.velocity.y);

        //判断Player的GameObject是否需要左右翻转
        ClipPlayer();
    }

    public void Jump()
    {
        //判断玩家是否按下跳跃键
        if (Input.GetKey(KeyCode.W) && canRun)
        {
            //使用射线检测检测从checkGroundPoint为中心或一个圆形，这个圆形是否跟LayerGround重合
            bool isGround = Physics2D.OverlapCircle(checkGroundPoint.position, radius, LayerMask.GetMask("Ground"));
            Vector2 directForce = Vector2.up * jumpForce;

            //当Player的y轴速度小于0.01f的时候才能起跳, 并且玩家处在地面上
            if (playerRb.velocity.y < 0.01f && isGround)
            {
                //添加一个瞬间向上的力
                playerRb.AddForce(directForce, ForceMode2D.Impulse);
            }
        }
    }

    /// <summary>
    /// 检测玩家是否掉进了水里，如果掉进去了就传送玩家
    /// </summary>
    public void CheckPlayerInWater()
    {
        //检测玩家是否掉进水里（默认false）
        bool isInWater = false;

        if(transform.position.x < transformPoint_Left.position.x)
        {
            //使用射线检测检测从checkGroundPoint为中心或一个圆形，这个圆形是否跟LayerGround重合
            isInWater = Physics2D.OverlapCircle(checkGroundPoint.position, radius, LayerMask.GetMask("Water"));
            //传送回左岸边
            if (isInWater)
            {
                transform.position = transformPoint_Left.position;
            }
        }

        if (transform.position.x > transformPoint_Right.position.x)
        {
            //使用射线检测检测从checkGroundPoint为中心或一个圆形，这个圆形是否跟LayerGround重合
            isInWater = Physics2D.OverlapCircle(checkGroundPoint.position, radius, LayerMask.GetMask("Water"));
            //传送回右岸边
            if (isInWater)
            {
                transform.position = transformPoint_Right.position;
            }
        }
    }

    /// <summary>
    /// 根据速度方向和bool值水平翻转玩家
    /// </summary>
    public void ClipPlayer()
    {
        //判断条件为如果人物的速度大于0（马上就要向右走了），但是面朝左，就给人物转方向，同理另外一个方向也一样
        if((playerRb.velocity.x > 0 && !isFaceToRight) || playerRb.velocity.x < 0 && isFaceToRight)
        {
            isFaceToRight = !isFaceToRight;
            Vector3 local = playerTransform.localScale;
            local.x = local.x * -1;
            playerTransform.localScale = local;
        }
    }

    /// <summary>
    /// 切换动画方法
    /// </summary>
    public void SwitchAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRb.velocity.x));
    }
}
