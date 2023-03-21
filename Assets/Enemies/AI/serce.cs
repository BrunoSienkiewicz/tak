using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class serce : MonoBehaviour
{
    [Header("Behaviour")]

    public string state = "chase";
    [SerializeField] float timeTillStateChange; //aktualny czas odliczajacy do zmiany zachowania, np do konca ataku
    float[] maxCooldowns = {0.1f}; //tablica maksymalnych cooldownow konrektnych atakow (zgodnie z indexami)
    public float[] curCooldowns = {0f}; //tablica aktualnych cooldownow konrektnych atakow (zgodnie z indexami)
    public float timeBetweenAttacks; //czas miedzy koncem jednego ataku a poczatkiem drugiego
    public float rotvel, rotacc;
    public float damage;
    public float brakes;

    [Header("GameObjects")]

    public GameObject explosion; //eksplozja przy smierci przeciwnika
    GameObject player;
    GameObject attackGO; //attack GameObject, uzywany np. przy zapisywaniu obiektu podczas Instantiate
    Quaternion attackRot; //attack rotation, uzywany np. kiedy chcemy ustalic obrot ataku przyzywanego przez Instantiate

    enemy_stat es; //enemy_stat jest dolaczony do kazdego przeciwnika i zawiera rozne statystyki i funkcje uniwersalne lub prawie uniwersalne dla kazdego przeciwnika
    float attackStopwatch; //attack Stopwatch jest zmienna czasowa uzywana kiedy w ataku uzywamy zegarka, np. gdy chcemy wielokrotnie co krotki czas przyzwac pocisk


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        es = GetComponent<enemy_stat>(); 

        GetComponent<BoxCollider>().center = new Vector3(0, 2, -3f);
        GetComponent<BoxCollider>().size = new Vector3(1, 1, 7);
    }



    void AddCooldown(int attackID) //AddCooldown nadaje atakowi o attackID cooldown okreslony w tabeli maxCooldowns
    {
        curCooldowns[attackID] = maxCooldowns[attackID];
        AddTBA();
    }   

    void AddTBA()   //Add Time Between Attack, dzieki ktoremu po jednym ataku nie nastepuje natychmiastowo drugi - ustawia on wszystkim aktualnym cooldownom wartosc nie mniejsza od timeBetweenAttacks
    {
        for (int i = 0; i < curCooldowns.Length; i++)
        {
            if(curCooldowns[i] < timeBetweenAttacks)
                curCooldowns[i] = timeBetweenAttacks;
        }
    }

    void Attack0()
    {
        state = "attack0";
        timeTillStateChange = 1.8f;
        AddCooldown(0);
    }

    void Death()
    {
        Instantiate(explosion, transform.position + new Vector3(0, 1f, 0), transform.rotation);
        Destroy(gameObject);
        if(transform.parent != null)
        {
            transform.parent.GetComponent<kloc>().Death();
        }
    }

    public void Awaken()
    {
        for(int i=0; i < 3; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
        Start();
        GetComponent<NavMeshAgent>().enabled = true;
        es.walkingSpeed = 15;
        es.rotationSpeed = 40;
        es.aware = true;
        es.chasingObject = true;
        es.target = player.transform;
        transform.parent = null;
    }

    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Death();
            print("hit");
            player.GetComponent<player_stat>().hp -= damage;
        }
    }
    
    void FixedUpdate()
    {
        if(es.aware)
        {
            es.target = player.transform;

            es.agent.speed = 0; //ustawiamy predkosc agenta na 0 w razie gdyby przeciwnik przy ataku mial sie nie poruszac
            es.agent.angularSpeed = 0;

            if(curCooldowns[0] <=0 && state == "chase" && (es.playerDistance < 1.5f || (es.playerDistance <= es.walkingSpeed * 0.8f + 2 && es.LooksAtYou(7))))
                Attack0();
            else if(timeTillStateChange <= 0) //jesli nie wykonuje zadnych atakow 
            {
                if(state == "attack0")
                {
                    state = "rotating";
                }
                AddTBA(); //wlasnie skonczyl atak, wiec ustawiamy Time Between Attacks
                timeTillStateChange = timeBetweenAttacks + 0.1f; //tak aby ten if nie wykonywal sie caly czas, lecz jednorazowo po ataku czasowym
            }
                


            if(state == "chase")
            {
                es.rotationSpeed = es.defaultRotationSpeed;
                es.walkingSpeed = es.defaultWalkingSpeed;
                es.RotateTowardsPlayer();
                es.Walk();
            }

            else if(state == "attack0") //macki, 1.8s
            {
                es.WalksForwards();
                if(timeTillStateChange > 1.0f){}
                else 
                {
                    if (timeTillStateChange > 0.5f)
                    {
                        rotvel += rotacc * Time.fixedDeltaTime;
                    }
                    else
                    {
                        rotvel -= rotacc * Time.fixedDeltaTime;
                    }
                    Quaternion backfliprot = Quaternion.AngleAxis(-rotvel, Vector3.right);
                    transform.Find("Cube").rotation *= backfliprot;
                    
                }
                es.walkingSpeed -= brakes * Time.fixedDeltaTime;
            }

            else if(state == "rotating")
            {
                es.RotateTowardsPlayer();
                if(es.LooksAtYou(25))
                {
                    state = "chase";
                }
            }
            
            
            timeTillStateChange -= Time.fixedDeltaTime;

            for (int i = 0; i < curCooldowns.Length; i++)
            {
                if(curCooldowns[i] > 0)
                    curCooldowns[i] -= Time.fixedDeltaTime;
            }
        }

        if(es.hp <= 0)
        {
            Death();
        }
    }
}
