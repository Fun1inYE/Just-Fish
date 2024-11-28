using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoot : MonoBehaviour
{
    //初始化Panel
    InitPanel initPanel;

    public void Awake()
    {
        initPanel = new InitPanel("Prefab/UI/MainUI");
        initPanel.InitTypes(1);

        //创建存档格子
        initPanel = new InitPanel("Prefab/UI/ArchiveInfoPanel");
        initPanel.InitTypes(20);
    }
}
