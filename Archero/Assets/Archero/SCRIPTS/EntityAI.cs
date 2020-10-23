﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
public partial class Entity : MonoBehaviour
{
    [Header("AI")]
    public GameObject currentTarget;
    public NavMeshAgent agent; 
    private NavMeshAgent shotSpawnAgent;

    public float speedBullet;
    public List<GameObject> targets;


    public GameObject AreaGames;
    public GameObject moveTarget;



    public Vector2 xAreaGames;
    public Vector2 zAreaGames;

    public float x;
    public float z;

    /// <summary>
    /// Archo
    /// </summary>

    void AIStart()
    {
        AreaGames = GameObject.FindWithTag("AreaGames");
        agent = GetComponent<NavMeshAgent>();
        FindEnemy();
        CoordinatesAreaGames();

      
       
     
    }



    void LateUpdate()
    {
      //  StartCoroutine(GetCurrentTarget());
        if (currentTarget != null)
        {
            agent.SetDestination(new Vector3(x, AreaGames.transform.position.y, z));
            shotSpawn.transform.rotation = Quaternion.RotateTowards(shotSpawn.transform.rotation, Quaternion.LookRotation(currentTarget.transform.position - shotSpawn.transform.position), 10 * Time.deltaTime * 100);
            shotSpawn.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, shotSpawn.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    void CoordinatesAreaGames()
    {

        float width = AreaGames.transform.localScale.x;// ширина
        float height = AreaGames.transform.localScale.z; // высота

        float xc = AreaGames.transform.position.x,
            yc = AreaGames.transform.position.z;//- координаты центра.

        float xl = xc - width / 2; //"левый" x Верхний 
        float xr = xc + width / 2; //"правый" x Верхний   

        float yt = yc + height / 2;//"верхний" Z  
        float yb = yc - height / 2;//"нижний"Z

        xAreaGames.x = xl;
        xAreaGames.y = xr;

        zAreaGames.x = yt;
        zAreaGames.y = yb;
        RandomMoveForEnemy();
    }





    void FindEnemy()
    {
        targets.Clear();
        Entity[] entity = GameObject.FindObjectsOfType<Entity>();
        for (int j = 0; j < entity.Length; j++)
        {
            if (entity[j].owner != owner)
            {
                targets.Add(entity[j].gameObject);
            }
        }
        if (targets.Count <= 0)
        {
            StartCoroutine(GetFindEnemyAlways()); return;

        }
        StartCoroutine(GetCurrentTarget());
        StartCoroutine(GetFindEnemyAlways());

    }

    IEnumerator GetFindEnemyAlways()
    {
        yield return new WaitForSeconds(5f);
        FindEnemy();
        if (owner == Owner.Monster) { RandomMoveForEnemy(); }
    }


    void RandomMoveForEnemy()  // enemy
    {
        if (owner == Owner.Monster)
        {

            x = Random.Range(xAreaGames.x, xAreaGames.y);
            z = Random.Range(zAreaGames.x, zAreaGames.y);

         //  agent.SetDestination(new Vector3(x, AreaGames.transform.position.y, z));

            GetFindEnemyAlways(); 
        }

    }

    /////////////    /////////////  /////////////    Стрый код     /////////////  /////////////  /////////////  /////////////  ///////////// 

    IEnumerator GetCurrentTarget()
    {
        float tmpDist = float.MaxValue;
        currentTarget = null;
        for (int i = 0; i < targets.Count; i++)
        {
            if (agent.SetDestination(targets[i].transform.position))
            {
                //ждем пока вычислится путь до цели
                while (agent.pathPending)
                { 
                    yield return null;
                }
        
                // проверяем, можно ли дойти до цели
                if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    float pathDistance = 0;
                    //вычисляем длину пути
                    pathDistance += Vector3.Distance(transform.position, agent.path.corners[0]);
                    for (int j = 1; j < agent.path.corners.Length; j++)
                    {
                        pathDistance += Vector3.Distance(agent.path.corners[j - 1], agent.path.corners[j]);
                    }

                    if (tmpDist > pathDistance)
                    {
                        tmpDist = pathDistance;
                        currentTarget = targets[i];


                        agent.ResetPath();
                    }
                }
                else
                {
                    Debug.Log("невозможно дойти до " + targets[i].name);
                }

            }

        }

    }






}