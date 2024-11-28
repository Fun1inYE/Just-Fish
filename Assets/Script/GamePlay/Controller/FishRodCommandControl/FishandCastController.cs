using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ִ�е�������ĵ�������Ŀ�����
/// </summary>
public class FishandCastController : MonoBehaviour, IController
{
    /// <summary>
    /// ��͵Ľű�
    /// </summary>
    public FishRod fishRodScript;
    /// <summary>
    /// �����Ľű�
    /// </summary>
    public Drift driftScript;
    /// <summary>
    /// �ж��Ƿ��ʼ�������(Ĭ��false)
    /// </summary>
    public bool isInitFishRod = false;
    /// <summary>
    /// �ж��Ƿ��ʼ��������(Ĭ��false)
    /// </summary>
    public bool isInitDrift = false;
    /// <summary>
    /// �ж��Ƿ��������ͣ�Ĭ��Ϊtrue��
    /// </summary>
    public bool canPutAwayRod = true;
    /// <summary>
    /// �ж��Ƿ��ó����(Ĭ��Ϊfalse)
    /// </summary>
    public bool takeFishRode = false;
    /// <summary>
    /// �ж��Ƿ��׸�(Ĭ��Ϊfalse)
    /// </summary>
    public bool hasCastRod = false;
    /// <summary>
    /// �ո˵�����
    /// </summary>
    public float ReelRodForce = 15f;
    /// <summary>
    /// �׸͵�������Ĭ��Ϊ3��
    /// </summary>
    public float castingRodForce = 3f;
    /// <summary>
    /// ��ͺ�������������(Ĭ��6.5)
    /// </summary>
    public float distance = 6.5f;
    /// <summary>
    /// ������
    /// </summary>
    public DrawLine drawLine;

    /// <summary>
    /// ������ICommand�ӿڣ��жϸÿ������Ƿ�������
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    /// <summary>
    /// ����ָ����б�
    /// </summary>
    public List<IFishCommand> commandList { get; private set;}

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        drawLine = ComponentFinder.GetChildComponent<DrawLine>(gameObject, "FishRodPoint");

        // ��ʼ�������б�
        commandList = new List<IFishCommand>();
    }

    /// <summary>
    /// ��װ�����֮������������
    /// </summary>
    public void EquipmentFishRod()
    {
        //��ֹ�ظ�װ��
        if(isInitFishRod == false)
        {
            //ֱ�ӻ�ȡ��Player�µ�����͵ĵ�λ
            fishRodScript = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("Player").transform, "FishRodPoint").GetChild(0).GetComponent<FishRod>();
            //��ʼ�����λ��
            fishRodScript.transform.localPosition = Vector3.zero;
            //������߶�װ�����˾ͻ�����������
            PutDriftToWire();
            isInitFishRod = true;
        }
        
    }
    /// <summary>
    /// ж����͵ķ���
    /// </summary>
    public void UnEquipmentFishRod()
    {   
        //���ű��ÿ�
        fishRodScript = null;
        isInitFishRod = false;

        //�ֿ����ո��ˣ��ָ�Ĭ�ϣ�
        canPutAwayRod = true;
        //�����ջ��ˣ��ָ�Ĭ�ϣ�
        takeFishRode = false;
    }

    /// <summary>
    /// ��װ����������ʱ������������
    /// </summary>
    public void EquipmentDrift()
    {
        //��������û�б���ʼ����ʱ�򣬲��ܾ���װ������ֹ�ظ�װ��
        if(isInitDrift == false)
        {
            driftScript = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("Player").transform, "DriftPoint").GetChild(0).GetComponent<Drift>();
            //��ʼ����Ưλ��
            driftScript.transform.localPosition = Vector3.zero;
            //������߶�װ�����˾ͻ�����������
            PutDriftToWire();
            isInitDrift = true;
        }
        
    }
    /// <summary>
    /// ж�������ķ���
    /// </summary>
    public void UnEquipmentDrift()
    {
        driftScript = null;
        isInitDrift = false;
    }

    /// <summary>
    /// ���������ڸ�ͷλ�õķ���
    /// </summary>
    public void PutDriftToWire()
    {
        //ֻ����ͺ���Ư����ȡ���ˣ����ܽ��г�ʼ������ͺ���Ư�ĳ�ʼ������������������
        if(driftScript != null && fishRodScript != null)
        {
            //��ȡ��������ߺ�Ư�Ĺ�ͬ���ڵ������ߵı�������,�����ƶ�֮������transform.posiiton�������
            Vector3 wireWorldPosition = fishRodScript.wireTransform.transform.position;
            Vector3 wireLocalPosiitonInParent = gameObject.transform.InverseTransformPoint(wireWorldPosition);
            //��Ư�ƶ�����ͷȥ
            driftScript.transform.localPosition = wireLocalPosiitonInParent;

            //���°ѻ������ĳ�ʼ��
            drawLine.InitLineRenderer();
        }
    }

    /// <summary>
    /// �л�����������ͣ�����ģʽ��
    /// </summary>
    public void TakeRod()
    {
        //�л����������, Ҫ����ͳ�ʼ��֮��Ϳ����ո͵�ʱ����
        if (Input.GetKeyDown(KeyCode.Q) && isInitFishRod && canPutAwayRod)
        {
            takeFishRode = !takeFishRode;

            commandList.Add(new TakeFishRodCommand(fishRodScript.fishRodTransform, takeFishRode));
        }
    }

    /// <summary>
    /// �������������
    /// </summary>
    public void CastandReelInRod()
    {
        //��ʼ�׸Ͷ���
        if (Input.GetMouseButtonDown(0) && !hasCastRod && isInitDrift && isInitFishRod)
        {
            //ֻ�������ø��ӵ�ʱ��Ż�����׸�
            if (takeFishRode)
            {
                //�������ո���
                canPutAwayRod = false;
                //��������ϵȡ�����ƶ���DriftContainer
                driftScript.driftTransform.SetParent(null);
                //����˦���ӵ�����
                AudioManager.Instance.PlayAudio("ThrowRod", true);
                //��ʼ����
                drawLine.canDrawing = true;
                //�׸˵�ʱ��ر���Ư����Ч��
                driftScript.canAdsorb = false;
                commandList.Add(new CastingRodCommand(driftScript.driftTransform, driftScript.driftRb, fishRodScript.wireTransform, castingRodForce));
            }
        }

        //��ʼ�ո˶���
        if (Input.GetMouseButtonDown(0) && hasCastRod && isInitDrift && isInitFishRod)
        {
            //ֻ�������ø��ӵ�ʱ��Ż�����ո�
            if (takeFishRode)
            {
                //�ո˵�ʱ������Ư����Ч��
                driftScript.canAdsorb = true;
                commandList.Add(new ReelInRodCommand(fishRodScript.wireTransform, driftScript.driftTransform, driftScript.driftRb));
            }
        }
    }

    /// <summary>
    /// ��������������ӵķ���
    /// </summary>
    public void IndividualTrigger_TakeRod()
    {
        takeFishRode = !takeFishRode;
        commandList.Add(new ReelInRodCommand(fishRodScript.wireTransform, driftScript.driftTransform, driftScript.driftRb));
    }

    /// <summary>
    /// ֱ�ӵ��������ո˶���
    /// </summary>
    public void IndividualTrigger_ReelInRodCommand()
    {
        commandList.Add(new ReelInRodCommand(fishRodScript.wireTransform, driftScript.driftTransform, driftScript.driftRb));
    }

    /// <summary>
    /// ִ�������б�
    /// </summary>
    public void runCommandList()
    {
        //������һ֡�����е������ִ�������е�Execute����
        foreach (IFishCommand command in commandList)
        {
            command.Execute();
        }
        //ִ����֮������б�
        commandList.Clear();
    }

    /// <summary>
    /// ���������ķ���
    /// </summary>
    public void MoveToWire()
    {
        driftScript.driftTransform.position = Vector2.MoveTowards(driftScript.driftTransform.position, fishRodScript.wireTransform.position, ReelRodForce * Time.deltaTime);
    }

    /// <summary>
    /// ���߿��������Ѿ����ϵ��������������
    /// </summary>
    public void DrawLineController()
    {
        //����ͺ�������׼����ʱ���ٿ�ʼ����
        if(isInitFishRod && isInitDrift)
        {
            drawLine.DrawFishingLine();
        }
        else
        {
            drawLine.lineRenderer.positionCount = 0;
        }
    }
}
