using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotScript : MonoBehaviour
{
    public enum DebrisState { NONE, LIGHT, MEDIUM, HEAVY  };

    [SerializeField] private List<Sprite> debrisSpriteList;
    [SerializeField] private Sprite plowedPlotSprite;
    private Dictionary<DebrisState, Sprite> debrisMap;

    [SerializeField]
    private DebrisState debris;
    public DebrisState Debris
    {
        get { return debris; }
        set { SetDebrisLevel(value); }
    }

    [SerializeField] private bool plowed;
    [SerializeField] private float timeTillWater;
    public float growth;
    public Crop plantedCrop;
    [SerializeField] private Image cropImage;

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

    public void PlowPlot()
    {
        if (debris != DebrisState.NONE) { return; }

        plowed = true;
        gameObject.GetComponent<Image>().sprite = plowedPlotSprite;
    }

    public void PlantPlot(Crop crop)
    {
        if (debris != DebrisState.NONE) { return; }
        if (!plowed) { return; }
        
        plantedCrop = crop;
        cropImage.gameObject.SetActive(true);
        cropImage.sprite = crop.sprites[0];
    }

    private bool waterInProgress = false;
    public void WaterPlot(float waterDuration)
    {
        if (debris != DebrisState.NONE) { return; }
        if (plantedCrop == null) { return; }


        timeTillWater = waterDuration;

        StartCoroutine(StartGrowthCycle());
    }

    private const float waterHVal = 33f;
    [SerializeField] [Range(0f, 0.7f)]
    private float waterMaxSat;
    [SerializeField] [Range(0, 1f)]
    private float waterMaxOversat;

    private IEnumerator StartGrowthCycle()
    {
        if (waterInProgress)
        {
            yield break;
        }
        
        waterInProgress = true;
        Image plotImage = gameObject.GetComponent<Image>();

        while (timeTillWater > 0)
        {
            timeTillWater -= Time.deltaTime;
            growth += plantedCrop.growthRate * Time.deltaTime;

            Color plotColor = Color.HSVToRGB(
                waterHVal / 360f,
                Mathf.Clamp(waterMaxSat * (timeTillWater / plantedCrop.waterInterval), 0f, waterMaxOversat),
                1f);
            plotImage.color = plotColor;

            yield return null;
        }

        waterInProgress = false;
        timeTillWater = 0;
    }
}
