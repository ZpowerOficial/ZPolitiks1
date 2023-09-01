using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    PrimarySector primarySector;SecondarySector secondarySector;TertiarySector tertiarySector;

void Start()
{
    primarySector = GetComponent<PrimarySector>();secondarySector = GetComponent<SecondarySector>();tertiarySector = GetComponent<TertiarySector>();
}

void Update()
{
    // Atualizando as estatísticas do setor
    primarySector.UpdateProductionData(new float[] {1000f, 500f, 250f, 250f});
    //Debug.Log("Primary Sector GDP: " + primarySector.primarySectorGDP);

    secondarySector.UpdateProductionData(new float[] {1000f, 500f, 250f, 250f});
    //Debug.Log("Primary Sector GDP: " + secondarySector.secondarySectorGDP);

    tertiarySector.UpdateProductionData(new float[] {1000f, 500f, 250f, 250f});
    //Debug.Log("Tertiary Sector GDP: " + tertiarySector.tertiarySectorGDP);
}

}
