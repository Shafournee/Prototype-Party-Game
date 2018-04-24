using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPop : MonoBehaviour {

    void Start() {
        Invoke("KillMyself", 0.31f);
    }

    void KillMyself() {
        Destroy(gameObject);
    }
}
