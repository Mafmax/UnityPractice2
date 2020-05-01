using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{

    public static Vector3 RandomScreenPoint()
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

    public static Vector3 RandomOutScreenPoint(float additive)
    {

            var rect = Camera.main.pixelRect;
            
            switch (Random.Range(1, 4))
            {

                case 1: return Camera.main.ScreenPointToRay(new Vector3(Random.Range(rect.xMax, rect.xMax + additive), Random.Range(rect.yMin - additive, rect.yMax + additive), 0f)).origin;
                case 2: return Camera.main.ScreenPointToRay(new Vector3(Random.Range(rect.xMin-additive, rect.xMin), Random.Range(rect.yMin - additive, rect.yMax + additive), 0f)).origin;
                case 3: return Camera.main.ScreenPointToRay(new Vector3(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin - additive, rect.yMin), 0f)).origin;
                case 4: return Camera.main.ScreenPointToRay(new Vector3(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMax , rect.yMax+additive), 0f)).origin;
                default: break;
            
            }

            return default(Vector3);

          

        
    }

    /* public static Vector3 RandomCameraPoint()
     {

         return Camera.main.ScreenPointToRay(new Vector3(Random.Range(-Camera.main.pixelWidth / 2, Camera.main.pixelWidth / 2), Random.Range(-Camera.main.pixelHeight / 2, Camera.main.pixelHeight / 2))).origin;

     }*/

}
