using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MoveOnClick : MonoBehaviour
{
    
    public Canvas canvas;
    private AstarMoveClickController Driver;
    private float speed = 50f;
    private Text speedText;
    private float GizmosRadius;
    private Text[] texts;
    TargetClick TargetClick;
    // Start is called before the first frame update
    void Start()
    {
        GizmosRadius =this.gameObject.transform.localScale.x / 2f;
        Driver = new AstarMoveClickController(this.gameObject, speed, FindObjectOfType<Canvas>());
        Debug.Log(Driver.IsEnable);
        Driver.Enable();
        Debug.Log(Driver.IsEnable);
        TargetClick = new TargetClick(this.gameObject);
        texts = canvas.GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetClick.IsCorrect())
        {
            Driver.UpdateTarget(TargetClick.Target);
        }
        Driver.Go();
        
    }

    public void OnDrawGizmos()
    {
        if (Driver != null)
        {
            Debug.LogError("РРРРРРРРРРР");
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
