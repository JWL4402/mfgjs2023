using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    };

    [SerializeField]
    private int money;
    public int Money
    {
        get { return money; }
        set { SetMoney(value); }
    }

    public Season season;
    public int currentYear;
    [SerializeField] private int gameLength; // in years
    [SerializeField] private float seasonLength; // in seconds

    [SerializeField] private TextMeshProUGUI cashDisplay;
    [SerializeField] private TextMeshProUGUI yearDisplay;
    [SerializeField] private TextMeshProUGUI seasonDisplay;
    [SerializeField] private TextMeshProUGUI timeDisplay;

    private void Start()
    {
        SetMoney(money);

        StartCoroutine(StartTime());
    }

    private IEnumerator StartTime()
    {
        currentYear = 1;

        while (currentYear <= gameLength)
        {
            yearDisplay.text = "Year " + currentYear.ToString();
            yield return StartCoroutine(BeginSeasonCycle(season));
            currentYear++;
        }

        GameOver();
    }

    private IEnumerator BeginSeasonCycle(Season currentSeason)
    {
        float timer = 0f;
        seasonDisplay.text = currentSeason.ToString();

        while (timer < seasonLength)
        {
            timer += Time.deltaTime;
            timeDisplay.text = Mathf.CeilToInt(seasonLength - timer).ToString() + "s";
            yield return null;
        }

        int seasonCount = Enum.GetValues(typeof(Season)).Length;

        season = (Season)((int)(season + 1) % seasonCount);
        if (season != Season.Spring)
        {
            yield return StartCoroutine(BeginSeasonCycle(season));
        }
    }

    private void GameOver()
    {
        Debug.Log("Game over");
    }

    private void SetMoney(int amount)
    {
        money = amount;

        cashDisplay.text = "$ " + money.ToString();
    }
}
