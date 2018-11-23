using System;
using System.Linq;
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
        recalculateSaveNr();
    }

    public void saveWave(WaveDetails waveDetails) {
        saveFileNr++;
        saveWave(waveDetails, saveFileNr);
    }

    public void saveWave(WaveDetails waveDetails, int waveNr) {
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
            return convertToWaveDetails((WaveSaveObject) binaryFormatter.Deserialize(fileStream));
        }
    }

    public static FileInfo[] GetFilePaths() {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        var directoryInfo = new DirectoryInfo(folderPath);
        return directoryInfo.GetFiles();
    }


    private WaveDetails convertToWaveDetails(WaveSaveObject waveSaveObject) {
        var waveDetails = Instantiate(sampleWaveDetails);
        waveDetails.buildTime = waveSaveObject.buildTime;
        waveDetails.spawnDelay = waveSaveObject.spawnDelay;
        var waveEnemies = new List<WaveEnemy>();
        foreach (var data in waveSaveObject.enemies) {
            waveEnemies.Add(new WaveEnemy(Resources.Load<Enemy>("Prefabs/" + data.Key), data.Value));
        }

        waveDetails.enemies = waveEnemies;

        // Salvestama peabki need kõik neli asja + MapGeneration.instance.seed
        //MapGeneration.instance.minX = waveDetails.mapMinX;
        //MapGeneration.instance.maxX = waveDetails.mapMaxX;
        //MapGeneration.instance.minY = waveDetails.mapMinY;
        //MapGeneration.instance.maxY = waveDetails.mapMaxY;
        //MapGeneration.instance.generateMap(waveDetails.seed);

        return waveDetails;
    }

    public void deleteAll() {
        foreach (var item in GetFilePaths()) {
            File.Delete(item.FullName);
        }
        recalculateSaveNr();
    }

    public void recalculateSaveNr() {
        var files = GetFilePaths().OrderBy(f => f.Name);
        if (files.Count() != 0) {
            FileInfo lastFile = files.Last();
            saveFileNr = int.Parse(lastFile.Name.Substring(0, lastFile.Name.Length - 4));
        } else {
            saveFileNr = 0;
        }
    }
}