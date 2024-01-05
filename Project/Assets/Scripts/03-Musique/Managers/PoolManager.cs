using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
public class PoolCreator : MonoBehaviour
{
	[SerializeField]
	private GameObject prefab;

	[SerializeField]
	private int poolCount;

	private void Awake()
	{
		PoolManager.instance.CreatePool(prefab, poolCount);
	}
}

public class PoolObject : MonoBehaviour
{
	public virtual void OnObjectReuse(){}

	protected void Destroy()
	{
		base.gameObject.SetActive(value: false);
	}
}

public class ObjectPooled : PoolObject
{
	public float Delay;

	private void Awake(){}

	private void OnEnable()
	{
		if (GetComponent<RectTransform>() != null)
		{
			GetComponent<RectTransform>().localScale = Vector3.one;
		}
		else
		{
			GetComponent<Transform>().localScale = Vector3.one;
		}
	}

	public override void OnObjectReuse(){}

	private void Dst()
	{
		Destroy();
	}
}

public class PoolManager : MonoBehaviour
{
	public class ObjectInstance
	{
		private GameObject gameobject;
		private Transform transform;
		private bool hasPoolObjectComponent;
		private PoolObject poolObjectScript;

		public ObjectInstance(GameObject objectInstance)
		{
			gameobject = objectInstance;
			transform = objectInstance.transform;
			gameobject.SetActive(value: false);
			if ((bool)gameobject.GetComponent<PoolObject>())
			{
				hasPoolObjectComponent = true;
				poolObjectScript = gameobject.GetComponent<PoolObject>();
			}
		}

		public void Reuse(Vector3 position, Quaternion rotation)
		{
			gameobject.SetActive(value: true);
			transform.position = position;
			transform.rotation = rotation;
			if (hasPoolObjectComponent)
			{
				poolObjectScript.OnObjectReuse();
			}
		}

		public void SetParent(Transform parent)
		{
			transform.SetParent(parent);
		}

		public GameObject Get()
		{
			return gameobject;
		}
	}

	private static PoolManager _instance;

	public Dictionary<int, Queue<ObjectInstance>> poolDictionary { get; private set; } = new Dictionary<int, Queue<ObjectInstance>>();

	public static PoolManager instance
	{
		get
		{
			if (_instance == null || !_instance.gameObject.activeSelf || !_instance.gameObject.activeInHierarchy)
			{
				_instance = Object.FindObjectOfType<PoolManager>();
			}
			return _instance;
		}
	}

	public void CreatePool(GameObject prefab, int poolSize, bool OtherParent = false, Transform Parent = null)
	{
		int instanceID = prefab.GetInstanceID();
		GameObject gameObject = new GameObject(prefab.name + "pool");
		if (!OtherParent)
		{
			gameObject.transform.parent = base.transform;
		}
		if (poolDictionary.ContainsKey(instanceID))
		{
			return;
		}
		poolDictionary.Add(instanceID, new Queue<ObjectInstance>());
		for (int i = 0; i < poolSize; i++)
		{
			ObjectInstance objectInstance = new ObjectInstance(Object.Instantiate(prefab));
			poolDictionary[instanceID].Enqueue(objectInstance);
			if (!OtherParent)
			{
				objectInstance.SetParent(gameObject.transform);
			}
			else
			{
				objectInstance.SetParent(Parent);
			}
		}
	}

	public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int instanceID = prefab.GetInstanceID();
		if (poolDictionary.ContainsKey(instanceID))
		{
			ObjectInstance objectInstance = poolDictionary[instanceID].Dequeue();
			poolDictionary[instanceID].Enqueue(objectInstance);
			objectInstance.Reuse(position, rotation);
			return objectInstance.Get();
		}
		return null;
	}
}

}