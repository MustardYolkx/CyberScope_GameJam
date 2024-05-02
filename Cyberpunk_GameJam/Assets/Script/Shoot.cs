using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private GameManager gameManager;
    public CameraShake camShake;
    //带有碰撞体的LineRenderer, 要有PolygonCollider2D、LineRenderer组件, 
    [SerializeField] GameObject colliderLinePrefab;

    public Vector2 mousePos;
    public float followSmooth;
    public Camera cam;

    //cam zoom
    public Camera camZoom;
    public float zoomSpeed;
    public float zoomMin;
    public float zoomMax;

    public Transform startPoint;                     //射线发射起点
    private float initialHight = 0;                  //射线开始发射的初始高度
    public float initialVelocity = 0;                //初始速度
    private float velocity_Horizontal, velocity_Vertical;  //水平分速度和垂直分速度
    private float includeAngle = 0;                  //与水平方向的夹角
    private float totalTime = 0;                     //抛出到落地的总时间
    private float timeStep = 0;                      //时间步长

    private LineRenderer line;
    [SerializeField] private float lineWidth = 0.01f;
    [SerializeField] private Material lineMaterial;
    private RaycastHit hits;

    [Range(2, 1000)] public int line_Accuracy = 10;   //射线的精度（拐点的个数)
    private float grivaty = 9.8f;
    private int symle = 1;                           //确定上下的符合
    private Vector3 parabolaPos = Vector3.zero;      //抛物线的坐标
    private Vector3 lastCheckPos, currentCheckPos;   //上一个和当前一个监测点的坐标
    private Vector3 checkPointPosition;              //监测点的方向向量
    private Vector3[] checkPointPos;                 //监测点的坐标数组
    private float timer = 0;                         //累计时间
    private int lineCount = 0;
    // Start is called before the first frame update

    public float timeDuration;
    public float bulletSpeed;
    //ShootEffect

    public bool isShootTarget;
    public LayerMask shootVfxLayer;
    public LayerMask targetLayer;
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
        
        //timeDuration += Time.deltaTime;
    }

    public void MouseFollow()
    {
        if (Vector2.Distance(startPoint.transform.position, mousePos) > 0.1f)
        {
            startPoint.transform.position = Vector2.Lerp(startPoint.transform.position, mousePos, Time.deltaTime * followSmooth);
        }
    }
    public void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShootTarget = false;
            camShake.PlayerShakeAnimation();
            //camShake.ShakeCamera();
            Calculation_parabola();
        }
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
                    count++;
                }

                if (targetCol != null)
                {
                    Debug.Log("collide");
                    GameObject targetColVFX = Instantiate(targetBrokenVFX, hitinfo.transform);
                    targetColVFX.transform.position = hitinfo.point;
                    count++;
                }
            }

            bool isShoot = Physics.Raycast(collideRay[i], out RaycastHit hitTargetinfo, 1);

            if (isShoot)
            {

                Target_Info target = hitTargetinfo.collider.gameObject.GetComponent<Target_Info>();
                if (target != null)
                {
                    Debug.Log(target.score);
                    target.Shoot();
                    if (target.GetComponentInParent<Level2TargetMovement>() != null)
                    {
                        target.GetComponentInParent<Level2TargetMovement>().StopMovement();
                        break;
                    }
                    //isShootTarget = true;
                    //Destroy(target.gameObject);

                }
            }
            yield return new WaitForSeconds(timeDuration);
        }
        
    }
    IEnumerator BulletFlyVFX(float time,Ray collideRay)
    {
        yield return new WaitForSeconds(time);
        

        bool isCollide = Physics.Raycast(collideRay, out RaycastHit hitinfo, 1, shootVfxLayer);
        if (isCollide)
        {
            WindowCollider windows = hitinfo.collider.gameObject.GetComponent<WindowCollider>();
            TargetCollider targetCol = hitinfo.collider.gameObject.GetComponent<TargetCollider>();
            if (windows != null)
            {
                Instantiate(windowBrokenVFX, hitinfo.point, Quaternion.identity);
            }

            if (targetCol != null)
            {
                Debug.Log("collide");
                GameObject targetColVFX = Instantiate(targetBrokenVFX, hitinfo.transform);
                targetColVFX.transform.position = hitinfo.point;
            }
        }

    }
    IEnumerator BulletFlyTarget(float time, Ray collideRay)
    {

            yield return new WaitForSeconds(time);
            bool isShoot = Physics.Raycast(collideRay, out RaycastHit hitTargetinfo, 1);

            if (isShoot)
            {
                
                Target_Info target = hitTargetinfo.collider.gameObject.GetComponent<Target_Info>();
                if (target != null)
                {
                    Debug.Log(target.score);
                    target.Shoot();
                    if (target.GetComponentInParent<Level2TargetMovement>() != null)
                    {

                        target.GetComponentInParent<Level2TargetMovement>().StopMovement();
                    }
                    isShootTarget = true;
                    //Destroy(target.gameObject);

                }

            
        }
        
    }
}
