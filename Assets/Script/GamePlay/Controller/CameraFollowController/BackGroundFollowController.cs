using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������Ч��������
/// </summary>
public class BackGroundFollowController : MonoBehaviour
{
    /// <summary>
    /// ��ȡ���������Transform
    /// </summary>
    public Transform cameraTransform;

    /// <summary>
    /// ��ȡ����Ϸ��GameObject��֧��
    /// </summary>
    public GameObject prop;
    /// <summary>
    /// ��ȡ��BackGround
    /// </summary>
    public GameObject background;
    /// <summary>
    /// ��ȡ��Sun
    /// </summary>
    public GameObject sun;
    /// <summary>
    /// ��ȡ��cloud
    /// </summary>
    public GameObject cloud;
    /// <summary>
    /// �Ƶ��ƶ��ٶ�
    /// </summary>
    public float cloudMoveSpeed = 5f;
    /// <summary>
    /// �����ܿ��������ڻ�ȡ����ҵ�λ��
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        prop = SetGameObjectToParent.FindFromFirstLayer("Prop");
        cameraTransform = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<Transform>();
        background = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "BackGround").gameObject;
        sun = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "Sun").gameObject;
        cloud = SetGameObjectToParent.FindChildBreadthFirst(prop.transform, "Cloud").gameObject;

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        //Ĭ�ϲ���ʼִ����������
        enabled = false;
    }

    /// <summary>
    /// ע���ʼ����������ű�
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
        //�������������
        BackGroundFollow();

        SunFollow();
    }

    /// <summary>
    /// ������������ķ���
    /// </summary>
    public void BackGroundFollow()
    {
        //��ȡ���������x��λ��
        Vector2 cameraXposition = new Vector2(cameraTransform.position.x, background.transform.position.y);
        //������λ�ý��и�ֵ
        background.transform.position = cameraXposition;
    }

    /// <summary>
    /// ̫����������ķ���
    /// </summary>
    public void SunFollow()
    {
        //��ȡ���������x��λ��
        Vector2 cameraXposition = new Vector2(cameraTransform.position.x, background.transform.position.y);
        //��̫��λ�ý��и�ֵ
        sun.transform.position = cameraXposition;
    }

    /// <summary>
    /// �Ʋ�Ʈ���͸��汳����Ч��
    /// </summary>
    public void CloudFollow()
    {   
        //��ȡ���������Ʋʵ�x��ľ���
        float distance = Vector2.Distance(new Vector2(background.transform.position.x, 0f), new Vector2(cloud.transform.position.x, 0f));
        
        //������볬��ԭʼ����ͻص�ԭʼλ��
        if(distance >= background.GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            //�ж�������Ʋʵ����λ�ã������жϳ�������ߵ�����߱�Ե�������ұ߱�Ե
            if(totalController.movePlayerController.playerTransform.position.x < cloud.transform.position.x)
            {
                //�Ʋ�Ӧ�ûص���λ��
                cloud.transform.position = new Vector2(background.transform.position.x - background.GetComponent<SpriteRenderer>().bounds.size.x / 2, cloud.transform.position.y);
            }
            else
            {
                //�Ʋ�Ӧ�ûص���λ��
                cloud.transform.position = new Vector2(background.transform.position.x + background.GetComponent<SpriteRenderer>().bounds.size.x / 2, cloud.transform.position.y);
            }
        }

        //�Ʋ�Ʈ����Ч��
        cloud.transform.Translate(Vector2.left * Time.deltaTime * cloudMoveSpeed);
    }
}
