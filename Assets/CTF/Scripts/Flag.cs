using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Offense owner;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Offense")
        {
            if (owner != null)
            {
                owner.hasFlag = false;
            }
            else
            {
                // 当前没有任何拥有者
                CTFGameManager.Instance.TakeFlag();
            }

            other.GetComponent<Offense>().hasFlag = true;
            transform.SetParent(other.transform);
            owner = other.GetComponent<Offense>();
        }
    }
}
