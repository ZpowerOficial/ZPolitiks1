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
        tax = GetComponent<Tax>();
        social = GetComponent<SocialPolicy>();
    }


    public void UpdateValues(float povertyRelief, float healthInvestment, float educationInvestment, float technologyLevel, float educationLevel)
    {
        this.povertyRelief = povertyRelief;
        this.healthInvestment = healthInvestment;
        this.educationInvestment = educationInvestment;
        this.technologyLevel = technologyLevel;
        this.educationLevel = educationLevel;
        totalIncome = CalculateIncome();
    }

    private float CalculateIncome()
    {
        lowIncome = 0f;
        middleIncome = 0f;
        highIncome = 0f;
        
        // Calculando renda da classe baixa
        lowIncome = (social.minimumWage * economy.employmentGrowthRate) * (1 - povertyRelief);
        lowIncome -= (tax.salesTax + tax.incomeTax) * 0.1f;
        lowIncome += social.bolsaFamiliaInvestment;

        // Calculando renda da classe média
        middleIncome = (social.averageWage * economy.employmentGrowthRate) * (1 - povertyRelief);
        middleIncome -= (tax.salesTax + tax.incomeTax) * 0.1f;
        middleIncome += social.bolsaFamiliaInvestment * 0.5f;

        // Calculando renda da classe alta
        highIncome = (social.maximumWage * economy.employmentGrowthRate) * (1 - povertyRelief);
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