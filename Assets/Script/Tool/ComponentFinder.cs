using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentFinder
{
    /// <summary>
    /// 检查obj有没有对应组件，如果有就返回，如果没有的话就创建一个
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
    /// 帮助parent中的子对象寻找对应组件
    /// </summary>
    /// <typeparam name="T">要获取的组件类型</typeparam>
    /// <param name="parent">父对象</param>
    /// <param name="childName">子对象名称</param>
    /// <returns>返回指定类型的组件，如果未找到则返回null</returns>
    public static T GetChildComponent<T>(GameObject parent, string childName) where T : Component
    {
        Transform child = SetGameObjectToParent.FindChildRecursive(parent.transform, childName);
        if (child == null)
        {
            Debug.LogError($"未找到子对象: {childName} 在 {parent.name}");
            return null;
        }

        T component = child.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"未找到组件 {typeof(T).Name} 在 {child.name}");
        }
        return component;
    }
}
