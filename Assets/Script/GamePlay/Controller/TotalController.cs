using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 控制玩家的类(命令模式)
/// </summary>
public class TotalController : MonoBehaviour
{
    /// <summary>
    /// 引用控制玩家行动的控制器
    /// </summary>
    public MovePlayerController movePlayerController { get; set; }
    /// <summary>
    /// 引用钓鱼控制器
    /// </summary>
    public FishandCastController fishandCastController { get; set; }
    /// <summary>
    /// 钓鱼指示器控制器
    /// </summary>
    public FishIndicatorController fishIndicatorController { get; set; }
    /// <summary>
    /// 引用背包控制器
    /// </summary>
    public InventoryController inventoryController { get; set; }
    /// <summary>
    /// 引用玩家相关UI的控制器
    /// </summary>
    public PlayerDataAndUIController playerDataAndUIController { get; set; }
    /// <summary>
    /// 引用
    /// 相关UI的控制器
    /// </summary>
    public StoreDataAndUIController storeDataAndUIController { get; set; }
    /// <summary>
    /// 引用商店相关的控制器
    /// </summary>
    public StoreController storeController { get; set; }
    /// <summary>
    /// 获取到相机跟随控制
    /// </summary>
    public CameraFollowController cameraFollowController { get; set; }
    /// <summary>
    /// 获取到背景跟随
    /// </summary>
    public BackGroundFollowController backGroundFollowController { get; set; }

    /// <summary>
    /// 不同控制器对应的操控状态机的名字
    /// </summary>
    public Dictionary<string, string> stateMachineNameWithControllerDic;

    /// <summary>
    /// 不同的控制器被挂起的指令列表
    /// </summary>
    public Dictionary<string, List<ConvertController>> commandListWithControllerDic;

    /// <summary>
    /// 单例模式
    /// </summary>
    public static TotalController Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        inventoryController = SetGameObjectToParent.FindFromFirstLayer("InventoryCanvas").GetComponent<InventoryController>();
        if (inventoryController == null)
        {
            Debug.LogError("inventoryController是空的，请检查代码！");
        }
        
        storeController = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<StoreController>();
        if (storeController == null)
        {
            Debug.LogError("storeController是空的，请检查代码！");
        }
        storeDataAndUIController = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<StoreDataAndUIController>();
        if(storeDataAndUIController == null)
        {
            Debug.LogError("storeDataAndUIController是空的，请检查代码！");
        }

        //初始化状态机名字列表
        stateMachineNameWithControllerDic = new Dictionary<string, string>();

        //初始化命令列表
        commandListWithControllerDic = new Dictionary<string, List<ConvertController>>();

        //单例初始化
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        //脚本默认初始化enable为false
        enabled = false;
    }

    /// <summary>
    /// 注册玩家GameObject的方法
    /// </summary>
    public void RegisterAboutPlayer()
    {
        //初始化移动玩家控制器
        movePlayerController = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<MovePlayerController>();
        if (movePlayerController == null)
        {
            Debug.LogError("movePlayerController是空的，请检查代码！");
        }
        //初始化鱼竿控制器
        fishandCastController = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<FishandCastController>();
        if (fishandCastController == null)
        {
            Debug.LogError("fishandCastController是空的，请检查代码！");
        }

        //钓鱼指示器的注册
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        if (fishIndicatorController != null)
        {
            fishIndicatorController.RegisterFishIndicatorController();
        }

        //背景跟随的注册
        backGroundFollowController = SetGameObjectToParent.FindFromFirstLayer("FollowCamera").GetComponent<BackGroundFollowController>();
        if (backGroundFollowController != null)
        {
            backGroundFollowController.RegisterBackGroundFollowController();
        }

        //寻找prop组件
        GameObject prop = SetGameObjectToParent.FindFromFirstLayer("Prop").gameObject;

        //水跟随人物的注册
        cameraFollowController = ComponentFinder.GetChildComponent<CameraFollowController>(prop, "Water");
        if(cameraFollowController != null)
        {
            cameraFollowController.RegisterCameraFollowController();
        }
        //反射相机跟随人物的注册
        cameraFollowController = ComponentFinder.GetChildComponent<CameraFollowController>(prop, "ReflectionCamera");
        if (cameraFollowController != null)
        {
            cameraFollowController.RegisterCameraFollowController();
        }
        //相机跟随人物的注册
        cameraFollowController = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<CameraFollowController>();
        if (cameraFollowController != null)
        {
            cameraFollowController.RegisterCameraFollowController();
        }
        //对人物的数据UI控制进行注册
        playerDataAndUIController = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<PlayerDataAndUIController>();
        if (playerDataAndUIController == null)
        {
            Debug.LogError("playerDataAndUIController是空的，请检查代码！");
        }

        //TotalController脚本开始运行
        enabled = true;
    }

    private void Update()
    {
        //什么控制器enable了，就使用什么控制器
        //使用控制器是否可以使用来判断钓鱼控制器是否可以使用
        if (fishandCastController.canRun)
        {
            //切换鱼竿的指令
            fishandCastController.TakeRod();
            //钓鱼的指令
            fishandCastController.CastandReelInRod();
        }
        if(inventoryController.canRun)
        {
            //开关背包的指令
            inventoryController.OpenAndCloseInventoryWithDetailPanel();

        }
        if(storeController.canRun)
        {
            //执行打开商店选项的命令
            storeController.OpenOrCloseOptionPanel();
            //执行关闭商店的命令
            storeController.CloseBuyPanel();
            //执行关闭卖出界面的命令
            storeController.CloseInventoryWithSalePanel();
        }
        

        //执行钓鱼命令列表
        fishandCastController.runCommandList();
        //执行背包命令
        inventoryController.runCommandList();
        //执行商店的命令列表
        storeController.runCommandList();

        //清空所有控制器中的命令列表，防止在玩家在进行某一个操作的时候导致命令存储
        fishandCastController.commandList.Clear();
        inventoryController.commandList.Clear();
        storeController.commandList.Clear();

        //不管什么情况，都要画线
        fishandCastController.DrawLineController();

        //不管什么情况，都要进行动画切换
        movePlayerController.SwitchAnimation();

        //----------------------------------TestCode-----------------------------------//

        #region 测试代码
        if (Input.GetKeyDown(KeyCode.U))
        {
            //加100个coin
            playerDataAndUIController.ChangeCoin(100);
        }
        #endregion
    }

    /// <summary>
    /// 控制TotalController中所有控制器的enable属性
    /// </summary>
    /// <typeparam name="T">TotalController中的控制器，继承于Monobehavior的类</typeparam>
    /// <param name="controllingStateMachineName">正在控制某一个控制器的状态机</param>
    /// <param name="enable">要控制器转变成的enable属性</param>
    /// <param name="controller">要控制的控制器</param>
    public void ControlManager<T>(string controllingStateMachineName, bool enable, T controller) where T : IController
    {
        //如果当前字典不包含控制器的键,也说明这个控制器是第一次被操作
        if(!stateMachineNameWithControllerDic.ContainsKey(controller.GetType().Name))
        {
            //连同创建好控制controller对应的状态机的名字
            stateMachineNameWithControllerDic[controller.GetType().Name] = null;
            //还有对controller的命令列表
            commandListWithControllerDic[controller.GetType().Name] = new List<ConvertController>();
        }

        // 当前没有对应状态机在控制对应的controller
        if (stateMachineNameWithControllerDic[controller.GetType().Name] == null)
        {
            //对应controller由对应状态机控制
            stateMachineNameWithControllerDic[controller.GetType().Name] = controllingStateMachineName;
            controller.canRun = enable;
            Debug.Log($"[{controller.GetType().Name}] {controllingStateMachineName} 获得控制权，设置 enable = {enable}");
            return;
        }

        // 当前有对应状态机在控制对应的controller
        if (stateMachineNameWithControllerDic[controller.GetType().Name] != controllingStateMachineName)
        {
            // 如果当前指令不是由对应状态机发出的，那么就对对应的命令列表存储对应命令
            Debug.Log($"[{controller.GetType().Name}] {controllingStateMachineName} 请求控制，但当前控制者是 {stateMachineNameWithControllerDic[controller.GetType().Name]}，命令被挂起");

            //判断当前命令字典中是否有对应controller的对应状态机的对应命令列表
            if (!commandListWithControllerDic.ContainsKey(controller.GetType().Name))
            {
                //没有就创建
                commandListWithControllerDic[controller.GetType().Name] = new List<ConvertController>();
            }
            //添加对应Controller的命令
            commandListWithControllerDic[controller.GetType().Name].Add(new ConvertController(controllingStateMachineName, controller, enable));

            return;
        }

        // 当前指令是对应控制状态机发出的，并且对应的controller的enable状态不同
        if (stateMachineNameWithControllerDic[controller.GetType().Name] == controllingStateMachineName && controller.canRun != enable)
        {
            controller.canRun = enable;
            Debug.Log($"[{controller.GetType().Name}] {controllingStateMachineName} 修改 enable = {enable}");

            // 处理对应controller的挂起的命令
            ProcessPendingCommands(controller);
            return;
        }
    }

    /// <summary>
    /// 处理挂起的命令
    /// </summary>
    private void ProcessPendingCommands<T>(T controller) where T : IController
    {
        //当有被挂起来的指令
        if (commandListWithControllerDic[controller.GetType().Name].Count > 0)
        {
            //先记录被更改前的控制器的enabled属性
            bool lastEnabled = controller.canRun;
            // 获取挂起的最后一个命令并执行
            var lastCommand = commandListWithControllerDic[controller.GetType().Name][commandListWithControllerDic[controller.GetType().Name].Count - 1];
            //执行命令
            lastCommand.Execute();
            //然后将当前的命令列表清空，以便下次存储
            commandListWithControllerDic[controller.GetType().Name].Clear();

            //如果更改前enabled和更改后的enabled一样，证明当前没有状态机发生逻辑冲突
            if (lastEnabled == controller.canRun)
            {
                stateMachineNameWithControllerDic[controller.GetType().Name] = null;
            }
            //否则就是发生了逻辑冲突
            else
            {
                // 设置新的控制权并执行命令
                stateMachineNameWithControllerDic[controller.GetType().Name] = lastCommand.StateMachineName;
                Debug.Log($"[ControlManager] {stateMachineNameWithControllerDic[controller.GetType().Name]} 获得控制权，执行挂起命令");
            }
        }

        // 没有挂起命令，清空控制权
        if (commandListWithControllerDic[controller.GetType().Name].Count == 0)
        {
            Debug.Log($"[ControlManager] {controller.GetType().Name} 没有挂起命令，释放控制权");
            stateMachineNameWithControllerDic[controller.GetType().Name] = null;
            return;
        }

    }
}
