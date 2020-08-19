using System.Collections.Generic;
using UnityEngine;

public static partial class Pools
{

    #region Fields

    // prefabs
    private static Dictionary<PoolItem, GameObject> itemPrefab =
        new Dictionary<PoolItem, GameObject>();

    // pools
    private static Dictionary<PoolItem, List<GameObject>> itemPools =
        new Dictionary<PoolItem, List<GameObject>>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Initializes pools
    /// </summary>
    public static void Initialize()
    {
        foreach(KeyValuePair<PoolItem, string> pair in Game.ResourcesPath.PoolItems)
        {
            string resourcePath = pair.Value;
            GameObject prefab = Resources.Load<GameObject>(resourcePath);
            List<GameObject> pool = new List<GameObject>(); 
            itemPrefab.Add(pair.Key, prefab);
            itemPools.Add(pair.Key, pool);
        }
    }

    /// <summary>
    /// Gets you a content from the pools
    /// </summary>
    /// <param name="item">The type of item to fish</param>
    /// <returns>The item</returns>
    public static GameObject FishFor(PoolItem item)
    {
        // select pool
        List<GameObject> pool = itemPools[item];
        // get content
        GameObject gameObject;
        if (pool.Count > 0)
        {
            gameObject = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            pool.Capacity++;
            gameObject = NewPoolItem(item);
        }
        gameObject.SetActive(true);
        return gameObject;
    }

    /// <summary>
    ///  Returns an used gameobject into the pools
    /// </summary>
    /// <param name="item">The type of item to return</param>
    /// <param name="gameObject">The gameobject returned</param>
    public static void Return(PoolItem item, GameObject gameObject)
    {
        gameObject.SetActive(false);
        itemPools[item].Add(gameObject);
    }

    /// <summary>
    /// Finds the default corresponding item per carracter
    /// </summary>
    /// <param name="characterName">The character to map a item to</param>
    /// <returns>The item mapped by default</returns>
    public static PoolItem DefaultThrowable(CharacterName characterName)
    {
        switch(characterName)
        {
            case CharacterName.Sebas:
                return PoolItem.Bottle;
            case CharacterName.Camacho:
                return PoolItem.BMW;
            case CharacterName.Chavarria:
                return PoolItem.Car;
            case CharacterName.Ramon:
                return PoolItem.PlayController;
            case CharacterName.Joaquin:
                return PoolItem.Anime;
            case CharacterName.Maritza:
                return PoolItem.Cat;
            case CharacterName.Majo:
                return PoolItem.Leaf;
            case CharacterName.Lucia:
                return PoolItem.Tulip;
        }
        return PoolItem.Bottle;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gets you a brand new shiny game object
    /// </summary>
    /// <param name="item">The type of item you want</param>
    /// <returns>The shiny car... i mean game object</returns>
    private static GameObject NewPoolItem(PoolItem item)
    {
        GameObject gameObject = GameObject.Instantiate(itemPrefab[item]);
        GameObject.DontDestroyOnLoad(gameObject);
        return gameObject;
    }

    #endregion

}
