using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationIncome : MonoBehaviour 
{
    private Economy economy;
    private Tax tax;
    private SocialPolicy social;

    private float povertyRelief;
    private float healthInvestment;
    private float educationInvestment;
    private float technologyLevel;
    private float educationLevel;
    public float veryLowIncome;
    public float lowIncome;
    public float middleIncome;
    public float highIncome;
    private float totalIncome;
    private float basicExpensespoor;
    private float basicExpensesmiddle;
    private float basicExpenseshigh;

    void Start()
    {
        economy = GetComponent<Economy>();
        if (economy == null)
        {
            Debug.LogError("Componente Economy não encontrado!");
        }

        tax = GetComponent<Tax>();
        if (tax == null)
        {
            Debug.LogError("Componente Tax não encontrado!");
        }

        social = GetComponent<SocialPolicy>();
        if (social == null)
        {
            Debug.LogError("Componente SocialPolicy não encontrado!");
        }
    }

    public void UpdateValues(float povertyRelief, float healthInvestment, float educationInvestment, float technologyLevel, float educationLevel)
    {
        this.povertyRelief = povertyRelief;
        this.healthInvestment = healthInvestment;
        this.educationInvestment = educationInvestment;
        this.technologyLevel = technologyLevel;
        this.educationLevel = educationLevel;

        if (economy != null && tax != null && social != null)
        {
            totalIncome = CalculateIncome();
        }
        else
        {
            Debug.LogError("Componentes Economy, Tax ou SocialPolicy não foram inicializados corretamente!");
        }
    }

    private float CalculateIncome()
    {
        float diferencaMP;
        float diferencaRM;

        // Calculando renda dos humildes desempregados
        veryLowIncome = social.bolsaFamiliaInvestment;
        veryLowIncome -= tax.salesTax * 0.15f;

        // Calculando renda da classe baixa
        lowIncome = (social.minimumWage * Random.Range(0.95f,1.19f));
        lowIncome -= (tax.salesTax + tax.incomeTax) * 0.1f;
        lowIncome += social.bolsaFamiliaInvestment * 0.15f;

        // Calculando renda da classe média
        middleIncome = (social.minimumWage * Random.Range(1.2f,1.4f));
        middleIncome -= (tax.salesTax + tax.incomeTax) * 0.1f;

        // Calculando renda da classe alta
        highIncome = (social.maximumWage * Random.Range(1.41f,2f));
        highIncome -= (tax.salesTax + tax.incomeTax) * 0.1f;
        highIncome -= (tax.empresaTax) * 0.1f;

        // Ajustando renda com base no nível de tecnologia e educação
        lowIncome += (technologyLevel + educationLevel) * 0.1f;
        middleIncome += (technologyLevel + educationLevel) * 0.1f;
        highIncome += (technologyLevel + educationLevel) * 0.1f;

        // Gastos básicos
        basicExpensespoor = CalculateBasicExpenses(social.minimumWage,lowIncome,technologyLevel,educationLevel);
        basicExpensesmiddle = CalculateBasicExpenses(social.averageWage,middleIncome,technologyLevel,educationLevel);
        basicExpenseshigh = CalculateBasicExpenses(social.maximumWage,highIncome,technologyLevel,educationLevel);

        Debug.Log(basicExpensespoor + " " + basicExpensesmiddle + " " + basicExpensesmiddle);
        // Adicionando gastos com alimentação, passagem, saúde etc
        lowIncome -= basicExpensespoor;
        middleIncome -= basicExpensesmiddle;
        highIncome -= basicExpenseshigh;

        return lowIncome + middleIncome + highIncome;
    }

    private float CalculateBasicExpenses(float income, float socialClass, float technologyLevel, float educationLevel)
    {
        float foodCost = (income * 0.2f) * (1 + (0.1f * socialClass));
        float transportationCost = (income * 0.1f) * (1 + (0.1f * socialClass));
        float healthCost = (income * 0.1f) * (1 + (0.1f * socialClass));
        float educationCost = (income * 0.1f) * (1 + (0.1f * socialClass)) * (1 + (technologyLevel/100)) * (1 + (educationLevel/100));
        float basicExpenses = foodCost + transportationCost + healthCost + educationCost;
        return basicExpenses;
    }
}