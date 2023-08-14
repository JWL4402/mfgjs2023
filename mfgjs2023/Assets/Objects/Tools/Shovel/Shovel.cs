using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shovel", menuName = "Tools/Shovel")]
public class Shovel : Tool
{
    public int removalCost;

    public override void OnUse(PlotScript plot)
    {
        plot.RemoveDebris(removalCost);
    }
}
