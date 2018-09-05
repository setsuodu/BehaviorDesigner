using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

// 用来追敌人，直到敌人跑出视野外
public class Defend : Action
{
    public SharedFloat viewDistance;
    public SharedFloat fieldOfViewAngle;

    public SharedFloat speed; //追的速度
    public SharedFloat angularSpeed; //追的角速度

    public SharedTransform target; //抢夺者

    private float viewDistanceSqr; //视野的平方
    private NavMeshAgent navMeshAgent;

    public override void OnAwake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        viewDistanceSqr = viewDistance.Value * viewDistance.Value;

        // 启用导航组件
        navMeshAgent.enabled = true;
        navMeshAgent.speed = speed.Value;
        navMeshAgent.angularSpeed = angularSpeed.Value;
        navMeshAgent.destination = target.Value.position;
    }

    public override void OnEnd()
    {
        // 禁用导航组件
        navMeshAgent.enabled = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (target == null || target.Value == null) //做安全校验
        {
            return TaskStatus.Failure;
        }

        float sqrDistance = (target.Value.position - transform.position).sqrMagnitude;
        float angle = Vector3.Angle(transform.forward, target.Value.position - transform.position);
        if (sqrDistance < viewDistanceSqr && angle < fieldOfViewAngle.Value * 0.5f)
        {
            if (navMeshAgent.destination != target.Value.position)
            {
                navMeshAgent.destination = target.Value.position;
            }
            // 在视野内就追
            return TaskStatus.Running;
        }

        // 否则认为防御成功
        return TaskStatus.Success;
    }
}
