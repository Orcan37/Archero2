using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10PoolExample : MonoBehaviour // название поменять на БуллетПулл и сделать статик       // весит в Пул и создает Обхекты по мере не обходимосоти от них избавляется Можно
    // можно сделать статическим? чтоб можно было быстрый доступ к нему получить
    // можно добавить код который Будет смотреть сколько сейчас в пуле обхектов ( например создать программистом в ручную) чтобы оперативку не забивать в начале игры 
{
    [SerializeField] private int poolCount = 3;  // изночально сколько пуль в памяти 
    [SerializeField] private bool autoExpand = false; // нужно ли авторасширяемсоть 
    [SerializeField] public    L10Cube cubePrefab; // 
    private PoolMono<L10Cube> pool;
    //  private PoolMono<Bullet> poolBul;

    public static L10PoolExample singolton;

    private void Start()
    {
        if (singolton == null) { singolton = this; }

        this.pool = new PoolMono<L10Cube>(this.cubePrefab, this.poolCount, this.transform); // в себе самао создать пулл объектов 
        this.pool.autoExpand = this.autoExpand; // будет ли создавать новыеу объекты если в пуле будет меньше всего объектов
    }

    private void Update()
    { 
            if (Input.GetMouseButtonDown(0)) this.CreateCube(); // создать новый объект  
    }

 



    private void CreateCube() // это всего лучше для списка использовать например для монстриков будет все работать
    {
        var rX = Random.Range(-5f, 5f);
        var rZ = Random.Range(-5f, 5f);
        var y = 0;

        var rPosition = new Vector3(rX, y, rZ);
        MyInstance(this.gameObject);
        var cube = this.pool.GetFreeElement();  // вот это типа Instatnce в Unity3d
        cube.transform.position = rPosition;  // дать такуюто позицию объекту 

    }

    /// <summary> 
    /// тут нужно чтоб он по названию класса создавал пул объектов
    /// почему нельзя в нутри объекта использовать ПУЛ пулей - так как когда стрелок будет создавать в себе эти пули тогда пули ТОЖЕ будут менять при хотьбе стрелка свои координаты
    /// должно быть при вызова сюда CreatObject( префаб объекта, дать ссылку на обхект чтоб иму дать нужные настрой и позицию)
    /// Лучше Через СТАТИК POOLS.createObject( pref, type Object, our gameObject)
    /// дрйгой способ создать Pools объектов и уже зная все статические классы вписать все что нужны есть в игре создавать обхекты // плохо в том что нужно всегда указывтаь что за херню нужно делать
    /// другой способ в Префабе смотреть какой класс тот и нужно будет
    /// </summary>
    public void MyInstance(GameObject prefab)
    {

      if( prefab.GetComponent<L10Cube>() != null) { var cube = this.pool.GetFreeElement(); } // вот это типа Instatnce в Unity3d



    }



}


public class NewClass : MonoBehaviour {


}
