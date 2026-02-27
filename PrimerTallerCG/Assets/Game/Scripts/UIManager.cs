using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour

{
    public ServidorManager Servidor;


    public Text textoCola;
    public Text textoHistorialProcesados;
    public Text textoUltimoProcesado;
    public Text textoPromedio;
    public Text textoEstadoServidor;

    public Button botonProcesar;
    public Button botonBuscarId;

    public InputField inputBuscarId;
    public Text textoResultadoBusqueda;

    void Start()
    {
        //botonProcesar.onClick.AddListener(ProcesarSiguiente);
        botonBuscarId.onClick.AddListener(BuscarPorId);
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarUI();
    }

    void ActualizarUI()
    {  
        // 1️ Cantidad en cola
        textoCola.text = "Paquetes en cola: " + Servidor.colaProcesamiento.Count;

        // 2️ Total procesados (Dictionary)
        textoHistorialProcesados.text =
            "Total procesados: " + Servidor.historialProcesados.Count;

        // 3️ Promedio de espera
        //textoPromedio.text =
            //"Promedio espera: " +
            //Servidor.promedioEspera().ToString("F2") + " s";
        
        
        // 4️ Estado del servidor
        if (Servidor.colaProcesamiento.Count > 20)
        {
            textoEstadoServidor.text = "SERVIDOR SATURADO";
            
        }
        else
        {
            textoEstadoServidor.text = "Servidor operando normal";
            
        }
    }




    void BuscarPorId()
    {
        string id = inputBuscarId.text;

        if (string.IsNullOrEmpty(id))
        {
            textoResultadoBusqueda.text = "Ingrese un ID válido.";
            return;
        }

        PaqueteDato paquete = Servidor.BuscarPorID(id);

        if (paquete != null)
        {
            textoResultadoBusqueda.text =
                "Paquete encontrado\n" +
                "Tamaño: " + paquete.TamanoCarga + " KB\n" +
                "Tiempo llegada: " + paquete.TiempoLlegada.ToString("F2");
        }
        else
        {
            textoResultadoBusqueda.text =
                "No existe un paquete con ese ID.";
        }
    }
}
