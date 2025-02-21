using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject _heroPrefab;
    public GameObject _monster1Prefab;
    public GameObject _monster2Prefab;

    GameObject _hero;
    GameObject _monster1;
    GameObject _monster2;
    // Start is called before the first frame update
    void Start()
    {
        _hero = GameObject.Instantiate(_heroPrefab);
        _monster1 = GameObject.Instantiate(_monster1Prefab);
        _monster2 = GameObject.Instantiate(_monster2Prefab);

        GameObject go = new GameObject();
        //_hero.transform.parent = go.transform;
        _monster1.transform.parent = go.transform;
        _monster2.transform.parent = go.transform;

        _hero.name = _heroPrefab.name;
        _monster1.name = _monster1Prefab.name;
        _monster2.name = _monster2Prefab.name;

        _hero.AddComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
