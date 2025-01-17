using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
public class BootstrapEntryPoint : MonoBehaviour
{
    [SerializeField] private bool devMode = false;
    [SerializeField] private TilemapEditor tilemapEditor;
    [SerializeField] private TilemapView view;
    private void Start()
    {
        /*if (devMode)
        {
            view.SetGrid(tilemapEditor.Init());
        }*/

    }
}
