using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fila<T>
{
    int inicio = 0;
    int fim = 0;

    public T[] dados;

    public Fila()
    {
        dados = new T[100];
    }

    public void Inserir(T dado)
    {
        if (Cheia() == false) {
            dados[fim] = dado;
            fim++;
        }
  
    }
    
    public T Remover()
    {
        if (Vazia() == false)
        {
            T retorno = dados[inicio];
            inicio++;
            return retorno;
        }

        return default(T);
    }

    public T Frente()
    {
        if (Vazia() == false)
        {
            return dados[inicio];
        }

        return default(T);
    }

    public bool Vazia()
    {
        if (inicio == fim)
        {
            Debug.Log("Pilha Vazia");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Cheia()
    {
        if(fim == 100)
        {
            Debug.Log("Pilha Cheia");
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Tamanho() {
        return fim - inicio;
    }

}
