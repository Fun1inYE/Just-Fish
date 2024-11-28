using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlayerController : MonoBehaviour, IController
{
    /// <summary>
    /// ��ȡ����ҵ�Transform
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// ��ȡ����ҵ�RigidBody
    /// </summary>
    public Rigidbody2D playerRb;
    /// <summary>
    /// ��ȡ����ҵĶ���������
    /// </summary>
    public Animator playerAnimator;

    /// <summary>
    /// ���UI֧�ŵ�
    /// </summary>
    public Transform propUIPoint;
    /// <summary>
    /// ����ָʾ��֧�ŵ�
    /// </summary>
    public Transform fishIndicatorPoint;
    /// <summary>
    /// ������ʾUI��֧�ŵ�
    /// </summary>
    public Transform TextUIPoint;

    /// <summary>
    /// ��ҵ��ƶ��ٶ�
    /// </summary>
    public float playerSpeed = 2f;
    /// <summary>
    /// ��ҵ���Ծ����
    /// </summary>
    public float jumpForce = 3f;
    /// <summary>
    /// ������ĵ�
    /// </summary>
    public Transform checkGroundPoint;
    /// <summary>
    /// ������ߵĳ���(Ĭ��0.1)
    /// </summary>
    public float radius = 0.01f;
    /// <summary>
    /// ����ָ����б�
    /// </summary>
    public List<IMoveCommand> commandList;
    /// <summary>
    /// player�Ƿ��泯����
    /// </summary>
    bool isFaceToRight = true;

    /// <summary>
    /// ������ICommand�ӿڣ��жϸÿ������Ƿ�������
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    //�󰶱ߴ��͵�
    public Transform transformPoint_Left;

    //�Ұ��ߴ��͵�
    public Transform transformPoint_Right;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        //��ȡ��player��GameObject
        playerTransform = GetComponent<Transform>();
        if(playerTransform == null)
        {
            Debug.LogError("playerTransform�ǿյģ�������룡");
        }

        propUIPoint = ComponentFinder.GetChildComponent<Transform>(gameObject, "PropUIPoint");
        fishIndicatorPoint = ComponentFinder.GetChildComponent<Transform>(gameObject, "FishIndicatorPoint");
        TextUIPoint = ComponentFinder.GetChildComponent<Transform>(gameObject, "TextUIPoint");

        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            Debug.LogError("playerRb�ǿյģ�������룡");
        }

        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("playerAnimator�ǿյģ�������룡");
        }

        //��ȡ������ĵ�
        checkGroundPoint = transform.Find("CheckGroundPoint");
        if (checkGroundPoint == null)
        {
            Debug.LogError("checkGroundPoint�ǿյģ�������룡");
        }

        //��ȡ���ߴ��͵�
        transformPoint_Left = SetGameObjectToParent.FindChildRecursive(SetGameObjectToParent.FindFromFirstLayer("Prop").transform, "TransformPoint_Left").GetComponent<Transform>();
        transformPoint_Right = SetGameObjectToParent.FindChildRecursive(SetGameObjectToParent.FindFromFirstLayer("Prop").transform, "TransformPoint_Right").GetComponent<Transform>();

        commandList = new List<IMoveCommand>();

    }

    private void FixedUpdate()
    {

        //�����ƶ���ҵķ���
        MovePlayer();
        //�����ƶ���ҵķ���
        Jump();
        //�������Ƿ����ˮ��
        CheckPlayerInWater();

    }

    /// <summary>
    /// �ƶ���ҵķ���
    /// </summary>
    public void MovePlayer()
    {
        //-1Ϊ������1Ϊ������
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

        //�ж�Player��GameObject�Ƿ���Ҫ���ҷ�ת
        ClipPlayer();
    }

    public void Jump()
    {
        //�ж�����Ƿ�����Ծ��
        if (Input.GetKey(KeyCode.W) && canRun)
        {
            //ʹ�����߼�����checkGroundPointΪ���Ļ�һ��Բ�Σ����Բ���Ƿ��LayerGround�غ�
            bool isGround = Physics2D.OverlapCircle(checkGroundPoint.position, radius, LayerMask.GetMask("Ground"));
            Vector2 directForce = Vector2.up * jumpForce;

            //��Player��y���ٶ�С��0.01f��ʱ���������, ������Ҵ��ڵ�����
            if (playerRb.velocity.y < 0.01f && isGround)
            {
                //���һ��˲�����ϵ���
                playerRb.AddForce(directForce, ForceMode2D.Impulse);
            }
        }
    }

    /// <summary>
    /// �������Ƿ������ˮ��������ȥ�˾ʹ������
    /// </summary>
    public void CheckPlayerInWater()
    {
        //�������Ƿ����ˮ�Ĭ��false��
        bool isInWater = false;

        if(transform.position.x < transformPoint_Left.position.x)
        {
            //ʹ�����߼�����checkGroundPointΪ���Ļ�һ��Բ�Σ����Բ���Ƿ��LayerGround�غ�
            isInWater = Physics2D.OverlapCircle(checkGroundPoint.position, radius, LayerMask.GetMask("Water"));
            //���ͻ��󰶱�
            if (isInWater)
            {
                transform.position = transformPoint_Left.position;
            }
        }

        if (transform.position.x > transformPoint_Right.position.x)
        {
            //ʹ�����߼�����checkGroundPointΪ���Ļ�һ��Բ�Σ����Բ���Ƿ��LayerGround�غ�
            isInWater = Physics2D.OverlapCircle(checkGroundPoint.position, radius, LayerMask.GetMask("Water"));
            //���ͻ��Ұ���
            if (isInWater)
            {
                transform.position = transformPoint_Right.position;
            }
        }
    }

    /// <summary>
    /// �����ٶȷ����boolֵˮƽ��ת���
    /// </summary>
    public void ClipPlayer()
    {
        //�ж�����Ϊ���������ٶȴ���0�����Ͼ�Ҫ�������ˣ��������泯�󣬾͸�����ת����ͬ������һ������Ҳһ��
        if((playerRb.velocity.x > 0 && !isFaceToRight) || playerRb.velocity.x < 0 && isFaceToRight)
        {
            isFaceToRight = !isFaceToRight;
            Vector3 local = playerTransform.localScale;
            local.x = local.x * -1;
            playerTransform.localScale = local;
        }
    }

    /// <summary>
    /// �л���������
    /// </summary>
    public void SwitchAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRb.velocity.x));
    }
}
