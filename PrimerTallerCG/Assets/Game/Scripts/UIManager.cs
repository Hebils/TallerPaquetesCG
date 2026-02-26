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
        textoPromedio.text =
            "Promedio espera: " +
            Servidor.ObtenerPromedioEspera().ToString("F2") + " s";
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
}
