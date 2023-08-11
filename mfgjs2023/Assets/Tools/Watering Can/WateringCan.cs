using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Watering Can", menuName = "Useable Objects/Watering Can")]
public class WateringCan : UseableObject
{
    public float timeMultiplier = 1f;
    private const float lenience = 0.1f;
    
    public override void OnUse(PlotScript plot)
    {
        plot.WaterPlot(lenience + (plot.plantedCrop.waterInterval * timeMultiplier));
    }
}