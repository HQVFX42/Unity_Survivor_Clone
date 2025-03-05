using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
        {
            return null;
        }

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
        {
            return null;
        }

        if (!recursive)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    T component = child.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }

    public static Vector2 GenerateMonsterSpawnPosition(Vector2 playerPosition, float minDistance = 10.0f, float maxDistance = 20.0f)
    {
        float angle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = UnityEngine.Random.Range(minDistance, maxDistance);

        float xDistance = Mathf.Cos(angle) * distance;
        float yDistance = Mathf.Sin(angle) * distance;

        Vector2 spawnPosition = playerPosition + new Vector2(xDistance, yDistance);

        return spawnPosition;
    }

    public static Color HexToColor(string color)
    {
        Color parsedColor;
        ColorUtility.TryParseHtmlString("#" + color, out parsedColor);

        return parsedColor;
    }

    //Enum값중 랜덤값 반환
    public static T GetRandomEnumValue<T>() where T : struct, Enum
    {
        Type type = typeof(T);

        if (!_enumDict.ContainsKey(type))
            _enumDict[type] = Enum.GetValues(type);

        Array values = _enumDict[type];

        int index = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(index);
    }

    //string값 으로 Enum값 찾기
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static ESkillType GetSkillTypeFromInt(int value)
    {
        foreach (ESkillType skillType in Enum.GetValues(typeof(ESkillType)))
        {
            int minValue = (int)skillType;
            int maxValue = minValue + 5; // 100501~ 100506 사이 값이면 100501값 리턴

            if (value >= minValue && value <= maxValue)
            {
                return skillType;
            }
        }

        Debug.LogError($" Faild add skill : {value}");
        return ESkillType.None;
    }
}