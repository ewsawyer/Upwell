using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    [SerializeField] private GameObject WallToReplace;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>())
            WallToReplace.SetActive(true);
    }
}
