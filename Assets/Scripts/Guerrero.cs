using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Guerrero : MonoBehaviour
{

    [Header("Tipo de guerrero")]
    public string Nombre;

    [Header("Vida")]
    public float life;

    [Header ("Defensa")]
    public float Defensa;

    [Header("Daño")]
    public float Damage;

    [Header("Speed")]
    public float Speed;

    //public bool HabState = false;
    [Header("Enchufe para modificar Player")]
    public GameObject Player;

    [Header("Establece Posiciones")]
    public Transform inicial;
    public Transform actual;
    public Transform destino;
    //-------------------------------------------------------------------------------------------------------
    [Header("Cuadro de Habilidades")]
    public GameObject Cuadro;

    //puro polimorfismo
    public virtual void ShowChart() {
        
    }

    public virtual void CloseChart()
    {
        
    }
    public bool PausePrincipal()
    {
        return true;
    }

    //Se activa un ataque codigo polimorfismo
    public virtual void Habilidad01()
    {
    }
    public virtual void Habilidad02()
    {
        
    }

    //Un ataque en el cuál se activa, en el que sin importar quien, todos atacan
    public void Grupal()
    {
        Debug.Log("Ataque grupal Realizado, finaliza tu turno");
    }


}
