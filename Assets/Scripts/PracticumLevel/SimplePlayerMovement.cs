using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad normal
    public float velocidadCorrer = 10f; // Velocidad al correr
    public float rotacionVelocidad = 700f; // Velocidad de rotación del jugador
    public float saltoFuerza = 7f; // Fuerza de salto
    [SerializeField] Transform cam;

    public Transform sueloDetectado; // Referencia al objeto vacío para detectar el suelo
    public float distanciaSuelo = 0.5f; // Distancia a la que se considera que el jugador está tocando el suelo

    private Rigidbody rb;
    private bool estaSaltando = false;
    public Animator anim;
    public float JumpTime;
    public GameManager Control;
    void Start()
    {
        // Obtener el Rigidbody adjunto al objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Control.Fight == false && Control.puzzle == false)
        {
            // Verifica si alguna de las teclas de movimiento (W, A, S, D) está presionada
            bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

            // Verifica si se está presionando Shift junto con las teclas de movimiento
            bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);

            // Si Shift está presionado, activa el parámetro "Run" y desactiva "Walk"
            if (isRunning)
            {
                anim.Play("Running");
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true); // Asegura que el estado de "Idle" no esté activado
            }
            else if (isWalking)
            {
                anim.Play("walking");
                // Si no se está corriendo, activa "Walk" y desactiva "Run"
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false); // Asegura que el estado de "Idle" no esté activado
            }
            else
            {
                anim.Play("Idle");
                // Si no se está presionando ninguna tecla de movimiento, activa "Idle"
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
            }
            //---------------------------------------------------------------------------------------------------------
            // Detectar si el jugador está corriendo
            float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidad;

            // Movimiento en el espacio del mundo (con fuerzas)
            float movimientoX = Input.GetAxis("Horizontal") * velocidadActual;
            float movimientoZ = Input.GetAxis("Vertical") * velocidadActual;

            //camera dir
            Vector3 camforward = cam.forward;
            Vector3 camright = cam.right;

            camforward.y = 0;
            camright.y = 0;
            camforward.Normalize();
            camright.Normalize();

            //relativecam
            Vector3 forwardrelative = movimientoZ * camforward;
            Vector3 rightrelative = movimientoX * camright;

            Vector3 movedir = forwardrelative + rightrelative;


            Vector3 movimiento = new Vector3(movedir.x, 0, movedir.z);

            // Mover al jugador
            rb.MovePosition(transform.position + movimiento * Time.deltaTime);

            // Si estamos moviéndonos (en cualquier dirección)
            if (movimiento.magnitude > 0.1f)
            {
                // Calcular la dirección hacia la que nos estamos moviendo (sin altura, solo en el plano horizontal)
                Quaternion rotacionObjetivo = Quaternion.LookRotation(movimiento.normalized);

                // Rotar suavemente hacia la dirección de movimiento
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, rotacionVelocidad * Time.deltaTime);
            }

            // Detectar si el jugador está tocando el suelo y si la tecla de salto es presionada
            if (Input.GetKeyDown(KeyCode.Space) && EstaEnElSuelo() && !estaSaltando)
            {
                Salto();
            }
        }
        
    }

    // Método para detectar si el jugador está tocando el suelo usando el objeto vacío
    bool EstaEnElSuelo()
    {
        // Verificar si la posición Y del objeto vacío está cerca de la altura esperada (es decir, que el jugador esté tocando el suelo)
        return sueloDetectado.position.y <= transform.position.y + distanciaSuelo;
    }

    // Método para hacer saltar al jugador
    void Salto()
    {
        estaSaltando = true;
        rb.AddForce(Vector3.up * saltoFuerza, ForceMode.Impulse);
        StartCoroutine(EsperaSalto());
    }

    // Coroutine para esperar el regreso al suelo
    IEnumerator EsperaSalto()
    {
        yield return new WaitForSeconds(JumpTime); // Esperar un pequeño intervalo para evitar múltiples saltos
        estaSaltando = false;
    }
}
