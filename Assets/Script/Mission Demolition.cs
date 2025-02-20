using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{   
    static private MissionDemolition S; // a private Singleton
    [Header("Inscribed")]
    public TextMeshPro uitLevel;
    public TextMeshPro uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        S = this; // Define the Singleton

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }
    
    void StartLevel() {
        // Get rid of the old castles if one exists
        if (castle != null)
            Destroy(castle);
        
        // Destroy old projectiles if they exist
        Projectile.DESTROY_PROJECTILES();

        // Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        // Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

    }

    void UpdateGUI() {
        // Show the data in the GUITexts
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots Taken: "+shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        // Check for level end
        if ( ( mode == GameMode.playing) && Goal.goalMet ) {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;

            // Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }

    }
    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    // Static method that alllows code anywhere to increment shotsTaken
    static public void SHOT_FIRED() {
        S.shotsTaken++;
    }

    // Static method that allows code anywhere to get a reference to S.castle
    static public GameObject GET_CASTLE() {
        return S.castle;
    }

}
