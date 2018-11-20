using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;
using Assets.Scripts.enemies;
using Assets.Scripts.waves;
using UnityEditor;
using UnityEngine;

public class SaveController : MonoBehaviour {

    public static SaveController instance;
    public WaveDetails sampleWaveDetails;
    
    const string folderName = "Waves";
    const string fileExtension = ".dat";


    private int saveFileNr = 1;

    void Start() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }

    public void SaveWave(WaveDetails waveDetails, int waveNr) {
        var wave = new WaveSaveObject(waveDetails);
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        string dataPath = Path.Combine(folderPath, waveNr + fileExtension);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate)) {
            binaryFormatter.Serialize(fileStream, wave);
        }
    }

    public WaveDetails LoadWave(string waveName) {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        string dataPath = Path.Combine(folderPath, waveName);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(dataPath, FileMode.Open)) {
            return convertToWaveDetails((WaveSaveObject)binaryFormatter.Deserialize(fileStream));
        }
    }

    public FileInfo[] GetFilePaths() {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        var directoryInfo = new DirectoryInfo(folderPath);
        return directoryInfo.GetFiles();
    }


    WaveDetails convertToWaveDetails(WaveSaveObject waveSaveObject) {
        var waveDetails = Instantiate(sampleWaveDetails);
        waveDetails.buildTime = waveSaveObject.buildTime;
        waveDetails.spawnDelay = waveSaveObject.spawnDelay;
        var waveEnemies = new List<WaveEnemy>();
        foreach (var data in waveSaveObject.enemies) {
            waveEnemies.Add(new WaveEnemy(Resources.Load<Enemy>("Prefabs/" + data.Key), data.Value));
        }

        return waveDetails;
    }
}