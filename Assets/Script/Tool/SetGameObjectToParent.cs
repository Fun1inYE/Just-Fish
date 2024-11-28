using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��GameObject�ƶ�����ӦGameObject��
/// </summary>
public static class SetGameObjectToParent
{
    /// <summary>
    /// ֻ�ڵ�ǰ������ĵ�һ��Ѱ�Ҷ�Ӧ���ֵ�GameObject
    /// </summary>
    /// <param name="parentGameObjectName">��GameObject������</param>
    /// <param name="childGameObject">��GameObject</param>
    public static void SetParent(string parentGameObjectName, GameObject childGameObject)
    {
        //��ͨ�������ҵ���GameObject
        GameObject parentGameObject = FindFromFirstLayer(parentGameObjectName);
        if (parentGameObject != null)
        {
            //����GameObject�ƶ�����Ӧ��GameObject�²��ұ��ֱ�������
            childGameObject.transform.SetParent(parentGameObject.transform, false);
        }
        else
        {
            Debug.LogError($"û���ҵ�����Ϊ{parentGameObjectName}��GameObject");
        }
    }

    /// <summary>
    /// �ӵ�һ���ҵ���gameObject�µ�gameObject��Ϊ��gameobject��gameObject
    /// </summary>
    /// <param name="firstParentGameObjectName">����ĸ�Object</param>
    /// <param name="secondParentGameObjectName">��Object</param>
    /// <param name="childGameObject">��gameObject</param>
    public static void SetParentFromFirstLayerParent(string firstParentGameObjectName, string secondParentGameObjectName, GameObject childGameObject)
    {
        //��ͨ�������ҵ���GameObject
        GameObject firstParentGameObject = FindFromFirstLayer(firstParentGameObjectName);
        if (firstParentGameObject != null)
        {
            //Ȼ��ͨ���ݹ��ҵ���Ӧ��GameObject
            Transform secondParentGameObjectTransfrom = FindChildBreadthFirst(firstParentGameObject.transform, secondParentGameObjectName);

            //���secondParentGameObjectTransfrom�Ƿ����
            if (secondParentGameObjectTransfrom != null)
            {
                //��GameObject��������Ӧλ����
                childGameObject.transform.SetParent(secondParentGameObjectTransfrom, true);
            }
            else
            {
                Debug.LogError($"û����{firstParentGameObject}���ҵ�{secondParentGameObjectName}������hierarchy���ڣ�");
            }
        }
        else
        {
            Debug.LogError("firstParentGameObject�ǿյģ�����Hierarchy���ڣ�");
        }
    }

    /// <summary>
    /// �ڵ�ǰ������ĵ�һ���ҵ���Ӧ���ֵ�GameObject
    /// </summary>
    /// <param name="FirstLayerGameObjectName">ҪѰ�ҵ�GameObject������</param>
    /// <returns></returns>
    public static GameObject FindFromFirstLayer(string FirstLayerGameObjectName)
    {
        //�Ȼ�ȡ��Ŀǰ���Scene
        Scene activeScene = SceneManager.GetActiveScene();
        //��ȡ�����еĵ�һ���GameObject
        GameObject[] gameObjects = activeScene.GetRootGameObjects();

        foreach(GameObject obj in gameObjects)
        {
            if(obj.name == FirstLayerGameObjectName)
            {
                return obj;
            }
        }
        Debug.LogError($"û���ڸû�����ҵ�{FirstLayerGameObjectName}");
        return null;
    }

    /// <summary>
    /// ͨ���ݹ��ҵ���GameObject�ж�Ӧ����GameObject (�������)
    /// </summary>
    /// <param name="parent">��gameObject��transform</param>
    /// <param name="childName">���������</param>
    /// <returns>Ŀ���Transform</returns>
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
    /// ͨ���ݹ��ҵ���GameObject�ж�Ӧ����GameObject (�������)
    /// </summary>
    /// <param name="parent">��gameObject��transform</param>
    /// <param name="childName">���������</param>
    /// <returns>Ŀ���Transform</returns>
    public static Transform FindChildBreadthFirst(Transform parent, string childName)
    {
        //�ȴ���һ������
        Queue<Transform> queue = new Queue<Transform>();
        //��������������
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

        // ��������������Ӷ���û�ҵ������� null
        return null;
    }
}
