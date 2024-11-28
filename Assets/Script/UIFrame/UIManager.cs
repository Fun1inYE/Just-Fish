using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UI的字典
    /// </summary>
    public Dictionary<string, GameObject> UIdic;

    /// <summary>
    /// 引用PanelManager
    /// </summary>
    public PanelManager panelManager;

    /// <summary>
    /// UIManager的单例
    /// </summary>
    public static UIManager Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //字典初始化
        UIdic = new Dictionary<string, GameObject>();
        //PanelManager初始化
        panelManager = new PanelManager();

        //单例初始化
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 游戏开始时，将游戏主菜单推入到BasePanel栈中
    /// </summary>
    public void Start()
    {
        panelManager.Push(new StartPanel());
        panelManager.Push(new WarningPanel());
    }

    /// <summary>
    /// 每帧执行当前活跃的窗口
    /// </summary>
    public void Update()
    {
        panelManager.OnUpdate();
    }

    /// <summary>
    /// UI信息加入字典并且池化的方法
    /// </summary>
    public void AddInfoAndPoolUI(GameObject obj, int poolSize)
    {
        //如果UI字典中不包含UI名字的话
        if(!UIdic.ContainsKey(obj.name))
        {
            //规定名字
            obj.name = obj.name.Replace("(Clone)", "").Trim();
            //加入字典
            UIdic.Add(obj.name, obj);
        }

        PoolManager.Instance.CreatGameObjectPool(obj, poolSize, "MainMenuCanvas");
    }

    /// <summary>
    /// 返回并且显示对应名字的UI
    /// </summary>
    /// <param name="name"></param>
    public GameObject GetAndDisPlayPoolUI(string name)
    {
        if(UIdic.ContainsKey(name))
        {
            GameObject obj = PoolManager.Instance.GetGameObjectFromPool(name);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning($"UIdic字典中没有名字为{name}的键");
            return null;
        }
    }

    /// <summary>
    /// 关闭UI的代码
    /// </summary>
    /// <param name="obj"></param>
    public void HideUI(GameObject obj)
    {
        PoolManager.Instance.ReturnGameObjectToPool(obj);
        obj.SetActive(false);
    }

    public void CheckItem()
    {
        foreach (string key in UIdic.Keys)
        {
            Debug.Log(key);
        }
    }
}
