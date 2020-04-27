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
    private static float MaxDeviation { get; set; }
    private const float FindPower = 40f;

    private static float Detalisation { get; set; }
    private static float CharacterRadius { get; set; }
    //Определяет, можно ли прыгать на диагональные клетки.
    private static bool DiagonalAdjacent { get; set; }


    private static WayCell GetAvailableTarget(Cell preferTarget)
    {

        if (Physics2D.OverlapCircle(new Vector2(preferTarget.X, preferTarget.Y), CharacterRadius))
        {
            return null;
        }


        return new WayCell(preferTarget, null, new Vector2(preferTarget.X, preferTarget.Y), Detalisation, float.MaxValue);
    }
    private static float GetMaxDeviation(float detalisation, float findPower)
    { 
        return detalisation * findPower;

    }

    private static Stack<WayCell> GetWay(Cell startCell, Cell finishCell, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {

        Detalisation = detalisation;
        MaxDeviation=GetMaxDeviation(Detalisation,FindPower);
        DiagonalAdjacent = diagonalAdjacent;
        Waiting = new List<WayCell>();
        Checked = new List<WayCell>();
        Stack<WayCell> way = new Stack<WayCell>();
        CharacterRadius = characterRadius;
        Start = new WayCell(finishCell, null, startCell.X, startCell.Y, Detalisation, 0f);
        Finish = GetAvailableTarget(finishCell);
        // Finish = new WayCell(finishCell, null, finishCell.X, finishCell.Y, Detalisation,float.MaxValue);
        if (Finish == null)
        {

            way.Push(Start);
            Debug.Log("Финиш нулевой");
            return way;
        }
        Detalisation = detalisation;
        DiagonalAdjacent = diagonalAdjacent;
        Checked.Add(Start);
        Waiting.AddRange(Start.GetAdjacent(diagonalAdjacent));


        while (Waiting.Count > 0)
        {
            //Вытаскиваем клетку с самым низким весом из ожидающих.
            //Какая-то клетка присвоить из ожидающих такую клетку, вес которой равен минимальному весу из ожидающих.
            var toCheck = Waiting.Where(x => x.ToTargetDistance == Waiting.Min(y => y.ToTargetDistance)).FirstOrDefault();
            //Debug.Log("toCheck.X" + toCheck.X);
            //Debug.Log("Finish.X" + Finish.X);
            if (toCheck.Equals(Finish))
            {
                Finish.PreviewCell = Waiting.Last();



                return CalculateWay(Finish);


            }
            else
            {
                Waiting.Remove(toCheck);
                //Случайное число 9, определяющее максимальное количество условий
                List<bool> conditions = new List<bool>();
                conditions.Add(Physics2D.OverlapCircle(new Vector2(toCheck.X, toCheck.Y), CharacterRadius));
                conditions.Add(toCheck.GetDistanсe(Start) > MaxDeviation);
                if (Check.Disjunction(conditions))
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


        Debug.Log("Окончательный путь не найден");

        return way;
    }

    public static Stack<WayCell> GetPath(Cell startCell, Cell finishCell, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {

        var result = GetWay(startCell, finishCell, detalisation, characterRadius, diagonalAdjacent);
        return result;
    }
    public static Stack<WayCell> GetPath(out List<WayCell> checkedCellsContainer, Cell startCell, Cell finishCell, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {
        var result = GetWay(startCell, finishCell, detalisation, characterRadius, diagonalAdjacent);

        checkedCellsContainer = Checked;

        return result;
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
        foreach (var cell in Checked)
        {
            Gizmos.DrawSphere(new Vector3(cell.X, cell.Y, -1), CharacterRadius);
        }
    }

}
