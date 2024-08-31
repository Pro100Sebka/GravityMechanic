using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotating : MonoBehaviour
{ 
    [SerializeField] private Transform target; // Цель, вокруг которой будет вращаться камера
    [SerializeField] private float distance; // Расстояние от камеры до цели
    [SerializeField] private float ydistance;
    [SerializeField] private float xSpeed; // Скорость вращения по оси X
    [SerializeField] private float ySpeed; // Скорость вращения по оси Y

    [SerializeField] private float yMinLimit; // Минимальный угол поворота по оси Y
    [SerializeField] private float yMaxLimit; // Максимальный угол поворота по оси Y

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(1)) // Вращение камеры по правой кнопке мыши
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }
            
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, ydistance, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
