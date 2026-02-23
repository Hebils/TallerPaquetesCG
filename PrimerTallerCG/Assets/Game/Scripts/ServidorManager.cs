using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ServidorManager : MonoBehaviour
{

    List<PaqueteDato> paqueteDato = new List<PaqueteDato>();

    Guid nuevoGuid = Guid.NewGuid();

    void Start()
    {
        PaqueteDato paqueteDato = new PaqueteDato("Paquete 1", 5, 2f);

    }

    void Update()
    {
        StartCoroutine(TiempoEspera());
    }

    IEnumerator TiempoEspera()
    {

        float tiempoEspera = Random.Range(2f, 4f);
        int generadorPaquete = Random.Range(1, 6);

        while (Time.time == tiempoEspera)
        {

            Debug.Log($"Generando paquete {generadorPaquete} con tiempo de llegada {tiempoEspera}");

            yield return null;

        }
        Debug.Log($"Tiempo de espera {tiempoEspera} ha terminado, generando paquete {generadorPaquete}");
    }

}
