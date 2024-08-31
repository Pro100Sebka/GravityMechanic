using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextureGenerator : MonoBehaviour
{
    [SerializeField]private Material _material,_atmosphere;
    private Color _mainColor, secondColor;
    private float secondValue, secondNoiseScale;
    
    
    private void Start()
    {
        GenerateColor();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))GenerateColor();
    }

    private void GenerateColor()
    {
        
        _material.SetFloat("_SecondValue", Random.Range(-0.5f, 1f));
        _material.SetFloat("Vector1_B49E724D_1", Random.Range(30f, 500f));
        
        var newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _material.SetColor("Color_38CAF3C8", newColor);
        
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _material.SetColor("_SecondColor", newColor);
        
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _atmosphere.SetColor("_Color", newColor);
    }
}
