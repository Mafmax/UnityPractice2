using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Move 
{


    public static Vector3 GetAddicted(float[] cosines, float speed, bool is2D)
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
    public static bool ApproximatelyEquals(Vector3 position1, Vector3 position2, float error)
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


    public static float[] GetDirectCosines(Vector3 vector)
    {
        float[] Cosines = new float[3];
        float length = vector.magnitude;

        Cosines[(int)DirCos.X] = vector.x / length;
        Cosines[(int)DirCos.Y] = vector.y / length;
        Cosines[(int)DirCos.Z] = vector.z / length;
        return Cosines;

    }

}
