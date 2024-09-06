using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorGenerator : MonoBehaviour
{
    private Material _material;

    private void Start()
    {
        _material = GetComponentInChildren<MeshRenderer>().material;
        _material.SetColor("_Color", new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _material = GetComponentInChildren<MeshRenderer>().material;
            _material.SetColor("_Color", new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)));
        }
    }
}
