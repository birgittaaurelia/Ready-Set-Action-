using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject notePrefab;
    public GameObject hitResultPrefab;
    public Canvas targetCanvas;

    [Header("References")]
    public EnemyVisual enemyVisual; 

    public List<NoteData> levelData = new List<NoteData>();

    private float timer = 0f;
    private int currentNoteIndex = 0;
    private RectTransform canvasRect;

    void Awake()
    {
        if (targetCanvas == null)
        {
            targetCanvas = FindFirstObjectByType<Canvas>();
            if (targetCanvas == null)
            {
                Debug.LogError("NoteSpawner: No Canvas found in scene!");
                return;
            }
        }

        canvasRect = targetCanvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (currentNoteIndex < levelData.Count &&
            timer >= levelData[currentNoteIndex].spawnTime)
        {
            SpawnNote(levelData[currentNoteIndex]);
            currentNoteIndex++;
        }
    }

    void SpawnNote(NoteData data)
    {
        if (notePrefab == null || canvasRect == null)
        {
            Debug.LogError("NoteSpawner: notePrefab or canvasRect is missing!");
            return;
        }

        GameObject newNote = Instantiate(notePrefab, canvasRect);

        RectTransform rt = newNote.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        float xFinal = (data.position.x - 0.5f) * canvasRect.rect.width;
        float yFinal = (data.position.y - 0.5f) * canvasRect.rect.height;
        rt.anchoredPosition = new Vector2(xFinal, yFinal);

        RhythmNote note = newNote.GetComponent<RhythmNote>();
        if (note != null)
        {
            note.hitResultPrefab = hitResultPrefab;
            note.canvasTransform = canvasRect;
            note.SetDuration(data.duration);
            note.noteData = data;
            note.enemyVisual = enemyVisual;
        }
        else
        {
            Debug.LogWarning("NoteSpawner: notePrefab is missing RhythmNote component!");
        }
    }
}