using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClick 
{

    private  bool CorrectMouseDown = false;
    private  bool CorrectMouseUp = false;
    private  Vector3 DownClickMousePosition;
    private  Vector3 UpClickMousePosition;
    private List<bool> conditions=new List<bool>();
    private GameObject Character;

    public Vector3 Target
    {
        get
        {
            return UpClickMousePosition;
        }
    }
    private bool isMove=false;
    public bool IsMove
    {
        get
        {
            return isMove;
        }
    }
    public TargetClick(GameObject character)
    {
        Character = character;


    }
    public bool IsCorrect()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CorrectMouseDown = Mouse.CheckCorrectButton(ref DownClickMousePosition, Character.transform.position, Character.transform.localScale.x, false);
            Debug.Log(CorrectMouseDown);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CorrectMouseUp = Mouse.CheckCorrectButton(ref UpClickMousePosition, DownClickMousePosition, Character.transform.localScale.x, true);
            Debug.Log(CorrectMouseUp);
        }
        if (conditions.Count > 0)
        {
            conditions.Clear();
        }

        conditions.Add(!isMove);
        conditions.Add(CorrectMouseDown);
        conditions.Add(CorrectMouseUp);
        if (Check.Conjunction(conditions))
        {
            Mouse.ResetCorrect(out CorrectMouseUp, out CorrectMouseDown);
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
