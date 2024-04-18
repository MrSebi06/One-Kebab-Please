using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


public class CommandHandler : MonoBehaviour
{
    public TMP_Text orderText;
    public Timer timer;
    public Button validate;
    public float timeRemaining = 20.0f;
    private bool timerStopped = false;
    private bool isGenerating = false;
    private Button btn;
    [SerializeField] private Delivery delivery;


    private void Start()
    {
        btn = validate.GetComponent<Button>();
        StartCoroutine(GenerateOrderRoutine());
    }

     private IEnumerator GenerateOrderRoutine()
    {
        while (true)
        {
            if (!isGenerating && !timer.timerIsRunning)
            {
                isGenerating = true;
                GenerateOrder();
                yield return new WaitForSeconds(5f); 
                isGenerating = false;
            }
            
            yield return null;
        }
    }

    public void GenerateOrder()
    {   
        if (timerStopped)
        {
            timer.timeRemaining = timeRemaining;
            timerStopped = false;
        }
         
        Tuple<SortedSet<Kebab.IngredientEnum>, SortedSet<SauceType>> order = CreateOrder();
        delivery.ingredients = order.Item1;
        delivery.sauces = order.Item2;
        DisplayOrder(order);
        timer.timeRemaining = timeRemaining; 
        timer.timerIsRunning = true;
    }

    private Tuple<SortedSet<Kebab.IngredientEnum>, SortedSet<SauceType>> CreateOrder()
    {
        bool hasSalad = Random.value > 0.5f;
        bool hasTomatoes = Random.value > 0.5f;
        bool hasOnions = Random.value > 0.5f;
        
        SortedSet<Kebab.IngredientEnum> ingredients = new SortedSet<Kebab.IngredientEnum>();
        
        if (hasSalad)
        {
            ingredients.Add(Kebab.IngredientEnum.Salad);
        }
        
        if (hasTomatoes)
        {
            ingredients.Add(Kebab.IngredientEnum.Tomatoes);
        }
        
        if (hasOnions)
        {
            ingredients.Add(Kebab.IngredientEnum.Onions);
        }
        
        SortedSet<SauceType> sauces = new SortedSet<SauceType>();
        int nbSauces = Random.Range(0, 3);
        for (int i = 0; i < nbSauces; i++)
        {
            int sauce = Random.Range(0, 4);
            switch (sauce)
            {
                case 0:
                    sauces.Add(SauceType.Mayo);
                    break;
                case 1:
                    sauces.Add(SauceType.Ketchup);
                    break;
                case 2:
                    sauces.Add(SauceType.WhiteSauce);
                    break;
                case 3:
                    sauces.Add(SauceType.HotSauce);
                    break;
            }
        }

        return new Tuple<SortedSet<Kebab.IngredientEnum>, SortedSet<SauceType>>(ingredients, sauces);
    }

    private void DisplayOrder(Tuple<SortedSet<Kebab.IngredientEnum>, SortedSet<SauceType>> order)
    {
        string orderString ="";
        
        List<string> ingredients = new List<string>();
        foreach (Kebab.IngredientEnum ingredient in order.Item1)
        {
            switch (ingredient)
            {
                case Kebab.IngredientEnum.Salad:
                    ingredients.Add("Salad");
                    break;
                case Kebab.IngredientEnum.Tomatoes:
                    ingredients.Add("Tomatoes");
                    break;
                case Kebab.IngredientEnum.Onions:
                    ingredients.Add("Onions");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        List<string> sauces = new List<string>();
        foreach (SauceType sauce in order.Item2)
        {
            switch (sauce)
            {
                case SauceType.Mayo:
                    sauces.Add("Mayo");
                    break;
                case SauceType.Ketchup:
                    sauces.Add("Ketchup");
                    break;
                case SauceType.WhiteSauce:
                    sauces.Add("White Sauce");
                    break;
                case SauceType.HotSauce:
                    sauces.Add("Hot Sauce");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        orderString += "Ingredients: ";
        for (int i = 0; i < ingredients.Count; i++)
        {
            orderString += ingredients[i];
            if (i < ingredients.Count - 1)
            {
                orderString += ", ";
            }
        }
        
        orderString += "\nSauces: ";
        for (int i = 0; i < sauces.Count; i++)
        {
            orderString += sauces[i];
            if (i < sauces.Count - 1)
            {
                orderString += ", ";
            }
        }
        
        orderText.text = orderString;
        btn.onClick.AddListener(BtnAction);
    }

    private void BtnAction() 
    {
        TimerStopped();
        GenerateOrder();
    }
   

    public void TimerStopped()
    {
        timerStopped = true;
    }
}


