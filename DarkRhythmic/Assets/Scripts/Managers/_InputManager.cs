using System.Collections.Generic;
using UnityEngine;

public class _InputManager : MonoBehaviour {
    // ! ### Gameplay ###
    // ! - Keyboard - Individual -

    // ! - Keyboard - Multiple -
    [SerializeField] public List<KeyState> moveKeys;

    //public bool escape;

    private void Update() {
        /*Escape key
        if (Input.GetKeyDown(KeyCode.Escape)) escape = true;
        else escape = false;
        */

        // Movement keys
        foreach (var moveKey in this.moveKeys) {
            if(Input.GetKey(moveKey.Key)) moveKey.Enabled = true;
            else moveKey.Enabled = false;
        }
    }

    [System.Serializable]
    public class KeyState {
        public KeyCode Key;
        public bool Enabled;

        public KeyState(KeyCode key, bool enabled) {
            Key = key;
            Enabled = enabled;
        }
    }
}
