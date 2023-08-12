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
    private int cropIndex;

    private ObjectScript objectScript;

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

    public void SetPlowed(bool plow)
    {
        if (debris != DebrisState.NONE) { return; }

        plowed = plow;
        gameObject.GetComponent<Image>().sprite = plow ? plowedPlotSprite : debrisSpriteList[0];
    }

    public void PlantPlot(Crop crop)
    {
        if (debris != DebrisState.NONE) { return; }
        if (!plowed) { return; }
        
        plantedCrop = crop;
        cropImage.gameObject.SetActive(true);
        UpdateCropSprite(0);
    }

    private bool waterInProgress = false;
    public void WaterPlot(float waterDuration)
    {
        if (debris != DebrisState.NONE) { return; }
        if (plantedCrop == null) { return; }


        timeTillWater = waterDuration;

        StartCoroutine(StartGrowthCycle());
    }

    public void SellCrop()
    {
        if (growth < 1f)
        {
            Debug.LogError("selling ungrown plant?");
            return;
        }

        LogicScript logic = FindObjectOfType<LogicScript>();
        logic.money += plantedCrop.value;

        ClearPlot();
    }

    public void ClearPlot()
    {
        SetPlowed(false);

        growth = 0f;
        plantedCrop = null;
        cropImage.gameObject.SetActive(false);
        SetMaturity(false);
    }

    private void UpdateCropSprite(int spriteIndex)
    {
        cropIndex = spriteIndex;
        cropImage.sprite = plantedCrop.sprites[cropIndex];
    }

    private void SetMaturity(bool mature)
    {
        if (mature)
        {
            objectScript = cropImage.gameObject.AddComponent<ObjectScript>();
            objectScript.draggedObject = cropImage.gameObject;
        }
        else
        {
            Destroy(objectScript);
        }
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
            if (plantedCrop == null)
            {
                yield break;
            }
            
            timeTillWater -= Time.deltaTime;
            growth += plantedCrop.growthRate * Time.deltaTime;

            Color plotColor = Color.HSVToRGB(
                waterHVal / 360f,
                Mathf.Clamp(waterMaxSat * (timeTillWater / plantedCrop.waterInterval), 0f, waterMaxOversat),
                1f);

            int growthIndex = Mathf.Clamp(Mathf.FloorToInt(growth * 3f), 0, 3);
            if (growthIndex != cropIndex)
            {
                UpdateCropSprite(growthIndex);
                if (growthIndex == 3)
                {
                    SetMaturity(true);
                }
            }

            plotImage.color = plotColor;

            yield return null;
        }

        waterInProgress = false;
        timeTillWater = 0;
    }
}
