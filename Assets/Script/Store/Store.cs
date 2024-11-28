using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̵�Ķ�Ӧ��
/// </summary>
public class Store : MonoBehaviour
{
    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// �̵��collider
    /// </summary>
    public Collider2D storeCol;
    /// <summary>
    /// �̵��Ƿ񱻽����ˣ�Ĭ����false��
    /// </summary>
    public bool wasEnterStore = false;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        storeCol = GetComponent<Collider2D>();
        if (storeCol == null)
        {
            Debug.LogError("storeCol�ǿյģ�����Hierarchy����! ");
        }
    }

    /// <summary>
    /// ������ײ���ڴ����ķ���
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //��ҽ����̵귶Χ��
            wasEnterStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //����˳��̵귶Χ��
            wasEnterStore = false;
        }
    }
}
