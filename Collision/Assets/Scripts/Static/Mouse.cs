using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Mouse
{
   
    public static void ResetCorrect(out bool up, out bool down)
    {
        up = false;
        down = false;
    }



    public static bool CheckCorrectButton(ref Vector3 getPosition, Vector3 positionToMatch, float error, bool inArea, bool Is2D = true)
    {

        getPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;

        if (Move.ApproximatelyEquals(getPosition, positionToMatch, error, Is2D))
        {
            if (inArea)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        else
        {
            if (inArea)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }


}
