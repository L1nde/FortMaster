using Assets.Scripts.enemies;
using Assets.Scripts.waves;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    public class Debugging : EditorWindow {
        public float gold = 1000;
        private float xp = 100;
        private bool spawning = true;

        public bool Spawning {
            get { return spawning; }
            set {
                
                if (value == spawning)
                    return;

                spawning = value;
                foreach (var spawnController in Resources.FindObjectsOfTypeAll<SpawnController>()) {
                    spawnController.gameObject.SetActive(spawning);
                }
                
            }
        }

        [MenuItem("Window/Debugging")]
        static void Init() {
            // Get existing open window or if none, make a new one:
            Debugging window = (Debugging)EditorWindow.GetWindow(typeof(Debugging));
            window.Show();
        }

        void OnGUI() {
            GUILayout.Label("Game settings", EditorStyles.boldLabel);
            gold = EditorGUILayout.FloatField("Gold", gold);
            if (GUILayout.Button("Add gold")) {
                GameObject.FindObjectOfType<GameController>().addGold(gold);
            }
            xp = EditorGUILayout.FloatField("xp", gold);
            if (GUILayout.Button("Add xp")) {
                GameObject.FindObjectOfType<GameController>().addXP(xp);
            }
            EditorGUILayout.Space();
            GUILayout.Label("Enemy settings", EditorStyles.boldLabel);
            Spawning = EditorGUILayout.Toggle("Spawning", Spawning);
            if (GUILayout.Button("Spawn archer")) {
                GameObject.FindObjectOfType<EnemySpawn>().spawn(Resources.Load<RangeEnemy>("Prefabs/EnemyArcher"));
            }
            if (GUILayout.Button("Spawn meele")) {
                GameObject.FindObjectOfType<EnemySpawn>().spawn(Resources.Load<MeeleEnemy>("Prefabs/EnemyMeele"));
            }
            if (GUILayout.Button("Kill all")) {
                foreach (var enemy in GameObject.FindObjectsOfType<Enemy>()) {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }
}