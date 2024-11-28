using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制器接口
/// </summary>
public interface IController
{
    /// <summary>
    /// 判断这个继承自这个接口的控制类是否可以运行
    /// </summary>
    public bool canRun { get; set; }
}
