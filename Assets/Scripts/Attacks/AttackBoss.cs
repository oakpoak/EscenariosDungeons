using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackBoss : MonoBehaviour
{
    public float Damage;
    public float Multiplicador;
    public Guerrero guerrero;//Hereda atrubuto daño nada más
    public void Update()
    {
        Damage = guerrero.Damage * Multiplicador;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.GetComponent<Life>().Damage(Damage);
        
        collision.GetComponent<Life>().Damage(Damage);
    }
    
}
