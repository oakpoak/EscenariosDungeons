
using UnityEngine;

public class PointerSelection : MonoBehaviour

{
    public Pointer[] Punteros = new Pointer[3];
    private int punteroActivo = 2;
    public Iconos Icon;
    private bool[] check = new bool[] {false, false, false};  
    private bool[] dead = new bool[]  {false, false, false};
    public Player Modificador;
    




    public void MoveRight(float r, float g, float b, float a)
    {
        //Funciona
        Punteros[punteroActivo].DesactivateColor(r, g, b); // Desactiva el color del puntero actual
        Icon.Desactivate(punteroActivo); //se pone un poco transparente el icono pequeño
        
        //FUNCIONA
        punteroActivo = (punteroActivo - 1) % Punteros.Length; // Retrocede al puntero anterior

        if (punteroActivo < 0)
        {
            punteroActivo = 2;
        }

        while (check[punteroActivo] == true)
        {
            punteroActivo--;
            if (punteroActivo < 0)
            {
                punteroActivo = 2;
            }
        }

        Punteros[punteroActivo].ActivateColor(r, g, b, a); // Activa el color del nuevo puntero activo
        Icon.Activate(punteroActivo);//Se activa el icono pequeño

    }

    public void MoveLeft(float r, float g, float b, float a)
    {
        Punteros[punteroActivo].DesactivateColor(r, g, b); // Desactiva el color del puntero actual
        if (check[punteroActivo] == false )
        {
            Icon.Desactivate(punteroActivo); //se pone un poco transparente el icono pequeño
        }
        else
        {
            Icon.Transparent(punteroActivo);
        }
        //--------------------------------------------------------------------------------------------------------
        punteroActivo = (punteroActivo + 1) % Punteros.Length; // Avanza al siguiente puntero

        while (check[punteroActivo] == true)
        {
            punteroActivo++;
            if (punteroActivo > 2)
            {
                punteroActivo = 0;
            }
        }

        Punteros[punteroActivo].ActivateColor(r, g, b, a); // Activa el color del nuevo puntero activo
        //---------------------------------------------------------------------------------------------------------
  
        Icon.Activate(punteroActivo);
        //-------------------------------------------------------------------------------------------
    }

    public int ReceiveActive()
    {
        return punteroActivo;
    }

    public void Modify(int a)
    {
        punteroActivo = a;
        ActivateIco(punteroActivo);
    }


    public void DesactivateIco(int a)
    {
        Icon.Desactivate(a);
    }
    public void ActivateIco(int a)
    {
        Icon.Activate(a);
    }

    public void TransparentIco(int a)
    {
        Icon.Transparent(a);
    }

    public void ChangeChecker(bool a, int b)
    {
        check[b] = a;
    }

    public void Reajuste()
    {
        for(int i = 0; i < 3; i++)
        {
            if (dead[i] == false)
            {
                check[i] = false;
            }
            else
            {
                check[i]= true;
            }
        }
    }

    public void Dead()
    {
        for (int i = 0;i < 3; i++)
        {
            if (Modificador.Michiguerreros[i].GetComponent<Life>().life <= 0)
            {
                
                dead[i] = true;
                
                punteroActivo = i; // Avanza al siguiente puntero
                

                punteroActivo++;
                    if (punteroActivo > 2)
                    {
                        punteroActivo = 0;
                    }

            }
            else
            {
                dead[i] = false;
            }
        }
    }

}
