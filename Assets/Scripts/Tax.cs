using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tax : MonoBehaviour
{
    public float fuelTax = 0.0f;
    private float fuelTaxImportance = 0.05f;
    public float incomeTax = 0.0f;
    private float incomeTaxImportance = 0.15f;
    public float salesTax = 0.0f;
    private float salesTaxImportance = 0.15f;
    public float empresaTax = 0.0f;
    private float empresaTaxImportance = 0.125f;
    public float propertyTax = 0.0f;
    private float propertyTaxImportance = 0.1f;
    public float luxuryGoodsTax = 0.0f;
    private float luxuryGoodsTaxImportance = 0.05f;
    public float corporateIncomeTax = 0.0f;
    private float corporateIncomeTaxImportance = 0.2f;
    public float taxgeral = 0.0f;
    public float governmentSpending;

    // Variável para rastrear o êxodo fiscal
    public bool isFiscalExodusHappening = false;

    private void Update()
    {
        taxgeral = (empresaTax * empresaTaxImportance) + (fuelTax * fuelTaxImportance) + (incomeTax * incomeTaxImportance) + (salesTax * salesTaxImportance) + (propertyTax * propertyTaxImportance) + (luxuryGoodsTax * luxuryGoodsTaxImportance) + (corporateIncomeTax * corporateIncomeTaxImportance);
        governmentSpending = Mathf.Clamp(governmentSpending, 0, 1250000);
        fuelTax = Mathf.Clamp01(fuelTax);
        incomeTax = Mathf.Clamp01(incomeTax);
        salesTax = Mathf.Clamp01(salesTax);
        empresaTax = Mathf.Clamp01(empresaTax);
        propertyTax = Mathf.Clamp01(propertyTax);
        luxuryGoodsTax = Mathf.Clamp01(luxuryGoodsTax);
        corporateIncomeTax = Mathf.Clamp01(corporateIncomeTax);

        // Verificar se a taxgeral é maior que 0.6 e, em caso afirmativo, iniciar o êxodo fiscal
        if (taxgeral > 0.6)
        {
            isFiscalExodusHappening = true;
            // Adicione aqui o código para implementar o êxodo fiscal
        }
        else
        {
            isFiscalExodusHappening = false;
        }
    }
}
