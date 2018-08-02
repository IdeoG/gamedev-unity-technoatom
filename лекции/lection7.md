# Лекция 7. Сети, Оптимизация

- Нельзя смешивать логику игрока и UI, поэтому это UI должен знать о том, у какого игрока брать Health. Можно использовать Rx чтобы по эвентам получать сообщения о изменении здоровья.

## Оптимизация

- `Singleton`
    - гарантирует что больше таких объектов больше не будет

``` Java
public class Singleton<T> : Monobehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            var instances = FindObjectsByType<T>();
            if (instances.Lenght > 1) 
            {
                // TODO: Error!
            }

            if (instances.Lenght == 0) {
                var someGameObject = new GameObject(typeof(T).Name)
                _instance = someGameObject.AddComponent<T>();
                return _instance;
            }

            _instance = instances[0];
            return _instance;
        }
    }
}

```

- `Object Pooling`
    - Инстанцировать из пула проще, чем делать это реально в игре
        - Будет меньше подлагиваний
``` Java
public class GameManager : Singletom<GameObject>
{
    private GameObjectPool _objectPool = new GameObjectPool();
}
```

``` Java
public class GameObjectPool
{
    private readonly Dicitonary<GameObject, List<GameObject>> _instances = new Dictionary<GameObject, List<GameObject>>();

    public GameObject Take(GameObject prefab) 
    {
        if (!_instances.ContainsKey(prefab)) 
        {
            AddNewInstance();
        }

        var gameObjects = _instances[prefab];
        foreach (var instance in gameObjects)
        {
            if (!instance.active) 
            {
                instance.SetActive(true);
                return instance;
            }
        }

        return AddNewInstance(prefab);
    }
    public GameObject AddNewinstance(GameObject prefab, int count = 1)
    {
        if (!_instances.ContainsKey(prefab)) 
        {
            _instances.ADd(prefab, new List<GameObject>());
        }

        var gameObjectInstances = _instances[prefab];
        
        for (int i=0; i<count; i++) 
        {
            // instantiate objects
        }
        // and so on
    }
}
```

- Избегать конкатенацию строк
    - Использовать `StringBuilder`, когда надо со строками много работать
- LINQ
    - toList, toArray - избегать
- Замыкания
- Рефлексия - это плохо
- CodeGeneration - это хорошо
- memory profiler
- profiler
    - `GFX wait for present` - сколько ждет на холостом ходу игра между update
    - есть вкладка `GC` - показывает сколько байт аллоцируется
    