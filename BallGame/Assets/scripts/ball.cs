using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private float _speed = 30f;

    Rigidbody _rigidbody;
    Vector3 _velocity;
    Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _renderer = GetComponent<Renderer>();
        Invoke("delay", 0.5f);
    }


    void delay()
    {
        _rigidbody.velocity = Vector3.up * _speed;    //Delay the ball movement by sending it upwards first
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;
        if(!_renderer.isVisible)
        {
            GameManager.Instance.Ball--;      //Destroying ball when not visible and substracting the count
            Destroy(gameObject);
        }

    }




    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
