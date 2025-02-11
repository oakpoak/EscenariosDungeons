using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iconos : MonoBehaviour
{
    public Ico []Icono =new Ico[3];
    // Start is called before the first frame update

    public void Activate(int a)
    {
        Icono[a].ActivateColor();
    }

    public void Desactivate(int a)
    {
        Icono[a].DesactivateColor();
    }

    public void Transparent(int a)
    {
        Icono[a].Transparent();
    }


}
