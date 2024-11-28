using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���ص���Ư�Ľű�
/// </summary>
public class Drift : MonoBehaviour
{
    /// <summary>
    /// ��Ư��λ��
    /// </summary>
    public Transform driftTransform;
    /// <summary>
    /// ��Ư����ײ��
    /// </summary>
    public Collider2D driftCollider;
    /// <summary>
    /// ��Ư��RigidBody
    /// </summary>
    public Rigidbody2D driftRb;
    /// <summary>
    /// Ҫ����ˮLayer��
    /// </summary>
    public LayerMask waterLayer;
    /// <summary>
    /// �ж���Ư�Ƿ�����ˮ(Ĭ��Ϊfalse)
    /// </summary>
    public bool isTouchWater = false;
    /// <summary>
    /// �ж���Ư�Ƿ����������һ����״̬�����ܸ��ģ�����һ��λ�þ��ǵ����������
    /// </summary>
    public bool canAdsorb = false;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        driftTransform = GetComponent<Transform>();
        driftCollider = GetComponent<Collider2D>();
        driftRb = GetComponent<Rigidbody2D>();
        waterLayer = LayerMask.GetMask("Water");
    }

    public void Update()
    {
        //��Ư�����߼��
        RayCastHitWithWater();
    }

    ///// <summary>
    ///// ��Ư�������ͷ�ķ���
    ///// </summary>
    //public void MoveToTarget()
    //{
    //    //driftRb.velocity = Vector3.zero;
    //    driftTransform.position = Vector2.MoveTowards(driftTransform.position, fishRodScript.wireTransform.position, speed * Time.deltaTime);
    //}

    /// <summary>
    /// �������߼�����Ư�Ƿ�Ӵ���ˮ��
    /// </summary>
    public void RayCastHitWithWater()
    {
        //������ֻ���waterLayer��
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.09f, waterLayer);
        if (hit.collider != null && hit.collider.CompareTag("Water"))
        {
            if(!isTouchWater)
            {
                Debug.Log("��Ư�Ӵ���ˮ��");
                isTouchWater = true;
            }
        }
        else
        {
            if (isTouchWater)
            {
                Debug.Log("��Ư�뿪ˮ��");
                isTouchWater = false;
            }
        }
    }

    //�ڳ�����ͼ�л��� Gizmos �����ӻ�����
    //private void OnDrawGizmos()
    //{
    //     ���� Gizmos ����ɫ
    //    Gizmos.color = Color.red;

    //     ������ĵ�ǰλ�����»������ߣ����ڳ�����ͼ����ʾ��
    //    Gizmos.DrawRay(transform.position, Vector2.down * 0.09f);
    //}

    /// <summary>
    /// ��ײ���
    /// </summary>
    /// <param name = "collision" ></ param >
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Water"))
    //    {
    //        Debug.Log("��Ư�Ӵ���ˮ��");
    //        isTouchWater = true;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Water"))
    //    {
    //        Debug.Log("��Ư�뿪ˮ��");
    //        isTouchWater = false;
    //    }
    //}
}
