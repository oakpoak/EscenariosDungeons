using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arquero : Guerrero
{
    [Header("Atributos Necesarios para Atacar")]
    public Transform Fnormal;
    public Transform Fbomba;
    public float velocidadDesplazamiento;
    public float Bomba;


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
        actual = Fnormal; 
        StartCoroutine(Lanzar());
    }

    public override void Habilidad02()
    {
        actual = Fbomba;
        StartCoroutine(Lanzar());
    }

    IEnumerator Lanzar()
    {
        GetComponent<Animator>().SetBool("Ataque", true);
        GetComponent<Animator>().Play("Attack");
        GetComponent<Animator>().SetBool("Ataque", false);
        yield return new WaitForSeconds(1.133333333f);

        actual.gameObject.SetActive(true);
        
        while (Vector2.Distance(actual.position, destino.position) > 0.0001f)
        {
            actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
            yield return null;
        }

        actual.position = destino.position;
        StartCoroutine(next());
        
    }
    IEnumerator next()
    {
        if (actual.CompareTag("Bomb"))
        {
            actual.GetComponent<Animator>().SetBool("Explotion", true);
            actual.GetComponent<Animator>().Play("explosion");
            yield return new WaitForSeconds(2f);
        }

        actual.position = inicial.position;
        actual.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        Player.GetComponent<Player>().Continue();
        CloseChart();
    }
}
