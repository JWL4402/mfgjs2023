using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Crops/Crop")]
public class Crop : UseableObject
{
    public string cropName;
    public List<Sprite> sprites;
    public Sprite seedSprite;

    public int value;
    public int price;

    public float growthRate;
    public float waterInterval;

    public override void OnUse(PlotScript plot)
    {
        plot.PlantPlot(this);
    }
}
