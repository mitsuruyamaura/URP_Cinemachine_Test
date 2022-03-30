using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Presenter : MonoBehaviour
{
    [SerializeField]
    private GameUI_Model model;

    [SerializeField]
    private GameUI_View view;

    [SerializeField]
    private int gameTime;

    private float timer;


    void Start()
    {
        // �Q�[�����Ԃ̏����l�ݒ�
        if (gameTime != 0) {
            model.GameTime.Value = gameTime;
        }

        // GameTime ���Ď� �ʏ�̏ꍇ(�����ɖ��Ȃ�)
        //model.GameTime.Subscribe(x => view.UpdateDisplayGameTime(x)).AddTo(gameObject);

        // GameTime ���Ď� DOTween �̏ꍇ
        model.GameTime.Zip(model.GameTime.Skip(1), (oldTime, newTime) => (oldTime, newTime)).Subscribe(x => view.UpdateDisplayGameTime(x.oldTime, x.newTime)).AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => model.GameTime.Value > 0)     // GameTime �̒l�� 0 �ȏ�̏ꍇ�̂݉��̏����ɍs��
            .Subscribe(_ =>                           // Update �Ŏ������������e������
            { 
                timer += Time.deltaTime;
                if (timer >= 1.0f) {
                    timer = 0;

                    // 0 �ȉ��ɂȂ�Ȃ��悤�ɐ���
                    // ���ڒl��ς���ꍇ(ReactiveProperty �̓v���p�e�B�Ȃ̂ł���ł� OK)
                    model.GameTime.Value = Mathf.Max(0, --model.GameTime.Value);

                    // ���\�b�h���g���ꍇ(�����ɖ��Ȃ�)
                    //model.UpdateGameTime(Mathf.Max(0, --model.GameTime.Value));
                }
            })
            .AddTo(gameObject);
    }    
}
