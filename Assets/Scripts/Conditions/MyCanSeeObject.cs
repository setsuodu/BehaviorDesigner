using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

// 用来判断目标是否在视野内
public class MyCanSeeObject : Conditional
{
    public Transform[] targets; //要寻找的敌人
    //public float fieldOfView = 90;
    public SharedFloat sharedFieldOfView = 90;
    //public float fieldViewDistance = 7f;
    public SharedFloat sharedFieldViewDistance = 7f;
    public SharedTransform target;

    public override TaskStatus OnUpdate()
    {
        if (targets == null)
        {
            return TaskStatus.Failure;
        }

        foreach (var _target in targets)
        {
            // 角度
            float angle = Vector3.Angle(transform.forward, _target.position - transform.position);
            // 距离
            float distance = (_target.position - transform.position).magnitude;

            if (distance < sharedFieldViewDistance.Value && angle < sharedFieldOfView.Value)
            {
                this.target.Value = _target;
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }

        return base.OnUpdate();
    }
}
