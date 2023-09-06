using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    // Códigos
    private PopulationIncome populationIncome;
    private SocialPolicy social;
    private Economy economy;

    // População
    public int population = 220000000;

    [Header("Divisão da população")]
    public float baby;
    public float teen;
    public float adult;
    public float old;
    public float men;
    public float women;
    public float white;
    public float nigga;

    [Header("Taxas e valores")]
    public float bornRate;
    public float deathRate;
    public float deathPerYear;

    private void Start()
    {
        economy = GetComponent<Economy>();
        populationIncome = GetComponent<PopulationIncome>();
        social = GetComponent<SocialPolicy>();
    }

    public void Natalidade()
    {
        bornRate = 13;
    }

    public void Mortalidade()
    {
        // Mortes a cada 100k pessoas
        deathRate = 22;
        deathRate -= (economy.healthLevel/10);
        deathRate += (economy.crimeRate/10) + (economy.unemploymentRate/100);

        population -= Mathf.RoundToInt((deathRate * (population/100000))/12);
        deathPerYear += Mathf.RoundToInt((deathRate * (population/100000))/12);
    }
}