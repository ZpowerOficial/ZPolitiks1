using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapinhaEstados : MonoBehaviour
{
    public GameObject visu; // visualização (por região, nacional, estados / de aprovação ou população)
    public List<GameObject> visuEstado; // visualização estadual

    public LifeController life;

    public int ver;

    public void Nacional()
    {
        if(ver < 2) ver++;
        else ver=0;

        Mudar();
    }

    public void Mudar()
    {
        for(int i = 0; i < visuEstado.Count; i++)
        {
            Image imagem = visuEstado[i].GetComponent<Image>();
            if(ver == 0) imagem.color = new Color(0.6f, life.populationData.aprov, 0.0f);
            if(ver == 1) imagem.color = new Color(0.6f, life.estados[i], 0.0f);
            if(ver == 2) imagem.color = new Color(0.6f, life.estadosAprov[i], 0.0f);
        }
    }
}