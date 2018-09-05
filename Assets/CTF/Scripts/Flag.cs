using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Offense owner;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Offense")
        {
            if (owner != null)
            {
                other.GetComponent<Offense>().hasFlag = false;
            }
            else
            {
                CTFGameManager.Instance.TakeFlag();
            }

            other.GetComponent<Offense>().hasFlag = true;
            transform.SetParent(other.transform);

            owner = other.GetComponent<Offense>();
        }
    }
}
