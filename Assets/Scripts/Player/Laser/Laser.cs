using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour
{
    [SerializeField] private int _damage,_timeToDelete;
    [SerializeField] private AudioSource _impactSource;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(DeleteAfterTime(_timeToDelete));
    }
    private void OnCollisionEnter(Collision other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health)
        {
            Instantiate(_impactSource, transform.position,transform.rotation);
            health.Damage(_damage);
            Destroy(gameObject);
        }
    }
    
    public void Fire(Transform _duloTransform, float speed)
    {
        Vector3 forceDirection = _duloTransform.forward * speed;
        _rb.AddForce(forceDirection, ForceMode.Impulse);
    }
    
    private void FixedUpdate()
    {
        Vector3 gravity = NBodySimulation.CalculateAcceleration(_rb.position);
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }

    IEnumerator DeleteAfterTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}