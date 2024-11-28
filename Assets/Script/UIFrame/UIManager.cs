using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI������
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UI���ֵ�
    /// </summary>
    public Dictionary<string, GameObject> UIdic;

    /// <summary>
    /// ����PanelManager
    /// </summary>
    public PanelManager panelManager;

    /// <summary>
    /// UIManager�ĵ���
    /// </summary>
    public static UIManager Instance;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //�ֵ��ʼ��
        UIdic = new Dictionary<string, GameObject>();
        //PanelManager��ʼ��
        panelManager = new PanelManager();

        //������ʼ��
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
    /// ��Ϸ��ʼʱ������Ϸ���˵����뵽BasePanelջ��
    /// </summary>
    public void Start()
    {
        panelManager.Push(new StartPanel());
        panelManager.Push(new WarningPanel());
    }

    /// <summary>
    /// ÿִ֡�е�ǰ��Ծ�Ĵ���
    /// </summary>
    public void Update()
    {
        panelManager.OnUpdate();
    }

    /// <summary>
    /// UI��Ϣ�����ֵ䲢�ҳػ��ķ���
    /// </summary>
    public void AddInfoAndPoolUI(GameObject obj, int poolSize)
    {
        //���UI�ֵ��в�����UI���ֵĻ�
        if(!UIdic.ContainsKey(obj.name))
        {
            //�涨����
            obj.name = obj.name.Replace("(Clone)", "").Trim();
            //�����ֵ�
            UIdic.Add(obj.name, obj);
        }

        PoolManager.Instance.CreatGameObjectPool(obj, poolSize, "MainMenuCanvas");
    }

    /// <summary>
    /// ���ز�����ʾ��Ӧ���ֵ�UI
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
            Debug.LogWarning($"UIdic�ֵ���û������Ϊ{name}�ļ�");
            return null;
        }
    }

    /// <summary>
    /// �ر�UI�Ĵ���
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
