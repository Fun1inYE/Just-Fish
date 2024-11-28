using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// Panel������
/// </summary>
public class PanelManager
{
    /// <summary>
    /// ���Panel��ջ
    /// </summary>
    public Stack<BasePanel> stackPanel;

    /// <summary>
    /// ��ǰ������panel
    /// </summary>
    public BasePanel panel;

    /// <summary>
    /// ���캯��
    /// </summary>
    public PanelManager()
    {
        stackPanel = new Stack<BasePanel>();
    }
    
    /// <summary>
    /// Panel����ջ����
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
        //�����ѹ�����ջ
        stackPanel.Push(nextPanel);
        //��ʼ������PanelManager
        nextPanel.panelManager = this;
        //ͨ�����ֻ�ȡ����ѹ���Panel
        GameObject displayPanel = UIManager.Instance.GetAndDisPlayPoolUI(nextPanel.Name);
        //����ǰ��Ծ��PanelתΪdisplayPanel
        panel.activePanel = displayPanel;
        //ִ��nextPanel��OnEnter����
        nextPanel.OnEnter();
    }

    /// <summary>
    /// �˳���ǰ���
    /// </summary>
    public void Pop()
    {
        //�˳���ǰUI
        if (stackPanel.Count > 0)
        {
            stackPanel.Peek().OnExit();
            stackPanel.Pop();
        }
        //�ָ���һ��UI��״̬
        if (stackPanel.Count > 0)
            stackPanel.Peek().OnResume();
    }

    /// <summary>
    /// �˳��������
    /// </summary>
    public void AllPop()
    {
        while (stackPanel.Count > 0)
        {
            stackPanel.Pop().OnExit();
        }
    }

    /// <summary>
    /// ִ�д��ڵ�ǰ���ִ�е�Update
    /// </summary>
    public void OnUpdate()
    {
        if(stackPanel.Count > 0)
        {
            stackPanel.Peek().OnUpdate();
        }
    }
}
