using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10.0f;
    public GameManager gm;

    private int hp = 3;
    private float kbForce = 0.7f;
    private bool isAlive = true;
    private Vector3 lastKnowPos;
    private bool AttackReady = true;
    private SpawnManager sm;
    public GameObject ammoPack;
    Animator enemyAnim;
    GameObject playerObj;
    Transform playerTransform;
    Rigidbody enemyRb;
    PlayerController playerController;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
        playerTransform = playerObj.GetComponent<Transform>();
        playerController = playerObj.GetComponent<PlayerController>();
        enemyAnim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        lastKnowPos = playerTransform.position;

        
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && gm.isGameActive)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance <= lookRadius)
            {
                lastKnowPos = playerTransform.position;
            }
            agent.SetDestination(lastKnowPos);
            if (distance <= agent.stoppingDistance && AttackReady)
            {
                FaceTarget();
                AttackReady = false;
                StartCoroutine(Attack());
            }
            enemyAnim.SetFloat("Speed_f", agent.velocity.magnitude);
            if (transform.position.x >= 30.0f)
            {
                transform.position = new Vector3(30.0f, transform.position.y, transform.position.z);
            }
            if (transform.position.x <= -30.0f)
            {
                transform.position = new Vector3(-30.0f, transform.position.y, transform.position.z);
            }
            if (transform.position.z <= -31.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -31.0f);
            }
        }
    }

    void FaceTarget()
    {
        Quaternion lookdir = Quaternion.LookRotation(new Vector3(lastKnowPos.x, 0, lastKnowPos.z));
        transform.rotation = lookdir;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Vector3 kb = (transform.position - other.gameObject.transform.position) * kbForce;
            --hp;
            AudioManager.instance.Play("zombieHurt");
            agent.Move(kb);
            Destroy(other.gameObject);
            lastKnowPos = playerTransform.position;
        }
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemyAnim.SetBool("Death_b", true);
        enemyRb.freezeRotation = true;
        enemyRb.isKinematic = true;
        isAlive = false;
        StartCoroutine(Death());
    }

    IEnumerator Attack()
    {
        enemyAnim.SetInteger("WeaponType_int", 10);
        yield return new WaitForSeconds(1.0f);
        playerController.TakeDamage(1);
        AttackReady = true;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        Instantiate(ammoPack,transform.position,ammoPack.transform.rotation);
        sm.totalAmmpPack++;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
