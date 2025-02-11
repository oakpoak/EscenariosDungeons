using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Life : MonoBehaviour
{
    // Start is called before the first frame update

    public float life, maxlife;
    public GameObject Personaje;

    public Image lifeBar;
    public PointerSelection Select;
    public Ico iconito;

    public void Start()
    {
        life = Personaje.GetComponentInChildren<Guerrero>().life;
        maxlife = life;
    }

    public void Damage(float a)
    {
        life = life - a;
        if (a > 0) {
            StartCoroutine(_Damage());
        }
        
    }

    public void Heal(float a)
    {

        
        if ((life+a) > maxlife)
        {
            life = maxlife;
        }
        else
        {
            life = life + a;
        }
        lifeBar.fillAmount = life / maxlife;
    }
    IEnumerator _Damage()
    {
        for (int i = 0; i<4; i++)
        {
            
            GetComponent<SpriteRenderer>().color = Color.red; 
            yield return new WaitForSeconds(.12f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.12f);
        }
        if (life > 0)
        {

            lifeBar.fillAmount = life / maxlife;
        }
        else
        {
            life = 0;
            GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 255 / 255f, 0 / 255f, 150 / 255f);
            lifeBar.fillAmount = life / maxlife;
            Select.Dead();


        }
        yield return new WaitForSeconds(.53f);
    }
}

