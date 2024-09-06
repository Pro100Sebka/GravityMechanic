using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    [SerializeField] private Material _sharedMaterial; 
    private MaterialPropertyBlock _propertyBlock;
    private Renderer _renderer;

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();

        GenerateColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateColor();
        }
    }

    private void GenerateColor()
    {

        Color mainColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color secondColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color atmosphereColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        _propertyBlock.SetFloat("_SecondValue", Random.Range(-0.5f, 0f));
        _propertyBlock.SetFloat("Vector1_B49E724D_1", Random.Range(30f, 500f));
        _propertyBlock.SetColor("Color_38CAF3C8", mainColor);
        _propertyBlock.SetColor("_SecondColor", secondColor);


        
            //        _renderer.GetPropertyBlock(_propertyBlock, 1); 
        _propertyBlock.SetColor("_Color", atmosphereColor);
        
        _renderer.SetPropertyBlock(_propertyBlock);
        //_renderer.SetPropertyBlock(_propertyBlock, 1);
    }
}