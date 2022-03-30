using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private int gameTime;

    private float timer;


    void Update() {
        if (gameTime <= 0) {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1.0f) {
            timer = 0;

            // 0 à»â∫Ç…Ç»ÇÁÇ»Ç¢ÇÊÇ§Ç…êßå‰
            // í èÌÇÃ UI çXêV
            uiManager.UpdateDisplayGameTime(Mathf.Max(0, --gameTime));

            // DOTween ÇÃèÍçá
            //uiManager.UpdateDisplayGameTime(gameTime, Mathf.Max(0, --gameTime));
        }
    }
}
