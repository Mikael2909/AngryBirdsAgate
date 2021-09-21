using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle,thrown,HitSomething}
    public GameObject Parent;
    public Rigidbody2D Rigidbody;
    public CircleCollider2D Collider;
    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };
    public BirdState State { get { return _state; } }
    private BirdState _state;
    private float _minVelocity = 0.5f;
    private bool _flagDestroy = false;
    public GameObject explosionEffect;
   
    // Start is called before the first frame update
    void OnDestroy()
    {
        if (_state == BirdState.thrown || _state == BirdState.HitSomething)
        {
            OnBirdDestroyed();
           
        }
    }
    void Start()
    {
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        _state = BirdState.Idle;
        
    }
     void OnCollisionEnter2D(Collision2D col)
    {
        _state = BirdState.HitSomething;
        Instantiate(explosionEffect, transform.position, transform.rotation);
        
    }
    void FixedUpdate()
    {
        if (_state == BirdState.Idle && Rigidbody.velocity.sqrMagnitude >= _minVelocity)         
        {
            _state = BirdState.thrown;
        }
        if(_state==BirdState.thrown || _state==BirdState.HitSomething &&  Rigidbody.velocity.sqrMagnitude<_minVelocity && !_flagDestroy)
        {
            //hancurkan game objek setelah 2 detik
            _flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }
    private IEnumerator DestroyAfter(float second)
    {
       
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }
    public void MoveTo(Vector2 target,GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }
    public void Shoot(Vector2 velocity,float distance, float speed)
    {
        Collider.enabled = true;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.velocity = velocity * speed * distance;
        OnBirdShot(this);
    }
    public virtual void OnTap()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
