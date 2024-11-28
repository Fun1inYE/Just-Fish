using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// Panel管理器
/// </summary>
public class PanelManager
{
    /// <summary>
    /// 存放Panel的栈
    /// </summary>
    public Stack<BasePanel> stackPanel;

    /// <summary>
    /// 当前操作的panel
    /// </summary>
    public BasePanel panel;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PanelManager()
    {
        stackPanel = new Stack<BasePanel>();
    }
    
    /// <summary>
    /// Panel的入栈操作
    /// </summary>
    /// <param name="nextPanel"></param>
    public void Push(BasePanel nextPanel)
    {
        if(stackPanel.Count > 0)
        {
            panel = stackPanel.Peek();
            panel.OnPasue();
        }

        panel = nextPanel;
        //给面板压入面板栈
        stackPanel.Push(nextPanel);
        //初始化面板的PanelManager
        nextPanel.panelManager = this;
        //通过名字获取到被压入的Panel
        GameObject displayPanel = UIManager.Instance.GetAndDisPlayPoolUI(nextPanel.Name);
        //将当前活跃的Panel转为displayPanel
        panel.activePanel = displayPanel;
        //执行nextPanel的OnEnter方法
        nextPanel.OnEnter();
    }

    /// <summary>
    /// 退出当前面板
    /// </summary>
    public void Pop()
    {
        //退出当前UI
        if (stackPanel.Count > 0)
        {
            stackPanel.Peek().OnExit();
            stackPanel.Pop();
        }
        //恢复后一个UI的状态
        if (stackPanel.Count > 0)
            stackPanel.Peek().OnResume();
    }

    /// <summary>
    /// 退出所有面板
    /// </summary>
    public void AllPop()
    {
        while (stackPanel.Count > 0)
        {
            stackPanel.Pop().OnExit();
        }
    }

    /// <summary>
    /// 执行处在当前面板执行的Update
    /// </summary>
    public void OnUpdate()
    {
        if(stackPanel.Count > 0)
        {
            stackPanel.Peek().OnUpdate();
        }
    }
}
