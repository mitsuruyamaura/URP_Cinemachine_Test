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


    void Start() {
        SoundManager.instance.PlayBGM(BgmType.Main);    
    }


    void Update() {
        if (gameTime <= 0) {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1.0f) {
            timer = 0;

            // 0 �ȉ��ɂȂ�Ȃ��悤�ɐ���
            // �ʏ�� UI �X�V
            uiManager.UpdateDisplayGameTime(Mathf.Max(0, --gameTime));

            // DOTween �̏ꍇ(�����ɖ��Ȃ�)
            //uiManager.UpdateDisplayGameTime(gameTime, Mathf.Max(0, --gameTime));
        }
    }
}
