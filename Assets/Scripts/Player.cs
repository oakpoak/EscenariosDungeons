
using System.Collections;

using UnityEngine;

public class Player : MonoBehaviour
{
    //Es solo para diferenciar el subt�tulo, y no aparezca en los dem�s en la parte de inspector
    [Header("Ajuste de color")]
    public float R; 
    public float G, B, A;

    [Header("Selección")]
    public PointerSelection Flechas;
    [Header("Personajes")]
    public GameObject [] Michiguerreros = new GameObject[3];

    [Header("Ataque especial")]
    public Transform inicial;
    public Transform actual;
    public Transform intermedio;
    public Transform destino;
    public float velocidadDesplazamiento;
    public float velocidadTemp;

    [Header("Vida")]
    public GameObject Vida_Michiguerreros;

    [Header("Jefe")]
    public Boss Jefe;

    [Header("Ataque Especial")]
    public Ico Especial; 

    private bool ActSelect = false; //Switch para poder controlar si activamos la selección
    private bool ActHab = false; //nos permite elegir las habilidades del personaje
    public bool ActEsp=false; //Detecta si ya se usó la habilidad especial
    private int counterR=1; //contdor que sirve para determinar que elementos no debe de repetir el puntero y nos permite activar y desactivar los elementos importantes
    private int CounterArray = 2; //controla el puntero del arreglo

    void Start()
    {
        /*Flechas.Punteros[1].DesactivateColor(R, G, B);
        Flechas.Punteros[0].DesactivateColor(R,G,B);
        Flechas.Punteros[2].ActivateColor(R, G, B, A);

        Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().ShowChart();*/
    }

    public void NewRound()
    {
        ActSelect = true;
        ActHab = false;
        counterR = 1;
        for(int i = 0; i < 3; i++)
        {
            if (Michiguerreros[i].GetComponent<Life>().life <= 0)
            {
                Flechas.TransparentIco(i);
                counterR ++;
                Flechas.Punteros[i].DesactivateColor(255, 255, 255);
            }
        }


        CounterArray = Flechas.ReceiveActive();

        Flechas.Punteros[CounterArray].ActivateColor(R, G, B, A);
        Flechas.ActivateIco(CounterArray);
        for (int i = 0; i <= 2; i++)
        {
            if (i != CounterArray)
            {
                Flechas.DesactivateIco(i);

            }
        }
        Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().ShowChart();
    }

    //Actualmente es la principal, pero luego se modificará para que se use entre elegir un guerrero y oprimir los botones para usar su habilidad, el void update se pasará al elemento turn
    void Update()
    {
        if(Jefe != null)
        {
            if (counterR > 1 && ActEsp==true)
            {
                Especial.DesactivateColor();
            }
            if(counterR == 1 && ActEsp == true)
            {
                Especial.ActivateColor();
            }
            if (ActSelect == true)
            {
                SelectWarrior();
            }
            if (ActHab == true)
            {
                SelectHab();
            }
            if (Input.GetKeyUp(KeyCode.Q) && (ActHab == true || ActSelect == true)) //Ataque Especial
            {

                if (counterR == 1 && ActEsp == true)
                { //SOLO SE PODRÁ EJECUTAR SI ESTÁS SELECCIONANDO EL PRIMER PERSONAJE
                    actual.gameObject.SetActive(true);
                    ActSelect = false;
                    ActHab = false;
                    Vida_Michiguerreros.SetActive(false);
                    counterR = 4;//LE SUMAMOS 3 DE UNA VEZ, ES DECIR: valor nuedo de counterR = valor antiguo del counterR + 3
                    

                    StartCoroutine(Lanzar());

                }
                else
                {
                    Especial.DesactivateColor();
                }
                //Solo ejecutar hasta la formación y el ataque especial se define, Después, automáticamente finaliza el turno;
            }
        }
        else
        {
            CounterArray = Flechas.ReceiveActive();

            Flechas.Punteros[CounterArray].ActivateColor(R, G, B, A);
            for (int i = 0; i <= 2; i++)
            {
                Flechas.TransparentIco(i);
                
            }
            Flechas.Punteros[CounterArray].DesactivateColor(R, G, B);
            Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().CloseChart();
        }
        
    }
    
    /*OPRIMES UNA TECLA: 1, 2, 3, O Q, PARA USAR UNA HABILIDAD DEL PERSONAJE EN SELECCIÓN
      BACKSPACE PERMITE REGRESAR A SELECTWARRIOR EN DADO CASO QUE NO QUIERAS ELEGIR ESTE PERSONAJE*/
    void SelectHab()
    {

        /*CAMBIAN SUS VALORES LOS BOOLEANOS PARA RETORNAR AUTOMÁTICAMENTE AL SELECTWARRIOR*/

        if (Input.GetKeyDown(KeyCode.Backspace) && counterR<3)
        {
            ActHab = false;
            ActSelect = true;
        }

        /*SE EJECUTAN LAS HABILIDAD DEL GUERRERO DE ACUERDO AL PUNTERO ACTIVADO*/
        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2))
        {  
            counterR++;//SE SUMA EL CONTADOR +1
            Flechas.Punteros[CounterArray].DesactivateColor(R, G, B);

            Vida_Michiguerreros.SetActive(false);

            //SE EJECUTA LA HABILIDAD DE ACUERDO A LA T
            //zzECLA OPRIMIDA

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().Habilidad01();
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().Habilidad02();
            }
        }
    }

    public void Continue()
    {
        /*MIENTRAS EL CONTADOR SEA MENOR O IGUAL A TRES, LO QUE HACEMOS ES VETAR PRACTICAMENTE AL 
        GUERRERON EL QUE SE EJECUTÓ LA ACCIÓN PARA QUE YA NO SE USE NUNCA MAS
        Y PASAMOS AUTOMÁTICAMENTE AL SIGUIENTE*/
        if (counterR <= 3)
        {
            Flechas.ChangeChecker(true, CounterArray);//HACEMOS QUE EL ARREGLO PASE A LA CLASE SELECTION PARA PODER VETARLO
            Flechas.MoveLeft(R, G, B, A); //SE MUEVE AL SIGUIENTE
            CounterArray = Flechas.ReceiveActive(); //RECIBIMOS EN EL CONTADOR ARREGLO EN EL QUE ESTÁ ACTUALMENTE EL PUNTERO[N], PARA QUE SEA MÁS FÁCIL MANIPULARLO DESDE AQUÍ
            //Michiguerreros[CounterArray].ShowChart(); 
            Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().ShowChart();//HACEMOS QUE EL TIPO GUERRERO MUESTRE SUS HABILIDADES DE ACUERDO AL HIJO EN EL QUE ESTÁ ASIGNADO.

        }
        CheckRound();//SE CAMBIAN LOS BOOLEANOS DE ACUERDO AL CONTADOR
    }

    void SelectWarrior() //DECIDES QUE GUERRERO ELEGIR
    {
        //Solo oprimes las teclas A y D, laterales, W y S para mover la selección de acuerdo a los íconos del lado izq inf.
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {

            Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().CloseChart();

            Flechas.MoveLeft(R, G, B, A);
            CounterArray = Flechas.ReceiveActive();

            Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().ShowChart();

        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().CloseChart();

            Flechas.MoveRight(R, G, B, A);
            CounterArray = Flechas.ReceiveActive();
            Michiguerreros[CounterArray].GetComponentInChildren <Guerrero>().ShowChart();

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ActSelect = false;
            ActHab = true;
            CounterArray = Flechas.ReceiveActive();
        }
        

    }

    //CHECA POR QUÉ NÚMERO DE RONDA ESTAMOS
    void CheckRound()
    {

        if (counterR < 3) //SI LA RONDA ES MENOR A TRES, REPITES EL PROCESO DE ELEGIR, Y ATACAR
        {

            ActSelect = true;
            ActHab = false;
            Vida_Michiguerreros.SetActive(true);

        }
        if (counterR == 3) //SI LA RONDA ES LA ÚLTIMA, DESACTIVAMOS LA SELECCIÓN, Y NOS PASAMOS PARA ELEGIR LA HABILIDAD DEL ÚLTIMO PERSONAJE
        {
            ActSelect = false;//aquí se desactiva ya
            ActHab = true;
            Vida_Michiguerreros.SetActive(true);
        }
        if (counterR > 3)//FINALIZAMOS EL TURNO DEL JUGADOR DE UNA VEZ POR TODAS 
        {

            ActHab = false;
            ActSelect = false;
            Flechas.Punteros[CounterArray].DesactivateColor(R, G, B);
            Flechas.TransparentIco(CounterArray);
            Vida_Michiguerreros.SetActive(true);


            if (Jefe != null)
            {
                Jefe.TurnoJefe();
            }
        }
    }

    public void FinishEverything() //SI SE LANZA EL ATAQUE GRUPAL, FORZAMOS A QUE ACABE EL CICLO
    {


        ActSelect = false;
        ActHab = false;
        for(int i = 0; i<=2; i++)
        {
            if (i == CounterArray)
            {
                Flechas.Punteros[i].DesactivateColor(R, G, B);
            }

            Flechas.TransparentIco(i);
        }
    }

    IEnumerator Lanzar()
    {
        Especial.DesactivateColor();
        Michiguerreros[CounterArray].GetComponentInChildren<Guerrero>().CloseChart();
        FinishEverything();//Forzamos que finalice todo
        Especial.DesactivateColor();
        yield return new WaitForSeconds(.5f);

        actual.gameObject.SetActive(true);
        /*while (Vector2.Distance(actual.position, intermedio.position) > 0.0001f)
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direccion = (intermedio.position - actual.position).normalized;
            // Moverse hacia el objetivo a una velocidad constante
            actual.Translate(direccion * velocidadTemp * Time.deltaTime);
            // Esperar hasta el próximo fotograma
            yield return null;
        }*/
        while (Vector2.Distance(actual.position, intermedio.position) > 0.0001f)
        {
            actual.position = Vector2.MoveTowards(actual.position, intermedio.position, velocidadTemp * Time.deltaTime);
            yield return null;
        }
        actual.position = intermedio.position;
         yield return new WaitForSeconds(3.0f);

        /*while (Vector2.Distance(actual.position, destino.position) > 0.000001f)
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direccion = (destino.position - actual.position).normalized;
            // Moverse hacia el objetivo a una velocidad constante
            actual.Translate(direccion * velocidadDesplazamiento * Time.deltaTime);
            // Esperar hasta el próximo fotograma
            yield return null;
        }*/
        while (Vector2.Distance(actual.position, destino.position) > 0.000001f)
        {
            actual.position = Vector2.MoveTowards(actual.position, destino.position, velocidadDesplazamiento * Time.deltaTime);
            yield return null;
        }
        actual.position = inicial.position;
        actual.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        ActEsp = false;
        

        CheckRound(); //SE CAMBIAN LOS BOOLEANOS DE ACUERDO AL CONTADOR
    }

    

}
     

