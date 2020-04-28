using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstarMoveClickController 
{



    private GameObject Character;
    
    private Text[] texts;

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



    public AstarMoveClickController(GameObject character, float speed,Canvas toPrint, bool diagonalAdjacent = true, bool Is2D = true,bool isEditableSpeed=true)
    {
        Character = character;
        this.speed = speed;
        //TextBox_Speed = Text.Instantiate<Text>(TextBox_Example);
       // GameObject.Find("Canvas").GetComponent<Text>().text = "asd";


        this.diagonalAdjacent = diagonalAdjacent;
        this.Is2D = Is2D;
        downToUp_epsilon = Character.transform.localScale.x;
        detalisation = Character.transform.localScale.x;
        isEditable = isEditableSpeed;

        texts = toPrint.GetComponentsInChildren<Text>();
        

        
    }


 


    public void Go()
    {
        if (isEnable)
        {
            /*foreach (Text textBox in texts)
            {
                if (string.IsNullOrEmpty(textBox.text) || textBox.text.StartsWith("Speed"))
                {
                    textBox.text = "Speed: " + Speed;
                    break;
                }
            }*/
            if (isEditable)
            {
                
                Check.ValueChange(ref speed, KeyCode.Z, KeyCode.X, 1f);

            }
            FinalSpeed = GetFinalSpeed(in speed);


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






    public void UpdateTarget(Vector3 position)
    {
        way= PathFinderAstar.GetPath(Character.transform.position, position, Character.transform.localScale.x, Character.transform.localScale.x, diagonalAdjacent);
        currentTarget = Character.transform.position;
        IsMove = true;
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
