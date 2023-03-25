using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLATEAU.Samples
{
    /// <summary>
    /// �Ԑ���N���X
    /// </summary>
    public class CarController : MonoBehaviour, GameSampleInputActions.ICarActions
    {
        [SerializeField, Tooltip("�ő�X�e�A�����O�p�x(degree)")] float maxAngle = 30.0f;
        [SerializeField, Tooltip("�X�e�A�����O�p���x(degree/sec)")] float angleSpeed = 30.0f;
        [SerializeField, Tooltip("�ő�g���N")] float maxTorque = 300.0f;
        [SerializeField, Tooltip("�u���[�L�g���N")] float brakeTorque = 30000.0f;
        [SerializeField, Tooltip("�z�C�[���v���n�u")] GameObject wheelPrefab;

        [SerializeField, Tooltip("�T�u�X�e�b�v�̑��x�������l")] float criticalSpeed = 5.0f;
        [SerializeField, Tooltip("���x��criticalSpeed�ȉ��̎��̃T�u�X�e�b�v��")] int stepsBelow = 5;
        [SerializeField, Tooltip("���x��criticalSpeed�ȏ�̎��̃T�u�X�e�b�v��")] int stepsAbove = 1;

        [SerializeField, Tooltip("�G���W�����̍ŏ��s�b�`")] float minSePitch = 0.2f;
        [SerializeField, Tooltip("�G���W�����̍ő�s�b�`")] float maxSePitch = 1.0f;
        [SerializeField, Tooltip("�G���W�������ő�s�b�`�ɂȂ鎞�̃z�C�[����RPM")] float maxRpmSe = 1.0f;

        [SerializeField, Tooltip("�Ԗ{�̂̃����_���[")] Renderer carRenderer;
        [SerializeField, ColorUsage(false, true), Tooltip("�Ԃ̃o�b�N���C�g�_������EmissionColor")] Color backlightEmissionColor;

        /// <summary>
        /// GameSample�pInputAction
        /// </summary>
        private GameSampleInputActions inputActions;

        /// <summary>
        /// WheelCollider
        /// </summary>
        private WheelCollider[] wheelColliders;

        /// <summary>
        /// �G���W�����̃I�[�f�B�I�\�[�X
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// �u���[�L�����v�}�e���A��
        /// </summary>
        private Material backlightMaterial;

        /// <summary>
        /// �X�e�A�����O�p�x
        /// </summary>
        private float angle;

        /// <summary>
        /// WheelCollider�̃g���N
        /// </summary>
        private float torque;

        /// <summary>
        /// WheelCollider�̃u���[�L�̋���
        /// </summary>
        private float brake;

        private void Awake()
        {
            inputActions = new GameSampleInputActions();
            inputActions.Car.SetCallbacks(this);
        }

        private void Start()
        {
            wheelColliders = GetComponentsInChildren<WheelCollider>();

            // �eWheelCollider�ɃC���X�^���X�������^�C���I�u�W�F�N�g���A�^�b�`���܂��B
            for (int i = 0; i < wheelColliders.Length; ++i)
            {
                var collider = wheelColliders[i];

                if (wheelPrefab != null)
                {
                    var ws = Instantiate(wheelPrefab);
                    ws.transform.parent = collider.transform;
                }
            }

            audioSource = GetComponent<AudioSource>();

            backlightMaterial = carRenderer.materials[4];
        }

        private void OnEnable()
        {
            inputActions.Enable(); 
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Update()
        {
            UpdateWheels();
            UpdateSound();
            UpdateBacklight();
        }   

        /// <summary>
        /// WheelCollider�̍X�V����
        /// </summary>
        private void UpdateWheels()
        {
            wheelColliders[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

            var desired = maxAngle * inputActions.Car.Steer.ReadValue<float>();
            angle = Mathf.MoveTowards(angle, desired, Time.deltaTime * angleSpeed);
            angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

            foreach (var wheelCollider in wheelColliders)
            {
                // �X�e�A�����O�p�x��O�ւɔ��f
                if (wheelCollider.transform.localPosition.z > 0)
                {
                    wheelCollider.steerAngle = angle;
                }

                // ��ւɃu���[�L���f
                if (wheelCollider.transform.localPosition.z < 0)
                {
                    wheelCollider.brakeTorque = brake;
                }

                // �O�ւɃg���N���f
                if (wheelCollider.transform.localPosition.z >= 0)
                {
                    wheelCollider.motorTorque = torque;
                }

                if (wheelPrefab)
                {
                    // WheelCollider�̏�Ԃ��^�C�����f���ɔ��f

                    wheelCollider.GetWorldPose(out Vector3 p, out Quaternion q);

                    var shapeTransform = wheelCollider.transform.GetChild(0);

                    if (wheelCollider.name == "a0l" || wheelCollider.name == "a1l" || wheelCollider.name == "a2l")
                    {
                        shapeTransform.SetPositionAndRotation(p, q * Quaternion.Euler(0, 180, 0));
                    }
                    else
                    {
                        shapeTransform.SetPositionAndRotation(p, q);
                    }
                }
            }
        }

        /// <summary>
        /// �G���W�����X�V����
        /// 
        /// WheelCollider��rpm�ɉ����ăs�b�`��ς��܂��B
        /// </summary>
        private void UpdateSound()
        {
            var rpm = Mathf.Abs(wheelColliders[0].rpm);
            var ratio = Mathf.Clamp(rpm / maxRpmSe, 0, 1.0f);
            var currentRatio = (audioSource.pitch - minSePitch) / (maxSePitch - minSePitch);
            ratio = Mathf.Lerp(currentRatio, ratio, 0.1f);

            audioSource.pitch = Mathf.Lerp(minSePitch, maxSePitch, ratio);
        }

        /// <summary>
        /// �u���[�L�����v�X�V����
        /// 
        /// �o�b�N�̎��Ƀu���[�L�����v��_�������܂��B
        /// </summary>
        private void UpdateBacklight()
        {
            if (torque < 0.0f)
            {
                backlightMaterial.SetColor("_EmissionColor", backlightEmissionColor);
            }
            else
            {
                backlightMaterial.SetColor("_EmissionColor", Color.black);
            }
        }

        /// <summary>
        /// �A�N�Z�����̓C�x���g�n���h��
        /// </summary>
        /// <param name="context"></param>
        public void OnAccelerate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                torque = maxTorque * context.ReadValue<float>();
            }
            else if(context.canceled)
            {
                torque = 0.0f;
            }
        }

        /// <summary>
        /// �u���[�L���̓C�x���g�n���h��
        /// </summary>
        /// <param name="context"></param>
        public void OnBrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                brake = brakeTorque * context.ReadValue<float>();
            }
            else if (context.canceled)
            {
                brake = 0.0f;
            }
        }

        /// <summary>
        /// �X�e�A�����O���̓C�x���g�n���h��
        /// 
        /// ICarActions�̎����̂��߂����ɗp�ӂ��Ă���̂ŁA�������܂���B
        /// Steer�̓��͂�UpdateWheels()�ŏ��ReadValue���Ă��܂��B
        /// </summary>
        /// <param name="context"></param>
        public void OnSteer(InputAction.CallbackContext context)
        {
        }
    }
}
