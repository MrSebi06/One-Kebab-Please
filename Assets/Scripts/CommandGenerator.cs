using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class CommandGenerator : MonoBehaviour
{

    public TMP_Text orderText;
    public Timer timer;
    public Button validate;
    private bool timerStopped = false;
    private bool isGenerating = false;
    private Button btn;

    
    private string[] ingredients = {  "salade", "tomate", "oignon", "frite", "boisson", "sauce biggy", "sauce algérienne" };

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
            timer.timeRemaining = 10;
            timerStopped = false;
        }
        List<string> order = CreateOrder();
        DisplayOrder(order);
        timer.timeRemaining = 10; 
        timer.timerIsRunning = true; 
    }

    private List<string> CreateOrder()
    {
        System.Random random = new System.Random();
        List<string> order = new List<string>();
        List<string> remainIngredients = new List<string>(ingredients);

        int nbIngredients = Random.Range(3, 6);
        for (int i = 0; i < nbIngredients; i++)
        {
            int index = random.Next(0, remainIngredients.Count);
            string ingredient = remainIngredients[index]; 
            order.Add(ingredient);
            remainIngredients.RemoveAt(index);
        }

        return order;
    }

    private void DisplayOrder(List<string> order)
    {
        string orderString ="";
        for (int i = 0; i < order.Count; i++)
        {
            orderString += $"{i + 1}. {order[i]}\n";
        }
        orderText.text = orderString;
        btn.onClick.AddListener(BtnAction);
    }

    private void BtnAction(){
        TimerStopped();
        GenerateOrder();
    }
   

    public void TimerStopped()
    {
        timerStopped = true;
    }
}


