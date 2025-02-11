using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{
    [Header("Dados fisicos")]
    public GameObject[] D_Normal = new GameObject[20];
    public GameObject[] D_Grande = new GameObject[20];
    [Header("Ataques de todos los personajes")]
    public Attack [] allies = new Attack[4];
    public AttackBoss enemy;
    [Header("Controlador de videos")]
    private GameObject actual;//Sirve para desactivar el dado depués de la ronda
    [Header("Mecánica principal")]
    public Player Jugador;//Permite continuar con la mecánica del juego y aquí se activa la habilidad especial
    public Boss Jefe;//aquí se activa la habilidad especial
    public PointerSelection reajuste;//permite continuar con la mecánica del juego
    public GameObject derrota;

    public int multiplier, enemymultiplier;//multiplicador de acuerdo al dado

    [Header("Indicador del multiplicador")]
    public Iconos MultipAliados, MultipBoss;

    private int array = -1;
    private int arrayenemy= -1;
    public bool ch=false;
    //Contador que permite elegir un número del 0 al 19
    int number;
    // Start is called before the first frame update
    void Start()
    {
        MovDado();
    }

    // Update is called once per frame
    public void MovDado()
    { if (ch == false)
        {
            if (arrayenemy != -1)
            {
                MultipBoss.Transparent(arrayenemy);
            }
            if (array != -1)
            {
                MultipAliados.Transparent(array);
            }

            if (actual != null)
            {
                actual.SetActive(false);
            }
            number = Random.Range(0, 20);
            D_Normal[number].SetActive(true);
            D_Grande[number].SetActive(true);
            StartCoroutine(Video());
        }
        else
        {
            //perdiste
            derrota.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator Video()
    {

        actual = D_Normal[number];
        yield return new WaitForSeconds(3f);
        D_Grande[number].SetActive(false);
        //dependiendo del numero es lo que valdrá el multiplicador
        if (number == 0) 
        {

            multiplier = 0;

            Jefe.Special = true;
            enemymultiplier = 1;
            
        }
        if (number > 0 && number < 7) 
        {

            multiplier = 1;
            enemymultiplier = 2;

        }
        if(number >= 7 && number <13) 
        {

            multiplier = 2;
            enemymultiplier = 3;
        }
        if (number >= 13 && number < 19)
        {
            multiplier = 3;
            enemymultiplier = 2;
        }
        if (number == 19)
        {

            Jugador.ActEsp = true;
            Jugador.Especial.ActivateColor();
            multiplier = 1;
            enemymultiplier = 0;
        }


        //----------------------------------------------------------------
        if (multiplier != 0)
        {
            array = multiplier - 1;
            MultipAliados.Activate(array);
        }
        if (enemymultiplier != 0)
        {
            arrayenemy = enemymultiplier - 1;
            MultipBoss.Activate(arrayenemy);
        }
        //---------------------------------------------------------------


        enemy.Multiplicador = enemymultiplier;
        allies[0].Multiplicador = multiplier;
        allies[1].Multiplicador = 2*multiplier;//en este caso es por el daño de la flecha bomba, hace el doble de daño que el de la flecha normal
        allies[2].Multiplicador = multiplier;
        allies[3].Multiplicador = multiplier;


        
        reajuste.Reajuste();
        Jugador.NewRound();

    }
}
