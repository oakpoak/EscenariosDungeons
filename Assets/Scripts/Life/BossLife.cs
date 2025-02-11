using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour
{
    public float life, maxlife;
    public GameObject BarraVida;
    public Image lifeBar;

    private void Start()
    {
        life = maxlife;
    }


    public void Damage(float a)
    {
        life = life - a;
        if (a > 0)
        {
            StartCoroutine(_Damage());
        }
         
    }

    IEnumerator _Damage()
    {
        for (int i = 0; i < 4; i++)
        {

            GetComponent<SpriteRenderer>().color = Color.green;
            yield return new WaitForSeconds(.12f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.12f);
        }
        if (life > 0)
        {
            //Inicias la corutina
            lifeBar.fillAmount = life / maxlife;
        }
        else
        {
            lifeBar.fillAmount = 0 / maxlife;
            StartCoroutine(Muere());
        }
        yield return new WaitForSeconds(.53f);
    }

    IEnumerator Muere() { 
    
        
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);



    }    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
