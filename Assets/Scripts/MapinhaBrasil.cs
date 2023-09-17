using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapinhaBrasil : MonoBehaviour
{
    private LifeController life; // script vida

    public RectTransform trans; // trans
    public float escala = 35.0f; // scale
    public Vector3 initialPos;
    public Vector3 mousePoze;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        initialPos = trans.position;
    }
    private void Update()
    {
        mousePoze = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0); //340x, 700y
        escala += Input.mouseScrollDelta.y;
        escala = Mathf.Clamp(escala,20,100);

        if(mousePoze.x > 340.0f && mousePoze.y < 700.0f)
        {
            if(Input.mouseScrollDelta.y != 0)
                ScaleAtualizado();
            if(Input.GetButtonDown("Fire2"))
                trans.position = initialPos;
            if(Input.GetButton("Fire1"))
                MexeBrasil();
        }
    }

    public void ScaleAtualizado()
    {
        trans.localScale = new Vector3(escala,escala,1);
    }

    public void MexeBrasil()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"),0);
        trans.position += dir*Mathf.Pow(35,2.086f)*Time.deltaTime;
    }
}
