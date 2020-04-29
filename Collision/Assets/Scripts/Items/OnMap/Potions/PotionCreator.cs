using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;






public class PotionCreator : MonoBehaviour
{

    public GameObject badPotion;
    private const int chance=2;
    public bool ContainsPotion;
    private float second;
    private static int dist = 1; //Выпуклость относительно сцены
   
    
    // Start is called before the first frame update
    void Start()
    {
       
        ContainsPotion = false;
          
    }


    private GameObject CreatePotion(GameObject potion)
    {
        ContainsPotion = true;
        var newPotion = Instantiate(potion, new Vector3(this.transform.position.x, this.transform.position.y, dist), this.transform.rotation);
        Regex regex = new Regex(@"[(]\d+[)]");

         
        newPotion.name = "potion " + regex.Match(this.name).Value;
        return newPotion;
    }
      
     
    
    // Update is called once per frame
    void Update()
    {
       
        if (!ContainsPotion)
        {
            second += Time.deltaTime;
            if (second > 1)
            {
                second = 0;
                switch (Random.Range(0,20))
                {
                    case 4: CreatePotion(badPotion); break;
                    default: break;
                }
            }
        }
       
        
    }
}
