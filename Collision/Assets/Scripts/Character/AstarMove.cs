using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstarMove : MonoBehaviour
{



    public GameObject Character;
    public Text TextBox_Speed;
    public bool IsMove  = false;


    private static readonly float downToUp_epsilon = 1f;
    private static readonly float detalisation = 1f;
    private static readonly bool diagonalAdjacent = true;
    private static readonly bool Is2D  = true;

    private float speed = 5f;

    private Vector3 TargetPosition=new Vector3();
    private Vector3 DownClickMousePosition = new Vector3();
    private Vector3 UpClickMousePosition = new Vector3();
    private Stack<WayCell> Way = new Stack<WayCell>();

   


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
    private Vector3 DeltaMove = default;




    // Start is called before the first frame update
    void Start()
    {
       FinalSpeed= GetFinalSpeed(Speed);
    }

    
    void Update()
    {


        Check.ValueChange(ref speed, KeyCode.Z, KeyCode.X, 1f);
      

        if (Input.GetMouseButtonDown(0))
        {
            CorrectMouseDown=Mouse.CheckCorrectButton(ref DownClickMousePosition, Character.transform.position, Character.transform.localScale.magnitude, false);
            Debug.Log(CorrectMouseDown);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CorrectMouseUp=Mouse.CheckCorrectButton(ref UpClickMousePosition, DownClickMousePosition, downToUp_epsilon, true);
            Debug.Log(CorrectMouseUp);
        }
        if (conditions.Count > 0)
        {
            conditions.Clear();
        }
        conditions.Add(!IsMove);
        conditions.Add(CorrectMouseDown);
        conditions.Add(CorrectMouseUp);
        
        if (Check.Conjunction(conditions))
        {
            Way = PathFinderAstar.GetPath(Character.transform.position, UpClickMousePosition, detalisation, Character.transform.localScale.magnitude, diagonalAdjacent);
            TargetPosition = Character.transform.position;
            IsMove = true;
            Mouse.ResetCorrect(out correctMouseUp,out correctMouseDown);
        }
        if (IsMove)
        {
            if (Move.ApproximatelyEquals(Character.transform.position, TargetPosition, Time.deltaTime * Speed))
            {
                Character.transform.position = TargetPosition;
                if (Way.Count > 0)
                {
                    TargetPosition = Move.GetTarget(Character, Way);
                    DeltaMove = Move.GetAddicted(Character.transform.position, TargetPosition, FinalSpeed, Is2D);
                    
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
    

    

   



    private float GetFinalSpeed(float customSpeed)
    {
        float sameCoeff = 0.1f;
        return  sameCoeff * detalisation * customSpeed;
    }

    public void OnDrawGizmos()
    {

        if (Way.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (var item in Way)
            {
                Gizmos.DrawSphere(new Vector3(item.X, item.Y, -1), detalisation / 4f);
            }
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(TargetPosition, Character.transform.localScale.magnitude / 4f);
        }

    }

}
