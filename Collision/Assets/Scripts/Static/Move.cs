using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DirCos
{
    X,
    Y,
    Z
}

public static class Move
{

    public static bool ApproximatelyEquals(Cell cell1, Cell cell2, float error, bool Is2D)
    {

        return cell1.GetDistanсe(cell2, Is2D) < error;

    }
    public static bool ApproximatelyEquals(Vector3 position1, Vector3 position2, float error, bool Is2D = true)
    {

        Vector3 vector3 = position2 - position1;
        if (Is2D)
        {
            if ((float)Mathf.Sqrt(Mathf.Pow(vector3.x, 2) + Mathf.Pow(vector3.y, 2)) < error)
            {
                return true;
            }
            else
            {
                return false;
            }



        }
        else
        {
            if (vector3.magnitude < error)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static Vector3 GetAddicted(float[] cosines, float speed, bool is2D = true)
    {
        float coeff = speed * Time.deltaTime;
        if (is2D)
        {
            return new Vector3(cosines[(int)DirCos.X] * coeff,
                             cosines[(int)DirCos.Y] * coeff, 0); //z=0 
        }
        else
        {

            return new Vector3(cosines[(int)DirCos.X] * coeff,
                               cosines[(int)DirCos.Y] * coeff,
                               cosines[(int)DirCos.Z] * coeff);
        }
    }
    public static Vector3 GetAddicted(Vector3 startPosition, Vector3 endPosition,float speed, bool Is2D=true)
    {
        return GetAddicted(GetDirectCosines(GetVector(startPosition, endPosition)), speed, Is2D);
    }



    public static float[] GetDirectCosines(Vector3 vector)
    {
        float[] Cosines = new float[3];
        float length = vector.magnitude;

        Cosines[(int)DirCos.X] = vector.x / length;
        Cosines[(int)DirCos.Y] = vector.y / length;
        Cosines[(int)DirCos.Z] = vector.z / length;
        return Cosines;

    }

    public static Vector3 GetVector(Vector3 startPoint, Vector3 endPoint)
    {
        return endPoint - startPoint;
    }


    public static Vector3 GetTarget(GameObject character, Stack<WayCell> cells)
    {
        if (cells.Count > 0)
        {


            return character.transform.position + cells.Pop().GetMoveVector();
        }
        else
        {
            return character.transform.position;
        }
    }
}
