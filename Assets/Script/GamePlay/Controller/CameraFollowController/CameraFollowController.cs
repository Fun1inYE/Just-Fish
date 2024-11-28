using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

/// <summary>
/// �������Ľű�
/// </summary>
public class CameraFollowController : MonoBehaviour
{
    /// <summary>
    /// ��ȡ���ܿ���������Ҫ��Ϊ�˻�ȡ���ܿ������е����λ��
    /// </summary>
    TotalController totalController;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        enabled = false;
    }

    /// <summary>
    /// ע���������ķ���
    /// </summary>
    public void RegisterCameraFollowController()
    {
        enabled = true;
    }

    /// <summary>
    /// ������������LateUpdate
    /// </summary>
    public void LateUpdate()
    {
        //ֻ��ȡ��ҵ�x��Ķ�ά����
        Vector3 newPosition = new Vector3(totalController.movePlayerController.playerRb.position.x, transform.position.y, transform.position.z);
        //��λ�ø���������
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5f);
    }
}
