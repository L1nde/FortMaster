using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;
using Assets.Scripts.enemies;
using Assets.Scripts.saving;
using Assets.Scripts.waves;
using UnityEditor;
using UnityEngine;

// Salvestama peabki need kõik neli asja + MapGeneration.instance.seed
//MapGeneration.instance.minX = waveDetails.mapMinX;
//MapGeneration.instance.maxX = waveDetails.mapMaxX;
//MapGeneration.instance.minY = waveDetails.mapMinY;
//MapGeneration.instance.maxY = waveDetails.mapMaxY;
//MapGeneration.instance.generateMap(waveDetails.seed);

public class SaveController : MonoBehaviour {
    public static SaveController instance;


    public const string waveFolderName = "Waves";
    public const string dataFolderName = "Data";
    private const string dataSaveName = "data";
    const string fileExtension = ".dat";


    void Start() {
        string folderPath = Path.Combine(Application.persistentDataPath, waveFolderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        folderPath = Path.Combine(Application.persistentDataPath, dataFolderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public void saveData(GameSaveObject gameSaveObject) {
        string folderPath = Path.Combine(Application.persistentDataPath, dataFolderName);
        string dataPath = Path.Combine(folderPath, dataSaveName + fileExtension);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(dataPath, FileMode.Create)) {
            binaryFormatter.Serialize(fileStream, gameSaveObject);
        }
    }

    public GameSaveObject LoadData() {
        string folderPath = Path.Combine(Application.persistentDataPath, dataFolderName);
        string dataPath = Path.Combine(folderPath, dataSaveName + fileExtension);
        if (!File.Exists(dataPath)) {
            return null;
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(dataPath, FileMode.Open)) {
            return (GameSaveObject)binaryFormatter.Deserialize(fileStream);
        }
    }

    public void saveWave(WaveDetails waveDetails, int waveNr)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, waveFolderName);
        string dataPath = Path.Combine(folderPath, waveNr + fileExtension);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Open(dataPath, FileMode.Create))
        {
            binaryFormatter.Serialize(fileStream, waveDetails);
        }
    }

    public WaveDetails LoadWave(string waveName) {
        string folderPath = Path.Combine(Application.persistentDataPath, waveFolderName);
        string dataPath = Path.Combine(folderPath, waveName);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(dataPath, FileMode.Open)) {
            return (WaveDetails) binaryFormatter.Deserialize(fileStream);
        }
    }

    public static FileInfo[] GetFilePaths(string foldername) {
        string folderPath = Path.Combine(Application.persistentDataPath, foldername);
        var directoryInfo = new DirectoryInfo(folderPath);
        return directoryInfo.GetFiles();
    }

    public void deleteAll() {
        foreach (var item in GetFilePaths(waveFolderName)) {
            File.Delete(item.FullName);
        }
        foreach (var item in GetFilePaths(dataFolderName)) {
            File.Delete(item.FullName);
        }
    }
}