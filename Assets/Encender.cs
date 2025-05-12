using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encender : ItemInteractive
{

    public GameObject Antorcha;
    public Light luz;
    public TercerCuarto Puzzle;
    public GameObject Principal;
    
    // Start is called before the first frame update
    void Start()
    {
        Antorcha.SetActive(false);
        luz.enabled = false;
        Principal = this.gameObject;
    }

    public override void Interact()
    {
        Antorcha.SetActive(true);
        luz.enabled = true;

        Puzzle.OnPuzzleObjectInteracted(Principal);
    }
}
