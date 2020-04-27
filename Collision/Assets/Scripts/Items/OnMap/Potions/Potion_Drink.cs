using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System;

public class Potion_Drink : MonoBehaviour
{
    
    public Text Text;
    private Animator animator;
    public GameObject poison;
    private float duration;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        duration = 2f;
        animator = GetComponent<Animator>();

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.Log("Триггер");
        if(collider.tag == "potion")
        {
            Debug.Log("Схавал поушен" + collider.name);
            animator.SetBool("isPoisoned", true);
            timer = 0;
            GameObject.Find(("Potion spawn "+collider.name).Replace(" potion","")).GetComponent<PotionCreator>().ContainsPotion=false;
            Destroy(GameObject.Find(collider.name));
        }
    }
    // Update is called once per frame
    void Update()
    {
        


        if (animator.GetBool("isPoisoned"))
        {

            timer+=Time.deltaTime;
            Text.text = String.Format("Poisoned: {0}", Math.Round(duration - timer,1));
            if (timer > duration)
            {
                Text.text = null;
                animator.SetBool("isPoisoned", false);
                timer = 0;
            }
        }
    }
}
