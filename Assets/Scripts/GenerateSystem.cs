using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GenerateSystem : MonoBehaviour
{
    
    public GameObject sun;
    private GameObject _sun; 
    [SerializeField] private List<CelestialBody> _celestialBodies;
    [SerializeField] private List<GameObject> templatePlanets;
    [SerializeField] private List<Planet> planets;
    [SerializeField] private OrbitDebugDisplay _orbitDebugDisplay;
    private NBodySimulation _nBodySimulation;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            DeleteSystem();
            Generate();
        }
    }

    private void Awake()
    {
        _nBodySimulation = FindObjectOfType<NBodySimulation>();
        Generate();
    }


    private void OnValidate()
    {
        if(planets.Count!=templatePlanets.Count)
            Debug.LogError("planets and templatePLanets must be same size");
    }

    private void Generate()
    {
        _sun = Instantiate(sun, Vector3.zero, Quaternion.identity, transform);
        var celBody = _sun.GetComponent<CelestialBody>();
        _orbitDebugDisplay.centralBody = celBody;
        _nBodySimulation.bodies.Add(celBody);
        _celestialBodies.Add(celBody);
        
        for (int index = 0; index < planets.Count; index++)
        {
            var i = templatePlanets[index];
            var g = Instantiate(i, transform);
            CelestialBody celestialBody = g.GetComponent<CelestialBody>();
            _celestialBodies.Add(celestialBody);
            _nBodySimulation.bodies.Add(celestialBody);
            celestialBody.initialVelocity = planets[index].speed;
            celestialBody.surfaceGravity = planets[index].surfaceGravity;
            celestialBody.radius = planets[index].radius;
            g.transform.position=new Vector3(planets[index].offset,0,0);
        }
    }

    private void DeleteSystem()
    {
        for (int index = 0; index < _celestialBodies.Count; index++)
        {
            _nBodySimulation.bodies.Remove(_celestialBodies[index]);
            Destroy(_celestialBodies[index].gameObject);
        }
        _celestialBodies.Clear();
        
    }
}