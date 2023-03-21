using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fasolka : MonoBehaviour
{
    [Header("Behaviour")]

    public string state = "look";
    [SerializeField] float timeTillStateChange; //aktualny czas odliczajacy do zmiany zachowania, np do konca ataku
    //int curStateID, lastStateID;
    //string[] states = {"idle", "look", "chase", "attack0", "attack1"};
    //string[] attacks = {"seria", "magiczna_kula", "raczka", "obszarowy"};
    float[] maxCooldowns = {14f, 22f, 1.5f, 15f}; //tablica maksymalnych cooldownow konrektnych atakow (zgodnie z indexami)
    public float[] curCooldowns = {0f, 0f, 0f, 0f}; //tablica aktualnych cooldownow konrektnych atakow (zgodnie z indexami)
    public float timeBetweenAttacks; //czas miedzy koncem jednego ataku a poczatkiem drugiego
    public float maxOdlPT; //w jakiej odleglosci od Punktu Taktycznego musi byc gremlin aby uznac ze stoi on w punkcie taktycznym
    public float attack1speed;

    [Header("GameObjects")]

    public GameObject explosion; //eksplozja przy smierci przeciwnika
    GameObject player;
    public GameObject magicBall; 
    public GameObject bigBall;
    public GameObject gremlin_attack2, gremlin_attack3;
    GameObject attackGO; //attack GameObject, uzywany np. przy zapisywaniu obiektu podczas Instantiate
    Quaternion attackRot; //attack rotation, uzywany np. kiedy chcemy ustalic obrot ataku przyzywanego przez Instantiate

    enemy_stat es; //enemy_stat jest dolaczony do kazdego przeciwnika i zawiera rozne statystyki i funkcje uniwersalne lub prawie uniwersalne dla kazdego przeciwnika
    float attackStopwatch; //attack Stopwatch jest zmienna czasowa uzywana kiedy w ataku uzywamy zegarka, np. gdy chcemy wielokrotnie co krotki czas przyzwac pocisk


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        es = GetComponent<enemy_stat>(); 
        ChangePT();
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

    void Attack3()
    {
        state = "chase";
        Instantiate(gremlin_attack3, transform.position, transform.rotation);
        AddCooldown(3);
    }
    void Attack2()
    {
        state = "chase";
        attackRot = transform.rotation * Quaternion.Euler(0, 90, 0);
        attackGO = Instantiate(gremlin_attack2, transform.position, attackRot);
        attackGO.transform.parent = transform;
        AddCooldown(2);
    }
    void Attack1()
    {
        state = "attack1";
        timeTillStateChange = 3.5f;
        AddCooldown(1);
    }
    void Attack0()
    {
        state = "attack0";
        timeTillStateChange = 4f;
        AddCooldown(0);
    }

    void Death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        transform.GetChild(0).parent = null;
    }

    void ChangePT() //zmien Punkt Taktyczny
    {
        GameObject[] punkty_taktyczne = GameObject.FindGameObjectsWithTag("punkt_taktyczny"); //znajduje wszystkie punkty taktyczne
                    Transform testTarget = punkty_taktyczne[Random.Range(0, punkty_taktyczne.Length)].transform; //wybiera losowy punkt taktyczny jako test
                    while(es.target == testTarget) //jesli wylosuje ten sam target co byl przed chwila
                        testTarget = punkty_taktyczne[Random.Range(0, punkty_taktyczne.Length)].transform; //losuje jeszcze raz az do skutku
                    es.target = testTarget; //kiedy mu sie uda wybiera testowy punkt taktyczny jako cel
    }


    
    void FixedUpdate()
    {
        if(es.aware)
        {
            es.agent.speed = 0; //ustawiamy predkosc agenta na 0 w razie gdyby przeciwnik przy ataku mial sie nie poruszac
            es.agent.angularSpeed = 0;
            if(es.playerDistance < 6 && curCooldowns[3] <=0)
                Attack3();
            else if(es.playerDistance < 4 && curCooldowns[2] <=0 && curCooldowns[3] > 0)
                Attack2();
            else if(es.playerDistance >= 6 && curCooldowns[1] <=0 && state == "chase")
                Attack1();
            else if(es.playerDistance >= 6 && curCooldowns[0] <=0 && curCooldowns[1] > 0 && state == "chase")
                Attack0();
            else if(timeTillStateChange <= 0) //jesli nie wykonuje zadnych atakow 
            {
                if(state == "attack1" && (transform.position - es.target.position).magnitude < maxOdlPT) //po wykowaniu ataku1
                {
                    ChangePT();
                }

                state = "chase"; //chase jest stanem w ktorym przeciwnik nie zajmuje sie atakami czasowymi
                AddTBA(); //wlasnie skonczyl atak, wiec ustawiamy Time Between Attacks
                timeTillStateChange = timeBetweenAttacks + 0.1f; //tak aby ten if nie wykonywal sie caly czas, lecz jednorazowo po ataku czasowym
            }
                


            else if(state == "chase")
            {
                es.Rotate();
                es.Walk();
            }

            else if(state == "attack0")
            {
                es.agent.speed = attack1speed;
                es.RotateTowardsPlayer();

                if(timeTillStateChange > 3.5f){}
                else
                {
                    if(attackStopwatch <= 0f)
                    {
                        attackStopwatch = 0.2f;
                        Instantiate(magicBall, transform.position + new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(1f, 2.8f), Random.Range(-1.8f, 1.8f)), transform.rotation);
                    }
                    attackStopwatch -= Time.fixedDeltaTime;
                }
            }

            else if(state == "attack1")
            {
                es.RotateTowardsPlayer();

                if(timeTillStateChange > 3.0f){}
                else
                {
                    if(attackStopwatch <= 0f)
                    {
                        attackStopwatch = 3.1f;
                        attackGO = Instantiate(bigBall, transform.position + new Vector3(0, 2f, 0), transform.rotation);
                        attackGO.GetComponent<big_ball>().parento = transform;
                        
                    }
                    attackStopwatch -= Time.fixedDeltaTime;
                    
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
    //bardzo wazne
}
