using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GenerateSystem : MonoBehaviour
{
    
    public GameObject sun;
    [SerializeField] private List<GameObject> templatePlanets;
    [SerializeField] private List<Planet> planets;
    [SerializeField] private OrbitDebugDisplay _orbitDebugDisplay;

    private void Awake()
    {
        var gm = Instantiate(sun, Vector3.zero, Quaternion.identity, transform);
        _orbitDebugDisplay.centralBody = gm.GetComponent<CelestialBody>();

        for (int index = 0; index < 10; index++)
        {
            var i = templatePlanets[index];
            var g = Instantiate(i);
            CelestialBody celestialBody = g.GetComponent<CelestialBody>();
            celestialBody.initialVelocity = planets[index].speed;
            celestialBody.surfaceGravity = planets[index].surfaceGravity;
            celestialBody.radius = planets[index].radius;
            g.transform.position=new Vector3(planets[index].offset,0,0);
        }
    }
}
