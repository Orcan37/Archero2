using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class AiEnemy : MonoBehaviour
{
     
    void Start()
    {
        subscribe();
    } 
   public void subscribe() // когда  UI польщоваться начинает

    {
       // подписаться на жизни
       // подписать ся к удару
       //подписаться к Полученому урону
       // подписаться к своим жизням
       // подписаться 

    }
    public void unSubscribe() // обычно когда перихватывает управление Человек
    {
        // отписаться от всего что подписался в subscribe
    }


}
