using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// �浵������
/// </summary>
public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// SaveManager����
    /// </summary>
    public static SaveManager Instance;

    /// <summary>
    /// �����ܿ����������ڴ洢�������
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //������ʼ��
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //��Ҫɾ��gameObject
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �浵����
    /// </summary>
    /// <param name="data"></param>
    public void Save(string archiveName, string saveName, object data)
    {
        SaveAndLoad.SaveByJson(archiveName, saveName, data);
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    public T Load<T>(string archiveName, string saveName)
    {
        var data = SaveAndLoad.LoadByJson<T>(archiveName, saveName);
        return data;
    }

    /// <summary>
    /// ɾ���浵����
    /// </summary>
    /// <param name="archiveName"></param>
    public void Delete(string archiveName)
    {
        SaveAndLoad.DeleteByJson(archiveName);
    }

    /// <summary>
    /// ����ļ��Ƿ����
    /// </summary>
    /// <param name="archiveName"></param>
    /// <param name="saveFileName"></param>
    /// <returns></returns>
    public bool CheckDataExist(string archiveName, string saveFileName)
    {
        bool exist = SaveAndLoad.CheckDataExist(archiveName, saveFileName);
        return exist;
    }

    /// <summary>
    /// �г��浵���ֵķ���
    /// </summary>
    public List<string> ListArchiveName()
    {
        //�浵�ļ���
        string archivePath = Path.Combine(Application.persistentDataPath, "SaveFiles");

        //����ļ��в����ڵĻ�
        if(!Directory.Exists(archivePath))
        {
            Debug.LogWarning($"�浵�ļ��в�����");
            Debug.LogWarning($"{archivePath}������");
            return new List<string>();
        }

        // ��ȡ���д浵�ļ��У����ļ��У�
        string[] directories = Directory.GetDirectories(archivePath);

        // �洢�浵�ļ��е�����
        List<string> saveFolderNames = new List<string>();

        // ��ȡÿ���ļ��е����ƣ��ļ������Ƽ��浵���ƣ�
        foreach (var dir in directories)
        {
            // ��ȡ�ļ������ƣ�ȥ��·����
            string folderName = Path.GetFileName(dir);
            saveFolderNames.Add(folderName);
        }

        // ���ش浵�ļ��������б�
        return saveFolderNames;
    }

    /// <summary>
    /// �洢��Ϸ�����иô洢������
    /// </summary>
    public void SaveAllData()
    {
        //��ȡ��totaltotalController
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //�ж�totaltotalController�Ƿ��ѱ���ʼ��
        if (totalController != null)
        {
            //������ݴ洢
            totalController.playerDataAndUIController.SaveData();
            //�������ݴ洢
            InventoryManager.Instance.SaveData();
            //��Ϸ�������ݴ洢
            GameManager.Instance.SaveData();
        }
    }

    /// <summary>
    /// ������Ϸ�����е�����
    /// </summary>
    public void LoadAllData()
    {
        //��ȡ��totaltotalController
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //�ж�totaltotalController�Ƿ��ѱ���ʼ��
        if (totalController != null)
        {
            //������ݶ�ȡ
            totalController.playerDataAndUIController.LoadData();
            //�������ݶ�ȡ
            InventoryManager.Instance.LoadData();
            //��Ϸ�������ݶ�ȡ
            GameManager.Instance.LoadData();
        }
    }
}
