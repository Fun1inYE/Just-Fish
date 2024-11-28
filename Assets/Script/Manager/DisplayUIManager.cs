using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示UI的管理器
/// </summary>
public class DisplayUIManager : MonoBehaviour
{
    /// <summary>
    /// 储存要显示的ui和对应的文字，便于查找
    /// </summary>
    public Dictionary<string, GameObject> disPlayDic;
    /// <summary>
    /// 记录UI显示的状态
    /// </summary>
    public Dictionary<string, bool> UIStateDic;
    /// <summary>
    /// Text的字典，键为Name，值为文字内容
    /// </summary>
    public Dictionary<string, string> textDic;
    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// DisplayUIManager的单例
    /// </summary>
    public static DisplayUIManager Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //字典初始化
        disPlayDic = new Dictionary<string, GameObject>();
        UIStateDic = new Dictionary<string, bool>();
        textDic = new Dictionary<string, string>();

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        //单例初始化
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckItem();
        }
    }

    /// <summary>
    /// 添加显示UI的信息
    /// </summary>
    public GameObject AddorGetDisplayUI(string key, GameObject gameObj)
    {
        if (!disPlayDic.ContainsKey(key))
        {
            //先将对应UI提示生成
            GameObject obj = Instantiate(gameObj);
            //然后关闭UI显示
            obj.SetActive(false);
            //然后将UI显示移动到DisplayButtonUICanvas下
            SetGameObjectToParent.SetParent("DisplayUICanvas", obj);
            //加入显示UI信息的字典
            disPlayDic.Add(key, obj);
            //加入显示UI状态的字典
            UIStateDic.Add(key, false);
        }
        //返回对应类信息
        return disPlayDic[key];
    }
    /// <summary>
    /// 通过key寻找对应的UI
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject AddorGetDisplayUI(string key)
    {
        if (disPlayDic.ContainsKey(key))
        {
            //返回对应类信息
            return disPlayDic[key];
        }
        Debug.LogError($"字典中没有{key}这个键");
        return null;
    }

    /// <summary>
    /// 添加文字的方法
    /// </summary>
    /// <param name="textName">文字名字</param>
    /// <param name="textContent">文字内容</param>
    public string AddTextInTextDic(string textName, string textContent)
    {
        //将信息加入字典
        if (!textDic.ContainsKey(textName))
        {
            //加入显示UI信息的字典
            textDic.Add(textName, textContent);
            
        }
        //判断名字在UIStateDic是否存在
        if (!UIStateDic.ContainsKey(textName))
        {
            //加入显示UI状态的字典
            UIStateDic.Add(textName, false);
        }
        else
        {
            Debug.LogError($"{textName}文本名字已经在UIStateDic中存在，请重新起名字");
        }
        //返回对应类信息
        return textDic[textName];
    }

    /// <summary>
    /// 显示UI的方法
    /// </summary>
    /// <param name="UIName"></param>
    public void DisplayUI(string UIName)
    {
        //如果字典中含有这个要显示的UI信息，并且该UI没有正在显示，那么可以正常显示
        if(disPlayDic.ContainsKey(UIName) && UIStateDic[UIName] == false)
        {
            //将该UI的显示状态改成正在显示
            UIStateDic[UIName] = true;
            //将UI显示
            disPlayDic[UIName].SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏显示UI的方法
    /// </summary>
    /// <param name="UIName"></param>
    public void HideUI(string UIName)
    {
        if(disPlayDic.ContainsKey(UIName) && UIStateDic[UIName] == true)
        {
            //将该UI的显示状态改成不再显示
            UIStateDic[UIName] = false;
            disPlayDic[UIName].SetActive(false);
        }
    }

    /// <summary>
    /// 显示UI，并且返回对应UI的gameObject
    /// </summary>
    /// <param name="textName"></param>
    /// <returns></returns>
    public GameObject DisplayTextUI(string textName)
    {
        //如果字典中含有这个要显示的UI信息，并且该UI没有正在显示，那么可以正常显示
        if (textDic.ContainsKey(textName) && UIStateDic[textName] == false)
        {
            //获取到一个TextUI
            GameObject gameObj = PoolManager.Instance.GetGameObjectFromPool("TextUI");
            //将对应的UI文字赋值给TextUI
            gameObj.GetComponent<Text>().text = textDic[textName];
            //将TextUI传给DisplayUICanvas
            SetGameObjectToParent.SetParent("DisplayUICanvas", gameObj);
            //打开gameObj
            gameObj.SetActive(true);
            return gameObj;
        }
        else
        {
            Debug.LogWarning($"textDic字典中没有{textName}键");
            return null;
        }
    }

    /// <summary>
    /// 要关闭的UI
    /// </summary>
    /// <param name="UI"></param>
    public void HideTextUI(GameObject gameObj)
    {
        //将TextUI传回ScreenModCanvasPool
        SetGameObjectToParent.SetParent("ScreenModCanvasPool", gameObj);
        //将gameObj退回对象池
        PoolManager.Instance.ReturnGameObjectToPool(gameObj);
    }

    /// <summary>
    /// 想要偏移的显示UI
    /// </summary>
    public void UIOffSet(string UIName)
    {
        if (UIStateDic[UIName] == true)
        {
            RectTransform rectTransform = disPlayDic[UIName].GetComponent<RectTransform>();

            rectTransform.anchoredPosition = UIOffset.CalculationOffset(
                totalController.movePlayerController.propUIPoint.position,
                rectTransform,
                OffsetLocation.Down);
        }
    }

    public void CheckItem()
    {
        foreach(string key in textDic.Keys)
        {
            Debug.Log(key);
        }
        
    }
}
