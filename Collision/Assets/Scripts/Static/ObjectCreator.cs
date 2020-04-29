using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectCreator
{

    public static void CreateOnRandomScreenPoint(GameObject toCreate, params string[] unavailableLayerNames)
    {
        float counter=0;
        bool success=false;
        while (true)
        {
            if(success || MyTimer.Wait(1f,ref counter)  )
            {
                
                break;
            }
            var randomPoint = Helper.RandomScreenPoint;
            foreach (var name in unavailableLayerNames)
            {
                 success = Check.IsAvailableTarget(randomPoint, toCreate.transform.localScale.x, LayerMask.NameToLayer(name));
                if (success)
                {
                    GameObject.Instantiate(toCreate, randomPoint, toCreate.transform.localRotation);
                    break;
                }
                
            }


        }
        
    }
}
