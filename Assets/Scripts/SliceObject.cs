using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObject : MonoBehaviour
{
    [SerializeField] private Vector3 slashVelocity;
    [SerializeField] private Transform startSlicePoint;
    [SerializeField] private Transform endSlicePoint;
    [SerializeField] private LayerMask sliceableLayer;
    [SerializeField] private Material crossSectionMaterial;
    
    private int layerNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        layerNumber = (int)Mathf.Log(sliceableLayer.value, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit,
            sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            if (target.GetComponent<SliceableObject>().lives > 0)
            {
                Slice(target);
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 startPosition = startSlicePoint.position;
        Vector3 endPosition = endSlicePoint.position;
    
        Vector3 planeNormal = Vector3.Cross(endPosition - startPosition, slashVelocity).normalized;
        SlicedHull hull = target.Slice(endPosition, planeNormal);
    
        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            SetupHullComponent(upperHull, target);
            SetupHullComponent(lowerHull, target);

            Destroy(target);
        }
    }

    private void SetupHullComponent(GameObject hullPart, GameObject original)
    {
        SliceableObject sliceableComponent = hullPart.AddComponent<SliceableObject>();
        sliceableComponent.lives = original.GetComponent<SliceableObject>().lives - 1;
        sliceableComponent.tag = original.tag;
        hullPart.layer = layerNumber;
        hullPart.AddComponent<BoxCollider>();
        XRGrabInteractable xrGrab = hullPart.AddComponent<XRGrabInteractable>();
        xrGrab.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        SetupSliceComponent(hullPart);
    }
    
    public void SetupSliceComponent(GameObject slicedObject)
    {
        MeshCollider slicedCollider = slicedObject.AddComponent<MeshCollider>();
        slicedCollider.convex = true;
    }
}


