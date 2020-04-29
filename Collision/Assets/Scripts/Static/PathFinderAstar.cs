using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PathFinderAstar
{
    private static WayCell Start { get; set; }

    private static WayCell Finish { get; set; }

    private static List<WayCell> Waiting;

    private static List<WayCell> Checked { get; set; }
    private static float MaxDeviation { get; set; }
    private const float FindPower = 25f;

    private static float Detalisation { get; set; }
    private static float CharacterRadius { get; set; }
    //Определяет, можно ли прыгать на диагональные клетки.
    private static bool DiagonalAdjacent { get; set; }


    private static WayCell GetAvailableTarget(Vector3 preferTarget) //Параметр предпочитаемая точка
    {
        int layer = LayerMask.NameToLayer("Wall");
        int layerMask = LayerMask.GetMask(LayerMask.LayerToName(layer));

        Check.UsedByCompositeOff(layer);
        

        if (Physics2D.OverlapCircle(preferTarget, CharacterRadius, layerMask))
        {
            Check.UsedByCompositeOn(layer);
            return null;
        }
        Check.UsedByCompositeOn(layer);
        return new WayCell(new Cell(preferTarget, Detalisation), null, preferTarget, float.MaxValue);
    }
    private static float GetMaxDeviation(float detalisation, float findPower)
    {
        return detalisation * findPower;

    }

    private static Stack<WayCell> GetWay(Vector3 startPos, Vector3 finishPos, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {
        Detalisation = detalisation;
        MaxDeviation = GetMaxDeviation(Detalisation, FindPower);
        DiagonalAdjacent = diagonalAdjacent;
        Waiting = new List<WayCell>();
        Checked = new List<WayCell>();
        Stack<WayCell> way = new Stack<WayCell>();
        CharacterRadius = characterRadius;

        Finish = GetAvailableTarget(finishPos);

        // Debug.LogError("Финиш: " + Finish);

        // Finish = new WayCell(finishCell, null, finishCell.X, finishCell.Y, Detalisation,float.MaxValue);
        if (Finish == null)
        {
            //Нет разницы, какая цель, поскольку она недействительна
            Start = new WayCell(new Cell(0f, 0f, 1f), null, startPos, 0f);
            way.Push(Start);
            Debug.Log("Финиш нулевой");
            return way;
        }
        Start = new WayCell(Finish, null, startPos, 0f);

        DiagonalAdjacent = diagonalAdjacent;
        Checked.Add(Start);
        Start.AddNewAdjacents(ref Waiting, diagonalAdjacent);




        while (Waiting.Count > 0)
        {
            //Вытаскиваем клетку с самым низким весом из ожидающих.
            //Какая-то клетка присвоить из ожидающих такую клетку, вес которой равен минимальному весу из ожидающих.
            var toCheck = Waiting.Where(x => x.Weight == Waiting.Min(y => y.Weight)).FirstOrDefault();
            //Debug.Log("toCheck.X" + toCheck.X);
            //Debug.Log("Finish.X" + Finish.X);
            if (toCheck.Equals(Finish))
            {
                Finish.PreviewCell = toCheck.PreviewCell;



                return CalculateWay(Finish);


            }
            else
            {
                Waiting.Remove(toCheck);
                //Случайное число 9, определяющее максимальное количество условий
                List<bool> conditions = new List<bool>();
                conditions.Add(Physics2D.OverlapCircle(new Vector2(toCheck.X, toCheck.Y), CharacterRadius, LayerMask.GetMask("Wall")));




                // conditions.Add(toCheck.GetDistanсe(Start) > MaxDeviation);
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
                        toCheck.AddNewAdjacents(ref Waiting, diagonalAdjacent);
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

    public static Stack<WayCell> GetPath(Vector3 startPos, Vector3 finishPos, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {
        if (true)
        {
            Debug.LogWarning("Ну допустим мы тут №1");
            // Debug.LogWarning("startPos: " + startPos);
            Debug.LogWarning("finishPos: " + finishPos);
            Debug.LogWarning("finishPos.Detalisation: " + finishPos);
        }
        return GetWay(startPos, finishPos, detalisation, characterRadius, diagonalAdjacent);
    }
    public static Stack<WayCell> GetPath(out List<WayCell> checkedCellsContainer, Vector3 startPos, Vector3 finishPos, float detalisation, float characterRadius, bool diagonalAdjacent = false)
    {
        var result = GetWay(startPos, finishPos, detalisation, characterRadius, diagonalAdjacent);

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



        return way;
    }



}
