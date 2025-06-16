using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Range;
    [SerializeField] private LayerMask HittableLayers;
    [SerializeField] private Transform ImpactJuice;
    [SerializeField] private float Damage;
    
    private LineRenderer _line;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Vector3 origin, Vector2 direction)
    {
        _line = GetComponent<LineRenderer>();
        
        // Raycast to see what we hit
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, Range, HittableLayers);
        
        // Get endpoint of raycast
        Vector2 endpoint = hit ? hit.point : (Vector2)origin + Range * direction;
        
        // Set impact juice to play from point of impact
        ImpactJuice.position = endpoint;
        
        // Set first line point at origin, second at raycast hit location
        _line.SetPosition(0, origin);
        _line.SetPosition(1, endpoint);

        // Past this point we're just processing hits
        if (!hit)
            return;
        
        // Tell whatever we hit that we hit it
        EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
        if (enemy)
            enemy.Hit(Damage);
    }
}
