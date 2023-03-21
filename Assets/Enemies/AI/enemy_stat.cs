using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_stat : MonoBehaviour
{
    [Header("Awareness")]

    public bool aware; //czy przeciwnik jest swiadom ze gracz jest w poblizu?
    [SerializeField] float awarenessDistance; //maksymalna odleglosc od ktorej przeciwnik staje sie swiadom
    [SerializeField] float minimalAwarenessDistance;
    public float playerDistance; //aktualna odleglosc przeciwnika od gracza
    public float playerAngle; //kat miedzy obrotem przeciwnika a gracza
    public Quaternion rotToPlayer = new Quaternion(); //obrot od osi patrzenia do osi do gracza

    [Header("NavMesh")]

    public bool chasingObject; //czy przeciwnik goni jakis obiekt?
    public NavMeshAgent agent; //NavMesh Agent
    public Transform target; //w kierunku czego podazamy?

    [Header("HP")]

    public float maxhp;
    public float hp; //wiadomix

    [Header("Speeds")]

    public float defaultRotationSpeed; //szybkosc obracania sie przeciwnika w stopniach na sekunde
    public float defaultWalkingSpeed; //szybkosc chodzenia przeciwnika
    public float rotationSpeed; //szybkosc obracania sie przeciwnika w stopniach na sekunde
    public float walkingSpeed; //szybkosc chodzenia przeciwnika

    [Header("Other")]

    public float fury;
    
    GameObject player;
    Vector3 toPlayer; //vector od przeciwnika do gracza

    
    
    private void Aware() //probuje uswiadomic przeciwnika oraz oblicza odleglosc i kat do gracz
    {
        toPlayer = transform.position - player.transform.position;
        playerDistance = toPlayer.magnitude;

        rotToPlayer.SetFromToRotation(transform.forward, player.transform.position - transform.position); //tutaj przyjmuje wartosc (Quaternion)
        playerAngle = Quaternion.Angle(Quaternion.identity, rotToPlayer); //tutaj przyjmuje wartosc w stopniach

        if(hp < maxhp)
            minimalAwarenessDistance++;

        if(playerDistance < (awarenessDistance-minimalAwarenessDistance) * (1-(playerAngle/180)) + minimalAwarenessDistance) //kat do gracza moze zmniejszyc odleglosc uswiadamiania przeciwnika (jesli przeciwnik stoi tylem do gracza, zauwazy go z mniejszej odlegosci)
            aware = true;
    }

    public bool LooksAtYou(float maxAngle) //sprawdza czy przeciwnika patrzy na gracza, z bledem pomiarowym rownym maxAngle
    {
        if (Mathf.Abs(playerAngle) < maxAngle)
            return true;
        return false;
    }

    public void WalksForwards()
    {
        transform.position += transform.forward * walkingSpeed * Time.fixedDeltaTime;
    }

    public void Rotate() //obraca przeciwnika w strone celu zgodnie z predkoscia obracania
    {
        agent.angularSpeed = rotationSpeed;
    }

    public void RotateTowardsPlayer() //obraca przeciwnika w strone gracza zgodnie z predkoscia obracania
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * rotToPlayer, rotationSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void Walk() //idzie do przodu zgodnie z walkingSpeed 
    {
        agent.speed = walkingSpeed;
    }

    



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0;
        agent.angularSpeed = 0;
        walkingSpeed = defaultWalkingSpeed;
        rotationSpeed = defaultRotationSpeed;
    }

    void FixedUpdate()
    {
        player = GameObject.FindWithTag("Player");
        Aware();

        if(chasingObject)
        {
            agent.destination = target.position;
        }
    }
}
