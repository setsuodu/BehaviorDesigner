using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

// 这个任务脚本的作用，是控制游戏物体到达指定目标位置
public class MySeek : Action
{
    public float speed;
    public SharedTransform target;
    public float arriveDistance = 0.1f;
    private float sqrArriveDistance;

    public override void OnStart()
    {
        sqrArriveDistance = arriveDistance * arriveDistance;
        base.OnStart();
    }

    // 一直调用直到任务结束（返回成功或失败时）
    public override TaskStatus OnUpdate()
    {
        if (target == null || target.Value == null)
        {
            return TaskStatus.Failure;
        }

        transform.LookAt(target.Value.position);
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);

        if ((transform.position - target.Value.position).sqrMagnitude < sqrArriveDistance)
        {
            return TaskStatus.Success; //距离比较小时，认为认为执行成功
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}
