using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    protected TurretState currentState;

    public Transform Target { get; set; }

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Vector3 aimOffset;

    [SerializeField]
    private Transform rotator;

    [SerializeField]
    private Transform ghostRotator;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform[] gunBarrels;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Animator animator;


    public Quaternion DefaultRotation { get; set; }

    public Transform Rotator { get => rotator; set => rotator = value; }
    public Vector3 AimOffset { get => aimOffset; set => aimOffset = value; }
    public Transform GhostRotator { get => ghostRotator; set => ghostRotator = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public Transform[] GunBarrels { get => gunBarrels; set => gunBarrels = value; }
    public Animator Animator { get => animator; set => animator = value; }

    public Vector3 directionToTarget;

    public AudioSource AudioLaser; 
    private void Start()
    {
        DefaultRotation = rotator.rotation;
        ChangeState(new IdleState());
        // Initialize LineRenderer
        
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();

            // Set LineRenderer color to red
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;

            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.enabled = true; // Enable LineRenderer by default
        }
        else
        {
            lineRenderer.enabled = true; // Ensure LineRenderer is enabled
        }
    }

    private void Update()
    {
        currentState.Update();
        if (Target != null)
        {
            // Calculate the direction to the target
            directionToTarget = (Target.position - rotator.position).normalized;

            // Visualize the ray towards the target
            //VisualizeRay(directionToTarget);
            

            // Check if the target is within a certain distance (adjust as needed)
            float distanceToTarget = Vector3.Distance(rotator.position, Target.position);
            if (distanceToTarget < 10f)
            {
                
                //VisualizeRay(directionToTarget);
                // Adjust turret rotation to face the target
                RotateTurretTowardsTarget(directionToTarget);
            }
        }
        else
        {
            // Disable LineRenderer when there is no target
            lineRenderer.enabled = false;
        }
    }

    private void VisualizeRay(Vector3 direction)
    {
        // Visualization of the ray
        Debug.DrawRay(rotator.position, direction * 10f, Color.blue);

        RaycastHit hit;
        Vector3 endPoint;

        // Check if the ray hits something
        if (Physics.Raycast(rotator.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            endPoint = hit.point;

            // Enable LineRenderer and set its positions based on raycast hit point
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, rotator.position);
            lineRenderer.SetPosition(1, endPoint);
        }
        else
        {
            endPoint = rotator.position + direction * 10f; // Extend the line if no hit

            // Enable LineRenderer and set its positions for the extended line
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, rotator.position);
            lineRenderer.SetPosition(1, endPoint);
        }
    }

    private void RotateTurretTowardsTarget(Vector3 direction)
    {
        // Calculate the rotation to face the target
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate the turret towards the target
        rotator.rotation = Quaternion.Slerp(rotator.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public bool CanSeeTarget(Vector3 direction, Vector3 origin, string tag)
    {
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == tag)
            {
                
                return true;
            }
        }

        return false;
    }

    public void Shoot(int index)
    {
        VisualizeRay(directionToTarget);
        
        Quaternion headingDirection = Quaternion.FromToRotation(projectile.transform.forward, GunBarrels[index].forward);

        Instantiate(projectile, GunBarrels[index].position, headingDirection).GetComponent<Projectile>().Direction = GunBarrels[index].forward;
        AudioLaser.Play(); 
    }
    public void ChangeState(TurretState newState)
    {
        if (newState != null)
        {
            newState.Exit();
        }
        this.currentState = newState;

        newState.Enter(this);
      //Debug.Log("current state cs: " + newState);
        if (newState.ToString() != "shootState" && lineRenderer != null)
        {
            lineRenderer.enabled = false; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
}
