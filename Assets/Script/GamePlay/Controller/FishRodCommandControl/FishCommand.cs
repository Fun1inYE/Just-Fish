using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ִ�е��㶯��������
/// </summary>
public interface IFishCommand
{
    /// <summary>
    /// ִ������ķ���
    /// </summary>
    public void Execute();
}

/// <summary>
/// �ó���͵���
/// </summary>
public class TakeFishRodCommand : IFishCommand
{
    /// <summary>
    /// ��͵�transform
    /// </summary>
    public Transform fishRodPointTransform;
    /// <summary>
    /// �ж��Ƿ��ó������
    /// </summary>
    public bool takeFishRode;

    /// <summary>
    /// ���캯��
    /// </summary>
    public TakeFishRodCommand(Transform fishRodPointTransform, bool takeFishRode)
    {
        this.fishRodPointTransform = fishRodPointTransform;
        this.takeFishRode = takeFishRode;
    }

    /// <summary>
    /// �л����������״̬
    /// </summary>
    public void Execute()
    {
        fishRodPointTransform.gameObject.SetActive(takeFishRode);
    }
}

/// <summary>
/// �׸͵���
/// </summary>
public class CastingRodCommand : IFishCommand
{
    /// <summary>
    /// ��Ư��Transform
    /// </summary>
    public Transform driftTransform;
    /// <summary>
    /// ��Ư��RigidBody
    /// </summary>
    public Rigidbody2D driftRb;
    /// <summary>
    /// ���ߵ�Transform
    /// </summary>
    public Transform rodWireTransform;
    /// <summary>
    /// �׸͵�������Ĭ��Ϊ3��
    /// </summary>
    public float castingRodForce = 50f;

    /// <summary>
    /// ���캯��
    /// </summary>
    public CastingRodCommand(Transform driftTransform, Rigidbody2D driftRb, Transform rodWireTransform, float castingRodForce)
    {
        this.driftTransform = driftTransform;
        this.driftRb = driftRb;
        this.rodWireTransform = rodWireTransform;
        this.castingRodForce = castingRodForce;
    }

    /// <summary>
    /// ִ������
    /// </summary>
    public void Execute()
    {
        //�Ȼ�ȡ�����������Ļλ��
        Vector3 mouseClickPoint = Input.mousePosition;
        //ȷ���������λ�õ�z���������������������
        mouseClickPoint.z = Camera.main.nearClipPlane;
        //���������λ��ת������������
        Vector3 mouseClickWroldPoint = Camera.main.ScreenToWorldPoint(mouseClickPoint);
        //���������λ�õ�z��ת����0
        mouseClickWroldPoint.z = 0;
        // �����ĵ�Ϊ�ο��������������
        Vector3 relativePosition = (mouseClickWroldPoint - rodWireTransform.position).normalized;

        //�Ƚ���ƯgameObject����
        driftTransform.gameObject.SetActive(true);
        //��һ�������ķ������
        driftRb.AddForce(relativePosition * castingRodForce, ForceMode2D.Impulse);
    }
}

/// <summary>
/// ����͵���
/// </summary>
public class ReelInRodCommand : IFishCommand
{
    /// <summary>
    /// ���ߵ�Transform
    /// </summary>
    public Transform rodWireTransform;
    /// <summary>
    /// ��Ư��RigidBody
    /// </summary>
    public Rigidbody2D driftRb;
    /// <summary>
    /// ��Ư��Transform
    /// </summary>
    public Transform driftTransform;
    /// <summary>
    /// �ո��ӵ�����
    /// </summary>
    public float roolInRodForce = 5f;

    /// <summary>
    /// ���캯��
    /// </summary>
    public ReelInRodCommand(Transform rodWireTransform, Transform driftTransform, Rigidbody2D driftRb)
    {
        this.rodWireTransform = rodWireTransform;
        this.driftTransform = driftTransform;
        this.driftRb = driftRb;
    }

    /// <summar>
    /// ִ������
    /// </summary>
    public void Execute()
    {
        //RigidBody��Ϊ��
        if(driftRb != null && rodWireTransform != null)
        {
            //�ո�֮ǰ���Ȱ��ٶȹرգ�ȷ���ո˵�ʱ��ֻ�������ͷ����
            driftRb.velocity = new Vector2(0, 0);
            //����Ư�������ر�
            driftRb.gravityScale = 0f;
            //����Ưʩ��һ����
            driftRb.AddForce(Vector2.up * roolInRodForce, ForceMode2D.Impulse);
        }

    }
}
