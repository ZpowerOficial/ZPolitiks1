using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondarySector : MonoBehaviour
{
    // Variáveis que armazenam as estatísticas do setor
    public float[] sectorProductions;
    public float secondarySectorGDP;

    // Método para atualizar as estatísticas do setor
    public void UpdateProductionData(float[] productions)
    {
        sectorProductions = productions;
        secondarySectorGDP = 0f;

        // Atualizando o PIB do setor
        for (int i = 0; i < productions.Length; i++)
        {
            secondarySectorGDP += sectorProductions[i];
        }
    }
}
