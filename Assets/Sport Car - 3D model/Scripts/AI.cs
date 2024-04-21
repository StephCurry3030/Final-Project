using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public enum enemyType
    {
        watchTower,
        guards,
    }
    [Header("watchTower enemy")]
    public enemyType type;
    public float health = 100f;
    public float range = 10f;
    private float attackCounter;
    public float attackCooldown = 3f;
    [Header("normalGuards")]
    public float guardSpeed = 5f;
    private Transform player;
    public bool isChasingPlayer = false;
    public float stopChasing = 2f;
    [Header("bulletinform")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootingSpeed = 20f;
    public float damage = 25f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PLayerControl>().transform;
    }

    // Update is called once per frame
    // increment the timer
    void Update()
    {
        attackCounter += Time.deltaTime;
        switch (type)
            //
        {
            case enemyType.watchTower:
                if (Vector3.Distance(transform.position, player.position) <= range&& attackCounter >= attackCooldown)
                {
                    AttackPlayer();
                    attackCounter = 0;
                }
                break;
            case enemyType.guards:
                if (isChasingPlayer || Vector3.Distance(transform.position, player.position) <= range)
                {
                    isChasingPlayer = true;
                    if (Vector3.Distance(transform.position, player.position) > stopChasing)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, guardSpeed * Time.deltaTime);
                      
                    }
                    if(Vector3.Distance(transform.position, player.position) <= stopChasing&& attackCounter >= attackCooldown)
                    {
                        AttackPlayer();
                        print("Guards attacking player");
                        attackCounter = 0;
                    } 
                }
                break;
        }
    }
    void AttackPlayer()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletInstance.gameObject.SetActive(true);
        // calculate the direction to the player
        Vector3 shootingDirecton = (player.position - bulletSpawnPoint.position).normalized;
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = shootingDirecton * shootingSpeed;
        }

        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.damage = damage;
            Debug.Log("shooting");
     
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0f)
        {
            print("You killed an enemy");
            Destroy(gameObject, 0.1f);

        }
    }


}
