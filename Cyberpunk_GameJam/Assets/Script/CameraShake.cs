using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //private static CameraShake instance;
    //public static CameraShake Instance => instance;
    //private void Awake()
    //{
    //    instance = this;
    //}
    public CinemachineImpulseSource impulseSource;

    
    /// <summary>
    /// Camera Shake
    /// </summary>
    public CinemachineVirtualCamera virtualCamera;
    public float shakeIntensity = 1f;
    public float shakeTime = 0.2f;

    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //StopShake();
    }

    // Update is called once per frame
    void Update()
    {
        //if (shakeTimer > 0)
        //{
        //    shakeTimer -= Time.deltaTime;
        //    if (shakeTimer <= 0)
        //    {
        //        StopShake();
        //    }
        //}
    }
    public void PlayerShakeAnimation()
    {
        Debug.Log("ShakeCamera");
        impulseSource.GenerateImpulse();
    }

    public void ShakeCamera()
    {

        _cbmcp.m_AmplitudeGain = shakeIntensity;
        shakeTimer = shakeTime;

    }

    void StopShake()
    {
        _cbmcp.m_AmplitudeGain = 0f;
        shakeTimer = 0;
    }
}
