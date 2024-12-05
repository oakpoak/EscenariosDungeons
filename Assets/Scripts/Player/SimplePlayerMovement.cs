using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad normal
    public float velocidadCorrer = 10f; // Velocidad al correr
    public float rotacionVelocidad = 700f; // Velocidad de rotaci�n del jugador
    public float saltoFuerza = 7f; // Fuerza de salto
    [SerializeField] Transform cam;

    public Transform sueloDetectado; // Referencia al objeto vac�o para detectar el suelo
    public float distanciaSuelo = 0.5f; // Distancia a la que se considera que el jugador est� tocando el suelo

    private Rigidbody rb;
    private bool estaSaltando = false;

    void Start()
    {
        // Obtener el Rigidbody adjunto al objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detectar si el jugador est� corriendo
        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidad;

        // Movimiento en el espacio del mundo (con fuerzas)
        float movimientoX = Input.GetAxis("Horizontal") * velocidadActual;
        float movimientoZ = Input.GetAxis("Vertical") * velocidadActual;


        //inputs
        Vector3 camforward = cam.forward;
        Vector3 camright = cam.right;
        //camera dir
        camforward.y = 0;
        camright.y = 0;
        camforward.Normalize();
        camright.Normalize();
        //realtive cam direction
        Vector3 forwardrelative = movimientoZ * camforward;
        Vector3 rightrelative = movimientoX * camright;

        Vector3 movedir = forwardrelative + rightrelative;



        // Vector de movimiento
        Vector3 movimiento = new Vector3(movedir.x, 0, movedir.z);

        // Mover al jugador
        rb.MovePosition(transform.position + movimiento * Time.deltaTime);

        // Si estamos movi�ndonos (en cualquier direcci�n)
        if (movimiento.magnitude > 0.1f)
        {
            // Calcular la direcci�n hacia la que nos estamos moviendo (sin altura, solo en el plano horizontal)
            Quaternion rotacionObjetivo = Quaternion.LookRotation(movimiento.normalized);

            // Rotar suavemente hacia la direcci�n de movimiento
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, rotacionVelocidad * Time.deltaTime);
        }

        // Detectar si el jugador est� tocando el suelo y si la tecla de salto es presionada
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnElSuelo() && !estaSaltando)
        {
            Salto();
        }
    }

    // M�todo para detectar si el jugador est� tocando el suelo usando el objeto vac�o
    bool EstaEnElSuelo()
    {
        // Verificar si la posici�n Y del objeto vac�o est� cerca de la altura esperada (es decir, que el jugador est� tocando el suelo)
        return sueloDetectado.position.y <= transform.position.y + distanciaSuelo;
    }

    // M�todo para hacer saltar al jugador
    void Salto()
    {
        estaSaltando = true;
        rb.AddForce(Vector3.up * saltoFuerza, ForceMode.Impulse);
        StartCoroutine(EsperaSalto());
    }

    // Coroutine para esperar el regreso al suelo
    IEnumerator EsperaSalto()
    {
        yield return new WaitForSeconds(0.1f); // Esperar un peque�o intervalo para evitar m�ltiples saltos
        estaSaltando = false;
    }
}
