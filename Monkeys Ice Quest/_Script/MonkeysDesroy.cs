using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeysDesroy : MonoBehaviour {

	public void EndMonkeys() {
        FindObjectOfType<LoadGame>().DestroyMonkeys();
    }
}
