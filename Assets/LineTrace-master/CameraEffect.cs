using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;   // Vignette
using Cinemachine.PostFX;                     // CinemachinePostProcessing
using DG.Tweening;

public class CameraEffect : MonoBehaviour
{
    [SerializeField]
    private CinemachinePostProcessing postProcessing;

    private Vignette vignette;


    private void Start() {

        if (postProcessing.m_Profile.TryGetSettings(out vignette)) {
            ChangeVignetteIntensity(0, 0); // �ύX�����܂܂ɂȂ��Ă��܂��̂ŏ�����
            
            // �f�o�b�O�p
            //ChangeVignetteIntensity(0.65f, 0.5f);
        }
    }

    /// <summary>
    /// Vignette �� Intensity �̕ύX
    /// </summary>
    /// <param name="value"></param>
    /// <param name="duration"></param>
    public void ChangeVignetteIntensity(float value, float duration) {
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, value, duration).SetEase(Ease.InQuart);
    }
}
