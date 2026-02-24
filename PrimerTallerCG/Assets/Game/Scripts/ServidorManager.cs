using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ServidorManager : MonoBehaviour
{

    List<PaqueteDato> paqueteDato = new List<PaqueteDato>();

    Dictionary<string, PaqueteDato> historialProcesados = new Dictionary<string, PaqueteDato>();

    Queue<PaqueteDato> colaProcesamiento = new Queue<PaqueteDato>();

    Guid nuevoGuid = Guid.NewGuid();

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
        float tiempoProcesamiento = Random.Range(0.5f, 3f);
        PaqueteDato paqueteDato = new PaqueteDato(nombrePaquete, tamanoCarga, tiempoProcesamiento);
        Debug.Log($"Creado {paqueteDato.Id} con un peso de {paqueteDato.TamanoCarga} KB y tiempo de procesamiento de {paqueteDato.TiempoLlegada} segundos.");

    }

    IEnumerator TiempoEspera()
    { 

        while (true)
        {

            float tiempoEspera = Random.Range(2f,4f);
            int generadorPaquete = Random.Range(1, 6);
            

            if (generadorPaquete == 1)
            {
                Debug.Log($"Tiempo de espera {tiempoEspera} para generar {generadorPaquete} paquete");
            }else
            {
                Debug.Log($" Tiempo de espera {tiempoEspera} para generar {generadorPaquete} paquetes");
            }
            yield return new WaitForSeconds(tiempoEspera);

            CrearPaquete();
            //foreach (int  i in generadorPaquete)
            //{


            //}

            if (generadorPaquete == 1)
            {
                Debug.Log($"Tiempo de espera {tiempoEspera} ha terminado, generando {generadorPaquete} paquete");
            }
            else
            {
                Debug.Log($" Tiempo de espera {tiempoEspera} ha terminado, generando {generadorPaquete} paquetes");
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
