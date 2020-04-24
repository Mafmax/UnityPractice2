using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
enum DirCos
{
    X,
    Y,
    Z
}

public class MoveCharacter : MonoBehaviour
{
    private float bounce;
    private bool Is2D;
    private float speed;
    private Vector3 toMove;
    private Vector3 moveVector;
    private float[] cosines;
    private bool isMove;
    private float epsilon;
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        bounce = 0.004f;
        Is2D = true;
        isMove = false;
        epsilon = 0.1f;
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {

      
        if (isMove)
        {
            Vector3 goMove = GoMove(cosines, speed);
            if (!ApproximatelyEquals(transform.position, toMove, epsilon))
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

            if (Input.GetMouseButtonDown(0))
            {
                ChangeLock(ref isMove);
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                toMove = ray.origin;
                if (Is2D)
                {
                    toMove.z = default;
                }
                moveVector = toMove - transform.position;
                cosines = GetDirectingCosines(moveVector);


            }
        }


    }
    private void Stop()
    {
        isMove = false;
    }    
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision");
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
    private float[] GetDirectingCosines(Vector3 vector)
    {

        float[] cosines = new float[3];
        float length = vector.magnitude;
        cosines[(int)DirCos.X] = (vector.x) / length;
        cosines[(int)DirCos.Y] = (vector.y) / length;
        cosines[(int)DirCos.Z] = (vector.z) / length;


        return cosines;
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
