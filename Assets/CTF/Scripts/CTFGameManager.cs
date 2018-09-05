using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class CTFGameManager : MonoBehaviour
{
    private static CTFGameManager _instance;
    public static CTFGameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] List<BehaviorTree> flagNotTakenBehaviorTrees = new List<BehaviorTree>();
    [SerializeField] List<BehaviorTree> flagTakenBehaviorTrees = new List<BehaviorTree>();

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        BehaviorTree[] bts = GameObject.FindObjectsOfType<BehaviorTree>();
        foreach (var bt in bts)
        {
            if (bt.Group == 1)
            {
                flagNotTakenBehaviorTrees.Add(bt);
            }
            else if (bt.Group == 2)
            {
                flagTakenBehaviorTrees.Add(bt);
            }
        }
    }

    // 捡起旗帜
    public void TakeFlag()
    {
        foreach (var bt in flagNotTakenBehaviorTrees)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.DisableBehavior(); //禁用自身
            }
        }

        foreach (var bt in flagTakenBehaviorTrees)
        {
            if (!BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.EnableBehavior(); //启用自身
            }
        }
    }

    // 丢弃旗帜
    public void DropFlag()
    {
        foreach (var bt in flagNotTakenBehaviorTrees)
        {
            if (!BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.EnableBehavior(); //启用自身
            }
        }

        foreach (var bt in flagTakenBehaviorTrees)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.DisableBehavior(); //禁用自身
            }
        }
    }
}
