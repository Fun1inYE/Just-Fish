using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentFinder
{
    /// <summary>
    /// ���obj��û�ж�Ӧ���������оͷ��أ����û�еĻ��ʹ���һ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        if(obj.GetComponent<T>())
        {
            return obj.GetComponent<T>();
        }
        else
        {
            return obj.AddComponent<T>();
        }
    }

    /// <summary>
    /// ����parent�е��Ӷ���Ѱ�Ҷ�Ӧ���
    /// </summary>
    /// <typeparam name="T">Ҫ��ȡ���������</typeparam>
    /// <param name="parent">������</param>
    /// <param name="childName">�Ӷ�������</param>
    /// <returns>����ָ�����͵���������δ�ҵ��򷵻�null</returns>
    public static T GetChildComponent<T>(GameObject parent, string childName) where T : Component
    {
        Transform child = SetGameObjectToParent.FindChildRecursive(parent.transform, childName);
        if (child == null)
        {
            Debug.LogError($"δ�ҵ��Ӷ���: {childName} �� {parent.name}");
            return null;
        }

        T component = child.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"δ�ҵ���� {typeof(T).Name} �� {child.name}");
        }
        return component;
    }
}
