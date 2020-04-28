using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Check
{
   


    public static void UsedByCompositeOff(int layer)
    {
        var maps = GameObject.Find("Grid").GetComponentsInChildren<Tilemap>();
        TilemapCollider2D collider = new TilemapCollider2D();
        foreach (Tilemap map in maps)
        {

            if (map.gameObject.layer == layer)
            {
                collider = map.GetComponent<TilemapCollider2D>();
                collider.usedByComposite = false;
                break;
            }
        }
    }
    public static void UsedByCompositeOn(int layer)
    {
        TilemapCollider2D collider;
        var maps = GameObject.Find("Grid").GetComponentsInChildren<Tilemap>();
        foreach (Tilemap map in maps)
        {

            if (map.gameObject.layer == layer)
            {
                collider = map.GetComponent<TilemapCollider2D>();
                collider.usedByComposite = true;
                break;
            }
        }

    }

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
