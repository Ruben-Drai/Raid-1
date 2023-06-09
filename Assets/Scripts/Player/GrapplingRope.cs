using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("General Refernces:")]
    public GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;
    public Transform m_Fist;

    [Header("General Settings:")]
    [SerializeField] private int precision = 40;
    [Range(0, 20)][SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)][SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField][Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;
    [HideInInspector] public bool isReturning = false;

    bool strightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        strightLine = false;

        LinePointsToFirePoint();

        if (!grapplingGun.FistOnly)
            m_lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        if (!grapplingGun.FistOnly)
            m_lineRenderer.enabled = false;

        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update()
    {

        if (!isReturning && !isGrappling)
            moveTime += Time.deltaTime;
        else if (isReturning)
            moveTime -= Time.deltaTime;

        DrawRope();
    }
    void DrawRope()
    {
        if (!isReturning)
        {
            if (!strightLine)
            {//problem here with it being reset before the shot
                if (m_lineRenderer.GetPosition(precision - 1).x == grapplingGun.grapplePoint.x
                    || (grapplingGun.FistOnly && Vector2.Distance(m_Fist.position, grapplingGun.grapplePoint) < 0.1f))
                {
                    strightLine = true;
                    if (grapplingGun.FistOnly)
                        isReturning = true;
                }
                else
                {
                    DrawRopeWaves();
                }
            }
            else
            {
                if (!isGrappling)
                {
                    if (!grapplingGun.FistOnly)
                        grapplingGun.Grapple();

                    isGrappling = true;
                }
                if (waveSize > 0)
                {
                    waveSize -= Time.deltaTime * straightenLineSpeed;
                    DrawRopeWaves();
                }
                else
                {
                    waveSize = 0;

                    if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }

                    DrawRopeNoWaves();
                }
            }

        }
        else
        {
            ReturnRope();
            if (Vector2.Distance(m_Fist.position, grapplingGun.firePoint.position) < 0.1f)
            {
                isReturning = false;
                enabled = false;
                m_Fist.parent = transform.parent.parent;
                m_Fist.gameObject.SetActive(false);
                grapplingGun.HasShot = false;
            }
        }

    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);

            m_Fist.position = currentPosition;
            m_Fist.transform.parent = null;
        }
    }
    public void ReturnRope()
    {
        m_lineRenderer.positionCount = precision;
        for (int i = precision - 1; i >= 0; i--)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.grapplePoint, grapplingGun.firePoint.position, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);

            m_Fist.position = currentPosition;
            m_Fist.transform.parent = null;
        }
    }

    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}