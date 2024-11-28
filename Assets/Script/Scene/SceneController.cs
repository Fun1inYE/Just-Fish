using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene的控制类
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 要加载的玩家prefab
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// 判断玩家是否在新建存档（默认为true）
    /// </summary>
    public bool isNewArchive = true;

    /// <summary>
    /// SceneController类的单例
    /// </summary>
    public static SceneController Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //单例初始化
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    /// <summary>
    /// 进行游戏场景传送
    /// </summary>
    public void TransformGameScene(string sceneName)
    {
        //执行协程
        StartCoroutine(LoadGameSceneWithCallBack(sceneName));
    }
    public void TransformMainMenuSence(string senceName)
    {
        //执行协程
        StartCoroutine(LoadMainMenuSceneWithCallBack(senceName));
    }


    /// <summary>
    /// 进行游戏场景的异步加载
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadGameSceneWithCallBack(string sceneName)
    {
        //先判断要转换的场景是否为要转换的场景
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            //如果要转的场景不为主菜单的话，就向委托中注册OnGameSceneLoaded
            if (sceneName != "MainMenu")
            {
                SceneManager.sceneLoaded += OnGameSceneLoaded;
            }
            else
            {
                Debug.LogError("转的场景不是游戏内场景，请使用别的异步加载场景");
                yield break;
            }
            
            //创建异步加载的协程变量，开始异步加载
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            //不允许sceneName场景自动执行生命周期
            asyncOperation.allowSceneActivation = false;

            //如果异步写成加载还没有完成
            while (!asyncOperation.isDone)
            {
                //因为如果asyncOperation.allowSceneActivation = false的话，进度是会停在在0.9的，还会在当前场景保留
                if (asyncOperation.progress >= 0.9f)
                {
                    //在当前场景中的MainUI弹出，防止在下一个场景下还会继续使用导致空引用
                    UIManager.Instance.panelManager.AllPop();
                    //将当前场景的PoolManager中的对象池清空，不影响下一个场景的对象池
                    PoolManager.Instance.ClearAllGameObjectPool();

                    //允许完全加载下一个场景，同时下一个场景开始执行生命周期函数
                    asyncOperation.allowSceneActivation = true;
                }

                //此协程在加载场景完毕前不会结束
                yield return null;
            }
        }
    }

    /// <summary>
    /// 进行主菜单场景的异步加载
    /// </summary>
    /// <param name="senceName"></param>
    /// <returns></returns>
    IEnumerator LoadMainMenuSceneWithCallBack(string senceName)
    {
        //先判断要转换的场景是否为要转换的场景
        if (SceneManager.GetActiveScene().name != senceName)
        {
            //如果要转的场景不为主菜单的话，就向委托中注册OnGameSceneLoaded
            if (senceName == "MainMenu")
            {
                SceneManager.sceneLoaded += OnMainMenuLoaded;
            }
            else
            {
                Debug.LogError("转的场景不是主菜单场景，请使用别的异步加载场景语句");
                yield break;
            }

            //创建异步加载的协程变量，开始异步加载
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(senceName);
            //不允许sceneName场景自动执行生命周期
            asyncOperation.allowSceneActivation = false;

            //如果异步写成加载还没有完成
            while (!asyncOperation.isDone)
            {
                //因为如果asyncOperation.allowSceneActivation = false的话，进度是会停在在0.9的，还会在当前场景保留
                if (asyncOperation.progress >= 0.9f)
                {
                    //保存游戏内玩家的数据
                    SaveManager.Instance.SaveAllData();
                    //在当前场景中的MainUI弹出，防止在下一个场景下还会继续使用导致空引用
                    UIManager.Instance.panelManager.AllPop();
                    //将当前场景的PoolManager中的对象池清空，不影响下一个场景的对象池
                    PoolManager.Instance.ClearAllGameObjectPool();

                    //获取到Player
                    GameObject player = SetGameObjectToParent.FindFromFirstLayer("Player");
                    //销毁Player的GameObject
                    Destroy(player);

                    //允许完全加载下一个场景，同时下一个场景开始执行生命周期函数
                    asyncOperation.allowSceneActivation = true;
                }

                //此协程在加载场景完毕前不会结束
                yield return null;
            }
        }
    }

    /// <summary>
    /// 异步加载游戏场景完毕的回调函数
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //生成玩家的GameObject
        GameObject player = Instantiate(playerPrefab, new Vector2(-5f, -3.23f), Quaternion.identity);
        //为玩家GameObject重新命名
        player.name = "Player";

        //进行注册玩家操作和数据相关的脚本
        GameManager.Instance.RigisterPlayer();

        //如果玩家没有新建存档的话，就不执行读取操作
        if(!isNewArchive)
        {
            //先进行一次读取数据
            SaveManager.Instance.LoadAllData();
            isNewArchive = true;
        }

        //初始化完毕之后然后进行存档的初次存储
        SaveManager.Instance.SaveAllData();

        //刷新玩家背包数据
        InventoryManager.Instance.RefreshAllContainer();

        //执行完回调函数中的方法之后取消委托，防止内存泄露
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }

    /// <summary>
    /// 异步加载主菜单场景完毕的回调函数
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public void OnMainMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        //先弹出所有Panel
        UIManager.Instance.panelManager.AllPop();
        //然后压入MainMenuPanel
        UIManager.Instance.panelManager.Push(new StartPanel());

        //执行完回调函数中的方法之后取消委托，防止内存泄露
        SceneManager.sceneLoaded -= OnMainMenuLoaded;
    }
}
