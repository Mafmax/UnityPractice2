using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyOnScreen : MonoBehaviour
{
    public GameObject Enemy;
    GameOptions options;

    // Start is called before the first frame update
    void Start()
    {
        options = new GameOptions();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(options.Buttons["EnemyCreate"]);
        if (Input.GetKeyDown(options.Buttons["EnemyCreate"]))
        {
            Debug.LogError("Ну так");
            ObjectCreator.CreateOnRandomScreenPoint(Enemy, "Wall");
        }
    }
}
