using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    // Códigos
    private PopulationIncome populationIncome;
    private SocialPolicy social;
    private Economy economy;

    [Header("População geral")]
    public int population = 220000000;
    public int menCount;
    public int womenCount;
    public int whiteCount;
    public int blackCount;
    public int otherCount;
    public int straightCount;
    public int lgbtCount;
    public int disableCount;

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
    [Range(0f, 1f)]
    public float other;
    [Range(0f, 1f)]
    public float straight;
    [Range(0f, 1f)]
    public float lgbt;
    [Range(0f, 1f)]
    public float disable;

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

        baby = 0.123f;
        teen = 0.208f;
        adult = 0.571f;
        old = 0.098f;

        men = 0.491f;
        women = 0.509f;

        white = 0.428f;
        black = 0.506f;
        other = 0.066f;

        straight = 0.917f;
        lgbt = 0.083f;

        disable = 0.075f;

        AlterarPorcentagensPopulacao();
    }

    public void AlterarPorcentagensPopulacao()
    {
        //Gambiarra por birra da Unity
        float mult = Mathf.Pow(10,4);

        // Gere valores aleatórios para cada variável dentro da soma total de 1
        baby *= (Random.Range(0.99f, 1.01f));
        teen *= (Random.Range(0.99f, 1.01f));
        adult *= (Random.Range(0.99f, 1.01f));
        old *= (Random.Range(0.99f, 1.01f));
        men *= (Random.Range(0.99f, 1.01f));
        women *= (Random.Range(0.99f, 1.01f));
        white *= (Random.Range(0.99f, 1.01f));
        black *= (Random.Range(0.99f, 1.01f));
        other *= (Random.Range(0.99f, 1.01f));
        straight *= (Random.Range(0.99f, 1.01f));
        lgbt *= (Random.Range(0.99f, 1.01f));
        disable *= (Random.Range(0.99f, 1.01f));

        // Verifique se a soma das variáveis é igual a 1
        float somaPopulacao = baby + teen + adult + old;
        float somaGenero = men + women;
        float somaRaca = white + black + other;
        float somaOrientacao = straight + lgbt;

        // Ajuste as variáveis para garantir que a soma seja igual a 1
        baby = Mathf.Round((baby / somaPopulacao) * mult) / mult;
        teen = Mathf.Round((teen / somaPopulacao) * mult) / mult;
        adult = Mathf.Round((adult / somaPopulacao) * mult) / mult;
        old = Mathf.Round((old / somaPopulacao) * mult) / mult;
        men = Mathf.Round((men / somaGenero) * mult) / mult;
        women = Mathf.Round((women / somaGenero) * mult) / mult;
        white = Mathf.Round((white / somaRaca) * mult) / mult;
        black = Mathf.Round((black / somaRaca) * mult) / mult;
        other = Mathf.Round((other / somaRaca) * mult) / mult;
        straight = Mathf.Round((straight / somaOrientacao) * mult) / mult;
        lgbt = Mathf.Round((lgbt / somaOrientacao) * mult) / mult;
        disable = Mathf.Round((disable / somaOrientacao) * mult) / mult;

        DistribuirPopulacao();
    }

    public  void DistribuirPopulacao() //base na ia - voltar para olhar
    {
        // Calcular a quantidade de cada grupo
        menCount = Mathf.RoundToInt(population * men);
        womenCount = Mathf.RoundToInt(population * women);
        whiteCount = Mathf.RoundToInt(population * white);
        blackCount = Mathf.RoundToInt(population * black);
        otherCount = Mathf.RoundToInt(population * other);
        straightCount = Mathf.RoundToInt(population * straight);
        lgbtCount = Mathf.RoundToInt(population * lgbt);
        disableCount = Mathf.RoundToInt(population * disable);

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