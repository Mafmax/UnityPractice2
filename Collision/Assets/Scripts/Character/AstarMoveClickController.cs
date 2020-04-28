﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstarMoveClickController
{



    private GameObject Character;
    private Text TextBox_Speed;

    private Stack<WayCell> way = new Stack<WayCell>();

    public Stack<WayCell> Way
    {
        get
        {
            return way;
        }
    }
    private bool isEnable=false;
    public bool IsEnable
    {
        get
        {
            return isEnable;
        }
    }

    private bool isMove = false;
    public bool IsMove
    {
        get
        {
            return isMove;
        }
        set
        {
            isMove = value;
        }
    }


    private readonly float downToUp_epsilon;
    private float detalisation = 1f;


    private readonly bool diagonalAdjacent = true;
    private readonly bool Is2D = true;

    private float speed = 5f;

    private Vector3 currentTarget= new Vector3();
    public Vector3 CurrentTarget
    {
        get
        {
            return currentTarget;
        }
    }
    private Vector3 DownClickMousePosition = new Vector3();
    private Vector3 UpClickMousePosition = new Vector3();
    




    private List<bool> conditions = new List<bool>();


    private bool correctMouseDown = false;
    private bool CorrectMouseDown
    {
        get
        {
            return correctMouseDown;
        }
        set
        {
            correctMouseDown = value;
        }
    }
    private bool correctMouseUp = false;
    private bool CorrectMouseUp
    {
        get
        {
            return correctMouseUp;
        }
        set
        {
            correctMouseUp = value;
        }
    }
    private bool isEditable;
    public bool IsEditable
    {
        get
        {
            return isEditable;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

    }
    private float FinalSpeed { get; set; }
    private Vector3 DeltaMove = default;



    public AstarMoveClickController(GameObject character, float speed, Text textBoard, bool diagonalAdjacent = true, bool Is2D = true,bool isEditableSpeed=true)
    {
        Character = character;
        this.speed = speed;
        TextBox_Speed = textBoard;
        this.diagonalAdjacent = diagonalAdjacent;
        this.Is2D = Is2D;
        downToUp_epsilon = Character.transform.localScale.x;
        detalisation = Character.transform.localScale.x;
        isEditable = isEditableSpeed;
        
       
        

        
    }


 


    public void Go()
    {
        if (isEnable)
        {
            TextBox_Speed.text = "Current speed: " + speed.ToString();
            if (isEditable)
            {
                Check.ValueChange(ref speed, KeyCode.Z, KeyCode.X, 1f);
            }
            FinalSpeed = GetFinalSpeed(in speed);
            #region MouseClickWait
            if (Input.GetMouseButtonDown(0))
            {
                CorrectMouseDown = Mouse.CheckCorrectButton(ref DownClickMousePosition, Character.transform.position, Character.transform.localScale.x, false);
                Debug.Log(CorrectMouseDown);
            }
            if (Input.GetMouseButtonUp(0))
            {
                CorrectMouseUp = Mouse.CheckCorrectButton(ref UpClickMousePosition, DownClickMousePosition, downToUp_epsilon, true);
                Debug.Log(CorrectMouseUp);
            }
            if (conditions.Count > 0)
            {
                conditions.Clear();
            }

            conditions.Add(!isMove);
            conditions.Add(CorrectMouseDown);
            conditions.Add(CorrectMouseUp);
            #endregion
            
            
            if (Check.Conjunction(conditions))
            {

                Debug.Log("СЕЙЧАС НАДО ЕХАТЬ");
                way = PathFinderAstar.GetPath(Character.transform.position, UpClickMousePosition, Character.transform.localScale.x, Character.transform.localScale.x, diagonalAdjacent);
                
                Debug.Log("Детализация: " + Character.transform.localScale.magnitude);
                currentTarget = Character.transform.position;
                IsMove = true;
                Mouse.ResetCorrect(out correctMouseUp, out correctMouseDown);
            }





            if (isMove)
            {
                if (Move.ApproximatelyEquals(Character.transform.position, currentTarget, Time.deltaTime * FinalSpeed))
                {
                    Character.transform.position = currentTarget;
                    if (way.Count > 0)
                    {
                        Debug.Log("way.Count: " + way.Count);
                        currentTarget = Move.GetTarget(Character, way);
                        Debug.Log("way.Count: " + way.Count);
                        DeltaMove = Move.GetAddicted(Character.transform.position, currentTarget, FinalSpeed, Is2D);

                    }
                    else
                    {


                        IsMove = false;
                    }

                }
                else
                {
                    Character.transform.position += DeltaMove;
                }

            }

        }


    }








    private float GetFinalSpeed(in float customSpeed)
    {
        float sameCoeff = 0.1f;
        return sameCoeff * detalisation * customSpeed;
    }

    


    public void Enable()
    {
        isEnable = true;
    }
    public void Disable()
    {
        isEnable = false;
    }
}