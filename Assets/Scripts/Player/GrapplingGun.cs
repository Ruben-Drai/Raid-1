using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private LayerMask mask;

    private Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    [HideInInspector] public Vector2 Direction;
    [HideInInspector] public bool FistOnly;

    private bool IsAiming = false;
    [HideInInspector] public bool HasShot;
    private Vector2 AimPoint;
    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_camera = Camera.main;

    }
    public void ActivateHook()
    {
        gameObject.SetActive(true);
        grappleRope.m_Fist.gameObject.SetActive(true);
        IsAiming = true;
    }
    public void ReturnHook()
    {
        grappleRope.isReturning = true;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1;
        IsAiming = false;
    }
    public void FireHook()
    {
        HasShot = true;
        IsAiming = false;
        SetGrapplePoint();

        if (launchToPoint && grappleRope.isGrappling)
        {
            if (launchType == LaunchType.Transform_Launch)
            {
                Vector2 firePointDistance = firePoint.position - gunHolder.localPosition;
                Vector2 targetPos = grapplePoint - firePointDistance;
                gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
            }
        }
    }

    private void Update()
    {
        if (IsAiming)
        {
            if (PlayerController.instance.Controller.currentControlScheme == "K&M")
                AimPoint = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            else
                AimPoint = new(PlayerController.instance.GamePadAimDirection.x + gunPivot.position.x, PlayerController.instance.GamePadAimDirection.y + gunPivot.position.y);

            RotateGun(AimPoint);

        }
        else if (!HasShot)
        {
            gunPivot.rotation = Quaternion.Slerp(gunPivot.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.down), Time.deltaTime * rotationSpeed);
            if (gunPivot.rotation.eulerAngles.z == Quaternion.LookRotation(Vector3.forward, Vector2.down).eulerAngles.z)
            {
                gameObject.SetActive(false);
                grappleRope.m_Fist.gameObject.SetActive(false);
            }

        }
        else
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint);
            }
        }
    }

    void RotateGun(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        gunPivot.rotation = Quaternion.Slerp(gunPivot.rotation, Quaternion.LookRotation(Vector3.forward, distanceVector), Time.deltaTime * rotationSpeed);
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = new(AimPoint.x - gunPivot.position.x, AimPoint.y - gunPivot.position.y);
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {

            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, 500, mask);
            if (_hit == true && _hit.collider.CompareTag("Fistable"))
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    FistOnly = !PlayerController.instance.UnlockedUpgrades["Hook"] || _hit.collider.GetComponent<Interactible>() != null;
                    grapplePoint = _hit.point;
                    if (FistOnly) 
                        grapplePoint += distanceVector.normalized / 2f;

                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
                else
                {
                    HasShot = false;
                    IsAiming = false;
                }
            }
            else
            {
                HasShot = false;
                IsAiming = false;
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }
}