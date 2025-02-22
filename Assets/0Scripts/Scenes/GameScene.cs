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
                StartLoaded();
            }
        });
    }

    void StartLoaded()
    {
        var player = Managers.Resource.Instantiate("Player_01.prefab");
        player.AddComponent<PlayerController>();

        var map = Managers.Resource.Instantiate("Map_01.prefab");
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");

        var monster01 = Managers.Resource.Instantiate("Monster_01.prefab");

        Camera.main.GetComponent<CameraController>().Target = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
