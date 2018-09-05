using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offense : MonoBehaviour
{
    public bool hasFlag;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Defense")
        {
            Debug.Log("吃掉");

            if (hasFlag)
            {
                CTFGameManager.Instance.DropFlag();

                if (transform.childCount > 0)
                {
                    Transform flagTransform = transform.GetChild(0);
                    flagTransform.GetComponent<Flag>().owner = null;
                    flagTransform.SetParent(null);

                    // 复位
                    transform.position = startPos;
                    transform.rotation = startRot;
                }
            }
        }
    }
}
