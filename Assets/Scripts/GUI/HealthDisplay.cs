using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthDisplay : MonoBehaviour {

    // ================================================================================
    //  const
    // --------------------------------------------------------------------------------

    const float size = 48.0f;
    const float margin = 20.0f;
    const float padding = 5.0f;

    const float lifePointsWidth = 958.0f / 5.0f;
    const float lifePointsHeight = 80.0f / 5.0f;

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Texture2D healthOverlayImage;
    public Texture2D healthBarImage;
    public Texture2D healthBarBackgroundImage;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    void OnGUI()
    {
        if (GameMaster.Instance.state == GameMaster.GameState.Playing && GameMaster.Instance.player != null)
        {
            DrawHealthBar(GameMaster.Instance.player.actor, margin);

            ActorController target = GameMaster.Instance.player.focusManager.focus;
            if (target != null)
            {
                DrawHealthBar(target.actor, Screen.width - lifePointsWidth - margin);
            }
        }
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private void DrawHealthBar(Actor actor, float xPos)
    {
        Rect rect;

        // draw background
        rect = new Rect(xPos, margin, lifePointsWidth, lifePointsHeight);
        GUI.DrawTexture(rect, healthBarBackgroundImage);

        // draw health bar
        float percent = actor.health / actor.maxHealth;
        rect = new Rect(xPos, margin, lifePointsWidth * percent, lifePointsHeight);
        Rect textCoords = new Rect(0, 0, percent, 1.0f);
        GUI.DrawTexture(rect, healthBarImage);
        GUI.DrawTextureWithTexCoords(rect, healthBarImage, textCoords);

        // draw overlay
        rect = new Rect(xPos, margin, lifePointsWidth, lifePointsHeight);
        GUI.DrawTexture(rect, healthOverlayImage);
    }

}