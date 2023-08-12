using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoe", menuName = "Tools/Hoe")]
public class Hoe : Tool
{
    public override void OnUse(PlotScript plot)
    {
        plot.PlowPlot();
    }
}
