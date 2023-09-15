using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapinhaBrasil : MonoBehaviour
{
    private LifeController life; // script vida

    public RectTransform trans; // trans

    public float escala = 40.0f; // scale
    public Vector3 mousePosition;
    public Vector3 dif;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        escala += Input.mouseScrollDelta.y;
        escala = Mathf.Clamp(escala,20,100);

        if(Input.mouseScrollDelta.y != 0) ScaleAtualizado();
        if(Input.GetButton("Fire1")) MexeBrasil();
    }

    public void ScaleAtualizado()
    {
        trans.localScale = new Vector3(escala,escala,1);
    }

    public void MexeBrasil()
    {
        //dif = mousePosition - trans.position;
        //mousePosition = Camera.main.ScreenToWorldPoint(escala*Input.mousePosition);
        trans.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, canvas.planeDistance));
    }
}
