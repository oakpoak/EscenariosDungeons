using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Warrior : Guerrero
{
    //public Habilidades hab;
    [Header("Elementos necesarios para atacar")]

    public float velocidadDesplazamiento;
    public GameObject Shield, Sword;


    //Muestra solo las habilidades
    public override void CloseChart()
    {
        Cuadro.SetActive(false);
    }

    public override void ShowChart()
    {
        Cuadro.SetActive(true);
    }

    



    public override void Habilidad01()
    {

        StartCoroutine(Destino());

    }

    public IEnumerator Destino()
    {
        yield return new WaitForSeconds(1.0f);
        while (Vector2.Distance(actual.position, destino.position) > 0.15f)
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direccion = (destino.position - actual.position).normalized;
            actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);

            // Esperar hasta el próximo fotograma
            yield return null;
        }
        actual.position = destino.position;
        
        StartCoroutine(Ataque1());
    }
    public IEnumerator Ataque1()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Animator>().SetBool("Ataque", true);
        GetComponent<Animator>().Play("Attack");

        yield return new WaitForSeconds(.45f);

        Sword.SetActive(true);
        GetComponent<Animator>().SetBool("Ataque", false);
        yield return new WaitForSeconds(1.0f);
        Sword.SetActive(false);
        
        StartCoroutine(Regresar());
    }
    public IEnumerator Regresar()
    {

        while (Vector2.Distance(actual.position, inicial.position) > 0.15f)
        {
            // Calcular la dirección hacia la posición inicial
            Vector2 direccion = (inicial.position - actual.position).normalized;
            // Moverse hacia la posición inicial a una velocidad constante
            actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);
            // Esperar hasta el próximo fotograma
            yield return null;
        }
        actual.position = inicial.position;
        yield return new WaitForSeconds(1.0f);

        Player.GetComponent<Player>().Continue();
        CloseChart();
    }




    public override void Habilidad02()
    {
        if(Shield.activeSelf) 
        {

            Player.GetComponent<Player>().Continue();
            CloseChart();
        }
        else
        {
            Shield.GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            StartCoroutine(Escudo());
        }
    }

    IEnumerator Escudo()
    {

        yield return new WaitForSeconds(1.0f);
        Shield.SetActive(true);
        Shield.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        while (Shield.GetComponent<Shield>().Check() == false)
        {
            yield return null;
        }

        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(2.0f);

        Player.GetComponent<Player>().Continue();
        CloseChart();
    }
}
