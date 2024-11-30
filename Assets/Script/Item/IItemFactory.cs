using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ľӿ�
/// </summary>
public interface IItemFactory<T>
{
    public T CreateItem(GameObject prefab);
}

/// <summary>
/// ����FishType�Ĺ�����
/// </summary>
public class FishTypeFactory : IItemFactory<FishType>
{
    public FishType CreateItem(GameObject prefab)
    {
        return new FishType(prefab);
    }
}

/// <summary>
/// ����ToolType�Ĺ�����
/// </summary>
public class ToolTypeFactory : IItemFactory<ToolType>
{
    public ToolType CreateItem(GameObject prefab)
    {
        return new ToolType(prefab);
    }
}

/// <summary>
/// ����PropType�Ĺ�����
/// </summary>
public class PropTypeFactory : IItemFactory<PropType>
{
    public PropType CreateItem(GameObject prefab)
    {
        return new PropType(prefab);
    }
}

/// <summary>
/// ʹ�ù���ģʽ����BaitTypeʵ��
/// </summary>
public class BaitTypeFactory : IItemFactory<BaitType>
{
    public BaitType CreateItem(GameObject prefab)
    {
        return new BaitType(prefab);
    }
}
