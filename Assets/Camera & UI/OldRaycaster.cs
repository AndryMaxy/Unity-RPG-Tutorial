//using UnityEngine;
//
//public class CameraRaycaster : MonoBehaviour
//{
//	public Layer[] layerPriorities = {
//		Layer.Enemy,
//		Layer.Walkable
//	};
//
//	// SerializeField keeps the variable private but shows it in the inspector
//	[SerializeField] float distanceToBackground = 100f;
//	Camera viewCamera;
//
//	RaycastHit m_hit;
//	// "getter" creates a publicly avaliable method to access the m_hit value (readonly)
//	public RaycastHit hit
//	{
//		get { return m_hit; }
//	}
//
//	Layer m_layerHit;
//	public Layer layerHit
//	{
//		get { return m_layerHit; }
//	}
//
//	public delegate void OnLayerChange(Layer newLayer); // declare new delegate type
//	public event OnLayerChange onLayerChange; // instantiate an observer set [this is the set of observers]
//
//	void Start() // TODO Awake?
//	{
//		viewCamera = Camera.main;
//	}
//
//	void Update()
//	{
//		// Look for and return priority layer hit
//		foreach (Layer layer in layerPriorities)
//		{
//			var hit = RaycastForLayer(layer);
//			if (hit.HasValue)
//			{
//				m_hit = hit.Value;
//				if (layerHit != layer) {
//					m_layerHit = layer;
//					onLayerChange (layer ); // call th delegates
//				}
//				return;
//			}
//		}
//
//		// basiclly like returning an empty string - protecting from null values
//		// Otherwise return background hit
//		m_hit.distance = distanceToBackground;
//		m_layerHit = Layer.RaycastEndStop;
//		onLayerChange (layerHit);
//	}
//
//	// question mark returns raycast hit OR null
//	RaycastHit? RaycastForLayer(Layer layer)
//	{
//		// bit shift the layer based on they '1' place in the numeric sequence
//		// layers 8/9 will be :
//		//						(8): 0.0000.......0010000000
//		// 						(9): 0.0000.......0100000000
//		int layerMask = 1 << (int)layer; // See Unity docs for mask formation
//		// https://docs.unity3d.com/ScriptReference/LayerMask.html
//
//		// use camera's screenpoint into a ray using the mouse position as a value
//		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
//
//		// out parameter is where you put the result of the raycast hit
//		RaycastHit hit; // used as an out parameter
//		// bool if it hits, shoot raycast, use ray from camera using mouse position, store into 'hit',
//		// set max distance for this cast, and lastly provide the layermask defined earlier
//		bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
//		// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html this is using the 4th method
//		if (hasHit)
//		{
//			return hit;
//		}
//		return null;
//	}
//}
