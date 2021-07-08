using System;
using System.Collections.Generic;

[System.Serializable]
public struct FStatusData
{
    public float MoveSpeed;
    public float Hp;
    public float Attack;
    public float Armor;

    public static FStatusData operator +(FStatusData a) => a;

    public static FStatusData operator -(FStatusData a) => new FStatusData()
    {
        MoveSpeed = -a.MoveSpeed,
        Hp = -a.Hp,
        Attack = -a.Attack,
        Armor = -a.Armor
    };

    public static FStatusData operator +(FStatusData a, FStatusData b) => new FStatusData()
    {
        MoveSpeed = a.MoveSpeed + b.MoveSpeed,
        Hp = a.Hp + b.Hp,
        Attack = a.Attack + b.Attack,
        Armor = a.Armor + b.Armor
    };

    public static FStatusData operator -(FStatusData a, FStatusData b) => a + (-b);

    public static FStatusData operator *(FStatusData a, FStatusData b) => new FStatusData()
    {
        MoveSpeed = a.MoveSpeed * b.MoveSpeed,
        Hp = a.Hp * b.Hp,
        Attack = a.Attack * b.Attack,
        Armor = a.Armor * b.Armor
    };

}


public delegate void DIncreaseStatus(FStatusData add);

public class CStatusTree
{
    public CStatusTree(Func<FStatusData> inOrigin)
    {
        origin = inOrigin;
    }

    public void UpdateStatus(in FStatusData data)
    {
        if (parent != null)
        {
            parent.UpdateStatus(data);
        }

        OnIncreaseStatus?.Invoke(data);
    }

    public void SetParent(CStatusTree other)
    {
        if (other != null)
        {
		    // AddChild에서 변수 셋팅
            other.AddChild(this);
            return;
        }

        if (parent != null)
        {
            parent.RemoveChild(this);
        }

    }

    public void AddChild(CStatusTree other)
    {
        if (other == null) return;
        if (other == this) return;
        if (other.parent == this) return;

        CStatusTree otherParent = other.parent;

        if (otherParent != null)
        {
            otherParent.RemoveChild(otherParent);
        }

        // 루프로 연결 되는 것을 방지하기 위해 ('A - B - C - A' 이런 형태가 되면 무한 루프가 된다)
        for (var p = this; p != null; p = p.parent)
        {
            // (A - B - C) 에서 B를 C 밑으로 부모를 넣는 다면 (A - C - B)로 만든다.
            if (p.parent == other)
            {
                p.SetParent(otherParent);
                break;
            }
        }

        childs.Add(other);
        other.parent = this;
        UpdateStatus(other.origin());
    }

    public void RemoveChild(CStatusTree other)
    {
        if (childs.Remove(other))
        {
            other.parent = null;
            UpdateStatus(-other.origin());
        }

    }

    // 부모 자식 관계 해제
    public void UnLink()
    {
        foreach (CStatusTree child in childs)
        {
            child.SetParent(parent);
        }
        SetParent(null);
    }

    public void AddListener(DIncreaseStatus value)
    {
        OnIncreaseStatus += value;
    }

    public void RemoveListener(DIncreaseStatus value)
    {
        OnIncreaseStatus -= value;
    }

    public void ClearListener()
    {
        OnIncreaseStatus = null;
    }


    private CStatusTree parent;
    private readonly List<CStatusTree> childs = new List<CStatusTree>();
    private readonly Func<FStatusData> origin;
    private DIncreaseStatus OnIncreaseStatus;

}

