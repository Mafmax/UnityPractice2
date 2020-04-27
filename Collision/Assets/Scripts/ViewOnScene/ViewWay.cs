using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewWay : MonoBehaviour
{

    public GameObject Player;
    private Stack<WayCell> Way = new Stack<WayCell>();
    private List<WayCell> CheckedCells = new List<WayCell>();
    private static float Detalisation { get; set; } = 1f;
    private static float period = 0.00002f;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (MyTimer.Wait(period, ref timer) && Input.GetMouseButton(1))
        {
            Way = PathFinderAstar.GetPath(out CheckedCells, new Cell(Player.transform.position, Detalisation), new Cell(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Detalisation), Detalisation, Player.transform.localScale.x, true);
           // Debug.Log("Прошло " + period + " сек.");
        }


    }

    public void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        foreach (WayCell cell in CheckedCells)
        {
            Gizmos.DrawSphere(new Vector3(cell.X, cell.Y), Detalisation / 2f);
        }
        Gizmos.color = Color.red;
        foreach (WayCell cell in Way)
        {
            Gizmos.DrawSphere(new Vector3(cell.X, cell.Y, -1), Detalisation / 2f);
        }

    }
}
