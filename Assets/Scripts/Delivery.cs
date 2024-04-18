using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public SortedSet<Kebab.IngredientEnum> ingredients;
    public SortedSet<SauceType> sauces;
    [SerializeField] private CommandHandler commandHandler;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Delivery");
        KebabTrigger kebabTrigger = other.GetComponent<KebabTrigger>();
        if (kebabTrigger != null)
        {
            Kebab kebab = kebabTrigger.kebab.GetComponent<Kebab>();
            Debug.Log(kebab.GetIngredients());
            Debug.Log(ingredients);
            Debug.Log(kebab.GetSauces());
            Debug.Log(sauces);
            CheckDelivery(kebab);
        }
    }
    
    private void CheckDelivery(Kebab kebab)
    {
        SortedSet<Kebab.IngredientEnum> kebabIngredients = kebab.GetIngredients();
        SortedSet<SauceType> kebabSauces = kebab.GetSauces();
        
        if (kebabIngredients.SetEquals(ingredients) && kebabSauces.SetEquals(sauces))
        {
            Destroy(kebab.gameObject);
            commandHandler.TimerStopped();
            commandHandler.GenerateOrder();
        }
        else
        {
            Debug.Log("Delivery failed!");
        }
    }
}
