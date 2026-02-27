using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using System.IO;

public class ServidorManager : MonoBehaviour
{

    List<PaqueteDato> lista_paqueteDato = new List<PaqueteDato>();

    public Dictionary<string, PaqueteDato> historialProcesados = new Dictionary<string, PaqueteDato>();

    public Queue<PaqueteDato> colaProcesamiento = new Queue<PaqueteDato>();

    private int totalProcesados;
    private float tiempoEsperaAcumulado;
    public float promedioEspera;

    public TMP_InputField inputBuscarId;

    void Start()
    {
        StartCoroutine(TiempoEspera());
    }

    void Update()
    {

    }

    private void CrearPaquete()
    {
        string nuevoGuidString = Guid.NewGuid().ToString();
        string nombrePaquete = nuevoGuidString;
        int tamanoCarga = Random.Range(1, 7);
        float tiempoProcesamiento = Time.time;

        PaqueteDato paqueteDato = new PaqueteDato(nombrePaquete, tamanoCarga, tiempoProcesamiento);
        lista_paqueteDato.Add(paqueteDato);
        Debug.Log($"Creado {paqueteDato.Id} con un peso de {paqueteDato.TamanoCarga} KB y tiempo de procesamiento de {paqueteDato.TiempoLlegada} segundos.");

        if (paqueteDato == null) return;
        colaProcesamiento.Enqueue(paqueteDato);
        Debug.Log($"Agregado {paqueteDato.Id} a la cola de procesamiento.");

    }

    IEnumerator TiempoEspera()
    {

        while (true)
        {

            if (colaProcesamiento.Count <= 20)
            {

                float tiempoEspera = Random.Range(2f, 4f);
                int generadorPaquete = Random.Range(1, 6);


                if (generadorPaquete == 1)
                {
                    Debug.Log($"Tiempo de espera {tiempoEspera} para generar {generadorPaquete} paquete");
                }
                else
                {
                    Debug.Log($" Tiempo de espera {tiempoEspera} para generar {generadorPaquete} paquetes");
                }
                yield return new WaitForSeconds(tiempoEspera);

                if (generadorPaquete == 1)
                {
                    Debug.Log($"Tiempo de espera {tiempoEspera} ha terminado, generando {generadorPaquete} paquete");
                }
                else
                {
                    Debug.Log($" Tiempo de espera {tiempoEspera} ha terminado, generando {generadorPaquete} paquetes");
                }
                for (int i = 0; i < generadorPaquete; i++)
                {
                    CrearPaquete();

                    yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                }

            }
            else
            {
                Debug.Log("Servidor saturado, esperando para generar nuevos paquetes...");

                yield return new WaitForSeconds(0.5f);
            }

        }
    }

    public void ProcesarSiguiente()
    {
        if (colaProcesamiento.Count == 0) return;

        PaqueteDato paqueteDato = colaProcesamiento.Dequeue();

        if (historialProcesados.ContainsKey(paqueteDato.Id)) return;

        historialProcesados.Add(paqueteDato.Id, paqueteDato);

        float tiempoEspera = Time.time - paqueteDato.TiempoLlegada;

        totalProcesados++;
        tiempoEsperaAcumulado += tiempoEspera;
        promedioEspera = tiempoEsperaAcumulado / totalProcesados;

        CreateJsonFile();

        Debug.Log($"Procesado {paqueteDato.Id} con un peso de {paqueteDato.TamanoCarga} KB. Tiempo de espera: {tiempoEspera} segundos. Promedio de espera: {promedioEspera} segundos.");
    }


    public PaqueteDato BuscarPorID(string id)
    {

        id = inputBuscarId.text;

        if (historialProcesados.ContainsKey(id))
        {

            Debug.Log($"Paquete encontrado: ID: {historialProcesados[id].Id}, Tamaño: {historialProcesados[id].TamanoCarga} KB, Tiempo de llegada: {historialProcesados[id].TiempoLlegada} s");
            return historialProcesados[id];

        }
        else
        {
            return null;
        }
    }

    public void CreateJsonFile()
    {
        Historial objLista = new Historial();
        objLista.paquetes = new List<PaqueteDato>(historialProcesados.Values);

        string json = JsonUtility.ToJson(objLista, true);

        string carpeta = Application.streamingAssetsPath;

        string rutaArchivo = Path.Combine(carpeta, "paqueteDatos.json");

        if (!Directory.Exists(carpeta))
        {
            Directory.CreateDirectory(carpeta);
        }

        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Archivo JSON creado en: " + rutaArchivo);
    }

    [System.Serializable]
    public class Historial
    {
        public List<PaqueteDato> paquetes;
    }
}
