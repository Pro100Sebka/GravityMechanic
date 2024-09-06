using System.Collections;
using UnityEngine;
using UnityEngine.UI;
    
public class SpaceShip : MonoBehaviour
{
    [Header("______________Stats______________")]
    [SerializeField] private float _laserForce;
    [SerializeField] private float _laserFireRate;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _rollingSpeed;   
    [SerializeField] private float _laserBeamRange;
    [SerializeField] private float _laserBeamFireRate;
    [SerializeField] private float _timeToOverheat;
    [SerializeField] private float _coolDownRate;

    [Header("______________Other______________")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private Transform _duloTransform;
    [SerializeField] private AudioSource _laserAudioSource;
    [SerializeField] private GameObject _muzleParticle;
    [SerializeField] private Slider _overheatSlider;
    [SerializeField] private Material _duloMaterial;
    [SerializeField] private GameObject _overheatAlarmUI;
    private bool _canFire = true;
    private bool _overheated = false;
    private LineRenderer _lineRenderer;
    private float overheat;
    private NBodySimulation _nBodySimulation;

    private void Start()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _nBodySimulation = FindObjectOfType<NBodySimulation>();
        overheat = 0f;
        UpdateOverheatUI();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !_overheated && _canFire) { FireLaser(); }
        
        if (Input.GetMouseButton(2) && !_overheated && _canFire) { FireLaserBeam(); }
        else {
                _lineRenderer.gameObject.SetActive(false);
                CoolDownLaser(); 
        }

       
        
        if (Input.GetKey(KeyCode.W)) { AddingForce(_transformCamera.forward); }
        if (Input.GetKey(KeyCode.S)) { AddingForce(-_transformCamera.forward); }
        if (Input.GetKey(KeyCode.Space)) { AddingForce(_transformCamera.up); }
        if (Input.GetKey(KeyCode.LeftControl)) { AddingForce(-_transformCamera.up); }
        if (Input.GetKey(KeyCode.A)) { AddingForce(-_transformCamera.right); }
        if (Input.GetKey(KeyCode.D)) { AddingForce(_transformCamera.right); }

        Quaternion targetRotation = Quaternion.LookRotation(_transformCamera.forward);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _rotatingSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 gravity = _nBodySimulation.CalculateAcceleration(_rb.position);
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void AddingForce(Vector3 direction)
    {
        Vector3 forceDirection = direction * (_speed / 100);
        _rb.AddForce(forceDirection, ForceMode.Impulse);
    }

    private void FireLaser()
    {
        overheat += 0.1f;
        Instantiate(_muzleParticle, _duloTransform.position, transform.rotation, transform);
        GameObject laser = Instantiate(_laserPrefab, _duloTransform.position, _duloTransform.rotation);
        Rigidbody laserRb = laser.GetComponent<Rigidbody>();
        laserRb.velocity = _rb.velocity;
        Laser laserScript = laser.GetComponent<Laser>();
        if (laserScript) { laserScript.Fire(_duloTransform, _laserForce); _laserAudioSource.Play(); }
        UpdateOverheatUI();
        if (overheat >= 1)
        {
            _overheated = true;
            _canFire = false;
            StartCoroutine(OverHeatAlarm());
        }
        StartCoroutine(Pause(_laserFireRate));
    }

    private void FireLaserBeam()
    {
        _lineRenderer.gameObject.SetActive(true);
        _lineRenderer.SetPosition(0, _duloTransform.position);

        if (Physics.Raycast(_duloTransform.position, _camera.transform.forward, out RaycastHit hit))
        {
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.TryGetComponent(out Health health))
            {
                health.Damage(0.01f);
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, _duloTransform.position + _transformCamera.forward * _laserBeamRange);
        }
        
        overheat += Time.deltaTime / _timeToOverheat;
        overheat = Mathf.Clamp01(overheat);
        UpdateOverheatUI();

        if (overheat >= 1)
        {
            _overheated = true;
            _canFire = false;
            _lineRenderer.gameObject.SetActive(false);
            StartCoroutine(OverHeatAlarm());
        }
        
        _duloMaterial.SetFloat("_Value", overheat);
    }

    public void sum(ref int a, int b, out int summa)
    {
        a = 10;
        summa = a + b;
    }
    private void CoolDownLaser()
    {
        //play sound
        if (overheat > 0)
        {
            overheat -= Time.deltaTime * _coolDownRate;
            overheat = Mathf.Clamp01(overheat);
            UpdateOverheatUI();
            _duloMaterial.SetFloat("_Value", overheat);
        }

        if (_overheated && overheat <= 0)
        {
            _overheated = false;
            _canFire = true;
        }
    }

    private void UpdateOverheatUI()
    {
        _overheatSlider.value = overheat;
    }

    IEnumerator Pause(float time)
    {
        _canFire = false;
        yield return new WaitForSeconds(time);
        _canFire = true;
    }

    IEnumerator OverHeatAlarm()
    {
        while (_overheated)
        {
            
            yield return new WaitForSeconds(0.5f);
            _overheatAlarmUI.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _overheatAlarmUI.SetActive(false);
        }
        
        _overheatAlarmUI.SetActive(false);
    }
}
