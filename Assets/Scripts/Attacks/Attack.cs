using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
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
        if (collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossLife>().Damage(Damage);
        }
        

    }
}
