using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Check
{
    public static void ValueChange(ref float value, KeyCode keyUp, KeyCode keyDown, float step)
    {

        if (Input.GetKeyDown(keyUp))
        {
            value += step;
        }

        if (Input.GetKeyDown(keyDown))
        {
            value -= step;
        }
    }
    public static bool Conjunction(params bool[] conditions)
    {
        foreach(bool condition in conditions)
        {
            if(!condition)
            {
                return false;
            }
        }
        return true;

    }
    public static bool Conjunction(List<bool> conditions)
    {
        foreach (bool condition in conditions)
        {
            if (!condition)
            {
                return false;
            }
        }
        return true;

    }
    public static bool Disjunction(params bool[] conditions)
    {
        foreach (bool condition in conditions)
        {
            if (condition)
            {
                return true;
            }
        }
        return false;


    }
    public static bool Disjunction(List<bool> conditions)
    {
        foreach (bool condition in conditions)
        {
            if (condition)
            {
                return true;
            }
        }
        return false;


    }
}
