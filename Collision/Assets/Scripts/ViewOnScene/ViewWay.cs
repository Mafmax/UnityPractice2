using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ViewWay : MonoBehaviour
{

    public GameObject Player;
    private Stack<WayCell> Way = new Stack<WayCell>();
    private List<WayCell> CheckedCells = new List<WayCell>();
    private static float Detalisation { get; set; } = 1f;
    private static float period = 0.00002f;

    private static readonly bool diagonalAdjacent = true;
    private Vector3 GizmosDrawClick=new Vector3(0f,0f,0f);
    private float timer = 0;


    private int layer=8;
    // Start is called before the first frame update
    void Start()
    {
        /*var maps = GameObject.Find("Grid").GetComponentsInChildren<Tilemap>();
        TilemapCollider2D collider = new TilemapCollider2D();
        Tilemap sameMap = new Tilemap();
        foreach (Tilemap map in maps)
        {
            if (map.gameObject.layer == layer)
            {

                sameMap=map;
                collider = map.GetComponent<TilemapCollider2D>();
                map.GetComponent<TilemapCollider2D>().usedByComposite = true;
                collider.usedByComposite = true;
                break;
            }
        }
        Debug.Log("UsedByComposite 1: " + sameMap.GetComponent<TilemapCollider2D>().usedByComposite);
        Debug.Log("UsedByComposite 2: " + collider.usedByComposite);
        */
    }

    // Update is called once per frame
    void Update()
    {

        if (MyTimer.Wait(period, ref timer) && Input.GetMouseButton(1))
        {
            GizmosDrawClick = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Way = PathFinderAstar.GetPath(out CheckedCells, Player.transform.position,Camera.main.ScreenPointToRay(Input.mousePosition).origin, Detalisation, Player.transform.localScale.x, diagonalAdjacent);
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
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GizmosDrawClick, Detalisation / 2f);

    }
}
