using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TeamLife : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Mago, Arquero, Guerrero;
    public float life;
    private float maxlife;
    public Image lifeBar;
    public Dado dice;
    public GameObject Derrota;
    private void Start()
    {
       // life = Mago.GetComponent<Life>().life + Arquero.GetComponent<Life>().life + Guerrero.GetComponent<Life>().life;
        maxlife = life;
    }
    private void Update ()
    {
        life = Mago.GetComponent<Life>().life + Arquero.GetComponent<Life>().life + Guerrero.GetComponent<Life>().life;

        if(life > maxlife)
        {
            life = maxlife;
        }

        if (life > 0)
        {            
            lifeBar.fillAmount = life / maxlife;
        }

        if (life <= 0)
        {

            //Inicias la corutina
            dice.ch = true;

    
        }
    }
}
