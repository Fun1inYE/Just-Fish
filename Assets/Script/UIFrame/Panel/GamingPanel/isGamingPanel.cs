using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 进入游戏之后的Panel，默认透明，等待玩家的操作
/// </summary>
public class isGamingPanel : BasePanel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public isGamingPanel() : base("isGamingPanel") { }

    public override void OnUpdate()
    {
        //这里不运行父类的OnUpdate方法
        //进入背包UI
        //进入商店选项UI
        
    }
}
