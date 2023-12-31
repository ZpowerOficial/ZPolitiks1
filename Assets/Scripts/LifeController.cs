using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopulationData
{
    [Range(0f,1f)]
    public float porcen;
    public int count;
    [Range(0f,1f)]
    public float aprov;

    [Range(0f,1f)]
    public float white;
    public float whiteCount;
    [Range(0f,1f)]
    public float whiteAprov;
    [Range(0f,1f)]
    public float black;
    public float blackCount;
    [Range(0f,1f)]
    public float blackAprov;
    [Range(0f,1f)]
    public float other;
    public float otherCount;
    [Range(0f,1f)]
    public float otherAprov;
    [Range(0f,1f)]
    public float straight;
    public float straightCount;
    [Range(0f,1f)]
    public float straightAprov;
    [Range(0f,1f)]
    public float lgbt;
    public float lgbtCount;
    [Range(0f,1f)]
    public float lgbtAprov;
    [Range(0f,1f)]
    public float disable;
    public float disableCount;
    [Range(0f,1f)]
    public float disableAprov;
    public float christian;
    public float christianCount;
    [Range(0f,1f)]
    public float christianAprov;
    public float otherReligion;
    public float otherReligionCount;
    [Range(0f,1f)]
    public float otherReligionAprov;
    public float atheism;
    public float atheismCount;
    [Range(0f,1f)]
    public float atheismAprov;
        
    public PopulationData(float porcen, int count, float aprov, float white, int whiteCount, float whiteAprov, float black, int blackCount, float blackAprov, float other, int otherCount, float otherAprov, float straight, int straightCount, float straightAprov, float lgbt, int lgbtCount, float lgbtAprov, float disable, int disableCount, float disableAprov,float christian, int christianCount, float christianAprov, float otherReligion, int otherReligionCount, float otherReligionAprov, float atheism, int atheismCount, float atheismAprov)
    {
        this.porcen = porcen;
        this.count = count;
        this.aprov = aprov;
        this.white = white;
        this.whiteCount = whiteCount;
        this.whiteAprov = whiteAprov;
        this.black = black;
        this.blackCount = blackCount;
        this.blackAprov = blackAprov;
        this.other = other;
        this.otherCount = otherCount;
        this.otherAprov = otherAprov;
        this.straight = straight;
        this.straightCount = straightCount;
        this.straightAprov = straightAprov;
        this.lgbt = lgbt;
        this.lgbtCount = lgbtCount;
        this.lgbtAprov = lgbtAprov;
        this.disable = disable;
        this.disableCount = disableCount;
        this.disableAprov = disableAprov;
        this.christian = christian;
        this.christianCount = christianCount;
        this.christianAprov = christianAprov;
        this.otherReligion = otherReligion;
        this.otherReligionCount = otherReligionCount;
        this.otherReligionAprov = otherReligionAprov;
        this.atheism = atheism;
        this.atheismCount = atheismCount;
        this.atheismAprov = atheismAprov;
    }

    public void AjustarSubdiv()
    {
        float somaRaca = white + black + other;
        float somaReli = christian + otherReligion + atheism;
        float somaSex = straight + lgbt;

        // Ajustar as subdivisões dentro do gênero proporcionalmente para que a soma seja igual a 1
        white /= somaRaca;
        black /= somaRaca;
        other /= somaRaca;
        christian /= somaReli;
        otherReligion /= somaReli;
        atheism /= somaReli;
        straight /= somaSex;
        lgbt /= somaSex;
    }
}

public class LifeController : MonoBehaviour
{
    // Códigos
    private PopulationIncome populationIncome;
    private SocialPolicy social;
    private Economy economy;

    [Header("População geral")]
    public PopulationData populationData;
    public PopulationData menData;
    public PopulationData womenData;

    [Header("Divisão da população")]
    [Range(0f, 1f)]
    public float baby;
    [Range(0f, 1f)]
    public float teen;
    [Range(0f, 1f)]
    public float adult;
    [Range(0f, 1f)]
    public float old;

    [Header("Taxas e valores")]
    public float homicidio;
    public float bornRate;
    public float bornPerYear;
    public float deathRate;
    public float deathPerYear;

    [Header("Migração e imigração")]
    public float migracao;
    public float imigracao;

    public List<float> estados;
    public List<float> estadosCount;
    public List<float> estadosAprov;

    private void Start()
    {
        economy = GetComponent<Economy>();
        populationIncome = GetComponent<PopulationIncome>();
        social = GetComponent<SocialPolicy>();

        populationData = new PopulationData(1.0f,220000000,0.5f,0.428f,Mathf.RoundToInt(0.428f*220000000),0.5f,0.506f,Mathf.RoundToInt(0.506f*220000000),0.5f,0.066f,Mathf.RoundToInt(0.066f*220000000),0.5f,0.917f,Mathf.RoundToInt(0.917f*220000000),0.5f,0.083f,Mathf.RoundToInt(0.083f*220000000),0.5f,0.075f,Mathf.RoundToInt(0.075f*220000000),0.5f,0.75f,Mathf.RoundToInt(0.75f*220000000),0.5f,0.11f,Mathf.RoundToInt(0.11f*220000000),0.5f,0.14f,Mathf.RoundToInt(0.14f*220000000),0.5f);
        populationData.AjustarSubdiv();

        baby = 0.123f;
        teen = 0.208f;
        adult = 0.571f;
        old = 0.098f;

        menData = new PopulationData(0.491f,0,0.5f,0.210f/0.491f,Mathf.RoundToInt((0.210f/0.491f)*108020000),0.5f,0.248f/0.491f,Mathf.RoundToInt((0.248f/0.491f)*108020000),0.5f,0.032f/0.491f,Mathf.RoundToInt((0.032f/0.491f)*108020000),0.5f,0.041f/0.491f,Mathf.RoundToInt((0.041f/0.491f)*108020000),0.5f,0.041f/0.491f,Mathf.RoundToInt((0.041f/0.491f)*108020000),0.5f,0.037f/0.491f,Mathf.RoundToInt((0.037f/0.491f)*108020000),0.5f,0.368f/0.491f,Mathf.RoundToInt((0.368f/0.491f)*108020000),0.5f,0.054f,Mathf.RoundToInt((0.054f/0.491f)*108020000),0.5f,0.069f,Mathf.RoundToInt((0.069f/0.491f)*108020000),0.5f);
        menData.AjustarSubdiv();
        womenData = new PopulationData(0.509f,0,0.5f,0.218f/0.509f,Mathf.RoundToInt((0.218f/0.509f)*111980000),0.5f,0.258f/0.509f,Mathf.RoundToInt((0.258f/0.509f)*111980000),0.5f,0.034f/0.509f,Mathf.RoundToInt((0.034f/0.509f)*111980000),0.5f,0.467f/0.509f,Mathf.RoundToInt((0.467f/0.509f)*111980000),0.5f,0.042f/0.509f,Mathf.RoundToInt((0.042f/0.509f)*111980000),0.5f,0.038f/0.509f,Mathf.RoundToInt((0.038f/0.509f)*111980000),0.5f,0.382f,Mathf.RoundToInt((0.382f/0.509f)*111980000),0.5f,0.056f,Mathf.RoundToInt((0.056f/0.509f)*111980000),0.5f,0.071f,Mathf.RoundToInt((0.071f/0.509f)*111980000),0.5f);
        womenData.AjustarSubdiv();

        estados = new List<float>()
        {
            0.219f,
            0.101f,
            0.079f,
            0.070f,
            0.056f,
            0.054f,
            0.045f,
            0.043f,
            0.041f,
            0.038f,
            0.035f,
            0.033f,
            0.020f,
            0.019f,
            0.019f,
            0.018f,
            0.016f,
            0.016f,
            0.015f,
            0.014f,
            0.014f,
            0.011f,
            0.008f,
            0.007f,
            0.004f,
            0.004f,
            0.003f
        };

        for(int i = 0; i < estados.Count; i++)
            estadosCount.Add(Mathf.RoundToInt(populationData.count*estados[i]));
        for(int i = 0; i < estados.Count; i++)
            estadosAprov.Add(0.5f);

        AlterarPorcentagensPopulacao();
        economy.Pega(); // popularidade na lateral
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
        menData.porcen *= (Random.Range(0.99f, 1.01f));
        womenData.porcen *= (Random.Range(0.99f, 1.01f));
        populationData.white *= (Random.Range(0.99f, 1.01f));
        populationData.black *= (Random.Range(0.99f, 1.01f));
        populationData.other *= (Random.Range(0.99f, 1.01f));
        populationData.straight *= (Random.Range(0.99f, 1.01f));
        populationData.lgbt *= (Random.Range(0.99f, 1.01f));
        populationData.disable *= (Random.Range(0.99f, 1.01f));
        menData.aprov *= (Random.Range(0.99f, 1.01f));
        womenData.aprov *= (Random.Range(0.99f, 1.01f));
        populationData.whiteAprov *= (Random.Range(0.99f, 1.01f));
        populationData.blackAprov *= (Random.Range(0.99f, 1.01f));
        populationData.otherAprov *= (Random.Range(0.99f, 1.01f));
        populationData.straightAprov *= (Random.Range(0.99f, 1.01f));
        populationData.lgbtAprov *= (Random.Range(0.99f, 1.01f));
        populationData.disableAprov *= (Random.Range(0.99f, 1.01f));
        populationData.christianAprov *= (Random.Range(0.99f, 1.01f));
        populationData.otherReligionAprov *= (Random.Range(0.99f, 1.01f));
        populationData.atheismAprov *= (Random.Range(0.99f, 1.01f));

        populationData.aprov = (menData.aprov*menData.porcen + womenData.aprov*womenData.porcen);

        float somaEs = 0;
        for(int i = 0; i < estados.Count; i++)
        {
            estados[i] *= (Random.Range(0.99f, 1.01f));
            somaEs += estados[i];
        }
        for(int i = 0; i < estados.Count; i++)
            estados[i] /= somaEs;

        DistribuirPopulacao();
    }

    public  void DistribuirPopulacao() //base na ia - voltar para olhar
    {
        float ajeitar = menData.porcen + womenData.porcen;

        menData.porcen /= ajeitar;
        womenData.porcen /= ajeitar;

        // Calcular a quantidade de cada grupo
        menData.count = Mathf.RoundToInt(populationData.count * menData.porcen);
        womenData.count = Mathf.RoundToInt(populationData.count * womenData.porcen);
        populationData.whiteCount = Mathf.RoundToInt(populationData.count * populationData.white);
        populationData.blackCount = Mathf.RoundToInt(populationData.count * populationData.black);
        populationData.otherCount = Mathf.RoundToInt(populationData.count * populationData.other);
        populationData.straightCount = Mathf.RoundToInt(populationData.count * populationData.straight);
        populationData.lgbtCount = Mathf.RoundToInt(populationData.count * populationData.lgbt);
        populationData.disableCount = Mathf.RoundToInt(populationData.count * populationData.disable);
        populationData.christianCount = Mathf.RoundToInt(populationData.count * populationData.christian);
        populationData.otherReligionCount = Mathf.RoundToInt(populationData.count * populationData.otherReligion);
        populationData.atheismCount = Mathf.RoundToInt(populationData.count * populationData.atheism);

        // Dividir a população por faixa etária
        int babyCount = Mathf.RoundToInt((menData.count + womenData.count) * baby);
        int teenCount = Mathf.RoundToInt((menData.count + womenData.count) * teen);
        int adultCount = Mathf.RoundToInt((menData.count + womenData.count) * adult);
        int oldCount = Mathf.RoundToInt((menData.count + womenData.count) * old);
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

        populationData.count -= Mathf.RoundToInt((deathRate*(populationData.count/100000))/12);
        deathPerYear += Mathf.RoundToInt((deathRate*(populationData.count/100000))/12);
    }

    public float Homicidio()
    {
        homicidio = 22.3f;
        homicidio *= (1-((economy.securityRate/10)*Random.Range(0.2f,1f)));
        homicidio *= (1+((economy.crimeRate/10)*Random.Range(0.1f,0.5f)));

        return homicidio;
    }
}