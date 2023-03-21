using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class kloc : MonoBehaviour
{
    [Header("Behaviour")]

    public string state = "chase";
    [SerializeField] float timeTillStateChange; //aktualny czas odliczajacy do zmiany zachowania, np do konca ataku
    float[] maxCooldowns = {3f, 12f}; //tablica maksymalnych cooldownow konrektnych atakow (zgodnie z indexami)
    public float[] curCooldowns = {0f, 0f}; //tablica aktualnych cooldownow konrektnych atakow (zgodnie z indexami)
    public float timeBetweenAttacks; //czas miedzy koncem jednego ataku a poczatkiem drugiego
    public float stoneDeathForce;
    public float minDis, maxDis; //odleglosc od gracza w zaleznosci od furii
    public float minSpeed, maxSpeed; //predkosc chodzenia w strone gracza powyzej 50% furii
    public float flameRotationSpeed, flameWalkingSpeed;

    [Header("GameObjects")]

    public GameObject explosion; //eksplozja przy smierci przeciwnika
    GameObject player;
    public GameObject stalaktyt, flame;
    GameObject attackGO; //attack GameObject, uzywany np. przy zapisywaniu obiektu podczas Instantiate
    Quaternion attackRot; //attack rotation, uzywany np. kiedy chcemy ustalic obrot ataku przyzywanego przez Instantiate

    enemy_stat es; //enemy_stat jest dolaczony do kazdego przeciwnika i zawiera rozne statystyki i funkcje uniwersalne lub prawie uniwersalne dla kazdego przeciwnika
    float attackStopwatch; //attack Stopwatch jest zmienna czasowa uzywana kiedy w ataku uzywamy zegarka, np. gdy chcemy wielokrotnie co krotki czas przyzwac pocisk


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        es = GetComponent<enemy_stat>(); 

        minSpeed = es.walkingSpeed;
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

    void Attack1()
    {
        state = "attack1";
        timeTillStateChange = 3.7f;
        AddCooldown(1);
    }
    void Attack0()
    {
        state = "chase";
        Instantiate(stalaktyt, transform.position + transform.forward * -1 + new Vector3(0, 4, 0), transform.rotation);
        AddCooldown(0);
    }

    public void Death()
    {
        if(transform.Find("throwflamer")!=null)
        {
            transform.Find("throwflamer").parent = null;
        }
        for(int i=0; i<10; i++)
        {
            Transform ch = transform.Find("kamien"); //znajdz dziecko
            ch.GetComponent<Rigidbody>().isKinematic = false;
            ch.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-stoneDeathForce, stoneDeathForce), 0,Random.Range(-stoneDeathForce, stoneDeathForce)));
            ch.parent = null;
        }

        if(transform.GetChild(0)!=null)
        {
            transform.GetChild(0).GetComponent<serce>().enabled = true;
            transform.GetChild(0).GetComponent<serce>().Awaken();
        }
        
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    
    void FixedUpdate()
    {
        es.fury = 100*(es.maxhp - es.hp)/es.maxhp; //furia jest liniowo zalezna od brakujacego zdrowia i jest podawana w procentach
        if(es.aware)
        {
            es.target = player.transform;

            es.agent.speed = 0; //ustawiamy predkosc agenta na 0 w razie gdyby przeciwnik przy ataku mial sie nie poruszac
            es.agent.angularSpeed = 0;

            if(es.playerDistance >= 3 && curCooldowns[0] <=0 && state == "chase")
                Attack0();
            else if (es.playerDistance <= 3 && curCooldowns[1] <= 0 && state == "chase")
                Attack1();
            else if(timeTillStateChange <= 0) //jesli nie wykonuje zadnych atakow 
            {
                if(state == "attack1")
                {
                    transform.Find("throwflamer(Clone)").parent = null;
                }
                state = "chase"; //chase jest stanem w ktorym przeciwnik nie zajmuje sie atakami czasowymi
                AddTBA(); //wlasnie skonczyl atak, wiec ustawiamy Time Between Attacks
                timeTillStateChange = timeBetweenAttacks + 0.1f; //tak aby ten if nie wykonywal sie caly czas, lecz jednorazowo po ataku czasowym
            }
                


            if(state == "chase")
            {
                es.rotationSpeed = es.defaultRotationSpeed;
                es.walkingSpeed = es.defaultWalkingSpeed;
                es.RotateTowardsPlayer();
                es.Walk();
                if(es.fury <= 50)
                {
                    float optimalDistance = Mathf.Lerp(maxDis, minDis, (es.fury)*2/100); //dobiera odleglosc w ktorej powinien znajdowac sie stoln kamien
                    es.agent.destination = new Vector3(Mathf.LerpUnclamped(player.transform.position.x, transform.position.x, (optimalDistance/es.playerDistance)), Mathf.LerpUnclamped(player.transform.position.y, transform.position.y, (optimalDistance/es.playerDistance)),Mathf.LerpUnclamped(player.transform.position.z, transform.position.z, (optimalDistance/es.playerDistance)));
                }
                else
                {
                    es.target = player.transform;
                    es.chasingObject = true;
                    float curSpeed = Mathf.Lerp(minSpeed, maxSpeed, (es.fury-50)*2/100);
                    es.walkingSpeed = curSpeed;
                }
            }

            else if(state == "attack1") //plomien miotaczy, 3.7s
            {
                es.rotationSpeed = flameRotationSpeed;
                es.walkingSpeed = flameWalkingSpeed;
                es.RotateTowardsPlayer();
                es.Walk();

                if(timeTillStateChange > 3.0f){}
                else
                {
                    if(attackStopwatch <= 0f)
                    {
                        attackStopwatch = 3.8f;
                        attackGO = Instantiate(flame, transform.position, transform.rotation);
                        attackGO.transform.parent = transform;
                        
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
}
