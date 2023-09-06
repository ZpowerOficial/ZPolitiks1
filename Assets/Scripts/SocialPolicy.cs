using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialPolicy : MonoBehaviour
{
    public float healthInvestment;
    public float educationInvestment;
    public float povertyRelief;
    public float minimumWage; // salário mínimo
    public float averageWage; // mínimo para ser classe média
    public float maximumWage; // mínimo para ser rico
    public float bolsaFamiliaInvestment; // bolsa para comprar pobres

    private Economy economy;
    
    private void Start()
    {
        economy = GetComponent<Economy>();
    }

    public void ApplySocialPolicies()
    {
        // Ajustando o PIB de acordo com o investimento em saúde
        economy.GDP += healthInvestment;

        // Ajustando o PIB de acordo com o investimento em educação
        economy.GDP += educationInvestment;

        // Ajustando o PIB de acordo com o alívio à pobreza
        economy.GDP += povertyRelief;

        // Ajustando o PIB de acordo com o emprego
        economy.GDP += povertyRelief * 0.1f;
    }
}

