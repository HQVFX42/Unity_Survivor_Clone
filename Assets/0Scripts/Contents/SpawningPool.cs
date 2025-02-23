using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    float _spawnInterval = 1.0f;
    int _maxMonsterCount = 1000;
    Coroutine _coUpdateSpawningPool;

    // Start is called before the first frame update
    void Start()
    {
        _coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    IEnumerator CoUpdateSpawningPool()
    {
        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    void TrySpawn()
    {
        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= _maxMonsterCount)
        {
            return;
        }

        //TODO : Get monster data
        Vector3 randPos = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));
    }
}
