using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab { get; }  //
    public bool autoExpand { get; set; } // авто расширяемость пула
    public Transform container { get; } // куда складываться 
    public List<T> pool; // где будет хрониться 

    public PoolMono(T prefab, int count) // конструктор емкость пишем 
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }


    public PoolMono(T prefab, int count, Transform conteiner) // 
    {
        this.prefab = prefab;
        this.container = conteiner;

        this.CreatePool(count);
    }
    private void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        } 
    }

    private T CreateObject(bool IsActiveByDefault = false) // создать сразу выключеным  Но если надо будет новый создать сразу включеный
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(IsActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element) // возращать если он не активен в ерарзии
    {
        foreach(var mono in pool){
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true; 
            }
        }
        element = null;
        return false; 
    }


    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;
        if (this.autoExpand) return this.CreateObject( true);
        throw new Exception($"There is no free element in pool of type{typeof(T)}");
        
    }

}
