using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Victoria;
    public GameObject dice;
    public Image vida;
    // Update is called once per frame
    void Update()
    {
        if(vida.fillAmount == 0) 
        { 

            StartCoroutine(victory());
        }
    }

    IEnumerator victory()
    {
        yield return new WaitForSeconds(1.5f);
        dice.SetActive(false);

        Victoria.SetActive(true);

    }
}
