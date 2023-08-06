using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Watering Can", menuName = "Useable Objects/Watering Can")]
public class WateringCan : UseableObject
{
    public float timeMultiplier = 1f;
    
    public override void OnUse(PlotScript plot)
    {
        float time = 15f; // TODO : will be decided by which crop is planted
        plot.timeTillWater = time * timeMultiplier;
    }
}
