using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTimer 
{

    public static bool Wait(float seconds, ref float timer)
    {

        timer += Time.deltaTime;

        if(timer>seconds)
        {
            timer = 0;
            return true;
        }
        else
        {
            return false;
        }

    }

}
