using UnityEngine;

public class TempScriptPointer : MonoBehaviour
{
    private Camera Cam;
    public float large;
    public Color Pointer;
    public LayerMask Player;
    //public InterfazTeclas Tecla;
    public bool Clicked = false;

    private Vector3 mouse;
    private Ray ray;
    private RaycastHit hit;
    //private Pawn pawn;
    private void Start()
    {
        Cam = GetComponent<Camera>();
        Clicked = false;
    }

    private void Update()

    {

        // Obtener la posici�n del mouse
        mouse = Input.mousePosition;
        //Dibuja un rayo para verlo en unity.
        ray = Cam.ScreenPointToRay(mouse);
        Debug.DrawRay(ray.origin, ray.direction * large, Color.red);

        /*if (Input.GetKeyDown(Tecla.Selection))
        {
            Clicked = true;
            CheckSelection();

        }
        if (!Input.GetKey(Tecla.Selection) && !Clicked && !Input.GetKey(Tecla.DragNav))
        {
            CheckSelection();
        }
        if (Input.GetKeyUp(Tecla.Selection))
        {
            Clicked = false;
        }
        
        if (Input.GetKeyDown(Tecla.Target))
        {
            Vector2 ScreenPosition = Input.mousePosition;
            Vector3 mouseWorld = Cam.ScreenToWorldPoint(ScreenPosition);
        }
        Modificar m�s adelante
         */

    }

    //Revisa el elemento que est� apuntando el jugador
    private void CheckSelection()
    {
        /*// Verificar si el rayo impacta contra un objeto en el LayerMask
        if (Physics.Raycast(ray, out hit, large, Player))
        {
            if (Clicked == false) //Si Solo Se se�ala, obtiene el objeto renerer para cambiar de color,
                                  //ya luego se puede modificar para que muestre un �cono o un aro alrededor
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                pawn = hit.collider.GetComponent<Pawn>();

                if (pawn.Selected == false)
                {
                    renderer.material.color = Pointer;
                }
            }
            else if (Clicked == true) //S� le dimos clic al objeto, modificaremos su propiedad
                                      //que posee el c�digo de Pawn para que cambie de color autom�ticamente.
            {
                pawn = hit.collider.GetComponent<Pawn>();

                pawn.Selected = Clicked;
            }
        }
        MODIFICAR M�S TARDE PARA LOS ELEMENTOS
         
         */
    }
}
