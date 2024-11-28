using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;  // ���� LitJson

/// <summary>
/// �洢�Ͷ�ȡ��
/// </summary>
public static class SaveAndLoad
{
    /// <summary>
    /// �浵����
    /// </summary>
    /// <param name="archiveName">��Ҵ浵����</param>
    /// <param name="saveFileName">���������ļ�����</param>
    /// <param name="data">Ҫ�洢����������</param>
    public static void SaveByJson(string archiveName, string saveFileName, object data)
    {
        // ������Ҵ浵��·��
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);

        // ��������ڸ��ļ��У��ʹ���һ������ļ���
        if (!Directory.Exists(playerFolderPath))
        {
            Directory.CreateDirectory(playerFolderPath);
        }

        // ʹ�� LitJson ���л�����
        var json = JsonMapper.ToJson(data);

        // �� json �ļ��洢����Ӧ·������Ҵ浵·�� + �洢����
        var path = Path.Combine(playerFolderPath, saveFileName);

        try
        {
            File.WriteAllText(path, json);
            Debug.Log("�ļ�����ɹ���·��Ϊ��" + path);
        }
        catch (System.Exception)
        {
            Debug.Log("�ļ�����ʧ��");
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <typeparam name="T">Ҫ��ȡ���ļ�����</typeparam>
    /// <param name="archiveName">��Ҵ浵����</param>
    /// <param name="saveFileName">���������ļ�����</param>
    /// <returns></returns>
    public static T LoadByJson<T>(string archiveName, string saveFileName)
    {
        // ������Ҵ浵��·��
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);
        // �������������ļ���
        if (!Directory.Exists(playerFolderPath))
        {
            Debug.LogWarning($"������{playerFolderPath}�ļ��У������������ļ���������");
            return default;
        }

        string path = Path.Combine(playerFolderPath, saveFileName).Replace("\\", "/");  // ����·���ϳɷ�ʽ

        // �������·�����Ա����
        Debug.Log("����·����" + path);

        if(File.Exists(path))
        {
            var json = File.ReadAllText(path);
            // ʹ�� LitJson �����л�
            var data = JsonMapper.ToObject<T>(json);
            Debug.Log("��ȡ�ļ��ɹ�");
            return data;
        }
        else
        {
            Debug.Log("û���� " + path + " ���ҵ� " + saveFileName);
            return default;
        }
    }

    /// <summary>
    /// ɾ������
    /// </summary>
    /// <param name="archiveName">�浵����</param>
    public static void DeleteByJson(string archiveName)
    {
        // ������Ҵ浵��·��
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);

        // �ж�·���Ƿ����
        if (Directory.Exists(playerFolderPath))
        {
            Directory.Delete(playerFolderPath, true);  // ɾ������Ŀ¼�����е��ļ�
            Debug.Log($"�浵{archiveName}ɾ���ɹ�");
        }
        else
        {
            Debug.LogWarning($"û���ҵ�{archiveName}�浵");
            Debug.LogWarning($"û���ҵ�{playerFolderPath}·��");
        }
    }

    /// <summary>
    /// ����ļ��Ƿ���ڵķ���
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <returns></returns>
    public static bool CheckDataExist(string archiveName, string saveFileName)
    {
        // ������Ҵ浵��·��
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);
        // �浵�ļ�·��
        var path = Path.Combine(playerFolderPath, saveFileName);  // ����·���ϳɷ�ʽ
        return File.Exists(path);
    }
}