using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameUI_Model : MonoBehaviour
{
    public ReactiveProperty<int> GameTime = new ReactiveProperty<int>(0);  // �����l�����Ă������ƂŁA�R�X�g���N�^���g���Ēl�̏��������o���܂�

    /// <summary>
    /// �l�X�V�p 
    /// </summary>
    /// <param name="newTime"></param>
    public void UpdateGameTime(int newTime) {
        GameTime.Value = newTime;
    }
}