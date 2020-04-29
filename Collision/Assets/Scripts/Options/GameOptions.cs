using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptions 
{

    private Dictionary<string, KeyCode> buttons=new Dictionary<string, KeyCode>();



    public Dictionary<string, KeyCode> Buttons { get { return buttons; } }




    public GameOptions()
    {

        SetOptions();

    }

    private void SetOptions()
    {

        buttons["EnemyCreate"] = KeyCode.S;
    }
    
    

}
