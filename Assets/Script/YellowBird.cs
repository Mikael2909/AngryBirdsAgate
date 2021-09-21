using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    [SerializeField]
    public float _boostForce = 100;
    public bool _hasBoost = false;
    // Start is called before the first frame update
    public void Boost()
    {
        if (State == BirdState.thrown && !_hasBoost)
        {
            Rigidbody.AddForce(Rigidbody.velocity * _boostForce);
           
            _hasBoost = true;
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }   
               
    }
    
    public override void OnTap()
    {
        Boost();      
    }
   
}
