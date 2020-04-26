using System;
using UnityEngine;
using UnityEngine.UI;

enum DirCos
{
    X,
    Y,
    Z
}

public class MoveCharacter : MonoBehaviour
{

    public Text speedText;
    private Vector3 downPosition;
    private float bounce;
    private bool Is2D;
    private float speed;
    private Vector3 upPosition;
    private Vector3 moveVector;
    private float[] cosines;
    public bool isMove;
    private float epsilonToStart;
    private float epsilonToStay;
    // Start is called before the first frame update
    void Start()
    {
        speedText.text = "Текущая скорость: ";
        bounce = 0.04f;
        Is2D = true;
        isMove = false;
        epsilonToStay = 0.1f;
        epsilonToStart = 1f;
        speed = 10f;
    }
  
    private void PrintSpeed()
    {
        speedText.text = String.Format("Текущая скорость: {0}", speed);
    }
    // Update is called once per frame
    void Update()
    {

        if (Physics2D.OverlapCircle(new Vector2(this.transform.position.x, this.transform.position.y), 0.1f, LayerMask.GetMask("Wall")))
        {
            Debug.Log("Warning!!!");
        }
      //  this.CheckSpeedChange();
        this.PrintSpeed();

        if (Input.GetMouseButtonDown(0))
        {
            downPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        }

        if (isMove)
        {
            Vector3 goMove = GoMove(cosines, speed);
            if (!ApproximatelyEquals(transform.position, upPosition, epsilonToStay))
            {
                transform.position += goMove;
            }
            else
            {
                ChangeLock(ref isMove);
            }
        }
        else
        {

            if (Input.GetMouseButtonUp(0))
            {
                upPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
                if (ApproximatelyEquals(downPosition, upPosition, epsilonToStart))
                {
                    ChangeLock(ref isMove);



                    if (Is2D)
                    {
                        upPosition.z = default;
                    }
                    moveVector = upPosition - transform.position;
                    cosines = Move.GetDirectCosines(moveVector);
                }

            }
        }


    }
    private void Stop()
    {
        isMove = false;

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        Stop();
        transform.position += Bounce(cosines, speed);
    }

    private void ChangeLock(ref bool flag)
    {
        flag = !flag;

    }
    private bool ApproximatelyEquals(Vector3 position1, Vector3 position2, float error)
    {

        Vector3 vector3 = position2 - position1;


        if (vector3.magnitude < error)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
    private Vector3 GoMove(float[] cosines, float speed)
    {
        Vector3 vector = new Vector3();
        float coeff = speed * Time.deltaTime;
        vector.x = cosines[(int)DirCos.X] * coeff;
        vector.y = cosines[(int)DirCos.Y] * coeff;
        vector.z = cosines[(int)DirCos.Z] * coeff;

        if (Is2D)
        {
            vector.z = default;
        }
        return vector;
    }
    private Vector3 Bounce(float[] cosines, float speed)
    {
        Vector3 vector = new Vector3();
        float coeff = -speed * Time.deltaTime * bounce;
        vector.x = cosines[(int)DirCos.X] * coeff;
        vector.y = cosines[(int)DirCos.Y] * coeff;
        vector.z = cosines[(int)DirCos.Z] * coeff;

        if (Is2D)
        {
            vector.z = default;
        }
        return vector;
    }

}
