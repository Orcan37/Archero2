using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class AiEnemy : MonoBehaviour
{
    public Entity entity;
     
    void Start()
    {
        entity = GetComponent<Entity>();
        subscribe();
        RandomMoveForEnemy();
    }
    public virtual void subscribe() // когда  UI польщоваться начинает

    {
       // подписаться на жизни
       // подписать ся к удару
       //подписаться к Полученому урону
       // подписаться к своим жизням 

    }
    public virtual void unSubscribe() // обычно когда перихватывает управление Человек
    {

        // отписаться от всего что подписался в subscribe
    }

    void LateUpdate()
    {
        Move();
        if (entity.delayAtak < 0) { entity.atakTrue = true; }
        FindTargetAndView();
    }
    void FindTargetAndView()
    {
        entity.GetCurrentTarget(); // тут делегат должен быть с подпиской кто будет Таргет
        //  StartCoroutine(GetCurrentTarget());
        if (entity.currentTarget != null) // поворот пушки 
        {
            entity.shotSpawn.transform.rotation = Quaternion.RotateTowards(entity.shotSpawn.transform.rotation, Quaternion.LookRotation(entity.currentTarget.transform.position - entity.shotSpawn.transform.position), 10 * Time.deltaTime * 100);
            entity.shotSpawn.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, entity.shotSpawn.transform.eulerAngles.y, transform.eulerAngles.z);
        }

    }

    public virtual void  Move() // только если включено все
    {
        if (entity.currentTarget != null) {
            entity.agent.
                SetDestination(new Vector3(entity.x, entity.AreaGames.transform.position.y, entity.z)); }

    }
    //  if (whoControls == WhoControls.AI) {  }

    public virtual void EndRespawn()
    {
      RandomMoveForEnemy();
    }

    public virtual void RandomMoveForEnemy()  // enemy
    { 
        entity.x = Random.Range(entity.xAreaGames.x, entity.xAreaGames.y);
        entity.z = Random.Range(entity.zAreaGames.x, entity.zAreaGames.y); 
        //  agent.SetDestination(new Vector3(x, AreaGames.transform.position.y, z)); 
        entity.GetFindEnemyAlways(); 
    }
}
