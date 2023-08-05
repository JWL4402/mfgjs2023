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

    [SerializeField] private int arablePlots;

    private void Start()
    {
        plots = new List<GameObject>();

        for (int i = 0; i < plotCount; i++)
        {
            GameObject plot = GameObject.Instantiate(plotTemplate, gameObject.transform);
            plots.Add(plot);
        }

        List<GameObject> plotsCopy = new List<GameObject>(plots);
        
        for (int i = 0; i < arablePlots; i++)
        {
            int plotIndex = Random.Range(0, plotsCopy.Count);
            plotsCopy[plotIndex].GetComponent<PlotScript>().debris = PlotScript.DebrisState.NONE;
            plotsCopy.RemoveAt(plotIndex);
        }

        // TODO : Better implementation (percentages?) for the rest of them
        foreach (GameObject plot in plotsCopy)
        {
            plot.GetComponent<PlotScript>().debris = PlotScript.DebrisState.MEDIUM;
        }
    }
}
