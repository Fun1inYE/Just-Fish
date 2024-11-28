using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 将GameObject移动到对应GameObject下
/// </summary>
public static class SetGameObjectToParent
{
    /// <summary>
    /// 只在当前活动场景的第一层寻找对应名字的GameObject
    /// </summary>
    /// <param name="parentGameObjectName">父GameObject的名字</param>
    /// <param name="childGameObject">子GameObject</param>
    public static void SetParent(string parentGameObjectName, GameObject childGameObject)
    {
        //先通过名字找到父GameObject
        GameObject parentGameObject = FindFromFirstLayer(parentGameObjectName);
        if (parentGameObject != null)
        {
            //将子GameObject移动到对应父GameObject下并且保持本地坐标
            childGameObject.transform.SetParent(parentGameObject.transform, false);
        }
        else
        {
            Debug.LogError($"没有找到名字为{parentGameObjectName}的GameObject");
        }
    }

    /// <summary>
    /// 从第一层找到父gameObject下的gameObject作为子gameobject的gameObject
    /// </summary>
    /// <param name="firstParentGameObjectName">父类的父Object</param>
    /// <param name="secondParentGameObjectName">父Object</param>
    /// <param name="childGameObject">子gameObject</param>
    public static void SetParentFromFirstLayerParent(string firstParentGameObjectName, string secondParentGameObjectName, GameObject childGameObject)
    {
        //先通过名字找到父GameObject
        GameObject firstParentGameObject = FindFromFirstLayer(firstParentGameObjectName);
        if (firstParentGameObject != null)
        {
            //然后通过递归找到对应子GameObject
            Transform secondParentGameObjectTransfrom = FindChildBreadthFirst(firstParentGameObject.transform, secondParentGameObjectName);

            //检测secondParentGameObjectTransfrom是否存在
            if (secondParentGameObjectTransfrom != null)
            {
                //将GameObject调整到对应位置上
                childGameObject.transform.SetParent(secondParentGameObjectTransfrom, true);
            }
            else
            {
                Debug.LogError($"没有在{firstParentGameObject}中找到{secondParentGameObjectName}，请检查hierarchy窗口！");
            }
        }
        else
        {
            Debug.LogError("firstParentGameObject是空的，请检查Hierarchy窗口！");
        }
    }

    /// <summary>
    /// 在当前活动场景的第一层找到对应名字的GameObject
    /// </summary>
    /// <param name="FirstLayerGameObjectName">要寻找的GameObject的名字</param>
    /// <returns></returns>
    public static GameObject FindFromFirstLayer(string FirstLayerGameObjectName)
    {
        //先获取到目前活动的Scene
        Scene activeScene = SceneManager.GetActiveScene();
        //获取到所有的第一层的GameObject
        GameObject[] gameObjects = activeScene.GetRootGameObjects();

        foreach(GameObject obj in gameObjects)
        {
            if(obj.name == FirstLayerGameObjectName)
            {
                return obj;
            }
        }
        Debug.LogError($"没有在该活动场景找到{FirstLayerGameObjectName}");
        return null;
    }

    /// <summary>
    /// 通过递归找到父GameObject中对应的子GameObject (深度优先)
    /// </summary>
    /// <param name="parent">父gameObject的transform</param>
    /// <param name="childName">子类的名字</param>
    /// <returns>目标的Transform</returns>
    public static Transform FindChildRecursive(Transform parent, string childName)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.name == childName)
                return child;

            Transform result = FindChildRecursive(child, childName);

            if (result != null)
                return result;
        }
        return null;
    }

    /// <summary>
    /// 通过递归找到父GameObject中对应的子GameObject (广度优先)
    /// </summary>
    /// <param name="parent">父gameObject的transform</param>
    /// <param name="childName">子类的名字</param>
    /// <returns>目标的Transform</returns>
    public static Transform FindChildBreadthFirst(Transform parent, string childName)
    {
        //先创建一个队列
        Queue<Transform> queue = new Queue<Transform>();
        //将父对象加入队列
        queue.Enqueue(parent);
        while (queue.Count > 0)
        {
            Transform current = queue.Dequeue();
            if (current.name == childName)
            {
                return current;
            }

            for (int i = 0; i < current.childCount; i++)
            {
                queue.Enqueue(current.GetChild(i));
            }
        }

        // 如果遍历完所有子对象都没找到，返回 null
        return null;
    }
}
