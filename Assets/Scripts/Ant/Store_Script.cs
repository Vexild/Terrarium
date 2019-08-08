using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Script : MonoBehaviour
{

    AI_Ant ant;
    Home_Script homeScript;
    AntUIcontroller UIcont;
    float foodprice;
    float foodPack;
    bool canIbuy;
    private void Start()
    {
        ant = GameObject.Find("Ant").GetComponent<AI_Ant>();
        homeScript = GameObject.Find("home").GetComponent<Home_Script>();
        UIcont = FindObjectOfType<AntUIcontroller>();
    }

    private void Update()
    {
        foodprice = UIcont.GetFoodPrice();
        foodPack = UIcont.GetFoodPack();
        //Debug.Log("Current Food Price: " + foodprice + ", current Food Pack size: " + foodPack);

        if (ant.getMoney() > 0)
        {
            canIbuy = true;
        }
        else
        {
            canIbuy = false;
        }
    }

    void purchaseSupplies(float amount)  // store can only give more food
    {
        homeScript.increaseFoodSupplies(amount);
        ant.DecreaseMoney(foodprice);
    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Bank balance :" + ant.getMoney());
        if (other.transform.tag == ("Ant"))
        {
            if (ant.getMoney() > 0)
            {
                // buy supplies
                purchaseSupplies(foodPack);
            }
            if (ant.getMoney() <= 0)
            {
                ant.setMoneyZero();
                    // we need to check if we are broke
                ant.changeState(1);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
       // Debug.Log("Bank balance :" + ant.getMoney());
        if (other.transform.tag == ("Ant")) 
        {
            if (ant.getMoney() > 0)
            {
                // buy supplies
                purchaseSupplies(foodPack);
                if(ant.getMoney() <= 0)
                {
                    ant.setMoneyZero();
                    ant.changeState(0);
                }
            }
            else
            {
                ant.changeState(0);
            }
        }
    }

   /* private void OnTriggerExit(Collider other)
    {
        if (ant.getMoney() > 0)
        {
            // buy supplies
            purchaseSupplies(0);
        }
        else
        {
            return;
        }
    }*/
    public float getFoodPrice()
    {
        return foodprice;
    }
}
