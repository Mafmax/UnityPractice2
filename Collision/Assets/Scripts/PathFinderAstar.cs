using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinderAstar
{
    private static WayCell Start { get; set; }

    private static WayCell Finish { get; set; }

    private static List<WayCell> Waiting { get; set; }

    private static List<WayCell> Checked { get; set; }
    private static float Detalisation { get; set; }
    private static float CharacterRadius { get; set; }
    //Определяет, можно ли прыгать на диагональные клетки.
    private static bool DiagonalAdjacent { get; set; }

    public static Stack<WayCell> GetPath(Cell startCell, Cell finishCell, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {
        Detalisation = detalisation;
        DiagonalAdjacent = diagonalAdjacent;
        Waiting = new List<WayCell>();
        Checked = new List<WayCell>();
        Stack<WayCell> way = new Stack<WayCell>();
        CharacterRadius = characterRadius;

        Start = new WayCell(finishCell, null, startCell.X, startCell.Y, Detalisation, 0f);


        Finish = new WayCell(finishCell, null, finishCell.X, finishCell.Y, Detalisation, WayCell.GetDetourDistanсe(startCell, finishCell));


        Detalisation = detalisation;
        DiagonalAdjacent = diagonalAdjacent;

        Checked.Add(Start);

        Waiting.AddRange(Start.GetAdjacent(diagonalAdjacent));

        /*foreach(var cell in Start.GetAdjacent(diagonalAdjacent))
        {
            Debug.Log("Точка сосед. X: " + cell.X + "Y: " + cell.Y);
        }*/
        if (Waiting.Count <= 0)
        {
            Debug.Log("Waiting.Count==0");
        }
        else
        {
            while (Waiting.Count > 0)
            {
                //Вытаскиваем клетку с самым низким весом из ожидающих.
                //Какая-то клетка присвоить из ожидающих такую клетку, вес которой равен минимальному весу из ожидающих.
                var toCheck = Waiting.Where(x => x.Weight == Waiting.Min(y => y.Weight)).FirstOrDefault();
                //Debug.Log("toCheck.X" + toCheck.X);
                //Debug.Log("Finish.X" + Finish.X);
                if (toCheck.Equals(Finish))
                {
                    Finish.PreviewCell = Waiting.Last();
                    
                    Debug.Log("НАШЕЛСЯ КОНЕЦ. Число ожидающих: " + Waiting.Count);

                    return CalculateWay(Finish);


                }
                else
                {
                    Waiting.Remove(toCheck);
                    if (Physics2D.OverlapCircle(new Vector2(toCheck.X, toCheck.Y), CharacterRadius))
                    {
                        Checked.Add(toCheck);
                    }
                    else
                    {
                        var justHaveCells = Checked.Where(x => x.X == toCheck.X).Where(y => y.Y == toCheck.Y);
                        if (!justHaveCells.Any())
                        {
                            Checked.Add(toCheck);
                            Waiting.AddRange(toCheck.GetAdjacent(diagonalAdjacent));
                        }
                        //else
                        //{
                        //    var sameCells = justHaveCells.ToList();

                        //}

                    }
                }
            }

        }
        Debug.Log("НАШЕЛСЯ КТО-ТО");

        //Debug.Log("FinishX: " + Finish.X);






        //WayCell cell_1 = new WayCell(Finish, Start, Start.X + Detalisation, Start.Y, Detalisation, 0f);

        //WayCell cell_2 = new WayCell(Finish, cell_1, cell_1.X + Detalisation, cell_1.Y, Detalisation, 0f);
        //WayCell cell_3 = new WayCell(Finish, cell_2, cell_2.X, cell_2.Y + Detalisation, Detalisation, 0f);

        //Debug.Log("cell_1.PreviewCell.X: " + cell_1.PreviewCell.X);
        //Debug.Log("cell_1.PreviewCell.Y: " + cell_1.PreviewCell.Y);


        //way.Push(cell_3);
        //way.Push(cell_2);
        //way.Push(cell_1);
        //way.Push(Start);

        return way;










    }

    private static Stack<WayCell> CalculateWay(WayCell lastCell)
    {

        Debug.Log("Начали калькулировать путь");
        Stack<WayCell> way = new Stack<WayCell>();
        WayCell sameCell = lastCell;
        do
        {

            way.Push(sameCell);
            sameCell = sameCell.PreviewCell;



        } while (sameCell != null);

        Debug.Log("Сосчитался путь!!! Полученные значения: ");
        foreach (WayCell cell in way)
        {

            Debug.Log("Point. X: " + cell.X + " Y: " + cell.Y);
        }

        return way;
    }

    public static void OndrawGizmos()
    {

        Gizmos.color = Color.green;
        foreach(var cell in Checked)
        {
            Gizmos.DrawSphere(new Vector3(cell.X, cell.Y,-1), CharacterRadius); 
        }
    }

}
