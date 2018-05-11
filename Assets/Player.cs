using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void Jump();
    public static event Jump upEvent;
    public static event Jump downEvent;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "floor")
        {//落地事件
            if (upEvent != null)
            {
                upEvent();
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "floor")
        {//跳跃事件
            if (downEvent != null)
            {
                downEvent();
            }
        }
    }
}