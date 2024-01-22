using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Main : MonoBehaviour
{
    /*
     Written By: Goh Min Rui
     Description: Enemy 1 Main Script
     */
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float Notice_Range = 100f;
    [SerializeField] private float Attack_Range = 50f;
    [SerializeField] private float Rotate_amt = 2.0f;
    [SerializeField] private GameObject Enemy1Ashes;
    public int EnemyHealth=200;
    [SerializeField] private GameObject DetectionZone;
    [HideInInspector]
    public enum Enemy_State
    {
        idle,
        patrol,
        chase,
        attack,
        death,
        hurt
    }

    public Enemy_State _state;
    public Enemy_State prev_state;

    private NavMeshAgent _nav;
    private Animator _ani;

    private bool playerPassed = false;
    private bool playerDead = false;
    private bool onPath = false;

    [Header("Patrol")]
    [SerializeField] private List<GameObject> PatrolPoints = new List<GameObject>();
    private int PatrolIndex = 0;


    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        _nav = enemy.GetComponent<NavMeshAgent>();
        _nav.SetDestination(PatrolPoints[PatrolIndex].transform.position);
        _ani = enemy.GetComponent<Animator>();
    }

     void Update()
    {
        if (EnemyHealth > 0)
        {
           
            //Enemy to patrol if player hasn't passed
            if (playerPassed == false)
            {
                _state = Enemy_State.patrol;
                if(Vector3.Distance(player.transform.position, DetectionZone.transform.position) < 3f)
                {
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
                    _nav.speed = 1f;
                    if (Vector3.Distance(PatrolPoints[PatrolIndex].transform.position, enemy.transform.position) < 1f)
                    {
                        _nav.ResetPath();
                        if (PatrolIndex == 1)
                        {
                            PatrolIndex = 0;
                        }
                        else
                        {
                            PatrolIndex = 1;
                        }
                        _nav.SetDestination(PatrolPoints[PatrolIndex].transform.position);
                        _ani.SetFloat("Motion", 0f);
                    }
                    
                }
            
            
            }
            else if (_state == Enemy_State.idle)
            {
                _ani.SetBool("Idle", false);
                _ani.SetBool("IdleWalking", true);
                _state = Enemy_State.chase;
            }
            else if (_state == Enemy_State.chase)
            {
                Vector3 _direction = player.transform.position - enemy.transform.position;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * Rotate_amt); float angleToRotate = Vector3.Angle(enemy.transform.forward, _direction);
                if (Vector3.Distance(player.transform.position, enemy.transform.position) < Attack_Range)
                {
                    _ani.SetBool("IdleWalking", false);
                    _nav.ResetPath();
                    _nav.isStopped=true;
                    _state = Enemy_State.attack;
                    _ani.SetTrigger("Attack");
                    onPath = false;
                }
                else if (Vector3.Distance(player.transform.position, enemy.transform.position) < Notice_Range)
                {

                   
                    _ani.SetFloat("Motion", 0.5f);
                    _ani.SetBool("IdleWalking", true);
                    if (onPath == false)
                    {
                        _nav.SetDestination(player.transform.position);
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
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * Rotate_amt); float angleToRotate = Vector3.Angle(enemy.transform.forward, _direction);
                _nav.ResetPath();
                if(Vector3.Distance(player.transform.position, enemy.transform.position) < 1f)
                {
                    Vector3 newPosition = enemy.transform.position;
                    newPosition.z -= 2f;
                    enemy.transform.position = Vector3.Lerp(enemy.transform.position,newPosition,Time.deltaTime*5f);
                }
                    if (Vector3.Distance(player.transform.position, enemy.transform.position) > Notice_Range-0.5f)
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
                    _ani.SetBool("Hurt", false);
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
        Enemy1Ashes.transform.position = new Vector3(enemy.transform.position.x, 2.5f, enemy.transform.position.z);
        
        ParticleSystem AshesBoom = Enemy1Ashes.GetComponent<ParticleSystem>();
        AshesBoom.Stop();
        AshesBoom.Clear();
        // Play the particle system again
        AshesBoom.Play();
        Destroy(AshesBoom, 5f);
        //enemy.GetComponent<Rigidbody>().useGravity = true;
        Destroy(enemy);
       
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
        StartCoroutine(GoDown());
    }

}

