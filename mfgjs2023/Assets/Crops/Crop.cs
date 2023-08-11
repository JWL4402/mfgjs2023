using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Crops/Crop")]
public class Crop : ScriptableObject
{
    public int value;
    public int price;
    public float growthRate;
    public float waterInterval;
}
