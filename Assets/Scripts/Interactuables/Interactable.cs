
public interface Interactable
{
    // M�todo que debe implementar cualquier clase que implemente esta interfaz
    void Interact();

    // Propiedad para determinar si el objeto puede ser interactuado
    bool CouldInteract { get; }
}
