using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public enum SauceType
{
    Mayo,
    Ketchup,
    WhiteSauce,
    HotSauce
}

public class SauceDispenser : MonoBehaviour
{
    private bool isKebabUnderneath;
    private Kebab kebab;
    [SerializeField] private SauceType sauceType;
    
    public void DispenseSauce()
    {
        if (isKebabUnderneath)
        {
            Debug.Log("dispensing sauce");
            kebab.AddSauce(sauceType);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Kebab")) return;
        
        KebabTrigger kebabTrigger = other.gameObject.GetComponent<KebabTrigger>();
        if (kebabTrigger != null)
        {
            isKebabUnderneath = true;
            kebab = kebabTrigger.kebab.GetComponent<Kebab>();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Kebab")) return;
        
        KebabTrigger kebabTrigger = other.gameObject.GetComponent<KebabTrigger>();
        if (kebabTrigger != null)
        {
            isKebabUnderneath = false;
            kebab = null;
        }
    }
}
