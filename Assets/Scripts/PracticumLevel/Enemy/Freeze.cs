using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public void FreezeScreen()
    {
        Time.timeScale = 0f;
    }
    public void UnFreezeScreen()
    {
        Time.timeScale = 1f;
    }
}
