using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene�Ŀ�����
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Ҫ���ص����prefab
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// �ж�����Ƿ����½��浵��Ĭ��Ϊtrue��
    /// </summary>
    public bool isNewArchive = true;

    /// <summary>
    /// SceneController��ĵ���
    /// </summary>
    public static SceneController Instance;

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
            DontDestroyOnLoad(gameObject);
        }
    }
    
    /// <summary>
    /// ������Ϸ��������
    /// </summary>
    public void TransformGameScene(string sceneName)
    {
        //ִ��Э��
        StartCoroutine(LoadGameSceneWithCallBack(sceneName));
    }
    public void TransformMainMenuSence(string senceName)
    {
        //ִ��Э��
        StartCoroutine(LoadMainMenuSceneWithCallBack(senceName));
    }


    /// <summary>
    /// ������Ϸ�������첽����
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadGameSceneWithCallBack(string sceneName)
    {
        //���ж�Ҫת���ĳ����Ƿ�ΪҪת���ĳ���
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            //���Ҫת�ĳ�����Ϊ���˵��Ļ�������ί����ע��OnGameSceneLoaded
            if (sceneName != "MainMenu")
            {
                SceneManager.sceneLoaded += OnGameSceneLoaded;
            }
            else
            {
                Debug.LogError("ת�ĳ���������Ϸ�ڳ�������ʹ�ñ���첽���س���");
                yield break;
            }
            
            //�����첽���ص�Э�̱�������ʼ�첽����
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            //������sceneName�����Զ�ִ����������
            asyncOperation.allowSceneActivation = false;

            //����첽д�ɼ��ػ�û�����
            while (!asyncOperation.isDone)
            {
                //��Ϊ���asyncOperation.allowSceneActivation = false�Ļ��������ǻ�ͣ����0.9�ģ������ڵ�ǰ��������
                if (asyncOperation.progress >= 0.9f)
                {
                    //�ڵ�ǰ�����е�MainUI��������ֹ����һ�������»������ʹ�õ��¿�����
                    UIManager.Instance.panelManager.AllPop();
                    //����ǰ������PoolManager�еĶ������գ���Ӱ����һ�������Ķ����
                    PoolManager.Instance.ClearAllGameObjectPool();

                    //������ȫ������һ��������ͬʱ��һ��������ʼִ���������ں���
                    asyncOperation.allowSceneActivation = true;
                }

                //��Э���ڼ��س������ǰ�������
                yield return null;
            }
        }
    }

    /// <summary>
    /// �������˵��������첽����
    /// </summary>
    /// <param name="senceName"></param>
    /// <returns></returns>
    IEnumerator LoadMainMenuSceneWithCallBack(string senceName)
    {
        //���ж�Ҫת���ĳ����Ƿ�ΪҪת���ĳ���
        if (SceneManager.GetActiveScene().name != senceName)
        {
            //���Ҫת�ĳ�����Ϊ���˵��Ļ�������ί����ע��OnGameSceneLoaded
            if (senceName == "MainMenu")
            {
                SceneManager.sceneLoaded += OnMainMenuLoaded;
            }
            else
            {
                Debug.LogError("ת�ĳ����������˵���������ʹ�ñ���첽���س������");
                yield break;
            }

            //�����첽���ص�Э�̱�������ʼ�첽����
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(senceName);
            //������sceneName�����Զ�ִ����������
            asyncOperation.allowSceneActivation = false;

            //����첽д�ɼ��ػ�û�����
            while (!asyncOperation.isDone)
            {
                //��Ϊ���asyncOperation.allowSceneActivation = false�Ļ��������ǻ�ͣ����0.9�ģ������ڵ�ǰ��������
                if (asyncOperation.progress >= 0.9f)
                {
                    //������Ϸ����ҵ�����
                    SaveManager.Instance.SaveAllData();
                    //�ڵ�ǰ�����е�MainUI��������ֹ����һ�������»������ʹ�õ��¿�����
                    UIManager.Instance.panelManager.AllPop();
                    //����ǰ������PoolManager�еĶ������գ���Ӱ����һ�������Ķ����
                    PoolManager.Instance.ClearAllGameObjectPool();

                    //��ȡ��Player
                    GameObject player = SetGameObjectToParent.FindFromFirstLayer("Player");
                    //����Player��GameObject
                    Destroy(player);

                    //������ȫ������һ��������ͬʱ��һ��������ʼִ���������ں���
                    asyncOperation.allowSceneActivation = true;
                }

                //��Э���ڼ��س������ǰ�������
                yield return null;
            }
        }
    }

    /// <summary>
    /// �첽������Ϸ������ϵĻص�����
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //������ҵ�GameObject
        GameObject player = Instantiate(playerPrefab, new Vector2(-5f, -3.23f), Quaternion.identity);
        //Ϊ���GameObject��������
        player.name = "Player";

        //����ע����Ҳ�����������صĽű�
        GameManager.Instance.RigisterPlayer();

        //������û���½��浵�Ļ����Ͳ�ִ�ж�ȡ����
        if(!isNewArchive)
        {
            //�Ƚ���һ�ζ�ȡ����
            SaveManager.Instance.LoadAllData();
            isNewArchive = true;
        }

        //��ʼ�����֮��Ȼ����д浵�ĳ��δ洢
        SaveManager.Instance.SaveAllData();

        //ˢ����ұ�������
        InventoryManager.Instance.RefreshAllContainer();

        //ִ����ص������еķ���֮��ȡ��ί�У���ֹ�ڴ�й¶
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }

    /// <summary>
    /// �첽�������˵�������ϵĻص�����
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public void OnMainMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        //�ȵ�������Panel
        UIManager.Instance.panelManager.AllPop();
        //Ȼ��ѹ��MainMenuPanel
        UIManager.Instance.panelManager.Push(new StartPanel());

        //ִ����ص������еķ���֮��ȡ��ί�У���ֹ�ڴ�й¶
        SceneManager.sceneLoaded -= OnMainMenuLoaded;
    }
}
