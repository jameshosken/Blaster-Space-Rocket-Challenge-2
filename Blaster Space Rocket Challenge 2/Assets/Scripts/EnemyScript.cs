using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {


    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreValue = 7;

    [SerializeField] int health = 100;
    [SerializeField] bool addCollider = true;

    ScoreBoard scoreBoard;

    // Use this for initialization
    void Start () {
        if (addCollider)
        {
            AddNonTriggerBoxCollider();
        }
        scoreBoard = FindObjectOfType<ScoreBoard>();
	}
	
	void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
        if(health <= 0)
        {
            OnDeath();
        }
    }
    void ProcessHit(GameObject otherWeapon)
    {
        int damage = otherWeapon.GetComponent<WeaponScript>().GetDamage();
        health -= damage;

    }

    void OnDeath()
    {
        scoreBoard.ScoreHit(scoreValue);
        GameObject fx = Instantiate(deathFX, transform.position, Random.rotation);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
