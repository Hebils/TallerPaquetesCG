using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour

{
    public ServidorManager servidor;


    public TextMeshProUGUI textoId;
    public TextMeshProUGUI textoTamano;
    public TextMeshProUGUI textoTiempoLlegada;

    public TextMeshProUGUI textoIdBuscar;
    public TextMeshProUGUI textoTamanoBuscar;
    public TextMeshProUGUI textoTiempoLlegadaBuscar;

    public TextMeshProUGUI textoCola;
    public TextMeshProUGUI textoHistorialProcesados;
    public TextMeshProUGUI textoPromedio;
    public TextMeshProUGUI textoEstadoServidor;

    public TMP_InputField inputBuscarId;

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
        textoCola.text = "Paquetes en cola: " + servidor.colaProcesamiento.Count;

        // 2️ Total procesados (Dictionary)
        textoHistorialProcesados.text =
            "Total procesados: " + servidor.historialProcesados.Count;

        // 3️ Promedio de espera
        textoPromedio.text =
        "Promedio espera: " +
        servidor.promedioEspera.ToString("F2") + " s";


        // 4️ Estado del servidor
        if (servidor.colaProcesamiento.Count > 20)
        {
            textoEstadoServidor.text = "SERVIDOR SATURADO";

        }
        else
        {
            textoEstadoServidor.text = "Servidor operando normal";

        }


        // PaqueteDato paquete = servidor.BuscarPorID(servidor.inputBuscarId.text);
        // if (paquete != null)
        // {
        //     textoId.text = "ID: " + paquete.Id;
        //     textoTamano.text = "Tamaño: " + paquete.TamanoCarga + " KB";
        //     textoTiempoLlegada.text = "Tiempo llegada: " + paquete.TiempoLlegada.ToString("F2") + " s";
        // }
        // else
        // {
        //     textoId.text = "ID: No encontrado";
        //     textoTamano.text = "Tamaño: N/A";
        //     textoTiempoLlegada.text = "Tiempo llegada: N/A";
        // }
    }




    public void BuscarPorId()
    {
        string id = inputBuscarId.text;

        PaqueteDato paquete = servidor.BuscarPorID(id);

        if (servidor.historialProcesados.ContainsKey(id) != false)
        {
            textoIdBuscar.text = "ID: " + paquete.Id;
            textoTamanoBuscar.text = "Tamaño: " + paquete.TamanoCarga + " KB";
            textoTiempoLlegadaBuscar.text = "Tiempo llegada: " + paquete.TiempoLlegada.ToString("F2") + " s";
        }
        else
        {
            textoIdBuscar.text = "ID: No encontrado";
            textoTamanoBuscar.text = "Tamaño: N/A";
            textoTiempoLlegadaBuscar.text = "Tiempo llegada: N/A";


        }
    }
}