using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"[Load] key: {key}, {count}/{totalCount}");

            if (totalCount == count)
            {
                StartLoaded2();
            }
        });
    }

    void StartLoaded()
    {
        var player = Managers.Resource.Instantiate("Player_01.prefab");
        player.AddComponent<PlayerController>();

        var monster01 = Managers.Resource.Instantiate("Monster_01.prefab");

        var map = Managers.Resource.Instantiate("Map_01.prefab");
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");


        Camera.main.GetComponent<CameraController>().Target = player;
    }

    void StartLoaded2()
    {
        PlayerController player = Managers.Object.Spawn<PlayerController>();

        for (int i = 0; i < 10; i++)
        {
            MonsterController monster = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
            monster.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
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
