using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private float counter;
    private AstarMoveController Driver;


    private float speed=20f;
    public float Speed { get { return speed; } }

    void Start()
    {
        Driver = new AstarMoveController(this.gameObject, speed, FindObjectOfType<Canvas>());
        Driver.Enable();

    }
    void Update()
    {

        if(MyTimer.Wait(4,ref counter))
        {

            Driver.UpdateTarget(Player.transform.position);
        }
        Driver.Go();
    }





}
