using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayCell : Cell
{
    private float ToStartDistance { get; set; }
    public float ToTargetDistance { get; set; }
    public WayCell PreviewCell { get; set; }
    public Cell FinishCell { get; set; }
    public float Weight { get; set; }

    public WayCell(Cell finish, WayCell preview, float x, float y, float toStartDistance)
        : base(x, y, finish.Detalisation)
    {
        FinishCell = finish;

        PreviewCell = preview;
        InitializeWeight(toStartDistance);
    }
    public WayCell(Cell finish, WayCell preview, Vector2 coord,  float toStartDistance)
        : this(finish, preview, coord.x, coord.y, toStartDistance)
    {
    }
    public WayCell(Cell finish, WayCell preview, Vector3 coord, float toStartDistance)
        : this(finish, preview, coord.x, coord.y, toStartDistance)
    {

    }
    public WayCell(Cell finish, WayCell preview, Cell currentCell, float toStartDistance)
       : this(finish, preview, currentCell.X, currentCell.Y, toStartDistance)
    {

    }

    private void InitializeWeight(float toStartDistance)
    {
       
        ToStartDistance = toStartDistance;
        ToTargetDistance = GetDetourDistanсe(this, FinishCell);
        Weight = ToStartDistance + ToTargetDistance;
    }


    public void AddNewAdjacents(ref List<WayCell> waiting, bool diagonalAdjacent)
    {
       

        int s = 0;
        float endY = default;
        float endX = default;
        for (int i = 0; i < 3; i++)
        {
           

            for (int j = 0; j < 3; j++)
            {
               
                if (s != 4)
                {

                    switch (i)
                    {
                        case 0: endY = this.Y + this.Detalisation; break;
                        case 1: endY = this.Y; break;
                        case 2: endY = this.Y - this.Detalisation; break;
                    }
                    switch (j)
                    {
                        case 0: endX = this.X - this.Detalisation; break;
                        case 1: endX = this.X; break;
                        case 2: endX = this.X + this.Detalisation; break;
                    }

                    var justWaiting = waiting.Where(x => x.X == endX).Where(y => y.Y == endY).ToList();


                    if (s % 2 == 0 && diagonalAdjacent)
                    {
                      //  Debug.Log("Созидание соседа-диагонали.");
                        float newToStartDistance = ToStartDistance + Detalisation * (float)Math.Sqrt(2);


                        foreach(var waitCell in justWaiting)
                        {
                                if(waitCell.ToStartDistance <= newToStartDistance)
                                {
                                    break;
                                }
                              
                                    
                                

                        }
                        waiting.Add(new WayCell(FinishCell, this, endX, endY, newToStartDistance));

                    }
                    else
                    {
                        //Debug.Log("Созидание соседа-оргогонали.");
                        float newToStartDistance = ToStartDistance + Detalisation;
                        foreach (var waitCell in justWaiting)
                        {

                            if (waitCell.ToStartDistance <= newToStartDistance)
                            {
                                break;
                            }
                         
                               
                            

                        }
                        waiting.Add(new WayCell(FinishCell, this, endX, endY, newToStartDistance));
                    }

                }
                s++;
            }
        }

    }
    public static float GetDetourDistanсe(Cell start, Cell target)
    {

        return Math.Abs(start.X - target.X) + Math.Abs(start.Y - target.Y);

    }
    public Vector3 GetMoveVector()
    {

       // Debug.Log("Расчет MoveVector");
        if (PreviewCell != null)
        {
            Debug.Log("PreviewCell != null");
            return new Vector3(this.X - PreviewCell.X, this.Y - PreviewCell.Y);
        }
        else
        {

            return new Vector3(0f, 0f);
        }
    }



}
