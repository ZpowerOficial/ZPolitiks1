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
    [Range(0f, 1f)]
    public float baby;
    [Range(0f, 1f)]
    public float teen;
    [Range(0f, 1f)]
    public float adult;
    [Range(0f, 1f)]
    public float old;
    [Range(0f, 1f)]
    public float men;
    [Range(0f, 1f)]
    public float women;
    [Range(0f, 1f)]
    public float white;
    [Range(0f, 1f)]
    public float black;


    [Header("Taxas e valores")]
    public float homicidio;
    public float bornRate;
    public float bornPerYear;
    public float deathRate;
    public float deathPerYear;

    [Header("Migração e imigração")]
    public float migracao;
    public float imigracao;

    private void Start()
    {
        economy = GetComponent<Economy>();
        populationIncome = GetComponent<PopulationIncome>();
        social = GetComponent<SocialPolicy>();

        DistribuirPopulacao();
    }

    private void DistribuirPopulacao() //base na ia - voltar para olhar
    {
        // Calcular a quantidade de cada grupo
        int menCount = Mathf.RoundToInt(population * men);
        int womenCount = Mathf.RoundToInt(population * women);
        int whiteCount = Mathf.RoundToInt(population * white);
        int blackCount = Mathf.RoundToInt(population * black);

        // Dividir a população por faixa etária
        int babyCount = Mathf.RoundToInt((menCount + womenCount) * baby);
        int teenCount = Mathf.RoundToInt((menCount + womenCount) * teen);
        int adultCount = Mathf.RoundToInt((menCount + womenCount) * adult);
        int oldCount = Mathf.RoundToInt((menCount + womenCount) * old);
    }

    public void Natalidade()
    {
        bornRate = 13;
        bornPerYear += 0;
    }

    public void Mortalidade()
    {
        // Mortes a cada 100k pessoas
        homicidio = Homicidio();
        deathRate = 22.15f;

        if (economy.healthLevel <= 5f)
        {
            deathRate *= (1f + ((5f - economy.healthLevel) / 10f) * Random.Range(0.2f, 1f));
        }
        else
        {
            deathRate *= (1f - ((economy.healthLevel - 5f) / 10f) * Random.Range(0.2f, 1f));
        }

        deathRate *= 1+(economy.unemploymentRate/100);
        deathRate += homicidio;

        population -= Mathf.RoundToInt((deathRate*(population/100000))/12);
        deathPerYear += Mathf.RoundToInt((deathRate*(population/100000))/12);
    }

    public float Homicidio()
    {
        homicidio = 22.3f;
        homicidio *= (1-((economy.securityRate/10)*Random.Range(0.2f,1f)));
        homicidio *= (1+((economy.crimeRate/10)*Random.Range(0.1f,0.5f)));

        return homicidio;
    }
}