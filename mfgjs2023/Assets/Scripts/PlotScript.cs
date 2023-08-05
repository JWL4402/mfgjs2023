using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotScript : MonoBehaviour, IPointerDownHandler
{
    public enum DebrisState { NONE, LIGHT, MEDIUM, HEAVY  };

    public DebrisState debris;
    public bool plowed;
    public float growth;
    public float timeTillWater;
    public Crop plantedCrop;


    private void Start()
    {
        AddPhysics2DRaycaster();
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    private void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
