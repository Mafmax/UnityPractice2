using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayCell : Cell
{
    private float ToStartDistance { get; set; }
    public float ToTargetDistance { get; set; }
    public WayCell PreviewCell { get; set; }
    public Cell FinishCell { get; set; }
    public float Weight { get; set; }

    public WayCell(Cell finish, WayCell preview, float x, float y, float detalisation, float toStartDistance)
        : base(x, y, detalisation)
    {
        FinishCell = finish;

        PreviewCell = preview;
        InitializeWeight(toStartDistance);
    }
    public WayCell(Cell finish, WayCell preview, Vector2 coord, float detalisation, float toStartDistance)
        : this(finish, preview, coord.x, coord.y, detalisation, toStartDistance)
    {
    }
    public WayCell(Cell finish, WayCell preview, Vector3 coord, float detalisation, float toStartDistance)
        : this(finish, preview, coord.x, coord.y, detalisation, toStartDistance)
    {

    }

    private void InitializeWeight(float toStartDistance)
    {
       
        ToStartDistance = toStartDistance;
        ToTargetDistance = GetDetourDistanсe(this, FinishCell);
        Weight = ToStartDistance + ToTargetDistance;
    }


    public List<WayCell> GetAdjacent(bool diagonalAdjacent)
    {
        var adjacent = new List<WayCell>();

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

                    if (s % 2 == 0 && diagonalAdjacent)
                    {
                      //  Debug.Log("Созидание соседа-диагонали.");
                        float newToStartDistance = ToStartDistance + Detalisation * (float)Math.Sqrt(2);

                        adjacent.Add(new WayCell(FinishCell, this, endX, endY, Detalisation, newToStartDistance));
                    }
                    else
                    {
                        //Debug.Log("Созидание соседа-оргогонали.");
                        float newToStartDistance = ToStartDistance + Detalisation;
                        adjacent.Add(new WayCell(FinishCell, this, endX, endY, Detalisation, newToStartDistance));
                    }

                }
                s++;
            }
        }
        return adjacent;
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
