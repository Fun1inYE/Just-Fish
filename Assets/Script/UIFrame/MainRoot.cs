using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoot : MonoBehaviour
{
    //��ʼ��Panel
    InitPanel initPanel;

    public void Awake()
    {
        initPanel = new InitPanel("Prefab/UI/MainUI");
        initPanel.InitTypes(1);

        //�����浵����
        initPanel = new InitPanel("Prefab/UI/ArchiveInfoPanel");
        initPanel.InitTypes(20);
    }
}
