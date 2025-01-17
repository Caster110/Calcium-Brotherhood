using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TilemapEditor : MonoBehaviour
{
    [SerializeField] private TilemapView view;
    [SerializeField] private new Camera camera;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 position;
    [SerializeField] private int TileID = 0;
    private Grid<int> tilemap;

    private void Start()
    {
        tilemap = new Grid<int>(width, height, cellSize, position, (int x, int y) => new GridObject<int>(x, y));
        view.SetGrid(tilemap);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TileID++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (TileID > 0)
                TileID--;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            tilemap = SaveSystem.LoadMostRecentObject<Grid<int>>();
            view.SetGrid(tilemap);
            Debug.Log("Tilemap loaded!");
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            tilemap.SetGridObjectValue(mouseWorldPosition, TileID);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.SaveObject(tilemap);
            Debug.Log("Tilemap saved!");
        }
    }
}
