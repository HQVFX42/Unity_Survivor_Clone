# Unity_Survivor_Clone

## Managers

## UI
- Bind
  ```cpp
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }
  ```

- Enum
  ```cpp
  #region Enum
  enum GameObjects
  {
      ContentObject,
      ResultRewardScrollContentObject,
      ResultGoldObject,
      ResultKillObject,
  }
  
  enum Texts
  {
      GameResultPopupTitleText,
      ResultStageValueText,
      ResultSurvivalTimeText,
      ResultSurvivalTimeValueText,
      ResultKillValueText,
      ConfirmButtonText,
      ResultGoldValueText
  }
  
  enum Buttons
  {
      StatisticsButton,
      ConfirmButton,
  }
  #endregion
  ```

## Addressable
