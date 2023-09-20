using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoliticaData
{
    public string nome; // Nome da lei
    public float nivel; // Porcentagem da lei
    public Dictionary<string, bool> opos; // Leis opositoras
    public float introduzir; // Custo para criar a lei
    public float cancelar; // Custo para cancelar a lei
    public float aumentar; // Custo para aumentar a lei
    public float reduzir; // Custo para reduzir a lei
    public float custMin; // Custo mínimo da lei
    public float custMax; // Custo máximo da lei
    public float custForm; // Fórmula do custo da lei
    public float custMult; // Multiplicador do custo da lei
    public float estatizar; // Gasto/ganho para estatização/privatização da lei
    public float ganMin; // Ganho mínimo da lei
    public float ganMax; // Ganho máximo da lei
    public float ganForm; // Fórmula do ganho da lei
    public float ganMult; // Multiplicador do ganho da lei
    public Dictionary<string, float> politicas; // Consequências da lei

    public PoliticaData(string nome, float nivel, Dictionary<string, bool> opos, float introduzir, float cancelar, float aumentar, float reduzir, float custMin, float custMax, float custForm, float custMult, float estatizar, float ganMin, float ganMax, float ganForm, float ganMult, Dictionary<string, float> politicas)
    {
        this.nome = nome;
        this.nivel = nivel;
        this.opos = opos;
        this.introduzir = introduzir;
        this.cancelar = cancelar;
        this.aumentar = aumentar;
        this.reduzir = reduzir;
        this.custMin = custMin;
        this.custMax = custMax;
        this.custForm = custForm;
        this.custMult = custMult;
        this.estatizar = estatizar;
        this.ganMin = ganMin;
        this.ganMax = ganMax;
        this.ganForm = ganForm;
        this.ganMult = ganMult;
        this.politicas = politicas;
    }

    public float CalculateCost(float inputValue)
    {
        // Aqui você pode implementar a lógica para calcular o custo com base na fórmula fornecida
        // Exemplo de fórmula simples: custo = inputValue * 2
        return 0+(1.0f*inputValue);
    }
}

public class ExampleUsage : MonoBehaviour
{
    private Economy economy;
    private Tax tax;
    private void Start()
    {
        // Exemplo de criação de leis genéricas
        // Lei para aumentar a taxa de imposto sobre combustíveis
        Dictionary<string, bool> opos1 = new Dictionary<string, bool>();
        opos1.Add("Lei de Redução de Taxa de Combustível", false);
        opos1.Add("Lei de Aumento de Imposto de Renda", true);
        Dictionary<string, float> politicas1 = new Dictionary<string, float>();
        politicas1.Add("Aumento da Taxa de Combustível", 0.1f);
        politicas1.Add("Aumento do Custo de Transporte", 0.05f);
        PoliticaData leiAumentoTaxaCombustivel = new PoliticaData("Lei de Aumento de Taxa de Combustível", tax.fuelTax, opos1, 0.5f, 0.1f, 0.2f, 0.1f, 0f, 100f, 2f, 1.0f, -500f, 0f, 1000f, 1.5f, 1.0f, politicas1);

        // Lei para aumentar a taxa de imposto de renda
        Dictionary<string, bool> opos2 = new Dictionary<string, bool>();
        opos2.Add("Lei de Redução de Taxa de Combustível", true);
        opos2.Add("Lei de Aumento de Taxa de Combustível", false);
        Dictionary<string, float> politicas2 = new Dictionary<string, float>();
        politicas2.Add("Aumento do Imposto de Renda", 0.2f);
        politicas2.Add("Redução da Renda Disponível", 0.1f);
        PoliticaData leiAumentoImpostoRenda = new PoliticaData("Lei de Aumento de Imposto de Renda", tax.incomeTax, opos2, 0.3f, 0.1f, 0.1f, 0.1f, 0f, 100f, 1.5f, 1.0f, 500f, 0f, 1000f, 2f, 1.0f, politicas2);

        // Exemplo de cálculo de custo com base na fórmula
        float inputValue = 10f;
        float custo = leiAumentoTaxaCombustivel.CalculateCost(inputValue);
        Debug.Log("Custo calculado: " + custo);
    }
}