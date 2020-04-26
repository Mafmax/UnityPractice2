using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarMoveCharacter : MonoBehaviour
{
    private Cell startCell;
    private Cell finishCell;
    private Vector3 TargetPosition;
    private Cell downPosition;
    private Cell upPosition;

    private static Vector3 UnavailablePoint = new Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue);

    private Stack<WayCell> Way = new Stack<WayCell>();
    private static float nearCharacter_epsilon = 1f;
    private static float downToUp_epsilon = 1f;
    private float CharacterRadius { get; set; }

    private static float detalisation = 1f;
    private float ToStay_epsilon;
    private bool CorrectMouseDown { get; set; } = false;
    private bool CorrectMouseUp { get; set; } = false;
    public bool IsMove { get; set; }
    private bool Is2D { get; set; }
    private float Speed { get; set; }
    private float FinalSpeed { get; set; }
    private float[] Cosines { get; set; }
    private Vector3 DeltaMove { get; set; } = default;
    private Vector3 MoveVector { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        CharacterRadius = this.transform.localScale.x;
        Way = new Stack<WayCell>();
        IsMove = false;
        Is2D = true;
        Speed = 5f;
        ToStay_epsilon = Time.deltaTime*Speed;
        GetFinalSpeed(Speed);
        MoveVector = new Vector3(0, 0);

        startCell = new Cell(this.transform.position, detalisation);

    }

    // Update is called once per frame
    void Update()
    {
        this.CheckSpeedChange();
        //  Debug.Log("Way.Count in Update(): "+Way.Count);
        if (Input.GetMouseButtonDown(0))
        {
            CorrectMouseDown = this.CheckCorrectMouseButtonDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CorrectMouseUp = this.CheckCorrectMouseButtonUp();
        }

        if (CorrectMouseDown && CorrectMouseUp && !IsMove)
        {
            Way = PathFinderAstar.GetPath(startCell, finishCell, detalisation, CharacterRadius, true);
            Debug.Log("Нашелся путь!!! Полученные значения: ");
            foreach (WayCell cell in Way)
            {
                
                Debug.Log("Point. X: " + cell.X + " Y: " + cell.Y);
            }
            TargetPosition = this.transform.position;

            IsMove = true;

            ResetCorrectMouse();
        }

        //Debug.Log("TargetPosition: " + TargetPosition);
        if (IsMove)
        {
            if (Move.ApproximatelyEquals(this.transform.position, TargetPosition, ToStay_epsilon))
            {
                this.transform.position = TargetPosition;
                if (Way.Count > 0)
                {

                    TargetPosition = GetTarget(Way, FinalSpeed);
                    Debug.Log("Берем следующую цель: " + TargetPosition);
                }
                else
                {


                    IsMove = false;
                }

            }
            else
            {
                transform.position += DeltaMove;
            }

        }



    }
    private void CheckSpeedChange()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Speed += 1;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Speed -= 1;
        }
    }

    private void ResetCorrectMouse()
    {
        CorrectMouseDown = false;
        CorrectMouseUp = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Way.Count > 0)
        {
            Debug.Log("DRAWWWWWWWWW");
            foreach (var item in Way)
            {
                Gizmos.DrawSphere(new Vector3(item.X, item.Y, -1), detalisation/4f);
            }
        }

    }

    private bool CheckCorrectMouseButtonDown()
    {
        Debug.Log("MouseDown");
        downPosition = new Cell(Camera.main.ScreenPointToRay(Input.mousePosition).origin, detalisation);
        if (Cell.ApproximatelyEquals(startCell, downPosition, nearCharacter_epsilon))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool CheckCorrectMouseButtonUp()
    {
        Debug.Log("MouseUp");
        upPosition = new Cell(Camera.main.ScreenPointToRay(Input.mousePosition).origin, detalisation);
        finishCell = upPosition;
        Debug.Log("Finish. X: " + finishCell.X + " Y: " + finishCell.Y);
        if (!IsMove && Cell.ApproximatelyEquals(downPosition, upPosition, downToUp_epsilon))
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 GetTarget(Stack<WayCell> cells, float speed)
    {
        WayCell wayCell;
        if (cells.Count > 0)
        {

            wayCell = cells.Pop();

            MoveVector = wayCell.GetMoveVector();
            Cosines = Move.GetDirectCosines(MoveVector);
            DeltaMove = Move.GetAddicted(Cosines, FinalSpeed, Is2D);
            // Debug.Log("Значение: "+ MoveVector);
            return this.transform.position + MoveVector;
        }
        else
        {
            return this.transform.position;
        }
    }


    private void GetFinalSpeed(float customSpeed)
    {
        FinalSpeed = 0.1f * detalisation * customSpeed;
    }

}
