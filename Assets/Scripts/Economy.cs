using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    // Pegar as variaveis de cada código dos setores
    private PrimarySector primary;
    private SecondarySector secondary;
    private TertiarySector tertiary;

    // Variáveis que armazenam os dados dos setores
    private float privateConsumption;
    private float investments;
    private float primarySectorGDP;
    private float secondarySectorGDP;
    private float tertiarySectorGDP;
    private float exports;
    private float imports;

    public Text gdpText;
    public int ano = 2023, mes = 1;

    // Variável que armazena o PIB total
    public float GDP;

    // Impostos
    private Tax tax;
    private PopulationIncome populationIncome;
    private SocialPolicy social;

    // Indicadores Econômicos
    public float inflation;
    private float currentIPC;
    public float lastIPC;
    public float interestRate;
    public float tradeBalance;
    public float paymentBalance;
    public float spendingLimit;
    public float employmentGrowthRate;
    public float oilPrice;
    public float fuelPrice;
    public float purchasingPower;

    // Investimentos em infraestrutura
    public float infrastructureInvestment;

    // Investimentos em P&D
    public float researchAndDevelopmentInvestment;

    // Políticas fiscais
    public float taxRate;
    public float governmentSpending;
    public float publicDebt;
    public float deficit;
    public float phillipsCurve;

    // População, tecnologia, criminalidade, educação
    private int population;
    private float technologyLevel;
    private float educationLevel;
    private float economicGrowth;
    private float unemploymentRate;

    // Adicione variáveis para rastrear os efeitos das taxas
    private float taxRevenue;
    private float economicGrowthRate;
    private float inflationRate;

    private void Start()
    {
        tax = GetComponent<Tax>();
        if (tax == null)
        {
            Debug.LogError("Tax component not found!");
        }
        primary = GetComponent<PrimarySector>();
        secondary = GetComponent<SecondarySector>();
        tertiary = GetComponent<TertiarySector>();
        tax = GetComponent<Tax>();
        populationIncome = GetComponent<PopulationIncome>();
        social = GetComponent<SocialPolicy>();
        population = 220000000;
        technologyLevel = 50f;
        educationLevel = 40f;

        primarySectorGDP = primary.primarySectorGDP;
        secondarySectorGDP = secondary.secondarySectorGDP;
        tertiarySectorGDP = tertiary.tertiarySectorGDP;

        UpdateEconomicIndicators();
        AtualizaGDP();
    }

    public void AtualizeValues()
    {
        if(12 > mes)
        {
            mes++;
        }
        else
        {
            mes = 1; ano++;
        }
        primarySectorGDP = primary.primarySectorGDP;
        secondarySectorGDP = secondary.secondarySectorGDP;
        tertiarySectorGDP = tertiary.tertiarySectorGDP;

        // Atualizando os Indicadores Econômicos
        UpdateEconomicIndicators();
        AtualizaGDP();
    }

    private void UpdateEconomicIndicators()
    {
        // Atualizando as variáveis de Indicadores Econômicos
        unemploymentRate = CalculateUnemploymentRate();
        currentIPC = RetrieveCurrentIPC();
        lastIPC = RetrieveLastIPC();
        inflation = CalculateInflation();
        interestRate = CalculateInterestRate();
        tradeBalance = CalculateTradeBalance();
        paymentBalance = CalculatePaymentBalance();
        spendingLimit = RetrieveSpendingLimit();
        employmentGrowthRate = CalculateEmploymentGrowthRate();
        oilPrice = CalculateOilPrice();
        fuelPrice = CalculateFuelPrice();
        purchasingPower = CalculatePurchasingPower();
        publicDebt = CalculatePublicDebt();
        deficit = CalculateDeficit();
        infrastructureInvestment = CalculateInfrastructureInvestment();
        researchAndDevelopmentInvestment = CalculateResearchAndDevelopmentInvestment();
        taxRate = CalculateTaxRate();
        governmentSpending = CalculateGovernmentSpending();
        privateConsumption = RetrievePrivateConsumption();
        investments = RetrieveInvestments();
        exports = RetrieveExports();
        imports = RetrieveImports();
        phillipsCurve = CalculatePhillipsCurve(unemploymentRate);
        taxRevenue = CalculateTaxRevenue();
        inflationRate = CalculateInflationRate();
        IncreaseInflation(inflation/12);
        populationIncome.UpdateValues(social.bolsaFamiliaInvestment,social.healthInvestment,social.educationInvestment,technologyLevel,educationLevel);
    }

    private float CalculateUnemploymentRate()
    {
        float naturalUnemploymentRate = 4.5f; 
        float situacao = Random.Range(-0.2f,0.2f);
        float unemploymentRate = (naturalUnemploymentRate) + (situacao * Mathf.Max(((tax.empresaTax + tax.incomeTax) * 0.1f),0.01f));
        unemploymentRate += naturalUnemploymentRate/phillipsCurve;
        return Mathf.Clamp(unemploymentRate, 0f, 100f);
    }

    private float CalculatePhillipsCurve(float unemploymentRatePhillips)
    {
        float beta = 0.5f;
        float alpha = 2.0f;

        return beta * unemploymentRatePhillips + alpha;
    }

    private float RetrieveCurrentIPC()
    {
        // Preços de uma cesta de bens de consumo de referência
        float foodPrice = 620f;
        float transportPrice = 121.5f;
        float healthPrice = 150f;
        //...

        // Cálculo do IPC
        float currentIPC = (foodPrice + transportPrice + healthPrice) / 3;

        return currentIPC;
    }

    private float RetrieveLastIPC()
    {
        // Preços de uma cesta de bens de consumo de referência de um período anterior
        float foodPrice = 540f;
        float transportPrice = 121.5f;
        float healthPrice = 120f;
        //...

        // Cálculo do IPC antigo
        float lastIPC = (foodPrice + transportPrice + healthPrice) / 3;

        return lastIPC;
    }

    private float CalculateInflation()
    {
        // Cálculo da Inflação
        return (currentIPC - lastIPC) / lastIPC;
    }

    private float CalculateInterestRate()
    {
        // Taxa de juros base
        float baseInterestRate = 6.96f;

        //Fatores que afetam a taxa de juros
        float inflationFactor = inflation; 
        float economicFactor = Random.Range(-0.01f, 0.01f);
        float monetaryPolicyFactor = Random.Range(-0.01f, 0.01f);

        // Cálculo da taxa de juros final
        float interestRate = baseInterestRate + (baseInterestRate * inflationFactor) + (baseInterestRate * economicFactor) + (baseInterestRate * monetaryPolicyFactor);

        return interestRate;
    }

    private float CalculateTradeBalance()
    {
        // Cálculo da Balança Comercial
        return 0f;
    }

    private float CalculatePaymentBalance()
    {
        // Cálculo da Balança de Pagamentos
        return 0f;
    }

    private float CalculateEmploymentGrowthRate()
    {
        // Cálculo da Taxa de Crescimento do Emprego
        return 0f;
    }

    private float CalculateOilPrice()
    {
        // Preço base do barril de petróleo
        float basePrice = 2.5f;

        //Fatores que afetam o preço do barril de petróleo
        float demandFactor = Random.Range(-0.1f, 0.1f); 
        float supplyFactor = Random.Range(-0.1f, 0.1f);
        float geopoliticalFactor = Random.Range(-0.1f, 0.1f);

        // Cálculo do preço final do barril de petróleo
        float oilPrice = basePrice + (basePrice * demandFactor) + (basePrice * supplyFactor) + (basePrice * geopoliticalFactor);

        return oilPrice;
    }

    private float CalculateFuelPrice()
    {
        // Preço base da gasolina
        float basePrice = 1f;

        // Calculando o preço final da gasolina
        float fuelPrice = basePrice + (basePrice * inflation) + (basePrice * oilPrice);
        fuelPrice += (fuelPrice * tax.fuelTax);

        return fuelPrice;
    }

    private float CalculatePurchasingPower()
    {
        // Cálculo do Poder de Compra
        return 0f;
    }

    private float CalculateInfrastructureInvestment()
    {
        // Cálculo dos investimentos em infraestrutura
        //...
        return 0f;
    }

    private float CalculateResearchAndDevelopmentInvestment()
    {
        // Cálculo dos investimentos em P&D
        //...
        return 0f;
    }
    private float CalculateTaxRate()
    {
        return tax.taxgeral;
    }

    private float CalculateGovernmentSpending()
    {
        float spendingLimit = RetrieveSpendingLimit();
        float actualGovernmentSpending = RetrieveActualGovernmentSpending();

        if(actualGovernmentSpending > spendingLimit)
        {
            inflation += CalculateInflationIncrease();
        }

        return actualGovernmentSpending;
    }

    private float CalculateInflationIncrease()
    {
        float excessSpending = governmentSpending - spendingLimit;
        float inflationIncrease = 0;
        if (excessSpending > 0)
        {
            float k = 0.01f; // constante pode ser ajustada para obter o comportamento desejado
            inflationIncrease = k * Mathf.Exp(excessSpending / spendingLimit);
        }
        return (currentIPC - lastIPC) / lastIPC + inflationIncrease;
    }

    private void IncreaseInflation(float increaseAmount)
    {
        inflation += increaseAmount;
    }

    private float RetrieveActualGovernmentSpending()
    {
        return tax.governmentSpending;
    }

    private float RetrieveSpendingLimit()
    {
        float previousYearSpending = RetrievePreviousYearSpending();
        float pibInflation = RetrievePIBInflation();
        float spendingLimit = previousYearSpending * (1 + pibInflation);
        return spendingLimit;
    }

    private float RetrievePreviousYearSpending()
    {
        return 5000;
    }

    private float RetrievePIBInflation()
    {
        float currentYearPIB = GDP;
        float previousYearPIB = RetrievePreviousYearPIB();
        float pibInflation = (currentYearPIB - previousYearPIB) / previousYearPIB;
        return pibInflation;
    }

    private float RetrievePreviousYearPIB()
    {
        return 5500;
    }

    private float CalculatePublicDebt()
    {
        // Cálculo da dívida pública
        float debt = publicDebt + deficit;
        float inflationAdjustment = debt * inflation;
        return debt + inflationAdjustment;
    }

    private float CalculateDeficit()
    {
        deficit = RetrieveGovernmentIncome() - governmentSpending;
        return deficit;
    }
    
    private float RetrievePrivateConsumption()
    {
        //Calculate the private consumption based on the population and the disposable income,
        // taking into account factors such as taxes and inflation
        return (population/1000000) * RetrieveDisposableIncome() * (1 - tax.taxgeral) / (1 + inflation);
    }

    // Calcule a receita do governo
    private float CalculateTaxRevenue()
    {
        float totalPopulation = population;

        return taxRate * totalPopulation * GDP;
    }

    private float CalculateCurrentInflation()
    {
        // Calcula a inflação do ano atual
        return (currentIPC - lastIPC) / lastIPC;
    }

    // Calcule a inflação
    private float CalculateInflationRate()
    {
        float previousYearInflation = inflation;
        float currentYearInflation = CalculateCurrentInflation();

        return currentYearInflation - previousYearInflation;
    }


    private float RetrieveDisposableIncome() {
        //Calculate disposable income by subtracting taxes and necessary expenses from total income
        float taxes = tax.taxgeral;
        float necessaryExpenses = RetrieveNecessaryExpenses();
        return RetrieveTotalIncome() - taxes - necessaryExpenses;
    }

    private float RetrieveTotalIncome() {
        //Calculate the total income of all individuals in the country
        float wages = RetrieveWages();
        //float selfEmploymentIncome = RetrieveSelfEmploymentIncome();
        float investmentIncome = RetrieveInvestmentIncome();
        float governmentIncome = RetrieveGovernmentIncome();
        return wages + investmentIncome + governmentIncome;
    }

    private float RetrieveGovernmentIncome()
    {
        return 500f;
    }

    private float RetrieveInvestmentIncome()
    {
        return 400f;
    }

    private float RetrieveWages() {
        //Calculate the total wages earned by all employees in the country
        float baseWage = 1200f; //base wage for all employees
        float educationMultiplier = 1 + (educationLevel * 0.1f); //multiplier based on education level
        float totalWages = baseWage * educationMultiplier;
        return totalWages*(1-tax.incomeTax);
    }


    private float RetrieveNecessaryExpenses() {
        //Calculate necessary expenses such as food, housing, transportation and healthcare
        float foodExpenses = 200;
        float housingExpenses = 100;
        float transportationExpenses = 300;
        float healthcareExpenses = 150;
        return foodExpenses + housingExpenses + transportationExpenses + healthcareExpenses;
    }


    private float RetrieveInvestments()
    {
        //Calculate the investments based on the economic growth, interest rate, and other economic factors
        return economicGrowth * interestRate * 0.25f;
    }

    private float RetrieveExports()
    {
        //Calculate the value of exports based on the amount and price of goods exported
        //return exportAmount * exportPrice;
        return 30*40;
    }

    private float RetrieveImports()
    {
        //Calculate the amount of imports based on technology level, education level, and other factors
        float technologyFactor = technologyLevel / 100f;
        float educationFactor = educationLevel / 100f;
        float importAmount = 500 * (1 - technologyFactor) * (1 - educationFactor);

        //Calculate the price of imports based on import amount, base price, and inflation
        float importPrice = 10 * (1 + inflation) * (importAmount / 500);

        return importAmount * importPrice;
    }


    public void AtualizaGDP()
    {
        // Atualizando o PIB total
        GDP = privateConsumption + investments + primarySectorGDP + secondarySectorGDP + tertiarySectorGDP + exports - imports;

        // Função de transferência não linear
        GDP *= Mathf.Exp(inflation) / (1 + Mathf.Exp(inflation));
        
        gdpText.text = "Ano: " + mes + "/" + ano + "\nPIB: " + GDP + "\nInflação: " + inflation*100 + "\nJuros: " + interestRate + "\nGasolina: " + fuelPrice + "\nTeto de gastos: " + spendingLimit + "\nGastos: " + governmentSpending + "\nDesemprego: " + unemploymentRate + "\nDinheiro Pobre: " + populationIncome.lowIncome + "\nDinheiro Meio: " + populationIncome.middleIncome + "\nDinheiro Rico: " + populationIncome.highIncome;
    }
}
