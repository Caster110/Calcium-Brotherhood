using System.Collections.Generic;
using UnityEngine;
public class TilemapAnimator : MonoBehaviour
{
    private float frameTime = 1f;
    private float timer = 0f;
    private List<TilemapView> tilemapViews = new List<TilemapView>();
    public void AddTilemap(TilemapView tilemapView)
    {
        tilemapViews.Add(tilemapView);
        //minimalTickDurations.Add(tilemapView.minimalTickDuration);
    }
    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= frameTime)
        {
            timer = 0f;
            for (int i = 0; i < tilemapViews.Count; i++)
            {
                tilemapViews[i].ChangeFrame();
            }
        }
        for (int i = 0; i < tilemapViews.Count; i++)
        {
            tilemapViews[i].ForLateUpdate();
        }
    }
}
