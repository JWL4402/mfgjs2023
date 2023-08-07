using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlotScript : MonoBehaviour
{
    public enum DebrisState { NONE, LIGHT, MEDIUM, HEAVY  };

    [SerializeField]
    private List<Sprite> debrisSpriteList;
    private Dictionary<DebrisState, Sprite> debrisMap;

    [SerializeField]
    private DebrisState debris;
    public DebrisState Debris
    {
        get { return debris; }
        set { SetDebrisLevel(value); }
    }

    public bool plowed;
    public float growth;
    public float timeTillWater;
    public Crop plantedCrop;

    private void SetDebrisLevel(DebrisState level)
    {
        debris = level;
        Sprite sprite;

        if (debrisMap == null)
        {
            InitializeDebrisMap();
        }

        if (debrisMap.TryGetValue(debris, out sprite))
        {
            gameObject.GetComponent<Image>().sprite = sprite;
        }
        else
        {
            Debug.LogError("Problem with sprite");
        }
    }

    private void InitializeDebrisMap()
    {
        debrisMap = new Dictionary<DebrisState, Sprite>();
        DebrisState[] debrisStateList = (DebrisState[])Enum.GetValues(typeof(DebrisState));

        for (int i = 0; i < debrisStateList.Length; i++)
        {
            debrisMap.Add(debrisStateList[i], debrisSpriteList[i]);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }
}
