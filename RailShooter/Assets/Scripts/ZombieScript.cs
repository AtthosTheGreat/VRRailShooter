using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 100;

    [SerializeField]
    private float killDistance = 3f;

    [SerializeField]
    private float spawnUndergroundValue = 2f;

    [SerializeField]
    private int risingSteps = 30;

    [SerializeField]
    private float risingTime = 1f;

    [SerializeField]
    private float movementSpeed = 1f;

    private int hp;

    private ControllerShooting playerController;

    private void Start()
    {
        hp = maxHP;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ControllerShooting>();
        transform.position = new Vector3(transform.position.x, transform.position.y - spawnUndergroundValue, transform.position.z);

        StartCoroutine(RiseFromTheGround());
    }

    private IEnumerator RiseFromTheGround()
    {
        var wait = new WaitForSeconds(risingTime / risingSteps);
        var step = spawnUndergroundValue / risingSteps;

        for(int i = 0; i < risingSteps; i++)
        {
            var pos = transform.position;
            pos.y += step;
            transform.position = pos;
            yield return wait;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) < killDistance)
        {
            playerController.PlayerDeath();
        }

        var moveDir = playerController.transform.position - transform.position;
        moveDir.y = 0;
        moveDir.Normalize();

        transform.parent.forward = moveDir;
        Debug.DrawRay(transform.parent.position, transform.parent.forward);
        transform.parent.position += moveDir * Time.deltaTime * movementSpeed;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
