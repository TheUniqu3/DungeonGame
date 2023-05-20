using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;

    private void Start()
    {
        StartCoroutine(SpawnEnemy(Enemy));
    }


    private IEnumerator SpawnEnemy(GameObject Enemy)
    {
        yield return new WaitForSeconds(5);
        Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
    }
}
