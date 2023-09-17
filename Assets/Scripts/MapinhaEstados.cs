using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            SpriteRenderer spriteRenderer = visuEstado[i].GetComponent<SpriteRenderer>();
            if(ver == 0) spriteRenderer.color = new Color(spriteRenderer.color.r, life.populationData.aprov, spriteRenderer.color.b);
            if(ver == 1) spriteRenderer.color = new Color(spriteRenderer.color.r, life.estados[i], spriteRenderer.color.b);
            if(ver == 2) spriteRenderer.color = new Color(spriteRenderer.color.r, life.estadosAprov[i], spriteRenderer.color.b);
        }
    }
}