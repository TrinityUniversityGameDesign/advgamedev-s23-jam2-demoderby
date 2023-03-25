using UnityEngine;

namespace PLATEAU.Samples
{
    /// <summary>
    /// �J��������N���X
    /// 
    /// �Ԃ̃J�����p���O�ɒǏ]���܂��B
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Tooltip("�J�����ʒu�^�[�Q�b�g")] Transform positionTarget;
        [SerializeField, Tooltip("�����_�^�[�Q�b�g")] Transform lookAtTarget;

        [SerializeField, Tooltip("�Ǐ]�̃X���[�W���O�p�����[�^")] float smoothing = 6.0f;

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);        
            transform.LookAt(lookAtTarget);
        }
    }
}
