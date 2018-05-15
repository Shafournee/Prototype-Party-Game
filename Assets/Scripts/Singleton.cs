using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton base class, used to conveniently set up singleton scripts.
public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
	
	// Public instance of this class
	public static T Instance;

	// Awake: Removes itself if there's already an instance, else sets this as the instance.
	protected virtual void Awake() {
		if (Instance == null) {
			Instance = (T)this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
