using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchivePanel : BasePanel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ArchivePanel() : base("ArchivePanel") { }

    //进入游戏的按钮
    public Button goToGameButton;

    /// <summary>
    /// 删除存档按钮
    /// </summary>
    public Button deleteArchiveButton;

    /// <summary>
    /// 取消按钮
    /// </summary>
    public Button cancelButton;

    /// <summary>
    /// 创建存档名字列表
    /// </summary>
    List<string> archiveNameList = new List<string>();
    /// <summary>
    /// 创建存档块信息列表
    /// </summary>
    List<GameObject> archiveInfoPanels = new List<GameObject>();

    /// <summary>
    /// 当前操作的存档块UI
    /// </summary>
    public GameObject currentArchiveInfoPanel;
    /// <summary>
    /// 当前操作的存档名字
    /// </summary>
    public string currentArchiveName;

    //重写开启方法
    public override void OnEnter()
    {
        //如果按钮有空的话，就要重新初始化一下
        if(goToGameButton == null || deleteArchiveButton == null || cancelButton == null)
        {
            goToGameButton = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "GoToGameButton").GetComponent<Button>();
            //因为刚初始化，所以不可交互
            goToGameButton.interactable = false;

            deleteArchiveButton = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "DeleteArchiveButton").GetComponent<Button>();
            //与上面同理
            deleteArchiveButton.interactable = false;

            cancelButton = SetGameObjectToParent.FindChildBreadthFirst(activePanel.transform, "CancelButton").GetComponent<Button>();
        }

        //绑定开始游戏事件
        goToGameButton.onClick.AddListener(() =>
        {
            //开始游戏
            GotoGame();
        });

        deleteArchiveButton.onClick.AddListener(() =>
        {
            //删除存档
            DeleteArchive();
        });

        //绑定按钮功能
        cancelButton.onClick.AddListener(() =>
        {
            //弹出窗口
            panelManager.Pop();
        });


        //获取游戏存档目录的名字，顺带计数
        archiveNameList = SaveManager.Instance.ListArchiveName();

        for(int i = 0; i < archiveNameList.Count; i++)
        {
            //显示UI顺带将此UI加入到archiveInfoPanels列表中
            archiveInfoPanels.Add(UIManager.Instance.GetAndDisPlayPoolUI("ArchiveInfoPanel"));
            //为存档块UI更改Text
            ReadPlayerGameDataFileAndChangeText(archiveNameList[i], archiveInfoPanels[i]);
            //为存档块UI添加对应的Button功能
            SetArchiveInfoPanelGameButton(archiveNameList[i], archiveInfoPanels[i]);
            //将存档块挪回ArchiveContent中
            SetGameObjectToParent.SetParentFromFirstLayerParent("MainMenuCanvas", "ArchiveContent", archiveInfoPanels[i]);

        }   

        #region TestCode

        //for (int i = 0; i < archiveInfoPanels.Count; i++)
        //{
        //    Debug.Log(archiveInfoPanels[i]);
        //}

        #endregion

    }

    public override void OnExit()
    {
        //取消绑定按钮功能
        goToGameButton.onClick.RemoveAllListeners();
        deleteArchiveButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        //清空archiveInfoPanels中的信息
        for (int i = 0; i < archiveInfoPanels.Count; i++)
        {
            //关闭存档块UI
            UIManager.Instance.HideUI(archiveInfoPanels[i]);
            //将UI放回原来的地方
            SetGameObjectToParent.SetParent("MainMenuCanvas", archiveInfoPanels[i]);
        }

        //清空archiveInfoPanels的GameObject，方便下次存储
        archiveInfoPanels.Clear();

        //关闭按钮交互
        goToGameButton.interactable = false;
        deleteArchiveButton.interactable = false;

        //执行父类的OnExit方法
        base.OnExit();
    }

    /// <summary>
    /// 为存档块更改Text
    /// </summary>
    /// <param name="archiveName">对应存档名字</param>
    /// <param name="archiveInfoPanel">对应存档块的UI</param>
    public void ReadPlayerGameDataFileAndChangeText(string archiveName, GameObject archiveInfoPanel)
    {
        //读取对应文件标识符
        PlayerDataIditentifier playerDataIditentifier = SaveManager.Instance.Load<PlayerDataIditentifier>(archiveName, "playerGameDataIdentifier.sav");
        GameDataIdentifier gameDataIdentifier = SaveManager.Instance.Load<GameDataIdentifier>(archiveName, "GameDataIdentifier.sav");
        //存档名字
        archiveInfoPanel.transform.GetChild(0).GetComponent<Text>().text = archiveName;
        //玩家名字
        archiveInfoPanel.transform.GetChild(1).GetComponent<Text>().text = "名字：" + playerDataIditentifier.playerNameIdetentifier;
        //玩家金币数量
        archiveInfoPanel.transform.GetChild(2).GetComponent<Text>().text = "金币：" + playerDataIditentifier.coinIdetentifier.ToString();
        //TODO:玩家经验

        //存档创建时间
        archiveInfoPanel.transform.GetChild(4).GetComponent<Text>().text = "创建时间：" + gameDataIdentifier.createArchiveTimeIdentifier.ToString();
        //存档更改时间
        archiveInfoPanel.transform.GetChild(5).GetComponent<Text>().text = "修改时间：" + gameDataIdentifier.reviseArchiveTimeIdentifier.ToString();
    }

    /// <summary>
    /// 设置ArchivePanel中的Button,以达到选中该存档块的功能
    /// </summary>
    public void SetArchiveInfoPanelGameButton(string archiveName, GameObject archiveInfoPanel)
    {
        //读取对应文件标识符
        PlayerDataIditentifier playerDataIditentifier = SaveManager.Instance.Load<PlayerDataIditentifier>(archiveName, "playerGameDataIdentifier.sav");
        GameDataIdentifier gameDataIdentifier = SaveManager.Instance.Load<GameDataIdentifier>(archiveName, "GameDataIdentifier.sav");

        //给存档块添加进入游戏按钮的功能
        archiveInfoPanel.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() =>
        {
            //第一次点击存档块
            if(currentArchiveInfoPanel == null)
            {
                currentArchiveInfoPanel = archiveInfoPanel;
                //将选中的存档块变暗(打开SelectPanel)
                currentArchiveInfoPanel.transform.GetChild(7).gameObject.SetActive(true);

                //将当前操作的存档名字更改成现在的存档名字
                currentArchiveName = archiveName;

                //允许按钮交互了
                goToGameButton.interactable = true;
                deleteArchiveButton.interactable = true;
            }
            //如果之前选中过存档块
            else if(currentArchiveInfoPanel != archiveInfoPanel)
            {
                //将上一个选中的Panel调回原来的颜色（关闭SelectPanel）
                currentArchiveInfoPanel.transform.GetChild(7).gameObject.SetActive(false);
                //将现在的Panel调成暗色
                currentArchiveInfoPanel = archiveInfoPanel;
                currentArchiveInfoPanel.transform.GetChild(7).gameObject.SetActive(true);

                //将当前操作的存档名字更改成现在的存档名字
                currentArchiveName = archiveName;

                //允许按钮交互了
                goToGameButton.interactable = true;
                deleteArchiveButton.interactable = true;
            }
            
        });
    }

    /// <summary>
    /// 删除存档UI和存档数据的方法
    /// </summary>
    public void DeleteArchive()
    {
        //如果当前存档名字不为空的话,也同时代表玩家选中了一个存档
        if(currentArchiveName != null)
        {
            //如果存档UI列表中包含这个存档UI
            if(archiveInfoPanels.Contains(currentArchiveInfoPanel))
            {
                //关闭存档块UI
                UIManager.Instance.HideUI(currentArchiveInfoPanel);
                //将UI放回原来的地方
                SetGameObjectToParent.SetParent("MainMenuCanvas", currentArchiveInfoPanel);
            }
            //从列表UI中移除当前UI
            archiveInfoPanels.Remove(currentArchiveInfoPanel);
            //从存档名字列表中移除当前存档名字
            archiveNameList.Remove(currentArchiveName);
            //将当前操作的UI置空
            currentArchiveInfoPanel = null;
            //通过当前存档名字删除对应存档数据
            SaveManager.Instance.Delete(currentArchiveName);
            //将当前操作的存档名字置空
            currentArchiveName = null;
        }

        //删除完存档，因为没有存档选中，所以按钮无法交互
        goToGameButton.interactable = false;
        deleteArchiveButton.interactable = false;
    }

    /// <summary>
    /// 进入游戏加载UI
    /// </summary>
    public void GotoGame()
    {
        if (currentArchiveName != null)
        {
            //将游戏存档名字更改成玩家所选存档名字
            GameManager.Instance.gameData.archiveSaveName = currentArchiveName;
            //弹出所有窗口，进入加载页面，并且告诉SenceController玩家在读档
            panelManager.AllPop();
            panelManager.Push(new LoadingPanel(false));
        }
    }
}
