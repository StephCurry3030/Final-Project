using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player")]
    public float playerHealth = 200f;
    public float playerDamage = 40f;
    public float playerShootingRange = 15f;
    public Camera playerCamera;
    public GameObject playerBullet;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public GameObject deathWindow;
    // Start is called before the first frame update
    void Start()
    {
        deathWindow.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();

        }
    }

    void Attack()
    {
        RaycastHit Hit;
        Vector3 shootingdirection;
        shootingdirection = playerCamera.transform.forward;
        //Pointed in the shooting of the camera
        GameObject bulletinstance = Instantiate(playerBullet, bulletSpawnPoint.position, Quaternion.LookRotation(shootingdirection));
        bulletinstance.gameObject.SetActive(true);
        Rigidbody rb = bulletinstance.GetComponent<Rigidbody>();
        rb.velocity = shootingdirection * bulletSpeed;
        Bullet bulletScirpt = bulletinstance.GetComponent<Bullet>();
        bulletScirpt.damage = playerDamage;
    }

    public void TakeDamage(float damageAmount)
    {
        //
        playerHealth = Mathf.Max(playerHealth - damageAmount, 0);
        if (playerHealth <= 0)
        {
            Die();
        }
    }
   void Die()
    {
        print("player Died!!!");
        deathWindow.gameObject.SetActive(true);
        Time.timeScale = 0f;


    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }
    public void Exit()
    {
        Application.Quit();
    }

}
