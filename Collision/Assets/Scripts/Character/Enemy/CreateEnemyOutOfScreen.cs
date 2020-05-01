using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyOutOfScreen : MonoBehaviour
{

    public GameObject Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(GameOptions.Button.EnemyCreate_OutOfScreen))
        {
            Debug.LogError("Ну так");
            ObjectCreator.CreateOutOfRandomScreenPoint(Enemy,10f,"Ground", "Wall");
        }
    }
}
