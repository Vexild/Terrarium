using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Script : MonoBehaviour {

    float foodSupplies;
    AI_Ant ant;
    AntUIcontroller UIcont;
    float foodPack;

    private void Start()
    {
        ant = GameObject.Find("Ant").GetComponent<AI_Ant>();
        UIcont = FindObjectOfType<AntUIcontroller>();
        foodSupplies = 15;
    }
    private void Update()
    {
        foodPack = UIcont.GetFoodPack();
    }

    void increaseFood(float amount)
    {
        ant.IncreaseFood(amount);
    }
    private void OnTriggerEnter(Collider other)
    {
        UIcont.FoodState(true);
        //Debug.Log("We collided with " + transform.name);
        if (other.transform.tag == ("Ant")) // we check if there's food at home
        {
            increaseFood(foodPack);
            decreaseFoodSuplies(1);
            UIcont.FoodState(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        UIcont.FoodState(true);
        //Debug.Log("entered home");
        if (other.transform.tag == ("Ant"))
        {
            increaseFood(foodPack);
            decreaseFoodSuplies(1);
        }
    }
    void decreaseFoodSuplies(float amount)
    {
        //foodSupplies--;
        foodSupplies -= amount * Time.deltaTime;
        if (foodSupplies <= 0)
        {
            foodSupplies = 0;
        }
    }
    public void increaseFoodSupplies(float amount)  // store can only give more food
    {
        foodSupplies += amount * Time.deltaTime;
    }

    public float getFoodSupplies()
    {
        return foodSupplies;
    }
}
