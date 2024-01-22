using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3Main : MonoBehaviour
{
    /*
     Written By: Goh Min Rui
     Description: Enemy 3 Main Script
     */
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float Notice_Range = 100f;
    [SerializeField] private float Attack_Range = 50f;
    [SerializeField] private float Rotate_amt = 2.0f;
    [SerializeField] private GameObject Enemy3Ashes;
    [SerializeField] private GameObject SpinningThing;
    public int EnemyHealth = 200;
    [SerializeField] private GameObject DetectionZone;
    [HideInInspector]
    public enum Enemy_State
    {
        idle,
        chase,
        attack,
        hurt,
        death
    }

    private List<GameObject> InstantiatedAttack = new List<GameObject>();

    public Enemy_State _state;
    public Enemy_State prev_state;

    private NavMeshAgent _nav;
    private Animator _ani;

    private bool playerPassed = false;
    private bool playerDead = false;
    private bool onPath = false;
    private bool canChange=true;

    [SerializeField] private List<GameObject> slidingDoors = new List<GameObject>();
    [Header("Reapper and disappear")]
    [SerializeField] private List<GameObject> PointOfAppearance = new List<GameObject>();
    [SerializeField] private GameObject DisappearSystem;
   private int AppearListIndex=0;

    [Header("Attacks")]
    [SerializeField] private GameObject Attack1Projectile;
    [SerializeField] private GameObject Attack2Projectile;
    [SerializeField] private GameObject Attack3Projectile;
    [SerializeField] private Transform Attack1TriggerRight;
    [SerializeField] private List<Transform> Attack2Trigger = new List<Transform>();
    [SerializeField] private List<Transform> Attack3Trigger = new List<Transform>();
    private bool isAttack1Done=false;
    private bool isAttack2Done = false;
    private bool isAttack3Done = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        _nav = enemy.GetComponent<NavMeshAgent>();
        _ani = enemy.GetComponent<Animator>();
        DisappearSystem.SetActive(false);
        foreach (var door in slidingDoors)
        {
            door.transform.position = new Vector3(door.transform.position.x, 6.46f, door.transform.position.z);
            door.SetActive(false);
        }
        SpinningThing.SetActive(false);
    }

    void Update()
    {
        if (EnemyHealth > 0)
        {
            if (playerPassed == false)
            {
               if (Vector3.Distance(player.transform.position, DetectionZone.transform.position) < 1f)
                {
                    _ani.SetBool("Idle", false);

                    _ani.SetFloat("Motion", 0.5f);
                    StartCoroutine(SlideUp());
                    Destroy(DetectionZone);
                    playerPassed = true;
                    _state = Enemy_State.chase;
                    _nav.ResetPath();
                    _nav.SetDestination(player.transform.position);
                    onPath = true;
                    _nav.speed = 3f;
                }
                else
                {
                    _state = Enemy_State.idle;
                    _ani.SetBool("Idle", true);
                }


            }

            else if (_state == Enemy_State.idle)
            {
                _ani.SetBool("Idle", true);
                _state = Enemy_State.chase;
            }
            else if (_state == Enemy_State.chase)
            {
                _ani.SetBool("Idle", false);
                Vector3 _direction = player.transform.position - enemy.transform.position;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * Rotate_amt);
                if (Vector3.Distance(player.transform.position, enemy.transform.position) < Attack_Range)
                {
                    if (_nav.isActiveAndEnabled)
                    {
                        _nav.ResetPath();
                        _nav.isStopped = true;
                    }
                    _state = Enemy_State.attack;
                    _ani.SetTrigger("Attack");
                    onPath = false;
                }
                else if (Vector3.Distance(player.transform.position, enemy.transform.position) < Notice_Range)
                {
                    _ani.SetFloat("Motion", 0.5f);
                    if (onPath == false)
                    {
                        if (_nav != null)
                        {
                            _nav.SetDestination(player.transform.position);
                        }
                    }
                    _state = Enemy_State.chase;
                }
                else
                {
                    _state = Enemy_State.idle;
                }
            }
            else if (_state == Enemy_State.attack)
            {
                Vector3 _direction = player.transform.position - enemy.transform.position;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * Rotate_amt);
                if (_nav.isActiveAndEnabled)
                {
                    _nav.ResetPath();
                }
                if (Vector3.Distance(player.transform.position, enemy.transform.position) < 1f)
                {
                    Vector3 newPosition = enemy.transform.position;
                    newPosition.z -= 2f;
                    enemy.transform.position = Vector3.Lerp(enemy.transform.position, newPosition, Time.deltaTime * 5f);
                }
                if (Vector3.Distance(player.transform.position, enemy.transform.position) > Notice_Range - 0.5f)
                {
                    _state = Enemy_State.chase;
                    _nav.SetDestination(player.transform.position);
                    onPath = true;

                }
                else
                {
                    _state = Enemy_State.chase;
                }

            }
            else if (_state == Enemy_State.hurt)
            {
                _ani.SetBool("Hurt", true);
                _state = Enemy_State.chase;
            }
        }
        else
        {
            if (playerDead == false)
            {
                playerDead = true;
                _state = Enemy_State.death;
                _ani.SetTrigger("Death");
            }
        }
    }


    public void hurtDone()
    {
        _ani.SetTrigger("FinishHurt");
        _ani.SetBool("Hurt", false);
        _state = Enemy_State.chase;
    }

    public void Death()
    {
        SpinningThing.SetActive(true);
        foreach (var door in slidingDoors)
        {
            door.SetActive(true);
            Animator doorAnimation = door.GetComponent<Animator>();
            doorAnimation.SetTrigger("SlideUp");
        }
        Vector3 Newposition = new Vector3(enemy.transform.position.x, 1.5f, enemy.transform.position.z);
        Destroy(Instantiate(Enemy3Ashes, Newposition, Quaternion.Euler(0, 0, 0)), 5f);
        //enemy.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject);
    }

    private IEnumerator GoDown()
    {
        float elapsedTime = 0f;
        float duration = 3f;
        Vector3 startPosition = enemy.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, -1.18f, startPosition.z);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // Calculate the interpolation factor
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }

    public void startDeath()
    {
        if (enemy.activeSelf == true)
        {
            StartCoroutine(GoDown());
        }
    }

    public void disappear()
    {
        isAttack1Done = false;
        isAttack2Done = false;
        isAttack3Done = false;
        int RandomNumber = Random.Range(0, 6);
        if (RandomNumber == 3&&canChange==true)
        {
            for(int RandomIndex=-1; RandomIndex != AppearListIndex;) {
                 RandomIndex = Random.Range(0, PointOfAppearance.Count);
                AppearListIndex = RandomIndex;
            }
           
            DisappearSystem.SetActive(true);
            DisappearSystem.transform.position = enemy.transform.position;
            ParticleSystem disappearParticles = DisappearSystem.GetComponent<ParticleSystem>();
            disappearParticles.Play();
            enemy.SetActive(false);
            StartCoroutine(AppearAgain());
            StartCoroutine(cannotChange());
         }
        foreach (var projectiles in InstantiatedAttack)
        {
            if (projectiles != null)
            {
                Destroy(projectiles);
            }
        }
        InstantiatedAttack = new List<GameObject>();
    }

    private IEnumerator AppearAgain()
    {
        yield return new WaitForSeconds(3f);
        DisappearSystem.transform.position = PointOfAppearance[AppearListIndex].transform.position;
        ParticleSystem disappearParticles = DisappearSystem.GetComponent<ParticleSystem>();
        disappearParticles.Clear();
        disappearParticles.Play();

        enemy.SetActive(true);
        enemy.transform.position = PointOfAppearance[AppearListIndex].transform.position;

    }

    public void Attack1()
    {
            isAttack1Done = false;
            StartCoroutine(InstantiateAttack1());
    }
    public void Attack2()
    {

            isAttack2Done = false;
            StartCoroutine(InstantiateAttack2());
    }
    public void Attack3()
    {
            isAttack3Done = false;
            StartCoroutine(InstantiateAttack3());
        
    
    }

    private IEnumerator InstantiateAttack1()
    {
        while (isAttack1Done == false)
        {

        if (enemy != null)
        {
                GameObject attack = Instantiate(Attack1Projectile, Attack1TriggerRight.position, Random.rotation);
                InstantiatedAttack.Add(attack);
            Destroy(attack, 1f);
            yield return new WaitForSeconds(1f);
        }
        }
    }

    private IEnumerator InstantiateAttack2()
    {
        while (isAttack2Done == false)
        {
            foreach (var trigger in Attack2Trigger)
            {

                if (enemy != null)
                {
                    GameObject attack = Instantiate(Attack2Projectile, trigger.position, Random.rotation);
                    InstantiatedAttack.Add(attack);
                    Destroy(attack, 1f);

                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }

    private IEnumerator InstantiateAttack3()
    {
        while (isAttack3Done==false)
        {
            foreach (var trigger in Attack3Trigger)
            {

                if (enemy != null)
                {
                    GameObject attack = Instantiate(Attack3Projectile, trigger.position, Random.rotation);
                    InstantiatedAttack.Add(attack);
                    Destroy(attack, 1f);
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }

    private IEnumerator cannotChange()
    {
        canChange = false;
        yield return new WaitForSeconds(3f);
        canChange = true;
    }

    public void Attack1Done()
    {
        isAttack1Done = true;
    }
    public void Attack2Done()
    {
        isAttack2Done = true;
    }
    public void Attack3Done()
    {
        isAttack3Done = true;
    }

    private IEnumerator SlideUp()
    {
        yield return new WaitForSeconds(2f);
        foreach (var door in slidingDoors)
        {
            Animator doorAnimation = door.GetComponent<Animator>();

            door.SetActive(true);
            doorAnimation.SetTrigger("SlideDown");
        }
    }
}

