using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ʾUI�Ĺ�����
/// </summary>
public class DisplayUIManager : MonoBehaviour
{
    /// <summary>
    /// ����Ҫ��ʾ��ui�Ͷ�Ӧ�����֣����ڲ���
    /// </summary>
    public Dictionary<string, GameObject> disPlayDic;
    /// <summary>
    /// ��¼UI��ʾ��״̬
    /// </summary>
    public Dictionary<string, bool> UIStateDic;
    /// <summary>
    /// Text���ֵ䣬��ΪName��ֵΪ��������
    /// </summary>
    public Dictionary<string, string> textDic;
    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// DisplayUIManager�ĵ���
    /// </summary>
    public static DisplayUIManager Instance;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //�ֵ��ʼ��
        disPlayDic = new Dictionary<string, GameObject>();
        UIStateDic = new Dictionary<string, bool>();
        textDic = new Dictionary<string, string>();

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        //������ʼ��
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
    /// �����ʾUI����Ϣ
    /// </summary>
    public GameObject AddorGetDisplayUI(string key, GameObject gameObj)
    {
        if (!disPlayDic.ContainsKey(key))
        {
            //�Ƚ���ӦUI��ʾ����
            GameObject obj = Instantiate(gameObj);
            //Ȼ��ر�UI��ʾ
            obj.SetActive(false);
            //Ȼ��UI��ʾ�ƶ���DisplayButtonUICanvas��
            SetGameObjectToParent.SetParent("DisplayUICanvas", obj);
            //������ʾUI��Ϣ���ֵ�
            disPlayDic.Add(key, obj);
            //������ʾUI״̬���ֵ�
            UIStateDic.Add(key, false);
        }
        //���ض�Ӧ����Ϣ
        return disPlayDic[key];
    }
    /// <summary>
    /// ͨ��keyѰ�Ҷ�Ӧ��UI
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject AddorGetDisplayUI(string key)
    {
        if (disPlayDic.ContainsKey(key))
        {
            //���ض�Ӧ����Ϣ
            return disPlayDic[key];
        }
        Debug.LogError($"�ֵ���û��{key}�����");
        return null;
    }

    /// <summary>
    /// ������ֵķ���
    /// </summary>
    /// <param name="textName">��������</param>
    /// <param name="textContent">��������</param>
    public string AddTextInTextDic(string textName, string textContent)
    {
        //����Ϣ�����ֵ�
        if (!textDic.ContainsKey(textName))
        {
            //������ʾUI��Ϣ���ֵ�
            textDic.Add(textName, textContent);
            
        }
        //�ж�������UIStateDic�Ƿ����
        if (!UIStateDic.ContainsKey(textName))
        {
            //������ʾUI״̬���ֵ�
            UIStateDic.Add(textName, false);
        }
        else
        {
            Debug.LogError($"{textName}�ı������Ѿ���UIStateDic�д��ڣ�������������");
        }
        //���ض�Ӧ����Ϣ
        return textDic[textName];
    }

    /// <summary>
    /// ��ʾUI�ķ���
    /// </summary>
    /// <param name="UIName"></param>
    public void DisplayUI(string UIName)
    {
        //����ֵ��к������Ҫ��ʾ��UI��Ϣ�����Ҹ�UIû��������ʾ����ô����������ʾ
        if(disPlayDic.ContainsKey(UIName) && UIStateDic[UIName] == false)
        {
            //����UI����ʾ״̬�ĳ�������ʾ
            UIStateDic[UIName] = true;
            //��UI��ʾ
            disPlayDic[UIName].SetActive(true);
        }
    }

    /// <summary>
    /// ������ʾUI�ķ���
    /// </summary>
    /// <param name="UIName"></param>
    public void HideUI(string UIName)
    {
        if(disPlayDic.ContainsKey(UIName) && UIStateDic[UIName] == true)
        {
            //����UI����ʾ״̬�ĳɲ�����ʾ
            UIStateDic[UIName] = false;
            disPlayDic[UIName].SetActive(false);
        }
    }

    /// <summary>
    /// ��ʾUI�����ҷ��ض�ӦUI��gameObject
    /// </summary>
    /// <param name="textName"></param>
    /// <returns></returns>
    public GameObject DisplayTextUI(string textName)
    {
        //����ֵ��к������Ҫ��ʾ��UI��Ϣ�����Ҹ�UIû��������ʾ����ô����������ʾ
        if (textDic.ContainsKey(textName) && UIStateDic[textName] == false)
        {
            //��ȡ��һ��TextUI
            GameObject gameObj = PoolManager.Instance.GetGameObjectFromPool("TextUI");
            //����Ӧ��UI���ָ�ֵ��TextUI
            gameObj.GetComponent<Text>().text = textDic[textName];
            //��TextUI����DisplayUICanvas
            SetGameObjectToParent.SetParent("DisplayUICanvas", gameObj);
            //��gameObj
            gameObj.SetActive(true);
            return gameObj;
        }
        else
        {
            Debug.LogWarning($"textDic�ֵ���û��{textName}��");
            return null;
        }
    }

    /// <summary>
    /// Ҫ�رյ�UI
    /// </summary>
    /// <param name="UI"></param>
    public void HideTextUI(GameObject gameObj)
    {
        //��TextUI����ScreenModCanvasPool
        SetGameObjectToParent.SetParent("ScreenModCanvasPool", gameObj);
        //��gameObj�˻ض����
        PoolManager.Instance.ReturnGameObjectToPool(gameObj);
    }

    /// <summary>
    /// ��Ҫƫ�Ƶ���ʾUI
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
