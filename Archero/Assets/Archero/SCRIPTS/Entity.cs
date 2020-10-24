using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public partial class Entity : MonoBehaviour
{ 
    public int damage = 1;
    public float speedAtak = 3;
    public float delayAtak = 3;
    public bool atakTrue = false;

    public float speed = 3;

    public int maxHealth = 3;
    public float currentHealth = 3;
    public float respawnGO = 7;
    [Header("Status")]
    public Owner owner = Owner.Player1;
    public WhoControls whoControls = WhoControls.AI;

    public Material[] materialsOwner;

    public VariableJoystick variableJoystick;
    public Rigidbody rb;


    public GameObject shotSpawn;
    public GameObject shot;  // конечно лучше через скилы делать но это чисто Техзадание 

    public void FixedUpdate()
    {
        if (delayAtak > 0)
        {

            delayAtak -= Time.deltaTime;

        }
        else
        {
            if (whoControls == WhoControls.AI || atakTrue)
            {
              //  StartCoroutine(GetCurrentMoveTarget());
             
                AtackEnemy();    // стрельнуть

            }
        }

        if (whoControls == WhoControls.Player)
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.AddForce(direction * speed * 10 * Time.fixedDeltaTime, ForceMode.VelocityChange);
            if (variableJoystick.Vertical != 0 || variableJoystick.Horizontal != 0)
            {
                atakTrue = false;
            }
            else
            {
                atakTrue = true;
            }
        }  // тоько если человек пользуется 
        

    }
    void Start()
    {
        shotSpawn =  transform.Find("shotSpawn").gameObject;
        rb = GetComponent<Rigidbody>();
        if (owner == Owner.Player1)
        {
            GetComponent<MeshRenderer>().material = materialsOwner[0];
        }
        else if (owner == Owner.Player2)
        {
            GetComponent<MeshRenderer>().material = materialsOwner[1];
        }

        currentHealth = maxHealth;

        AIStart();
    }
     

    public void AtackEnemy()
    {
        delayAtak = speedAtak;
        GameObject clone = Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation) as GameObject;
        clone.GetComponent<mover>().powerAtak = damage;
        clone.GetComponent<mover>().owner = owner;

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DeadThisGo();
        }
    }

    public void DeadThisGo()
    {
        GameObject Finish = GameObject.FindWithTag("Finish");
        this.gameObject.transform.SetParent(Finish.transform);
        this.gameObject.SetActive(false);

        Observable.Timer(System.TimeSpan.FromSeconds(respawnGO)).Subscribe(_ => { this.gameObject.SetActive(true); Respawn(); }).AddTo(this.gameObject);

    }

    public void Respawn()
    {

        currentHealth = maxHealth;
        GameObject RespawnGO = GameObject.FindWithTag("Respawn");
        x = Random.Range(xAreaGames.x, xAreaGames.y);
        z = Random.Range(zAreaGames.x, zAreaGames.y);
        this.gameObject.transform.position = new Vector3(x, AreaGames.transform.position.y, z);
        this.gameObject.transform.SetParent(RespawnGO.transform);
        AIStart();

        if (whoControls == WhoControls.AI) { RandomMoveForEnemy(); }
    }

}
