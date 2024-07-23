using UnityEngine;

public class _InputManager : MonoBehaviour {
    // ! ### Gameplay ###
    // ! - Keyboard -
    public bool Q_press;
    public bool W_press;
    public bool E_press;
    public bool A_press;
    public bool S_press;
    public bool D_press;

    //public bool escape;

    private void Update() {
        /*Escape key
        if (Input.GetKeyDown(KeyCode.Escape)) escape = true;
        else escape = false;
        */

        Q_press = Input.GetKey(KeyCode.Q);
        W_press = Input.GetKey(KeyCode.W);
        E_press = Input.GetKey(KeyCode.E);
        A_press = Input.GetKey(KeyCode.A);
        S_press = Input.GetKey(KeyCode.S);
        D_press = Input.GetKey(KeyCode.D);
    }
}
