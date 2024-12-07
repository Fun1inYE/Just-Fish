 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ����ع�������������
/// </summary>
public class PoolManager : MonoBehaviour
{
    /// <summary>
    /// PoolManager�����ı���
    /// </summary>
    public static PoolManager Instance;

    /// <summary>
    /// ����һ��keyΪ���ӵ����֣�valueΪGameObject���ֵ䣬���ڴ洢��ͬ�ĳ���
    /// </summary>
    public Dictionary<string, Queue<GameObject>> gameObjectPool;

    /// <summary>
    /// ���õ�GameObject���Է��ù�
    /// </summary>
    public Dictionary<string, GameObject> spareGameObject;

    /// <summary>
    /// ���ڼ�¼ÿһ���ڳ�����Ӧ�ó�ʼ���ڵ�λ����
    /// </summary>
    public Dictionary<string, string> spareGameObjectTransform;

    public void Awake()
    {
        //�����ĳ�ʼ��
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //�ֵ��ʼ��
        gameObjectPool = new Dictionary<string, Queue<GameObject>>();
        spareGameObject = new Dictionary<string, GameObject>();
        spareGameObjectTransform = new Dictionary<string, string>();
    }

    /// <summary>
    /// ����gameObject���ӵķ���
    /// </summary>
    /// <param name="gameObj">Ҫ�ػ��Ķ���</param>
    /// <param name="poolSize">�������ӵĴ�С</param>
    /// <param name="transformTOPoolName">Ҫ�ƶ�����λ�õ�����</param>
    /// TODO: ��Ҫ������������д
    public void CreatGameObjectPool(GameObject gameObj, int poolSize, string transformTOPoolName)
    {
        string poolKey = gameObj.name;

        //�ж��ֵ����Ƿ��ж�Ӧ���ֵ�
        if(!gameObjectPool.ContainsKey(poolKey))
        {
            //�����³��ӵĶ���
            gameObjectPool[poolKey] = new Queue<GameObject>();
            //��¼����GameObject�ֵ�
            spareGameObject[poolKey] = gameObj;
            //��¼���õ�GameObjectӦ��������ʲô�ط�
            spareGameObjectTransform[poolKey] = transformTOPoolName;

            for (int i = 0; i < poolSize; i++)
            {
                //����GameObject
                GameObject obj = Instantiate(gameObj);
                //�����ߵĿո�ȥ�������ҽ����ɵ�GameObject��������ֺ���ģ�Clone��ȥ��
                obj.name = obj.gameObject.name.Replace("(Clone)", "").Trim();
                //��GameObject�����ƶ���"Pool"
                SetGameObjectToParent.SetParent(transformTOPoolName, obj);
                //��GameObject�رգ��ȴ�����
                obj.SetActive(false);

                //��obj�������
                gameObjectPool[poolKey].Enqueue(obj);
            }
        }
    }

    /// <summary>
    /// ȡ��������еĶ���
    /// </summary>
    /// <param name="gameObj">Ҫȡ�õ�gameObject</param>
    /// <returns>���ִ�������ͷ��ض�Ӧ��gameObject��������ز������ͷ���null</returns>
    public GameObject GetGameObjectFromPool(string gameObjName)
    {
        string poolKey = gameObjName;

        //�жϳ����Ƿ����
        if(gameObjectPool.ContainsKey(poolKey))
        {
            //�жϳ����л��ж���
            if (gameObjectPool[poolKey].Count > 0)
            {
                GameObject obj = gameObjectPool[poolKey].Dequeue();
                return obj;
            }
            else
            {
                Debug.Log("***�������޶��������������ɶ���......");
                //ʹ�ñ��õ�GameObject
                GameObject obj = Instantiate(spareGameObject[poolKey]);
                //��GameObject�����ƶ�����¼��λ����
                SetGameObjectToParent.SetParent(spareGameObjectTransform[poolKey], obj);
                obj.name = gameObjName;
                return obj;
            }
        }
        else
        {
            Debug.LogError($"û������Ϊ{poolKey}�Ķ���أ�������룡");
            return null;
        }
    }    

    /// <summary>
    /// ����Ӧ��gameObject�Żس�����
    /// </summary>
    /// <param name="gameObj">Ҫ�Żض�Ӧ���ӵ�gameObject</param>
    public void ReturnGameObjectToPool(GameObject gameObj)
    {
        //ȷ����Ѱ�ҵ���Ӧ������
        string poolKey = gameObj.name.Replace("(Clone)", "").Trim();

        if(gameObjectPool.ContainsKey(poolKey))
        {
            gameObj.SetActive(false);
            gameObjectPool[poolKey].Enqueue(gameObj);

        }
        else
        {
            Debug.LogError($"û������Ϊ{poolKey}�Ķ���أ�������룡");
        }
    }

    /// <summary>
    /// ɾ����Ӧ�Ķ����
    /// </summary>
    /// <param name="gameObj">��Ӧ�Ķ����</param>
    public void DeleteGameObjectPool(GameObject gameObj)
    {
        string poolKey = gameObj.name;
        if(gameObjectPool.ContainsKey(poolKey))
        {
            //��ն�Ӧ�����еĶ���
            gameObjectPool[poolKey].Clear();
            //ɾ���ֵ��ж�Ӧ�Ķ���
            gameObjectPool.Remove(poolKey);
        }
        else
        {
            Debug.LogError($"û������Ϊ{poolKey}�Ķ���أ�������룡");
        }
    }

    /// <summary>
    /// ɾ�����еĶ����
    /// </summary>
    public void ClearAllGameObjectPool()
    {
        //��Ϊforeach����ֱ���ڱ������޸�Ҫ�����ı�����������ToList����һ������
        foreach(string poolKey in gameObjectPool.Keys.ToList())
        {
            gameObjectPool[poolKey].Clear();
        }
        //����ֵ�
        gameObjectPool.Clear();
    }

    
    public void CheckPool()
    {
        Debug.Log("Ŀǰ���ɵĳ����У�\n");
        foreach(string key in gameObjectPool.Keys)
        {
            Debug.Log($"����Ϊ{key}�ĳ��ӣ�����������ǣ�{gameObjectPool[key].GetType().Name},��СΪ��{gameObjectPool[key].Count}");
        }
    }

    #region TestCode
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CheckPool();
        }
    }
    #endregion
}
