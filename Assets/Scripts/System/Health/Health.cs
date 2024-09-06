using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private NBodySimulation _nBodySimulation; 
        
    public float _localHealth;
    private float _destroyenes = 0;
    private float _destroyenesAtmosphere;
    private bool _canDamage = true;
    private Material _material,_atmosphere;
    [SerializeField] private GameObject _explodeParticles;
    [SerializeField] private GameObject _explodeSource;
    [SerializeField] private GameObject _sphere;
    [SerializeField] private GameObject _atmosphereGameObject;
    
    [Header("Time To Destroy Works Only With Using Animation")]
    [SerializeField] private bool _usingAnimation;
    [SerializeField] private float _timeToDestroy;


    private void Awake()
    {
        _nBodySimulation = FindObjectOfType<NBodySimulation>();
    }

    public void Damage(float damage)
    {
        _localHealth -= damage;
        if (_localHealth <= 0 && _canDamage)
        {
            _canDamage = false;
            
            Instantiate(_explodeSource, transform.position, transform.rotation);
            Instantiate(_explodeParticles, transform.position, transform.rotation);
            if (_usingAnimation) StartCoroutine(Animate());
            else
            {
                _nBodySimulation.bodies.Remove(GetComponent<CelestialBody>());
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Animate()
    {
        _material = _sphere.GetComponent<MeshRenderer>().material;
        _material.SetFloat("Vector1_7C536670", _destroyenes);

        _atmosphere = _atmosphereGameObject.GetComponent<MeshRenderer>().material;
        _atmosphere.SetFloat("_Transparancy",0.5f - _destroyenes);
        var currentTime = 0f;
        
        while (currentTime < _timeToDestroy)
        {
            _destroyenes = Mathf.InverseLerp(0,_timeToDestroy,currentTime);
            _material.SetFloat("Vector1_7C536670", _destroyenes);
            _atmosphere.SetFloat("_Transparancy",0.5f - _destroyenes );
            currentTime += Time.deltaTime;
            yield return null;
        }
        _nBodySimulation.bodies.Remove(GetComponent<CelestialBody>());
        Destroy(gameObject);
    }
}
