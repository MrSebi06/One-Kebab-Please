using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class IngredientTrigger : XRBaseInteractable
{
    [SerializeField]
    private GameObject grabbableObject;

    [SerializeField]
    private Transform transformToInstantiate;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject newObject = Instantiate(grabbableObject, transformToInstantiate.position, Quaternion.identity);
        newObject.SetActive(true);
        
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();
        
        interactionManager.SelectExit(args.interactorObject, args.interactableObject);
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);
        
        base.OnSelectEntered(args);
    }
}
