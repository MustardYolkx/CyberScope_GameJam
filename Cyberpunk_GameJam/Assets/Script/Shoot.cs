using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private GameManager gameManager;
    public CameraShake camShake;


    public Vector2 mousePos;
    public float followSmooth;
    public Camera cam;

    //cam zoom
    public Camera camZoom;
    public float zoomSpeed;
    public float zoomMin;
    public float zoomMax;

    public Transform startPoint;                     //���߷������
    private float initialHight = 0;                  //���߿�ʼ����ĳ�ʼ�߶�
    public float initialVelocity = 0;                //��ʼ�ٶ�
    private float velocity_Horizontal, velocity_Vertical;  //ˮƽ���ٶȺʹ�ֱ���ٶ�
    private float includeAngle = 0;                  //��ˮƽ����ļн�
    private float totalTime = 0;                     //�׳�����ص���ʱ��
    private float timeStep = 0;                      //ʱ�䲽��

    private LineRenderer line;
    [SerializeField] private float lineWidth = 0.01f;
    [SerializeField] private Material lineMaterial;
    private RaycastHit hits;

    [Range(2, 1000)] public int line_Accuracy = 10;   //���ߵľ��ȣ��յ�ĸ���)
    private float grivaty = 9.8f;
    private int symle = 1;                           //ȷ�����µķ���
    private Vector3 parabolaPos = Vector3.zero;      //�����ߵ�����
    private Vector3 lastCheckPos, currentCheckPos;   //��һ���͵�ǰһ�����������
    private Vector3 checkPointPosition;              //����ķ�������
    private Vector3[] checkPointPos;                 //�������������
    private float timer = 0;                         //�ۼ�ʱ��
    private int lineCount = 0;
    // Start is called before the first frame update

    public float timeDuration;
    public float bulletSpeed;

    //Scope Shake
    public GameObject followMouseObj;
    public GameObject scope;
    public AnimationCurve animCurve;
    public GameObject shakingPos;
    public float duration = 1.0f;
    public float elapsedTime = 0.0f;
    private bool isShaking;

    //ShootingEffect

    public GameObject spark;
    //HitEffect

    public LayerMask shootVfxLayer;
    public LayerMask targetLayer;
    public GameObject hitParticleEffect;
    public GameObject windowBrokenVFX;
    public GameObject targetBrokenVFX;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //cam = FindObjectOfType<Camera>();
        zoomMax = camZoom.orthographicSize;
        if (!this.GetComponent<LineRenderer>())
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.material = lineMaterial;
        }
        camShake = FindObjectOfType<CameraShake>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startPoint == null)
        {
            return;
        }
        //Shooting();
        mousePos = new Vector2(cam.ScreenPointToRay(Input.mousePosition).origin.x, cam.ScreenPointToRay(Input.mousePosition).origin.y);
        
        MouseFollow();
        Shooting();
        ScopeScroll();
        ScopeShaking();
        //timeDuration += Time.deltaTime;
    }
    public void ScopeShaking()
    {

        if (isShaking)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
            {
                elapsedTime = duration;
                isShaking = false;
            }

            float movementFactor = animCurve.Evaluate(elapsedTime / duration);
            scope.transform.localPosition = Vector3.Lerp(new Vector3(0,0,0), new Vector3(shakingPos.transform.localPosition.x, shakingPos.transform.localPosition.y,0), movementFactor);

        }

    }
    public void MouseFollow()
    {
        if (Vector2.Distance(startPoint.transform.position, mousePos) > 0.1f)
        {
            followMouseObj.transform.position = Vector2.Lerp(followMouseObj.transform.position, mousePos, Time.deltaTime * followSmooth);
        }
    }
    public void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            elapsedTime = 0;
            isShaking = true;
            camShake.PlayerShakeAnimation();
            //camShake.ShakeCamera();
            StartCoroutine(ShootingEffect());
            Calculation_parabola();
        }
    }
    IEnumerator ShootingEffect()
    {
        spark.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        spark.SetActive(false);
    }
    public void ScopeScroll()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue != 0)
        {
            camZoom.orthographicSize += scrollValue * Time.deltaTime * zoomSpeed;
        }

        if (camZoom.orthographicSize < zoomMin)
        {
            camZoom.orthographicSize = zoomMin;
        }


        if (camZoom.orthographicSize > zoomMax)
        {
            camZoom.orthographicSize = zoomMax;
        }

    }

    private void Calculation_parabola()
    {
        List<Ray> ray_List = new List<Ray>(); 
        ray_List.Clear();
        velocity_Horizontal = initialVelocity * Mathf.Cos(includeAngle);
        velocity_Vertical = initialVelocity * Mathf.Sin(includeAngle);
        initialHight = Mathf.Abs(startPoint.transform.position.y);
        float time_1 = velocity_Vertical / grivaty;
        float time_2 = Mathf.Sqrt((time_1 * time_1) + (2 * initialHight) / grivaty);
        totalTime = time_1 + time_2;
        timeStep = totalTime / line_Accuracy;
        includeAngle = Vector3.Angle(startPoint.forward, Vector3.ProjectOnPlane(startPoint.forward, Vector3.up)) * Mathf.Deg2Rad;
        symle = (startPoint.position + startPoint.forward).y > startPoint.position.y ? 1 : -1;

        if (checkPointPos == null || checkPointPos.Length != line_Accuracy)
        {
            checkPointPos = new Vector3[line_Accuracy];
        }
        for (int i = 0; i < line_Accuracy; i++)
        {                     
            if (i == 0)
            {
                lastCheckPos = startPoint.position - startPoint.forward;
            }
            parabolaPos.z = velocity_Horizontal * timer;
            parabolaPos.y = velocity_Vertical * timer * symle + (-grivaty * timer * timer) / 2;
            currentCheckPos = startPoint.position + Quaternion.AngleAxis(startPoint.eulerAngles.y, Vector3.up) * parabolaPos;
            checkPointPosition = currentCheckPos - lastCheckPos;
            lineCount = i + 1;
            if (Physics.Raycast(lastCheckPos, checkPointPosition, out hits, checkPointPosition.magnitude + 3))
            {
                checkPointPosition = hits.point - lastCheckPos;
                checkPointPos[i] = hits.point;

                //point.SetActive(true);
                //point.transform.position = hits.point;
                //point.transform.localScale = Vector3.one / 3;
                //point.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                //if (hits.transform == null)
                //{
                //    point.SetActive(false);

                //}
            }

            Ray collideRay = new Ray(lastCheckPos, checkPointPosition * 1);
            ray_List.Add(collideRay);
            Debug.DrawRay(lastCheckPos, checkPointPosition * 1, Color.blue, 1f);

            //if(!isShootTarget)
            //{
            //    StartCoroutine(BulletFlyTarget(timeDuration, collideRay));
            //}
            //StartCoroutine(BulletFlyVFX(timeDuration, collideRay));
            //Debug.DrawLine(lastCheckPos, currentCheckPos,Color.red,1f);

            //bool isShoot = Physics.Raycast(collideRay, out RaycastHit hitTargetinfo, 1);

            //bool isCollide = Physics.Raycast(collideRay, out RaycastHit hitinfo, 1, shootVfxLayer);
            //if (isCollide)
            //{
            //    WindowCollider windows = hitinfo.collider.gameObject.GetComponent<WindowCollider>();
            //    TargetCollider targetCol = hitinfo.collider.gameObject.GetComponent<TargetCollider>();
            //    if (windows != null)
            //    {
            //        Instantiate(windowBrokenVFX, hitinfo.point, Quaternion.identity);
            //    }

            //    if (targetCol != null)
            //    {
            //        Debug.Log("collide");
            //        GameObject targetColVFX = Instantiate(targetBrokenVFX, hitinfo.transform);
            //        targetColVFX.transform.position = hitinfo.point;
            //    }
            //}

            //if (isShoot)
            //{

            //    Target_Info target = hitTargetinfo.collider.gameObject.GetComponent<Target_Info>();
            //    if (target != null)
            //    {
            //        Debug.Log(target.score);
            //        target.Shoot();
            //        if (target.GetComponentInParent<Level2TargetMovement>() != null)
            //        {
                        
            //            target.GetComponentInParent<Level2TargetMovement>().StopMovement();
            //        }
            //        //Destroy(target.gameObject);
            //        break;
            //    }

            //}

                checkPointPos[i] = currentCheckPos;
                lastCheckPos = currentCheckPos;
                timer += timeStep;
                   
        }
        StartCoroutine(DetectDelay(ray_List));
        line.positionCount = lineCount;
        line.SetPositions(checkPointPos);
        timer = 0;
    }

        
    IEnumerator DetectDelay(List<Ray> collideRay)
    {
        int count = 0;
        for(int i = 0;i < collideRay.Count; i++)
        {
            
            bool isCollide = Physics.Raycast(collideRay[i], out RaycastHit hitinfo, 1, shootVfxLayer);
            if (isCollide&&count ==0)
            {
                WindowCollider windows = hitinfo.collider.gameObject.GetComponent<WindowCollider>();
                TargetCollider targetCol = hitinfo.collider.gameObject.GetComponent<TargetCollider>();
                if (windows != null)
                {
                    Instantiate(windowBrokenVFX, hitinfo.point, Quaternion.identity);
                    Instantiate(hitParticleEffect, hitinfo.point, Quaternion.identity);
                    count++;
                }

                if (targetCol != null)
                {
                    Debug.Log("collide");
                    GameObject targetColVFX = Instantiate(targetBrokenVFX, hitinfo.transform);
                    Instantiate(hitParticleEffect, hitinfo.point, Quaternion.identity);
                    
                    targetColVFX.transform.position = hitinfo.point;
                    count++;
                }
            }

            bool isShoot = Physics.Raycast(collideRay[i], out RaycastHit hitTargetinfo, 1);
            //Debug.Log(isShoot);
            if (isShoot)
            {
                
                Target_Info target = hitTargetinfo.collider.gameObject.GetComponent<Target_Info>();
                if (target != null)
                {
                    
                    if (target.GetComponentInParent<Level2TargetMovement>() != null)
                    {
                        target.GetComponentInParent<Level2TargetMovement>().StopMovement();
                    }
                    if (target.GetComponentInParent<Level3TargetMovement>() != null)
                    {
                        target.GetComponentInParent<Level3TargetMovement>().StopMovement();
                    }
                    if(target.GetComponentInParent<Level3Target>() != null)
                    {
                        if (target.GetComponentInParent<Level3Target>().isTarget1)
                        {
                            target.GetComponentInParent<Level3Target>().TriggerTarget2();
                        }
                    }
                    target.Shoot();

                    Instantiate(hitParticleEffect, hitTargetinfo.point, Quaternion.identity);
                    
                    gameManager.GenerateHitInfoPanel(hitTargetinfo.point, target.score,target.bodyPart);
                    
                    
                    
                    break;
                    //isShootTarget = true;
                    //Destroy(target.gameObject);

                }
            }
            yield return new WaitForSeconds(timeDuration);
        }
        
    }
    
}
