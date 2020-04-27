using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarMove : MonoBehaviour
{

    public GameObject Character;


    private Vector3 TargetPosition=new Vector3();
    private Vector3 downPosition = new Vector3();
    private Vector3 upPosition = new Vector3();



    private Stack<WayCell> Way = new Stack<WayCell>();
    private static readonly float nearCharacter_epsilon = 1f;
    private static readonly float downToUp_epsilon = 1f;


    private List<bool> Conditions { get; set; } = new List<bool>();
    private static readonly float detalisation = 1f;
    private float ToStay_epsilon;
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
    public bool IsMove { get; set; }
    private bool Is2D { get; set; }
    private float speed;
    private float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    private float FinalSpeed { get; set; }
    private float[] Cosines { get; set; }
    private Vector3 DeltaMove { get; set; } = default;
    private Vector3 MoveVector { get; set; }

    public static float Detalisation => detalisation;


    // Start is called before the first frame update
    void Start()
    {

        downPosition = new Vector3();
        Way = new Stack<WayCell>();
        IsMove = false;
        Is2D = true;
        Speed = 5f;
        ToStay_epsilon = Time.deltaTime * Speed;
        GetFinalSpeed(Speed);
        MoveVector = new Vector3(0, 0);
        
        

    }

    // Update is called once per frame
    void Update()
    {


        Check.ValueChange(ref speed, KeyCode.Z, KeyCode.X, 1f);
      

        if (Input.GetMouseButtonDown(0))
        {
            CorrectMouseDown=Mouse.CheckCorrectButton(ref downPosition, Character.transform.position, nearCharacter_epsilon, false);
            Debug.Log(CorrectMouseDown);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CorrectMouseUp=Mouse.CheckCorrectButton(ref upPosition, downPosition, downToUp_epsilon, true);
            Debug.Log(CorrectMouseUp);
        }
        if (Conditions.Count > 0)
        {
            Conditions.Clear();
        }
        Conditions.Add(!IsMove);
        Conditions.Add(CorrectMouseDown);
        Conditions.Add(CorrectMouseUp);
        
        if (Check.Conjunction(Conditions))
        {
            Debug.Log("Погнали искать путь");

            Way = PathFinderAstar.GetPath(new Cell(Character.transform.position,Detalisation), new Cell(upPosition,Detalisation), Detalisation, Character.transform.localScale.magnitude, true);

            TargetPosition = this.transform.position;

            IsMove = true;

            Mouse.ResetCorrect(out correctMouseUp,out correctMouseDown);
        }

        //Debug.Log("TargetPosition: " + TargetPosition);
        if (IsMove)
        {
            if (Move.ApproximatelyEquals(this.transform.position, TargetPosition, ToStay_epsilon))
            {
                this.transform.position = TargetPosition;
                if (Way.Count > 0)
                {

                    TargetPosition = GetTarget(Way);
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
    

    

    public void OnDrawGizmos()
    {
        
        if (Way.Count > 0)
        {
            Gizmos.color = Color.red;
            Debug.Log("DRAWWWWWWWWW");
            foreach (var item in Way)
            {
                Gizmos.DrawSphere(new Vector3(item.X, item.Y, -1), Detalisation / 4f);
            }
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(TargetPosition, Character.transform.localScale.magnitude / 4f);
        }

    }

    private Vector3 GetTarget(Stack<WayCell> cells)
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
        FinalSpeed = 0.1f * Detalisation * customSpeed;
    }

}
