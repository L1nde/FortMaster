using Assets.Scripts;
using Assets.Scripts.enemies;
using Assets.Scripts.enemies.ammo;
using UnityEditor;


namespace Assets.Editor {


    [CustomEditor(typeof(RangeEnemyData))]
    public class RangeEnemyEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            RangeEnemyData myTarget = (RangeEnemyData)target;

            EditorGUILayout.LabelField("Basic", EditorStyles.boldLabel);
            myTarget.hp = EditorGUILayout.FloatField("Health", myTarget.hp);
            myTarget.speed = EditorGUILayout.FloatField("Movement Speed", myTarget.speed);
            myTarget.enemyScore = EditorGUILayout.FloatField("Difficulty score", myTarget.enemyScore);
            myTarget.minLevel = EditorGUILayout.IntField("Minimal level needed to spawn", myTarget.minLevel);
            myTarget.moneyOnDeath = EditorGUILayout.FloatField("Money on Death", myTarget.moneyOnDeath);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Attacking", EditorStyles.boldLabel);
            myTarget.attackDelay = EditorGUILayout.FloatField("Attacking delay", myTarget.attackDelay);
            myTarget.attackRange = EditorGUILayout.FloatField("Attacking range", myTarget.attackRange);
            myTarget.AccuracySpread = EditorGUILayout.FloatField("Accuracy(0 - Always accurate)", myTarget.AccuracySpread);
            myTarget.attackSound = (AudioClipGroup)EditorGUILayout.ObjectField("Attacking sound", myTarget.attackSound, typeof(AudioClipGroup), false);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            myTarget.enemyPrefab = (Enemy)EditorGUILayout.ObjectField("Enemy prefab", myTarget.enemyPrefab, typeof(Enemy), false);
            myTarget.ProjectilePrefab = (Projectile)EditorGUILayout.ObjectField("Projectile", myTarget.ProjectilePrefab, typeof(Projectile), false);
            EditorUtility.SetDirty(myTarget);
        }
    }
}