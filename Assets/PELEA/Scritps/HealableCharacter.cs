using UnityEngine;

public class HealableCharacter : MonoBehaviour
{
    void OnMouseDown()
    {
        InventoryUI inventory = FindObjectOfType<InventoryUI>();
        if (inventory != null)
        {
            inventory.TryUseOnCharacter(gameObject);
        }
    }
}
