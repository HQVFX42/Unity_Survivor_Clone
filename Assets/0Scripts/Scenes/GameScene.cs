using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"[Load] key: {key}, {count}/{totalCount}");

            if (totalCount == count)
            {
                StartLoaded();
            }
        });
    }

    SpawningPool _spawningPool;

    void StartLoaded()
    {
        Managers.Data.Init();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        PlayerController player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        for (int i = 0; i < 10; i++)
        {
            Vector3 randPos = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            MonsterController monster = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));
        }

        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        var map = Managers.Resource.Instantiate("Map_01.prefab");

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
