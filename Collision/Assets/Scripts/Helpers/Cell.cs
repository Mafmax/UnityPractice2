using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Cell
{

    public float Detalisation { get; set; }
    public float X { get; set; }
    public float Y { get; set; }

    public Cell(float x, float y, float detalisation)
    {
        Detalisation = detalisation;

        X = GetDetalisation(x, detalisation);
        Y = GetDetalisation(y, detalisation);

    }
    public Cell(Vector2 coord, float detalisation) : this(coord.x, coord.y, detalisation)
    {

    }
    public Cell(Vector3 coord, float detalisation) : this(coord.x, coord.y, detalisation)
    {

    }
    private float GetDetalisation(float n, float detalis)
    {

        return (float)((decimal)n - (decimal)n % (decimal)detalis);
    }
    public float GetDistanсe(Cell cell, bool Is2D = true)
    {
        if (Is2D)
        {
            return (float)Math.Sqrt(Math.Pow((cell.X - this.X), 2) + Math.Pow((cell.Y - this.Y), 2));
        }
        else
        {
            return default;
        }
    }




    /*
        private float GetFullWeight()
        {
            return ToStartDistance + DetourDistance;

        }
        private Cell GetMinimalWeightCell(List<Cell> cells)
        {

            return cells.Where(x => x.FullWeight == Waiting.Min(y => y.FullWeight)).FirstOrDefault();
        }


        private List<Cell> CalculateWay(Cell finishCell)
        {


            var way = new List<Cell>();
            Cell currentCell = finishCell;
            while (currentCell.Previews != null)
            {
                way.Add(currentCell);
                currentCell = currentCell.Previews;
            }

            return way;
        }
        public List<Cell> GetWay(Cell target, bool[] adjacentMask = default)
        {
            Checked.Add(this);
            if (adjacentMask == default)
            {
                GetDefaultAdjacentMask(ref adjacentMask);
            }
            this.Waiting.AddRange(this.GetAdjacent(adjacentMask));

            while (Waiting.Count > 0)
            {
                Cell toCheck = GetMinimalWeightCell(Waiting);

                if (toCheck == target)
                {
                    return CalculateWay(toCheck);
                }

                if (Physics2D.OverlapCircle(new Vector2(toCheck.X, toCheck.Y), objectRadius, LayerMask.GetMask("Wall")))
                {
                    Waiting.Remove(toCheck);
                    Checked.Add(toCheck);
                }
                else
                {

                    Waiting.Remove(toCheck);

                    if (!Checked.Where(x => x == toCheck).Any())
                    {
                        Checked.Add(toCheck);
                        this.Waiting.AddRange(toCheck.GetAdjacent(adjacentMask));
                    }

                }

            }



            Debug.Log("Почему-то null");
            return null;
        }

        */



    public override bool Equals(object obj)
    {
        Cell cell = obj as Cell;

        if (cell != null)
        {
            if (cell != null)
            {
                if (this.X == cell.X && this.Y == cell.Y)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}