using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float Damage;
    public float Multiplicador;
    public GameObject Mago, Arquero, Guerrero;
    public void Update()
    {
        Damage = (Mago.GetComponent<Guerrero>().Damage + Arquero.GetComponent<Guerrero>().Damage + Guerrero.GetComponent<Guerrero>().Damage)*3;
            //guerrero.Damage * Multiplicador;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossLife>().Damage(Damage);
        }


    }
}
