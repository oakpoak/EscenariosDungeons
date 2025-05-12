using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncenderDos : ItemInteractive
{
    public GameObject Antorcha;
    public PuzzleDOS Puzzle;
    public GameObject Principal;

    // Start is called before the first frame update
    void Start()
    {
        Antorcha.SetActive(false); 
        Principal = this.gameObject;
    }

    public override void Interact()
    {
        Antorcha.SetActive(true);
        Puzzle.AddTorch();
        
    }
}
