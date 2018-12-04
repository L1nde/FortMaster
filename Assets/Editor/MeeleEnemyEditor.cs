using Assets.Scripts;
using Assets.Scripts.enemies;
using Assets.Scripts.enemies.ammo;
using UnityEditor;


namespace Assets.Editor {


    [CustomEditor(typeof(MeeleEnemyData))]
    public class MeeleEnemyEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            MeeleEnemyData myTarget = (MeeleEnemyData)target;

            EditorGUILayout.LabelField("Basic", EditorStyles.boldLabel);
            myTarget.hp = EditorGUILayout.FloatField("Health", myTarget.hp);
            myTarget.speed = EditorGUILayout.FloatField("Movement Speed", myTarget.hp);
            myTarget.enemyScore = EditorGUILayout.FloatField("Difficulty score", myTarget.enemyScore);
            myTarget.minLevel = EditorGUILayout.IntField("Minimal level needed to spawn", myTarget.minLevel);
            myTarget.moneyOnDeath = EditorGUILayout.FloatField("Money on Death", myTarget.moneyOnDeath);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Attacking", EditorStyles.boldLabel);
            myTarget.meeleDamage = EditorGUILayout.FloatField("MeeleDamage", myTarget.meeleDamage);
            myTarget.attackDelay = EditorGUILayout.FloatField("Attacking delay", myTarget.attackDelay);
            myTarget.attackRange = EditorGUILayout.FloatField("Attacking range", myTarget.attackRange);
            myTarget.attackSound = (AudioClipGroup)EditorGUILayout.ObjectField("Attacking sound", myTarget.attackSound, typeof(AudioClipGroup), false);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            myTarget.enemyPrefab = (Enemy) EditorGUILayout.ObjectField("Enemy prefab", myTarget.enemyPrefab, typeof(Enemy), false);
        }
    }
}