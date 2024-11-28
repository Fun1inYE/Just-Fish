 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 对象池管理器（单例）
/// </summary>
public class PoolManager : MonoBehaviour
{
    /// <summary>
    /// PoolManager单例的变量
    /// </summary>
    public static PoolManager Instance;

    /// <summary>
    /// 创建一个key为池子的名字，value为GameObject的字典，用于存储不同的池子
    /// </summary>
    public Dictionary<string, Queue<GameObject>> gameObjectPool;

    /// <summary>
    /// 备用的GameObject，以防用光
    /// </summary>
    public Dictionary<string, GameObject> spareGameObject;

    /// <summary>
    /// 用于记录每一个在池子中应该初始化在的位置上
    /// </summary>
    public Dictionary<string, string> spareGameObjectTransform;

    public void Awake()
    {
        //单例的初始化
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //字典初始化
        gameObjectPool = new Dictionary<string, Queue<GameObject>>();
        spareGameObject = new Dictionary<string, GameObject>();
        spareGameObjectTransform = new Dictionary<string, string>();
    }

    /// <summary>
    /// 创建gameObject池子的方法
    /// </summary>
    /// <param name="gameObj">要池化的对象</param>
    /// <param name="poolSize">创建池子的大小</param>
    /// <param name="transformTOPoolName">要移动到的位置的名字</param>
    /// TODO: 需要根据需求重新写
    public void CreatGameObjectPool(GameObject gameObj, int poolSize, string transformTOPoolName)
    {
        string poolKey = gameObj.name;

        //判断字典中是否有对应名字的
        if(!gameObjectPool.ContainsKey(poolKey))
        {
            //创建新池子的队列
            gameObjectPool[poolKey] = new Queue<GameObject>();
            //记录备用GameObject字典
            spareGameObject[poolKey] = gameObj;
            //记录备用的GameObject应该生成在什么地方
            spareGameObjectTransform[poolKey] = transformTOPoolName;

            for (int i = 0; i < poolSize; i++)
            {
                //生成GameObject
                GameObject obj = Instantiate(gameObj);
                //将两边的空格去掉，并且将生成的GameObject对象的名字后面的（Clone）去掉
                obj.name = obj.gameObject.name.Replace("(Clone)", "").Trim();
                //将GameObject对象移动到"Pool"
                SetGameObjectToParent.SetParent(transformTOPoolName, obj);
                //将GameObject关闭，等待调用
                obj.SetActive(false);

                //将obj进入队列
                gameObjectPool[poolKey].Enqueue(obj);
            }
        }
    }

    /// <summary>
    /// 取出对象池中的对象
    /// </summary>
    /// <param name="gameObj">要取用的gameObject</param>
    /// <returns>如果执行正常就返回对应的gameObject，如果返回不正常就返回null</returns>
    public GameObject GetGameObjectFromPool(string gameObjName)
    {
        string poolKey = gameObjName;

        //判断池子是否存在
        if(gameObjectPool.ContainsKey(poolKey))
        {
            //判断池子中还有对象
            if (gameObjectPool[poolKey].Count > 0)
            {
                GameObject obj = gameObjectPool[poolKey].Dequeue();
                return obj;
            }
            else
            {
                Debug.Log("***池子中无对象，正在重新生成对象......");
                //使用备用的GameObject
                GameObject obj = Instantiate(spareGameObject[poolKey]);
                //将GameObject对象移动到记录的位置上
                SetGameObjectToParent.SetParent(spareGameObjectTransform[poolKey], obj);
                obj.name = gameObjName;
                return obj;
            }
        }
        else
        {
            Debug.LogError($"没有名字为{poolKey}的对象池，请检查代码！");
            return null;
        }
    }    

    /// <summary>
    /// 将对应的gameObject放回池子中
    /// </summary>
    /// <param name="gameObj">要放回对应池子的gameObject</param>
    public void ReturnGameObjectToPool(GameObject gameObj)
    {
        //确保能寻找到对应池子名
        string poolKey = gameObj.name.Replace("(Clone)", "").Trim();

        if(gameObjectPool.ContainsKey(poolKey))
        {
            gameObj.SetActive(false);
            gameObjectPool[poolKey].Enqueue(gameObj);

        }
        else
        {
            Debug.LogError($"没有名字为{poolKey}的对象池，请检查代码！");
        }
    }

    /// <summary>
    /// 删除对应的对象池
    /// </summary>
    /// <param name="gameObj">对应的对象池</param>
    public void DeleteGameObjectPool(GameObject gameObj)
    {
        string poolKey = gameObj.name;
        if(gameObjectPool.ContainsKey(poolKey))
        {
            //清空对应池子中的队列
            gameObjectPool[poolKey].Clear();
            //删除字典中对应的队列
            gameObjectPool.Remove(poolKey);
        }
        else
        {
            Debug.LogError($"没有名字为{poolKey}的对象池，请检查代码！");
        }
    }

    /// <summary>
    /// 删除所有的对象池
    /// </summary>
    public void ClearAllGameObjectPool()
    {
        //因为foreach不让直接在遍历中修改要遍历的变量，所以用ToList建立一个副本
        foreach(string poolKey in gameObjectPool.Keys.ToList())
        {
            gameObjectPool[poolKey].Clear();
        }
        //清空字典
        gameObjectPool.Clear();
    }

    
    public void CheckPool()
    {
        Debug.Log("目前生成的池子有：\n");
        foreach(string key in gameObjectPool.Keys)
        {
            Debug.Log($"名字为{key}的池子，里面的内容是：{gameObjectPool[key].GetType().Name},大小为：{gameObjectPool[key].Count}");
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
