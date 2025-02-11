using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Guerrero
{
    [Header("Atributos Necesarios para Atacar")]
    public Transform Standby;
    public float velocidadDesplazamiento;
    public float velocidadStand;
    public GameObject [] aliados = new GameObject[2];
    float heal=50;
    //Muestra solo las habilidades
    public override void CloseChart()
    {
        Cuadro.SetActive(false);
    }

    public override void ShowChart()
    {
       Cuadro.SetActive(true);
    }


    //Ataca el jugador con sus habilidades especiales afectando al jefe
    public override void Habilidad01()
    {
        GetComponent<Animator>().SetBool("Ataque hechizo", true);
        StartCoroutine(Lanzar());
    }

    public override void Habilidad02()
    {
        StartCoroutine(Sanar());
    }

    IEnumerator Sanar()

    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Life>().Heal(heal);
        for (int i = 0; i < aliados.Length; i++)
        {
            if (aliados[i].GetComponent<Life>().life > 0)
            {
                aliados[i].GetComponent<Life>().Heal(heal);
            }
        }
        yield return new WaitForSeconds(1.0f);

        Player.GetComponent<Player>().Continue();
        CloseChart();
    }
    public IEnumerator Lanzar()
    {
        
        yield return new WaitForSeconds(2.0f);
        GetComponent<Animator>().SetBool("Ataque hechizo", false);
        yield return new WaitForSeconds(1.30f);
        actual.gameObject.SetActive(true);

        while (Vector2.Distance(actual.position, Standby.position) > 0.0001f)
        {
            actual.position = Vector2.MoveTowards(actual.position, Standby.position, velocidadStand * Time.deltaTime);
            yield return null;
        }
        actual.position = Standby.position;

        /* while (Vector2.Distance(actual.position, Standby.position) > 0.01f)
         {
             // Calcular la dirección hacia el objetivo
             Vector2 direccion = (Standby.position - actual.position).normalized;
             actual.Translate(direccion * velocidadStand * Time.deltaTime);

             // Esperar hasta el próximo fotograma
             yield return null;
         }

         actual.position=Standby.position;
         */

        yield return new WaitForSeconds(2.0f);

       /*while ((Vector2.Distance(actual.position, destino.position) > 0.01f))
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direccion = (destino.position - actual.position).normalized;
            actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);

            // Esperar hasta el próximo fotograma
            yield return null;
        }*/
        while (Vector2.Distance(actual.position, destino.position) > 0.0001f)
        {
            actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
            yield return null;
        }
        actual.position = Standby.position;

        actual.position = inicial.position;
        actual.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);

        CloseChart();
        Player.GetComponent<Player>().Continue();

    }

}
