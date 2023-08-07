using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoe", menuName = "Useable Objects/Hoe")]
public class Hoe : UseableObject
{
    public override void OnUse(PlotScript plot)
    {
        plot.PlowPlot();
    }
}
