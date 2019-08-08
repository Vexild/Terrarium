using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntUIcontroller : MonoBehaviour {
    
    public Text foodText;
    public Text moneyText;
    public Text foodSuppliesText;
    public Text antStateText;
    public Image FoodArrowUp, FoodArrowDown, MoneyArrowUp, MoneyArrowDown;
    float food, money, foodSupplies;
    int state;
    float foodCheck, moneyCheck, foodSuppliesCheck;
    AI_Ant ant;
    Home_Script home;

    // manipulator canvas:
    public Slider salarySlider;
    public Slider foodPriceSlider;
    public Slider foodPackSlider;
    public Slider energySlider;
    public Text currentSalary;
    public Text currentFoodPrice;
    public Text currentFoodPack;
    public Text currentEnergyConsumption;
    float salaryAmount;
    float foodPriceAmount;
    float foodPackAmount;
    
	// Use this for initialization
	void Start () {
        ant = FindObjectOfType<AI_Ant>();
        home = FindObjectOfType<Home_Script>();

        foodText = GameObject.Find("FoodStat").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyStat").GetComponent<Text>();
        foodSuppliesText = GameObject.Find("FoodSuppliesStat").GetComponent<Text>();
        antStateText = GameObject.Find("StateStat").GetComponent<Text>();
        
        FoodArrowUp = GameObject.Find("FoodArrowUp").GetComponent<Image>();
        FoodArrowDown = GameObject.Find("FoodArrowDown").GetComponent<Image>();
        MoneyArrowUp = GameObject.Find("MoneyArrowUp").GetComponent<Image>();
        MoneyArrowDown = GameObject.Find("MoneyArrowDown").GetComponent<Image>();

        //HungerArrowUp.enabled = false;
        //HungerArrowDown.enabled = false;
        FoodArrowUp.enabled = false;
        FoodArrowDown.enabled = false;
        MoneyArrowUp.enabled = false;
        MoneyArrowDown.enabled = false;
        
        // manipulator canvas:
        currentSalary = GameObject.Find("CurrentSalary").GetComponent<Text>();
        currentFoodPrice = GameObject.Find("CurrentFoodPrice").GetComponent<Text>();
        currentFoodPack = GameObject.Find("CurrentFoodPack").GetComponent<Text>();
        currentEnergyConsumption = GameObject.Find("CurrentConsumption").GetComponent<Text>();

        salarySlider = GameObject.Find("SalarySlider").GetComponent<Slider>();
        foodPriceSlider = GameObject.Find("FoodSlider").GetComponent<Slider>();
        foodPackSlider = GameObject.Find("FoodPackSlider").GetComponent<Slider>();
        energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        // currentFoodPack = GameObject.Find("CurrentS").GetComponent<Text>();
        salarySlider.value = 4;
        foodPriceSlider.value = 3;
        foodPackSlider.value = 4;
        energySlider.value = 0.5f;

        
    }
	

	// Update is called once per frame
	void Update () {

        //hunger = ant.getHunger();
        food = ant.getFood();
        money = ant.getMoney();
        foodSupplies = home.getFoodSupplies();
        state = ant.getState();
        // lets check it the number is goin down 

        foodText.text = food.ToString();
        moneyText.text = money.ToString();
        foodSuppliesText.text = foodSupplies.ToString();
        antStateText.text = state.ToString();

        currentSalary.text = salarySlider.value.ToString();
        currentFoodPrice.text = foodPriceSlider.value.ToString();
        currentFoodPack.text = foodPackSlider.value.ToString();
        Debug.Log("Current Salary: " + salarySlider.value + ", current Food Price: " + foodPriceSlider.value + ", current Food Pack size: " + foodPackSlider.value);

        currentEnergyConsumption.text = energySlider.value.ToString();
    }
    

    /*void CheckFood()
    {
        
        foodCheck = ant.getFood();
        
        if (foodCheck > food)  // Hunger is a reducing ammount so we check if the new ammount is lesser than the first one
        {
            FoodState(true);
            Debug.Log("Ant is getting hungry");
        }
        else
        {
            FoodState(false);
        }
    }*/



    // SETTER FUNCTIONS

    public void MoneyState(bool state)
    {
        if (state == true)
        {
            MoneyArrowUp.enabled = true;
            MoneyArrowDown.enabled = false;
        }
        else
        {
            MoneyArrowUp.enabled = false;
            MoneyArrowDown.enabled = true;
        }
    }

    public void FoodState(bool state)
    {
        if(state == true)
        {
            FoodArrowUp.enabled = true;
            FoodArrowDown.enabled = false;
        }
        else
        {
            FoodArrowUp.enabled = false;
            FoodArrowDown.enabled = true;
        }
    }
    
    public void SliderForSalary(float newValue)
    {
        currentSalary.text = newValue.ToString();
    }
    public void SliderForFoodPrice(float newValue)
    {
        currentFoodPrice.text = newValue.ToString();
    }
    public void SliderForFoodPack(float newValue)
    {
        currentFoodPack.text = newValue.ToString();
    }
    public float GetSalary()
    {
        return salarySlider.value;
    }
    public float GetFoodPrice()
    {
        return foodPackSlider.value;
    }
    public float GetFoodPack()
    {
        return foodPackSlider.value;
    }
    public float GetEnergyConsumption()
    {
        return energySlider.value;
    }
}
