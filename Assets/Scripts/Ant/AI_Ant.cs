using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ant : MonoBehaviour {

    AntUIcontroller UIController;
    Home_Script homeScript;
    Store_Script storeScript;
    Work_Script workScript;
    public float food;
    public float money;
    public bool feed;
    public float energyConsumption;

    float satisfied;
    float hunger;
    float hungerCounterVariable;
    bool hungerBoolean;
    bool satisfiedBoolean;
    bool brokeBoolean;

    int AntBehaviourState;  // 0= idle, 1 seek home, 2 seek store, 3 seek work

    public Transform home, work, store;
    public Vector3 homeLocation, workLocation, storeLocation;

    
	// Use this for initialization
	void Start () {
        food = 10f;  // counter that affects to energ
        money = 10f;
        satisfied = food * 0.99f;  // at 10 food this limit will be 8
        hunger = food * 0.3f;  // ant will head back home when its hunger is 3

        UIController = FindObjectOfType<AntUIcontroller>();
        homeScript = FindObjectOfType<Home_Script>();
        storeScript = FindObjectOfType<Store_Script>();
        workScript = FindObjectOfType<Work_Script>();

        home = GameObject.Find("home").GetComponent<Transform>();
        work = GameObject.Find("work").GetComponent<Transform>();
        store = GameObject.Find("store").GetComponent<Transform>();

        homeLocation = home.transform.position;
        workLocation = work.transform.position;
        storeLocation = store.transform.position;
        //feed = false;
        hungerBoolean = false;
        brokeBoolean = false;
        AntBehaviourState = 0;

        
    }
	
	// In update happens the main choise making
    // ant has  need for food to stay alive but the supplies will grow short.
    // Ant needs to go to work to gain more money to spend in store in order
    // to fill the suppliment storage and survive

	void Update () {
        // First we need a constant reduction of energy 
        energyConsumption = UIController.GetEnergyConsumption();
        DecreaseFood(energyConsumption);

        // just to make sure our ant is alive..
        if (food <= 0)
        {
            Debug.Log("Ant died");
            food = 0;
            return;
        }
        else
        { // since it is..

            if (getFood() < hunger && !satisfiedBoolean)        // if low on food and not satisfied
            {
                hungerBoolean = true;                           // we are hungry
            }
            else if (getFood() > hunger && satisfiedBoolean)    // if high on food and satisfied
            {
                hungerBoolean = false;                          // we are not hungry
            }
            if (getFood() > satisfied)                          // if more than enough food
            {
                satisfiedBoolean = true;                        // we are satisfied
            }
            else if (getFood() < satisfied)                     // if not
            {
                satisfiedBoolean = false;                       // we are not satisfied
            }
            if(getMoney() <= 0)                                 // if we have no money
            {
                brokeBoolean = true;                            // we are broke
            }
            else if(getMoney() > 0)
            {
                brokeBoolean = false;   
            }
            switch (getState())                                 // here happens the choice making
            {
                case 0:
                    //idle
                    if (hungerBoolean)                              // if hungry, go home
                    {
                        changeState(1);
                    }
                    if (homeScript.getFoodSupplies() <= 0)          // if there's no food, go buy it
                    {
                        changeState(2);
                    }
                    if (satisfiedBoolean || brokeBoolean)           // if satisfied OR broken, go to work
                    {
                        changeState(3);
                    }
                    break;

                case 1:
                    //seek home
                    moveTo("home");
                    if (homeScript.getFoodSupplies() <= 0)          // if there's no food, go buy it
                    {
                        changeState(2);
                    }
                    else if (satisfiedBoolean || brokeBoolean)      // if satisfied OR broke, go to work
                    {
                        changeState(3);
                    }
                    break;

                case 2:
                    //seek store
                    moveTo("store");
                    if (hungerBoolean && homeScript.getFoodSupplies() >= 5) // if hungry AND food at home, go home
                    {
                        changeState(1);
                    }
                    else if (brokeBoolean)              
                    {
                        changeState(3);                                     // if broke, go to work
                    }
                    break;

                case 3:
                    //seek work
                    moveTo("work");
                    if (hungerBoolean && !brokeBoolean)                     // if hunger BUT NOT broken, go to home
                    {
                        changeState(1);
                    }
                    else if (homeScript.getFoodSupplies() <= 5 && !brokeBoolean && getMoney() >= storeScript.getFoodPrice())          
                    {
                        changeState(2);                                     // if there's low food at home AND Ant has money 
                    }                                                       //  AND it has more than enough money
                    break;                                                  // to buy food, go to store

                default:
                    break;
            }
        }
	}

   

    public void changeState(int state)
    {
        AntBehaviourState = state;
    }
    public int getState()
    {
        return AntBehaviourState;
    }
    
    void moveTo(string destination)
    {
        switch(destination)
        {
            case "home":
                transform.position = Vector3.Lerp(transform.position, homeLocation,  Time.deltaTime);
                break;
            case "work":
                transform.position = Vector3.Lerp(transform.position, workLocation,  Time.deltaTime);
                break;
            case "store":
                transform.position = Vector3.Lerp(transform.position, storeLocation, Time.deltaTime);
                break;
            default:
                break;
        }
    }
    
    public float getFood()
    {
        return food;
    }
    public float getMoney()
    {
        return money;
    }
    public void setMoneyZero()
    {
        money = 0;
    }

    public void turnFeed(bool state)
    {
        feed = state;
    }



    public void IncreaseFood(float amount)
    {
        food += 2 * Time.deltaTime;
        // both of these work

        //hungerCounterVariable = amount * -1;
        //UIController.FoodState(true);
        //changeHungerState();
    }
    public void DecreaseFood(float amount)
    {
        hungerCounterVariable = amount;
        UIController.FoodState(false);
        changeHungerState();
    }

    void changeHungerState()
    {
        food -= hungerCounterVariable * Time.deltaTime;
    }

    public void IncreaseMoney(float amount)
    {

        money += amount  * Time.deltaTime;
    }
    public void DecreaseMoney(float amount)
    {
        money -= amount * Time.deltaTime;
    }
}
