using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工厂类的接口
/// </summary>
public interface IItemFactory<T>
{
    public T CreateItem(GameObject prefab);
}

/// <summary>
/// 返回FishType的工厂类
/// </summary>
public class FishTypeFactory : IItemFactory<FishType>
{
    public FishType CreateItem(GameObject prefab)
    {
        return new FishType(prefab);
    }
}

/// <summary>
/// 返回ToolType的工厂类
/// </summary>
public class ToolTypeFactory : IItemFactory<ToolType>
{
    public ToolType CreateItem(GameObject prefab)
    {
        return new ToolType(prefab);
    }
}

/// <summary>
/// 返回PropType的工厂类
/// </summary>
public class PropTypeFactory : IItemFactory<PropType>
{
    public PropType CreateItem(GameObject prefab)
    {
        return new PropType(prefab);
    }
}

/// <summary>
/// 使用工厂模式返回BaitType实例
/// </summary>
public class BaitTypeFactory : IItemFactory<BaitType>
{
    public BaitType CreateItem(GameObject prefab)
    {
        return new BaitType(prefab);
    }
}
