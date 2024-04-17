using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kebab : MonoBehaviour
{
    [SerializeField] private GameObject salad;
    [SerializeField] private GameObject tomatoes;
    [SerializeField] private GameObject onions;
    
    void OnEnable()
    {
        salad.SetActive(false);
        tomatoes.SetActive(false);
        onions.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
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
}
