using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class GridInputHandler : MonoBehaviour, IInputHandler
{
    public class CellCoords : EventArgs
    {
        public int fromX;
        public int fromY;
        public int toX;
        public int toY;
    }
    private int hoveredX;
    private int hoveredY;
    private int clickedX = -1;
    private int clickedY;
    private new Camera camera;
    public event EventHandler<CellCoords> OnCellClicked;
    public event EventHandler<CellCoords> OnMouseMovedToCell;
    private Vector2 cellSize;
    private Vector3 lastClickPoint;
    private Vector3 lastMousePosition;

    public bool IsActive => clickedX != -1;

    public void Init(Camera camera, Vector2 cellSize)
    {
        this.camera = camera;
        this.cellSize = cellSize;
        clickedX = -1;
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Vector3 mousePosition = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 relativePosition = mousePosition - transform.position;
        if(relativePosition.x < 0 || relativePosition.y < 0)
        {
            return;
        }
        if (Input.GetMouseButton(0)) // change to click
        {
            int newX = Mathf.FloorToInt(relativePosition.x / cellSize.x);
            int newY = Mathf.FloorToInt(relativePosition.y / cellSize.y);
            OnCellClicked?.Invoke(this, new CellCoords { fromX = clickedX, fromY = clickedY, toX = newX, toY = newY });
            clickedX = newX;
            clickedY = newY;
            Debug.Log($"Clicked on: {clickedX} {clickedY}");
        }

        if (mousePosition != lastMousePosition)
        {
            lastMousePosition = mousePosition;
            if (Mathf.FloorToInt(relativePosition.x / cellSize.x) != hoveredX || hoveredY != Mathf.FloorToInt(relativePosition.y / cellSize.y))
            {
                int newX = Mathf.FloorToInt(relativePosition.x / cellSize.x);
                int newY = Mathf.FloorToInt(relativePosition.y / cellSize.y);
                OnMouseMovedToCell?.Invoke(this, new CellCoords { fromX = hoveredX, fromY = hoveredY, toX = newX, toY = newY });
                hoveredX = newX;
                hoveredY = newY;
            }
            //Debug.Log($"Mouse moved to: {mousePosition}");
        }
    }
    public void Toggle()
    {
        if (IsActive)
            clickedX = -1;
        else
            Debug.LogWarning("toggle func isnt finished");
    }

    public bool TryDisable()
    {
        throw new NotImplementedException("not implemented");
    }
}
