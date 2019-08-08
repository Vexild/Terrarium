using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work_Script : MonoBehaviour
{
    
    AI_Ant ant;
    Home_Script homeScript;
    AntUIcontroller UIcont;
    float salary;
    
    private void Start()
    {
        ant = GameObject.Find("Ant").GetComponent<AI_Ant>();
        homeScript = GameObject.Find("home").GetComponent<Home_Script>();
        UIcont = FindObjectOfType<AntUIcontroller>();
        
    }

    private void Update()
    {
        salary = UIcont.GetSalary();
        //Debug.Log("Current Salary: " + salary);
    }

    void paySalary(float ammount)  // store can only give more food
    {
        ant.IncreaseMoney(ammount);
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Collision with work");
        if (other.transform.tag == ("Ant")) // we check if there's food at home
        {
            // pay money
            paySalary(salary);
            //UIcont.MoneyState(true);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.tag == ("Ant")) // we check if there's food at home
        {
            // pay money
            paySalary(salary);
            UIcont.MoneyState(true);

        }
    }
   /* private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Ant"){
            paySalary(0);
            //UIcont.MoneyState(false);
        }
    }*/
    /* private void OnTriggerExit(Collider other)
     {
         Debug.Log("Leaving work");
         if (other.transform.tag == ("Ant"))
         {
             paySalary(0f);
             UIcont.MoneyState(false);
         }
     }*/
    /*
   private void OnCollisioStay(Collision collision)
   {
       Debug.Log("Collision with work");
       if (collision.transform.tag == ("Ant")) // we check if there's food at home
       {
           // pay money
           paySalary(2f);
           UIcont.MoneyState(true);

       }
   }
   private void OnCollisionExit(Collision collision)
   {
       if (collision.transform.tag == ("Ant"))
       {
           paySalary(0f);
           UIcont.MoneyState(false);
       }
   }*/



}
