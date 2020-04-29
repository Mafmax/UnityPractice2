using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{

    public static Vector3 RandomScreenPoint
    {
        get
        {
            var rect = Camera.main.pixelRect;
            

            return Camera.main.ScreenPointToRay(new Vector3(rect.x + Random.Range(0f, rect.width), rect.y + Random.Range(0f, rect.height), 0f)).origin;
            /*
            return  Camera.main.ScreenPointToRay(new Vector3(
                    Random.Range(-Camera.main.pixelWidth / 2, Camera.main.pixelWidth / 2), 
                    Random.Range(-Camera.main.pixelHeight / 2, Camera.main.pixelHeight / 2),
                    0f)).origin;
                    */
        }
    }

    /* public static Vector3 RandomCameraPoint()
     {

         return Camera.main.ScreenPointToRay(new Vector3(Random.Range(-Camera.main.pixelWidth / 2, Camera.main.pixelWidth / 2), Random.Range(-Camera.main.pixelHeight / 2, Camera.main.pixelHeight / 2))).origin;

     }*/

}
