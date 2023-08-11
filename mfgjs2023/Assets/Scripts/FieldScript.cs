using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    [SerializeField]
    private GameObject plotTemplate;
    [SerializeField]
    private int plotCount;
    private List<GameObject> plots;

    //[SerializeField] private int arablePlots;
    [SerializeField] private int lightPlots;
    [SerializeField] private int mediumPlots;
    [SerializeField] private int heavyPlots;

    private void Start()
    {
        plots = new List<GameObject>();

        for (int i = 1; i <= plotCount; i++)
        {
            GameObject plot = GameObject.Instantiate(plotTemplate, gameObject.transform);
            plot.name = "Plot " + i;
            plots.Add(plot);
        }

        RandomizePlotDebris();
    }

    private void RandomizePlotDebris()
    {
        List<GameObject> plotsCopy = new List<GameObject>(plots);

        for (int i = 0; i < heavyPlots; i++)
        {
            int plotIndex = Random.Range(0, plotsCopy.Count);
            plotsCopy[plotIndex].GetComponent<PlotScript>().Debris = PlotScript.DebrisState.HEAVY;
            plotsCopy.RemoveAt(plotIndex);
        }

        for (int i = 0; i < mediumPlots; i++)
        {
            int plotIndex = Random.Range(0, plotsCopy.Count);
            plotsCopy[plotIndex].GetComponent<PlotScript>().Debris = PlotScript.DebrisState.MEDIUM;
            plotsCopy.RemoveAt(plotIndex);
        }

        for (int i = 0; i < lightPlots; i++)
        {
            int plotIndex = Random.Range(0, plotsCopy.Count);
            plotsCopy[plotIndex].GetComponent<PlotScript>().Debris = PlotScript.DebrisState.LIGHT;
            plotsCopy.RemoveAt(plotIndex);
        }

        foreach (GameObject plot in plotsCopy)
        {
            plot.GetComponent<PlotScript>().Debris = PlotScript.DebrisState.NONE;
        }
    }
}