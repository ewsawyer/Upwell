using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private EnemyHealth PrefabEnemy;

    public void Spawn()
    {
        EnemyHealth enemy = Instantiate(PrefabEnemy, transform.position, Quaternion.identity);
    }
}
