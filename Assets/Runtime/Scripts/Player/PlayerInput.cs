using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool Press()
    {
        if (!enabled) return false;
        
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }

    public bool Release()
    {
        if (!enabled) return false;
        
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                return true;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return true;
        }

        return false;
    }
}
