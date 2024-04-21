using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;
    public float lifeTime = 3f;
    public bool isPlayerBullet = true;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerBullet)
        {
            AI enemy = other.GetComponent<AI>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            PlayerCombat player = other.GetComponent<PlayerCombat>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}   
