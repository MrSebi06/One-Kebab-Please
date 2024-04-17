using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kebab : MonoBehaviour
{
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
        if (other.gameObject.CompareTag("Salad"))
        {
            salad.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Tomatoes"))
        {
            tomatoes.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Onions"))
        {
            onions.SetActive(true);
            Destroy(other.gameObject);
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
}
