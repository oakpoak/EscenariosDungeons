using UnityEngine;

public class EnemyRemover : MonoBehaviour
{
    private void Start()
    {
        EnemyMapID[] enemigos = FindObjectsOfType<EnemyMapID>();

        foreach (var enemigo in enemigos)
        {
            if (WorldState.enemigosDerrotados.Contains(enemigo.enemyID))
            {
                Destroy(enemigo.gameObject);
            }
        }
    }
}
