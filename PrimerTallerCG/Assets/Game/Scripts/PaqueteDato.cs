using UnityEngine;


[System.Serializable]
public class PaqueteDato
{
    
    public string id;
    public int tamanoCarga;
    public float tiempoLlegada;


    public PaqueteDato()
    {
    }
    public PaqueteDato(string id, int tamanoCarga, float tiempoLlegada)
    {
        this.id = id;
        this.tamanoCarga = tamanoCarga;
        this.tiempoLlegada = tiempoLlegada;
    }

    public string Id { get => id; set => id = value; }
    public int TamanoCarga { get => tamanoCarga; set => tamanoCarga = value; }
    public float TiempoLlegada { get => tiempoLlegada; set => tiempoLlegada = value; }

    public override string ToString()
    {
        return $"PaqueteDato [id={id}, tamanoCarga={tamanoCarga}, tiempoLlegada={tiempoLlegada}]";
    }
}
