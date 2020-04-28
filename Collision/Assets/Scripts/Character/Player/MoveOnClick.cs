using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveOnClick : MonoBehaviour
{
    private AstarMoveClickController Driver;
    private float speed = 50f;
    public Text textBoard;
    private float GizmosRadius;

    // Start is called before the first frame update
    void Start()
    {
        GizmosRadius =this.gameObject.transform.localScale.magnitude / 4f;
        Driver = new AstarMoveClickController(this.gameObject, speed, textBoard);
        Driver.Enable();
        

    }

    // Update is called once per frame
    void Update()
    {
        Driver.Go();
    }

    public void OnDrawGizmos()
    {
        if (Driver != null)
        {
            if (Driver.Way.Count > 0)
            {
                Gizmos.color = Color.red;
                foreach (var item in Driver.Way)
                {
                    Gizmos.DrawSphere(new Vector3(item.X, item.Y, -1), GizmosRadius);
                }
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(Driver.CurrentTarget, GizmosRadius);
            }
        }

    }
}
