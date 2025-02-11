using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Boss : Guerrero
{
    public float velocidadDesplazamiento;
    public Player Jugador;
    public PointerSelection reajuste;
    public Dado Dice;
    public bool Special = false;
    public Transform shieldinicial;
    public GameObject Shield;

    public int target;
    public void TurnoJefe()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        if (Special==false) {
            
            target = Random.Range(0, 3);

            

            if (Jugador.Michiguerreros[target].GetComponent<Life>().life <= 0)
            {
                while (Jugador.Michiguerreros[target].GetComponent<Life>().life <= 0)
                {
                    target++;
                    if(target > 2)
                    {
                        target = 0;
                    }
                }


            }
            //------------------------------------------------------------------------------------------
            //Activa el boxcollider del destinatario en dado caso que no sea el caballero
            if (target != 2)
            {
                Jugador.Michiguerreros[2].GetComponent<BoxCollider2D>().enabled = false;

                Jugador.Michiguerreros[target].GetComponent<BoxCollider2D>().enabled = true;

            }
            //-----------------------------------------------------------------------------------------
            ShowChart();
            destino = Jugador.Michiguerreros[target].transform;
            Habilidad01();
        }
        else
        {
            Jugador.Michiguerreros[2].GetComponent<BoxCollider2D>().enabled = false;

            ShowChart();
            
            Habilidad02();
        }
            
    }
    public override void Habilidad01()
    {
        StartCoroutine(Lanzar());
    }
    public override void CloseChart()
    {
        Cuadro.SetActive(false);
    }

    public override void ShowChart()
    {
        Cuadro.SetActive(true);
    }

    public override void Habilidad02()
    {
        
        StartCoroutine(Continuo());
    }
    IEnumerator Lanzar()
    {  
        yield return new WaitForSeconds(2.0f);
        actual.gameObject.SetActive(true);

        if (Shield.activeSelf==true && target ==2 )
        {
            Jugador.Michiguerreros[2].GetComponent<BoxCollider2D>().enabled = false;
        }

        /*while (Vector2.Distance(actual.position, destino.position) > 0.1f)
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direccion = (destino.position - actual.position).normalized;
            // Moverse hacia el objetivo a una velocidad constante
            actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);
            // Esperar hasta el próximo fotograma
            yield return null;
        }*/
        while (Vector2.Distance(actual.position, destino.position) > 0.0001f)
        {
            actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
            yield return null;
        }
        actual.position = inicial.position;
        actual.gameObject.SetActive(false);

        //------------------------------------------------------------------------------------------
        GetComponent<BoxCollider2D>().enabled = true;
        if (target != 2)
        {
            Jugador.Michiguerreros[2].GetComponent<BoxCollider2D>().enabled = true;
            Jugador.Michiguerreros[target].GetComponent<BoxCollider2D>().enabled = false;
            
        }
        if(Shield.activeSelf==true && target == 2)
        {
            yield return new WaitForSeconds(1.5f);
            Shield.gameObject.transform.position = shieldinicial.position;
            Shield.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Shield.SetActive(false);
            Shield.GetComponent<Shield>().shield = false;
            Jugador.Michiguerreros[target].GetComponent<BoxCollider2D>().enabled = true;
        }
        //-------------------------------------------------------------------------------

        yield return new WaitForSeconds(2.0f);

        CloseChart();
        
        
        Dice.MovDado();
    }
    IEnumerator Continuo()
    {
        if (Jugador.Michiguerreros[0].GetComponent<Life>().life > 0)
        {
            Jugador.Michiguerreros[0].GetComponent<BoxCollider2D>().enabled = true;
            destino = Jugador.Michiguerreros[0].transform;
            yield return new WaitForSeconds(2.0f);
            actual.gameObject.SetActive(true);
            /*while (Vector2.Distance(actual.position, destino.position) > 0.1f)
            {
                // Calcular la dirección hacia el objetivo
                Vector2 direccion = (destino.position - actual.position).normalized;
                // Moverse hacia el objetivo a una velocidad constante
                actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);
                // Esperar hasta el próximo fotograma
                yield return null;
            }*/
            while (Vector2.Distance(actual.position, destino.position) > 0.0001f)
            {
                actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
                yield return null;
            }
            actual.position = inicial.position;
            actual.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
        

        //-------------------------------------------------------------------------------------------------------------
        Jugador.Michiguerreros[0].GetComponent<BoxCollider2D>().enabled = false;
        Jugador.Michiguerreros[1].GetComponent<BoxCollider2D>().enabled = true;

        if (Jugador.Michiguerreros[1].GetComponent<Life>().life > 0)
        {
            destino = Jugador.Michiguerreros[1].transform;

            actual.gameObject.SetActive(true);

            /*while (Vector2.Distance(actual.position, destino.position) > 0.1f)
            {
                // Calcular la dirección hacia el objetivo
                Vector2 direccion = (destino.position - actual.position).normalized;
                // Moverse hacia el objetivo a una velocidad constante
                actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);
                // Esperar hasta el próximo fotograma
                yield return null;
            }*/
            while (Vector2.Distance(actual.position, destino.position) > 0.0001f)
            {
                actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
                yield return null;
            }
            actual.position = inicial.position;
            actual.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }
        

        //-------------------------------------------------------------------------------------------------------------
        Jugador.Michiguerreros[1].GetComponent<BoxCollider2D>().enabled = false;
        Jugador.Michiguerreros[2].GetComponent<BoxCollider2D>().enabled = true;
        if(Shield.activeSelf == true )
        {
            Jugador.Michiguerreros[2].GetComponent<BoxCollider2D>().enabled = false;
        }
        

        if (Jugador.Michiguerreros[2].GetComponent<Life>().life > 0)
        {
            destino = Jugador.Michiguerreros[2].transform;

            actual.gameObject.SetActive(true);
            /*while (Vector2.Distance(actual.position, destino.position) > 0.1f)
            {
                // Calcular la dirección hacia el objetivo
                Vector2 direccion = (destino.position - actual.position).normalized;
                // Moverse hacia el objetivo a una velocidad constante
                actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);
                // Esperar hasta el próximo fotograma
                yield return null;
            }*/
            while (Vector2.Distance(actual.position, destino.position) > 0.0001f)
            {
                actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
                yield return null;
            }
            actual.position = inicial.position;
            actual.gameObject.SetActive(false);
        }
        if (Shield.activeSelf == true)
        {
            yield return new WaitForSeconds(1f);
            Shield.gameObject.transform.position = shieldinicial.position;
            Shield.SetActive(false);
            Shield.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
            Shield.GetComponent<Shield>().shield = false;
            Jugador.Michiguerreros[target].GetComponent<BoxCollider2D>().enabled = true;
        }

        //------------------------------------------------------------------------------------------
        StartCoroutine(Terminar());
    }
    IEnumerator Terminar()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        //-------------------------------------------------------------------------------
        CloseChart();
        yield return new WaitForSeconds(2.0f);
        Special = false;
        Dice.MovDado();
    }
}
