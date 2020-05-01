using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOptions
{

    private static Dictionary<string, KeyCode> buttons = new Dictionary<string, KeyCode>();



    public static Dictionary<string, KeyCode> Buttons { get { return buttons; } }


    public static void SetOptions()
    {
        SetButtons();
    }

    private static void SetButtons()
    {

        buttons["EnemyCreate_OnScreen"] = KeyCode.S;
        buttons["EnemyCreate_OutOfScreen"] = KeyCode.C;
        buttons["SpeedUp"] = KeyCode.Z;
        buttons["SpeedDown"] = KeyCode.X;
        buttons["Menu"] = KeyCode.Escape;
    }


    private static void SetButton(string name, KeyCode key)
    {
        buttons[name] = key;

    }
    public static class Button
    {

        public static KeyCode EnemyCreate_OnScreen = buttons["EnemyCreate_OnScreen"];
        public static KeyCode EnemyCreate_OutOfScreen = buttons["EnemyCreate_OutOfScreen"];
        public static KeyCode SpeedUp= buttons["SpeedUp"];
        public static KeyCode SpeedDown = buttons["SpeedDown"];
        public static KeyCode Menu = buttons["Menu"];
        /* 
        public static KeyCode EnemyCreate = buttons["EnemyCreate"];
        public static KeyCode EnemyCreate = buttons["EnemyCreate"];
        public static KeyCode EnemyCreate = buttons["EnemyCreate"];
        public static KeyCode EnemyCreate = buttons["EnemyCreate"];
        */
    }

}
