using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    private bool Is2D { get; set; }
    private static int CameraZPosition = -1;

    private float speed;

    private Vector3 moveVector;

    private float[] cosines;


    private Vector3 addictedMove;
  


    private float  CameraAcceleration { get; set; }

    enum DirCos
    {
        X,
        Y,
        Z
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraAcceleration = 1f;
        Is2D = true;
        transform.position = Player.transform.position;
        float x, y, z;
        x = Player.transform.position.x;
        y = Player.transform.position.y;
        if (Is2D)
        {
            z = CameraZPosition;
        }
        else
        {
            z = Player.transform.position.z;
        }
        transform.position = new Vector3(x, y, z);


    }


    
    // Update is called once per frame
    void Update()
    {
        moveVector = GetMoveVector(this.transform.position, Player.transform.position);
        cosines = GetDirectCosines(moveVector);
        speed= GetSpeed(moveVector);
        addictedMove = GetAddicted(cosines, speed);

        transform.position += addictedMove;
    }


    private Vector3 GetMoveVector(Vector3 coord, Vector3 toCoord)
    {
        return toCoord - coord;
    }

    private float[] GetDirectCosines(Vector3 vector)
    {
        float[] Cosines = new float[3];
        float length = vector.magnitude;

        Cosines[(int)DirCos.X] = vector.x / length;
        Cosines[(int)DirCos.Y] = vector.y / length;
        Cosines[(int)DirCos.Z] = vector.z / length;
        return Cosines;

    }
    private float GetSpeed(Vector3 vector)
    {
        return CameraAcceleration * vector.magnitude;
    }
    private Vector3 GetAddicted(float[] cosines, float speed)
    {
        float coeff = speed * Time.deltaTime;
        if (Is2D)
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
}
