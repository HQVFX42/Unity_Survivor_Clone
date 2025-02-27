using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>();

    public T Spawn<T>(Vector3 position, int templateID = 0) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            //TODO
            GameObject go = Managers.Resource.Instantiate("Player_01.prefab", pooling: true);
            go.name = "Player";
            go.transform.position = position;

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;
            Managers.Game.Player = pc;

            pc.Init();

            return pc as T;
        }
        else if (type == typeof(MonsterController))
        {
            string name = (templateID == 0 ? "Monster_01" : "Monster_02");
            GameObject go = Managers.Resource.Instantiate($"{name}.prefab", pooling: true);
            go.transform.position = position;

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monsters.Add(mc);
            mc.Init();

            return mc as T;
        }
        else if (type == typeof(GemController))
        {
            GameObject go = Managers.Resource.Instantiate(Define.EXP_GEM_PREFAB, pooling: true);
            go.transform.position = position;

            GemController gc = go.GetOrAddComponent<GemController>();
            Gems.Add(gc);
            gc.Init();

            string key = Random.Range(0, 2) == 0 ? "ExpGem_01.sprite" : "ExpGem_02.sprite";
            Sprite sprite = Managers.Resource.Load<Sprite>(key);
            go.GetComponent<SpriteRenderer>().sprite = sprite;

            //TODO: Add grid controller
            GameObject.Find("Grid").GetComponent<GridController>().Add(go);

            return gc as T;
        }
        else if (type == typeof(ProjectileController))
        {
            GameObject go = Managers.Resource.Instantiate("FireProjectile.prefab", pooling: true);
            go.transform.position = position;

            ProjectileController pc = go.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(pc);
            pc.Init();

            return pc as T;
        }
        else if (typeof(T).IsSubclassOf(typeof(SkillController)))
        {
            if (Managers.Data.SkillDictionary.TryGetValue(templateID, out Data.SkillData skillData) == false)
            {
                Debug.LogError($"ObjectManager Spawn Skill Failed {templateID}");
                return null;
            }

            GameObject go = Managers.Resource.Instantiate(skillData.prefab, pooling: true);
            go.transform.position = position;

            T sc = go.GetOrAddComponent<T>();
            sc.Init();

            return sc;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        Debug.Assert(obj.IsValid(), "Invalid object");

        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {

        }
        else if (type == typeof(MonsterController))
        {
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy(obj.gameObject);

            //TODO: Add grid controller
            GameObject.Find("Grid").GetComponent<GridController>().Remove(obj.gameObject);
        }
    }

    //public void KillAllMonsters()
    //{
    //    UI_GameScene scene = Managers.UI.SceneUI as UI_GameScene;

    //    if (scene != null)
    //        scene.DoWhiteFlash();
    //    foreach (MonsterController monster in Monsters.ToList())
    //    {
    //        if (monster.ObjectType == ObjectType.Monster)
    //            monster.OnDead();
    //    }
    //    DespawnAllMonsterProjectiles();
    //}

    //public void DespawnAllMonsterProjectiles()
    //{
    //    foreach (ProjectileController proj in Projectiles.ToList())
    //    {
    //        if (proj.Skill.SkillType == SkillType.MonsterSkill_01)
    //            Despawn(proj);
    //    }
    //}

    //public void CollectAllItems()
    //{
    //    foreach (GemController gem in Gems.ToList())
    //    {
    //        gem.GetItem();
    //    }

    //    foreach (SoulController soul in Souls.ToList())
    //    {
    //        soul.GetItem();
    //    }
    //}
}