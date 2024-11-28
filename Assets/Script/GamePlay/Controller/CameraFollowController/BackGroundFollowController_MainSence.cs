using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������Ч��������
/// </summary>
public class BackGroundFollowController_MainSence : MonoBehaviour
{
    /// <summary>
    /// ��ȡ���������λ��
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
    /// ��ȡ��cloud
    /// </summary>
    public GameObject cloud;
    /// <summary>
    /// �Ƶ��ƶ��ٶ�
    /// </summary>
    public float cloudMoveSpeed = 5f;

    /// <summary>
    /// ��ȡ���λ�õ����
    /// </summary>
    public Transform playerTransform;

    /// <summary>
    /// �ű���ʼ��
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
    /// �Ʋ�Ʈ���͸��汳����Ч��
    /// </summary>
    public void CloudFollow()
    {
        //��ȡ���������Ʋʵ�x��ľ���
        float distance = Vector2.Distance(new Vector2(background.transform.position.x, 0f), new Vector2(cloud.transform.position.x, 0f));

        //������볬��ԭʼ����ͻص�ԭʼλ��
        if (distance >= background.GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            //�ж�������Ʋʵ����λ�ã������жϳ�������ߵ�����߱�Ե�������ұ߱�Ե
            if (playerTransform.position.x < cloud.transform.position.x)
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
