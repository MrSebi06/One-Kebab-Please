using System;
using System.Collections.Generic;
using UnityEngine;

public class Kebab : MonoBehaviour
{
    public enum IngredientEnum
    {
        Salad,
        Tomatoes,
        Onions
    }
    
    public GameObject meat;
    
    [SerializeField] private GameObject salad;
    [SerializeField] private GameObject tomatoes;
    [SerializeField] private GameObject onions;

    [SerializeField] private GameObject mayo;
    [SerializeField] private GameObject ketchup;
    [SerializeField] private GameObject whiteSauce;
    [SerializeField] private GameObject hotSauce;
    
    void OnEnable()
    {
        salad.SetActive(false);
        tomatoes.SetActive(false);
        onions.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meat"))
        {
            meat.SetActive(true);
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Salad"))
        {
            AddIngredient(IngredientEnum.Salad);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Tomatoes"))
        {
            AddIngredient(IngredientEnum.Tomatoes);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Onions"))
        {
            AddIngredient(IngredientEnum.Onions);
            Destroy(other.gameObject);
        }
    }
    
    public void AddIngredient(IngredientEnum ingredientEnum)
    {
        switch (ingredientEnum)
        {
            case IngredientEnum.Salad:
                salad.SetActive(true);
                break;
            case IngredientEnum.Tomatoes:
                tomatoes.SetActive(true);
                break;
            case IngredientEnum.Onions:
                onions.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void AddSauce(SauceType sauceType)
    {
        switch (sauceType)
        {
            case SauceType.Mayo:
                mayo.SetActive(true);
                break;
            case SauceType.Ketchup:
                ketchup.SetActive(true);
                break;
            case SauceType.WhiteSauce:
                whiteSauce.SetActive(true);
                break;
            case SauceType.HotSauce:
                hotSauce.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public SortedSet<IngredientEnum> GetIngredients()
    {
        SortedSet<IngredientEnum> ingredients = new SortedSet<IngredientEnum>();
        if (salad.activeSelf)
        {
            ingredients.Add(IngredientEnum.Salad);
        }
        
        if (tomatoes.activeSelf)
        {
            ingredients.Add(IngredientEnum.Tomatoes);
        }
        
        if (onions.activeSelf)
        {
            ingredients.Add(IngredientEnum.Onions);
        }
        
        return ingredients;
    }
    
    public SortedSet<SauceType> GetSauces()
    {
        SortedSet<SauceType> sauces = new SortedSet<SauceType>();
        if (mayo.activeSelf)
        {
            sauces.Add(SauceType.Mayo);
        }
        
        if (ketchup.activeSelf)
        {
            sauces.Add(SauceType.Ketchup);
        }
        
        if (whiteSauce.activeSelf)
        {
            sauces.Add(SauceType.WhiteSauce);
        }
        
        if (hotSauce.activeSelf)
        {
            sauces.Add(SauceType.HotSauce);
        }
        
        return sauces;
    }
}
