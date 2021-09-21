using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };
    private bool _isHit = false;
     void OnDestroy()
    {
        if (_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }
     void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
        if (col.gameObject.tag == "Bird")
        {
            _isHit = true;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            health -= damage;
            if (health <= 0)
            {
                _isHit = true;
                Destroy(gameObject);
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
