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

        #region Smoothly camera follow

       /* moveVector = GetMoveVector(this.transform.position, Player.transform.position);
        cosines = Move.GetDirectCosines(moveVector);
        speed= GetSpeed(moveVector);
        addictedMove = GetAddicted(cosines, speed);
        transform.position += addictedMove;*/

        #endregion

        
        /*if (Input.GetKey(KeyCode.Q) || GameObject.Find(Player.name).GetComponent<AstarMoveClickController>().IsMove )
        {
            this.Start();
        }*/
    }


    private Vector3 GetMoveVector(Vector3 coord, Vector3 toCoord)
    {
        return toCoord - coord;
    }


    private float GetSpeed(Vector3 vector)
    {
        return CameraAcceleration * vector.magnitude;
    }

}
