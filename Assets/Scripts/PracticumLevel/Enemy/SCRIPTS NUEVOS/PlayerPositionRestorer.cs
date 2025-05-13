using UnityEngine;

public class PlayerPositionRestorer : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && WorldState.posicionJugador != Vector3.zero)
        {
            player.transform.position = WorldState.posicionJugador;
        }
    }
}
