using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 执行钓鱼命令的钓鱼命令的控制器
/// </summary>
public class FishandCastController : MonoBehaviour, IController
{
    /// <summary>
    /// 鱼竿的脚本
    /// </summary>
    public FishRod fishRodScript;
    /// <summary>
    /// 鱼鳔的脚本
    /// </summary>
    public Drift driftScript;
    /// <summary>
    /// 判断是否初始化了鱼竿(默认false)
    /// </summary>
    public bool isInitFishRod = false;
    /// <summary>
    /// 判断是否初始化了鱼鳔(默认false)
    /// </summary>
    public bool isInitDrift = false;
    /// <summary>
    /// 判断是否可以收鱼竿（默认为true）
    /// </summary>
    public bool canPutAwayRod = true;
    /// <summary>
    /// 判断是否拿出鱼竿(默认为false)
    /// </summary>
    public bool takeFishRode = false;
    /// <summary>
    /// 判断是否抛竿(默认为false)
    /// </summary>
    public bool hasCastRod = false;
    /// <summary>
    /// 收杆的力量
    /// </summary>
    public float ReelRodForce = 15f;
    /// <summary>
    /// 抛竿的力量（默认为3）
    /// </summary>
    public float castingRodForce = 3f;
    /// <summary>
    /// 鱼竿和鱼鳔的最大距离(默认6.5)
    /// </summary>
    public float distance = 6.5f;
    /// <summary>
    /// 画线器
    /// </summary>
    public DrawLine drawLine;

    /// <summary>
    /// 来自于ICommand接口，判断该控制器是否能运行
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    /// <summary>
    /// 储存指令的列表
    /// </summary>
    public List<IFishCommand> commandList { get; private set;}

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        drawLine = ComponentFinder.GetChildComponent<DrawLine>(gameObject, "FishRodPoint");

        // 初始化命令列表
        commandList = new List<IFishCommand>();
    }

    /// <summary>
    /// 当装备鱼竿之后调用这个方法
    /// </summary>
    public void EquipmentFishRod()
    {
        //防止重复装备
        if(isInitFishRod == false)
        {
            //直接获取到Player下的拿鱼竿的点位
            fishRodScript = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("Player").transform, "FishRodPoint").GetChild(0).GetComponent<FishRod>();
            //初始化鱼竿位置
            fishRodScript.transform.localPosition = Vector3.zero;
            //如果二者都装备好了就会调用这个方法
            PutDriftToWire();
            isInitFishRod = true;
        }
        
    }
    /// <summary>
    /// 卸载鱼竿的方法
    /// </summary>
    public void UnEquipmentFishRod()
    {   
        //将脚本置空
        fishRodScript = null;
        isInitFishRod = false;

        //又可以收竿了（恢复默认）
        canPutAwayRod = true;
        //将竿收回了（恢复默认）
        takeFishRode = false;
    }

    /// <summary>
    /// 当装备了鱼鳔的时候调用这个方法
    /// </summary>
    public void EquipmentDrift()
    {
        //当鱼鳔的没有被初始化的时候，才能就行装备，防止重复装备
        if(isInitDrift == false)
        {
            driftScript = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("Player").transform, "DriftPoint").GetChild(0).GetComponent<Drift>();
            //初始化鱼漂位置
            driftScript.transform.localPosition = Vector3.zero;
            //如果二者都装备好了就会调用这个方法
            PutDriftToWire();
            isInitDrift = true;
        }
        
    }
    /// <summary>
    /// 卸载鱼鳔的方法
    /// </summary>
    public void UnEquipmentDrift()
    {
        driftScript = null;
        isInitDrift = false;
    }

    /// <summary>
    /// 将鱼鳔放在竿头位置的方法
    /// </summary>
    public void PutDriftToWire()
    {
        //只有鱼竿和鱼漂都获取到了，才能进行初始化（鱼竿和鱼漂的初始化都会调用这个方法）
        if(driftScript != null && fishRodScript != null)
        {
            //获取到相对于线和漂的共同父节点的相对线的本地坐标,坐标移动之后两个transform.posiiton会变得相等
            Vector3 wireWorldPosition = fishRodScript.wireTransform.transform.position;
            Vector3 wireLocalPosiitonInParent = gameObject.transform.InverseTransformPoint(wireWorldPosition);
            //将漂移动到杆头去
            driftScript.transform.localPosition = wireLocalPosiitonInParent;

            //重新把画线器的初始化
            drawLine.InitLineRenderer();
        }
    }

    /// <summary>
    /// 切换空手与拿鱼竿（命令模式）
    /// </summary>
    public void TakeRod()
    {
        //切换空手与鱼竿, 要在鱼竿初始化之后和可以收竿的时候收
        if (Input.GetKeyDown(KeyCode.Q) && isInitFishRod && canPutAwayRod)
        {
            takeFishRode = !takeFishRode;

            commandList.Add(new TakeFishRodCommand(fishRodScript.fishRodTransform, takeFishRode));
        }
    }

    /// <summary>
    /// 钓鱼命令控制器
    /// </summary>
    public void CastandReelInRod()
    {
        //开始抛竿动作
        if (Input.GetMouseButtonDown(0) && !hasCastRod && isInitDrift && isInitFishRod)
        {
            //只有在手拿杆子的时候才会进行抛竿
            if (takeFishRode)
            {
                //不可以收竿了
                canPutAwayRod = false;
                //将父级关系取消，移动到DriftContainer
                driftScript.driftTransform.SetParent(null);
                //播放甩竿子的声音
                AudioManager.Instance.PlayAudio("ThrowRod", true);
                //开始画线
                drawLine.canDrawing = true;
                //抛杆的时候关闭鱼漂吸附效果
                driftScript.canAdsorb = false;
                commandList.Add(new CastingRodCommand(driftScript.driftTransform, driftScript.driftRb, fishRodScript.wireTransform, castingRodForce));
            }
        }

        //开始收杆动作
        if (Input.GetMouseButtonDown(0) && hasCastRod && isInitDrift && isInitFishRod)
        {
            //只有在手拿杆子的时候才会进行收杆
            if (takeFishRode)
            {
                //收杆的时候开启鱼漂吸附效果
                driftScript.canAdsorb = true;
                commandList.Add(new ReelInRodCommand(fishRodScript.wireTransform, driftScript.driftTransform, driftScript.driftRb));
            }
        }
    }

    /// <summary>
    /// 单独触发收起杆子的方法
    /// </summary>
    public void IndividualTrigger_TakeRod()
    {
        takeFishRode = !takeFishRode;
        commandList.Add(new ReelInRodCommand(fishRodScript.wireTransform, driftScript.driftTransform, driftScript.driftRb));
    }

    /// <summary>
    /// 直接单独触发收杆动作
    /// </summary>
    public void IndividualTrigger_ReelInRodCommand()
    {
        commandList.Add(new ReelInRodCommand(fishRodScript.wireTransform, driftScript.driftTransform, driftScript.driftRb));
    }

    /// <summary>
    /// 执行命令列表
    /// </summary>
    public void runCommandList()
    {
        //遍历这一帧中所有的命令，并执行命令中的Execute方法
        foreach (IFishCommand command in commandList)
        {
            command.Execute();
        }
        //执行完之后清空列表
        commandList.Clear();
    }

    /// <summary>
    /// 鱼鳔吸附的方法
    /// </summary>
    public void MoveToWire()
    {
        driftScript.driftTransform.position = Vector2.MoveTowards(driftScript.driftTransform.position, fishRodScript.wireTransform.position, ReelRodForce * Time.deltaTime);
    }

    /// <summary>
    /// 画线控制器，已经整合到钓鱼控制器中了
    /// </summary>
    public void DrawLineController()
    {
        //当鱼竿和鱼鳔都准备好时候，再开始画线
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
