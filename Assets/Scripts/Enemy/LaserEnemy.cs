using System.Collections;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{

    [Header("Duration of stages")]
    [SerializeField] private float DoNothingDuration;
    [SerializeField] private float AimDuration;
    [SerializeField] private float AlmostFireDuration;
    
    [Header("Aiming and firing")]
    [SerializeField] private LayerMask HittableLayers;
    [SerializeField] private Transform AimRotationAnchor;
    [SerializeField] private LineRenderer FireLine;

    [Header("Juice")]
    [SerializeField] private Juice JuiceStartAiming;
    [SerializeField] private Juice JuiceAlmostFire;
    [SerializeField] private Juice JuiceFire;

    private Transform _player;
    private bool _shouldAimAtPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(RepeatAimAndFire());
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldAimAtPlayer)
            AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        AimRotationAnchor.up = (_player.position - AimRotationAnchor.position).normalized;
    }

    private IEnumerator RepeatAimAndFire()
    {
        while (true)
            yield return StartCoroutine(AimAndFireCoroutine());
    }

    private IEnumerator AimAndFireCoroutine()
    {
        // Do nothing for a while
        yield return new WaitForSeconds(DoNothingDuration);
        // Start aiming
        _shouldAimAtPlayer = true;
        JuiceStartAiming.Play();
        yield return new WaitForSeconds(AimDuration);
        // Almost ready to fire. Stop aiming
        _shouldAimAtPlayer = false;
        JuiceAlmostFire.Play();
        yield return new WaitForSeconds(AlmostFireDuration);
        // FIRE!
        JuiceFire.Play();
        Fire();
    }

    private void Fire()
    {
        // Raycast to see if we hit anything
        RaycastHit2D hit = Physics2D.Raycast(AimRotationAnchor.position, AimRotationAnchor.up, float.PositiveInfinity,
            HittableLayers);

        // Draw the firing line
        Vector2 endpoint = hit ? hit.point : AimRotationAnchor.position + 100.0f * AimRotationAnchor.up;
        FireLine.SetPosition(0, AimRotationAnchor.position);
        FireLine.SetPosition(1, endpoint);

        // If we hit nothing, do nothing
        if (!hit)
            return;
        
        // If we hit the player
        PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
        if (playerHealth)
            playerHealth.Hit();
    }
    
}
