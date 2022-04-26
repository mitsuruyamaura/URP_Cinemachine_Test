using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private SkyboxChanger skyboxChanger;

    [SerializeField]
    private int gameTime;

    private float timer;


     async UniTask Start() {  // Awake ���� await ���Ă������͑҂��Ȃ��B���̂��߁A�擾�������������̂ŁA��U�~�߂�K�v������

        SoundManager.instance.PlayBGM(BgmType.Main);

        var token = this.GetCancellationTokenOnDestroy();

        // �f�[�^�擾�܂őҋ@������
        await UniTask.WaitUntil(() => !GSSReceiver.instance.IsLoading);

        Debug.Log("�Q�[���J�n");

        skyboxChanger.ChangeSkybox();
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
