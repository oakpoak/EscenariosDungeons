
public interface Interactable
{
    // Método que debe implementar cualquier clase que implemente esta interfaz
    void Interact();

    // Propiedad para determinar si el objeto puede ser interactuado
    bool CouldInteract { get; }
}
