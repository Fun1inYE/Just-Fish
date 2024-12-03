using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ������ҵ���(����ģʽ)
/// </summary>
public class TotalController : MonoBehaviour
{
    /// <summary>
    /// ���ÿ�������ж��Ŀ�����
    /// </summary>
    public MovePlayerController movePlayerController { get; set; }
    /// <summary>
    /// ���õ��������
    /// </summary>
    public FishandCastController fishandCastController { get; set; }
    /// <summary>
    /// ����ָʾ��������
    /// </summary>
    public FishIndicatorController fishIndicatorController { get; set; }
    /// <summary>
    /// ���ñ���������
    /// </summary>
    public InventoryController inventoryController { get; set; }
    /// <summary>
    /// ����������UI�Ŀ�����
    /// </summary>
    public PlayerDataAndUIController playerDataAndUIController { get; set; }
    /// <summary>
    /// ����
    /// ���UI�Ŀ�����
    /// </summary>
    public StoreDataAndUIController storeDataAndUIController { get; set; }
    /// <summary>
    /// �����̵���صĿ�����
    /// </summary>
    public StoreController storeController { get; set; }
    /// <summary>
    /// ��ȡ������������
    /// </summary>
    public CameraFollowController cameraFollowController { get; set; }
    /// <summary>
    /// ��ȡ����������
    /// </summary>
    public BackGroundFollowController backGroundFollowController { get; set; }

    /// <summary>
    /// ��ͬ��������Ӧ�Ĳٿ�״̬��������
    /// </summary>
    public Dictionary<string, string> stateMachineNameWithControllerDic;

    /// <summary>
    /// ��ͬ�Ŀ������������ָ���б�
    /// </summary>
    public Dictionary<string, List<ConvertController>> commandListWithControllerDic;

    /// <summary>
    /// ����ģʽ
    /// </summary>
    public static TotalController Instance;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        inventoryController = SetGameObjectToParent.FindFromFirstLayer("InventoryCanvas").GetComponent<InventoryController>();
        if (inventoryController == null)
        {
            Debug.LogError("inventoryController�ǿյģ�������룡");
        }
        
        storeController = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<StoreController>();
        if (storeController == null)
        {
            Debug.LogError("storeController�ǿյģ�������룡");
        }
        storeDataAndUIController = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<StoreDataAndUIController>();
        if(storeDataAndUIController == null)
        {
            Debug.LogError("storeDataAndUIController�ǿյģ�������룡");
        }

        //��ʼ��״̬�������б�
        stateMachineNameWithControllerDic = new Dictionary<string, string>();

        //��ʼ�������б�
        commandListWithControllerDic = new Dictionary<string, List<ConvertController>>();

        //������ʼ��
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        //�ű�Ĭ�ϳ�ʼ��enableΪfalse
        enabled = false;
    }

    /// <summary>
    /// ע�����GameObject�ķ���
    /// </summary>
    public void RegisterAboutPlayer()
    {
        //��ʼ���ƶ���ҿ�����
        movePlayerController = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<MovePlayerController>();
        if (movePlayerController == null)
        {
            Debug.LogError("movePlayerController�ǿյģ�������룡");
        }
        //��ʼ����Ϳ�����
        fishandCastController = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<FishandCastController>();
        if (fishandCastController == null)
        {
            Debug.LogError("fishandCastController�ǿյģ�������룡");
        }

        //����ָʾ����ע��
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        if (fishIndicatorController != null)
        {
            fishIndicatorController.RegisterFishIndicatorController();
        }

        //���������ע��
        backGroundFollowController = SetGameObjectToParent.FindFromFirstLayer("FollowCamera").GetComponent<BackGroundFollowController>();
        if (backGroundFollowController != null)
        {
            backGroundFollowController.RegisterBackGroundFollowController();
        }

        //Ѱ��prop���
        GameObject prop = SetGameObjectToParent.FindFromFirstLayer("Prop").gameObject;

        //ˮ���������ע��
        cameraFollowController = ComponentFinder.GetChildComponent<CameraFollowController>(prop, "Water");
        if(cameraFollowController != null)
        {
            cameraFollowController.RegisterCameraFollowController();
        }
        //����������������ע��
        cameraFollowController = ComponentFinder.GetChildComponent<CameraFollowController>(prop, "ReflectionCamera");
        if (cameraFollowController != null)
        {
            cameraFollowController.RegisterCameraFollowController();
        }
        //������������ע��
        cameraFollowController = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<CameraFollowController>();
        if (cameraFollowController != null)
        {
            cameraFollowController.RegisterCameraFollowController();
        }
        //�����������UI���ƽ���ע��
        playerDataAndUIController = SetGameObjectToParent.FindFromFirstLayer("Player").GetComponent<PlayerDataAndUIController>();
        if (playerDataAndUIController == null)
        {
            Debug.LogError("playerDataAndUIController�ǿյģ�������룡");
        }

        //TotalController�ű���ʼ����
        enabled = true;
    }

    private void Update()
    {
        //ʲô������enable�ˣ���ʹ��ʲô������
        //ʹ�ÿ������Ƿ����ʹ�����жϵ���������Ƿ����ʹ��
        if (fishandCastController.canRun)
        {
            //�л���͵�ָ��
            fishandCastController.TakeRod();
            //�����ָ��
            fishandCastController.CastandReelInRod();
        }
        if(inventoryController.canRun)
        {
            //���ر�����ָ��
            inventoryController.OpenAndCloseInventoryWithDetailPanel();

        }
        if(storeController.canRun)
        {
            //ִ�д��̵�ѡ�������
            storeController.OpenOrCloseOptionPanel();
            //ִ�йر��̵������
            storeController.CloseBuyPanel();
            //ִ�йر��������������
            storeController.CloseInventoryWithSalePanel();
        }
        

        //ִ�е��������б�
        fishandCastController.runCommandList();
        //ִ�б�������
        inventoryController.runCommandList();
        //ִ���̵�������б�
        storeController.runCommandList();

        //������п������е������б���ֹ������ڽ���ĳһ��������ʱ��������洢
        fishandCastController.commandList.Clear();
        inventoryController.commandList.Clear();
        storeController.commandList.Clear();

        //����ʲô�������Ҫ����
        fishandCastController.DrawLineController();

        //����ʲô�������Ҫ���ж����л�
        movePlayerController.SwitchAnimation();

        //----------------------------------TestCode-----------------------------------//

        #region ���Դ���
        if (Input.GetKeyDown(KeyCode.U))
        {
            //��100��coin
            playerDataAndUIController.ChangeCoin(100);
        }
        #endregion
    }

    /// <summary>
    /// ����TotalController�����п�������enable����
    /// </summary>
    /// <typeparam name="T">TotalController�еĿ��������̳���Monobehavior����</typeparam>
    /// <param name="controllingStateMachineName">���ڿ���ĳһ����������״̬��</param>
    /// <param name="enable">Ҫ������ת��ɵ�enable����</param>
    /// <param name="controller">Ҫ���ƵĿ�����</param>
    public void ControlManager<T>(string controllingStateMachineName, bool enable, T controller) where T : IController
    {
        //�����ǰ�ֵ䲻�����������ļ�,Ҳ˵������������ǵ�һ�α�����
        if(!stateMachineNameWithControllerDic.ContainsKey(controller.GetType().Name))
        {
            //��ͬ�����ÿ���controller��Ӧ��״̬��������
            stateMachineNameWithControllerDic[controller.GetType().Name] = null;
            //���ж�controller�������б�
            commandListWithControllerDic[controller.GetType().Name] = new List<ConvertController>();
        }

        // ��ǰû�ж�Ӧ״̬���ڿ��ƶ�Ӧ��controller
        if (stateMachineNameWithControllerDic[controller.GetType().Name] == null)
        {
            //��Ӧcontroller�ɶ�Ӧ״̬������
            stateMachineNameWithControllerDic[controller.GetType().Name] = controllingStateMachineName;
            controller.canRun = enable;
            Debug.Log($"[{controller.GetType().Name}] {controllingStateMachineName} ��ÿ���Ȩ������ enable = {enable}");
            return;
        }

        // ��ǰ�ж�Ӧ״̬���ڿ��ƶ�Ӧ��controller
        if (stateMachineNameWithControllerDic[controller.GetType().Name] != controllingStateMachineName)
        {
            // �����ǰָ����ɶ�Ӧ״̬�������ģ���ô�ͶԶ�Ӧ�������б�洢��Ӧ����
            Debug.Log($"[{controller.GetType().Name}] {controllingStateMachineName} ������ƣ�����ǰ�������� {stateMachineNameWithControllerDic[controller.GetType().Name]}���������");

            //�жϵ�ǰ�����ֵ����Ƿ��ж�Ӧcontroller�Ķ�Ӧ״̬���Ķ�Ӧ�����б�
            if (!commandListWithControllerDic.ContainsKey(controller.GetType().Name))
            {
                //û�оʹ���
                commandListWithControllerDic[controller.GetType().Name] = new List<ConvertController>();
            }
            //��Ӷ�ӦController������
            commandListWithControllerDic[controller.GetType().Name].Add(new ConvertController(controllingStateMachineName, controller, enable));

            return;
        }

        // ��ǰָ���Ƕ�Ӧ����״̬�������ģ����Ҷ�Ӧ��controller��enable״̬��ͬ
        if (stateMachineNameWithControllerDic[controller.GetType().Name] == controllingStateMachineName && controller.canRun != enable)
        {
            controller.canRun = enable;
            Debug.Log($"[{controller.GetType().Name}] {controllingStateMachineName} �޸� enable = {enable}");

            // �����Ӧcontroller�Ĺ��������
            ProcessPendingCommands(controller);
            return;
        }
    }

    /// <summary>
    /// ������������
    /// </summary>
    private void ProcessPendingCommands<T>(T controller) where T : IController
    {
        //���б���������ָ��
        if (commandListWithControllerDic[controller.GetType().Name].Count > 0)
        {
            //�ȼ�¼������ǰ�Ŀ�������enabled����
            bool lastEnabled = controller.canRun;
            // ��ȡ��������һ�����ִ��
            var lastCommand = commandListWithControllerDic[controller.GetType().Name][commandListWithControllerDic[controller.GetType().Name].Count - 1];
            //ִ������
            lastCommand.Execute();
            //Ȼ�󽫵�ǰ�������б���գ��Ա��´δ洢
            commandListWithControllerDic[controller.GetType().Name].Clear();

            //�������ǰenabled�͸��ĺ��enabledһ����֤����ǰû��״̬�������߼���ͻ
            if (lastEnabled == controller.canRun)
            {
                stateMachineNameWithControllerDic[controller.GetType().Name] = null;
            }
            //������Ƿ������߼���ͻ
            else
            {
                // �����µĿ���Ȩ��ִ������
                stateMachineNameWithControllerDic[controller.GetType().Name] = lastCommand.StateMachineName;
                Debug.Log($"[ControlManager] {stateMachineNameWithControllerDic[controller.GetType().Name]} ��ÿ���Ȩ��ִ�й�������");
            }
        }

        // û�й��������տ���Ȩ
        if (commandListWithControllerDic[controller.GetType().Name].Count == 0)
        {
            Debug.Log($"[ControlManager] {controller.GetType().Name} û�й�������ͷſ���Ȩ");
            stateMachineNameWithControllerDic[controller.GetType().Name] = null;
            return;
        }

    }
}
