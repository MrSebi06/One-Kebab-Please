using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject foodStack;
    [SerializeField] private GameObject foodFactory;
    [SerializeField] private Slider slider;
    [SerializeField] private float endPosition;
    [SerializeField] private float startPosition;
    private float _elevationStep;
    
    [Header("States")]
    [SerializeField] private int capacity;
    public int quantity;

    private string _tag;
    
    // Start is called before the first frame update
    void Start()
    {
        _tag = gameObject.tag;
        quantity = 0;
        startPosition = foodStack.transform.localPosition.y;
        _elevationStep = (endPosition - startPosition) / capacity;
        slider.value = 0;
        
        foodStack.SetActive(false);
        foodFactory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider food)
    {
        if (!food.CompareTag(tag) || quantity == capacity) return;
        Destroy(food.gameObject);
        AddElement();
        UpdateSlider();
    }

    private void CheckQuantity()
    {
        if(quantity == 0) foodStack.SetActive(false);
        foodStack.SetActive(true);
        foodFactory.SetActive(true);
    }
    private void AddElement()
    {
        quantity++;
        MoveObject("up");
        CheckQuantity();
    }

    public void DeleteElement()
    {
        quantity--;
        MoveObject("down");
        CheckQuantity();
    }

    private void UpdateSlider()
    {
        slider.value = (float)quantity / capacity;
    }

    private void MoveObject(string direction)
    {            
        var vector3 = foodStack.transform.localPosition;
        
        switch (direction)
        { 
            case "up" :
                vector3.y = foodStack.transform.localPosition.y + _elevationStep;
                foodStack.transform.localPosition = vector3;
                break;
            case "down" :
                vector3.y = foodStack.transform.localPosition.y - _elevationStep;
                foodStack.transform.localPosition = vector3;
                break;
        }
    }
}
