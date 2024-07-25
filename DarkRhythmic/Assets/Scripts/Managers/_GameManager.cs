using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour {
    // ! Player effects
    public GameObject player;
    public List<MagicEffect> activeEffects;

    // ! Player
    public GameObject playerPrefab;

    // ! Modifiers
    public List<MagicEffect> magicEffects;

    public void Start() {
        this.LoadPlayer();
    }

    private void LoadPlayer() {
        // Instantiate and get reference to Player
        player = Instantiate(this.playerPrefab, null);
        this.player.GetComponent<PlayerMotion>().InitializePlayer();
    }

    public void Update() {
        this.CheckForEffects();
    }

    private void CheckForEffects() {
        // Check for active effects
        if(this.activeEffects.Count > 0) {
            // Iterate each active effect
            foreach (var effect in this.activeEffects) {
                // TODO Particle visuals
            }
        }
    }
}