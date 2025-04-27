using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Unity.Game.Setup
{
    public class GameFlowManager : MonoBehaviour
    {
        [Header("WinScene")]
        [SerializeField, Tooltip("�Q�[���������Ƀ��[�h����V�[��")] string m_WinScene = "Menu Win";
        [SerializeField, Tooltip("�Q�[���������Ƀ��[�h����V�[���ւ̑J�ڎ���")] float m_WinSceneDelay = 5.0f;    // �A�j���[�V�����I������

        [Header("LoseScene")]
        [SerializeField, Tooltip("�Q�[���s�k���Ƀ��[�h����V�[��")] string m_LoseScene = "Menu Lose";
        [SerializeField, Tooltip("�Q�[���s�k���Ƀ��[�h����V�[���ւ̑J�ڎ���")] float m_LoseSceneDelay = 3.0f;

        [Header("StartGameLockedControllerTime")]
        [SerializeField, Tooltip("�Q�[���J�n���ɃJ��������𖳌��ɂ��鎞��")] float m_StartGameLockedControllerTime = 0.3f;

        CinemachineFreeLook m_FreeLookCamera;
        string m_ControllerAxisXName;
        string m_ControllerAxisYName;

        private void Awake()
        {
            m_FreeLookCamera = FindFirstObjectByType<CinemachineFreeLook>();

            #if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;   // �J�[�\�����b�N
            #endif

            if (m_FreeLookCamera)
            {
                m_ControllerAxisXName = m_FreeLookCamera.m_XAxis.m_InputAxisName;   // x �������i�[
                m_ControllerAxisYName = m_FreeLookCamera.m_YAxis.m_InputAxisName;   // y �������i�[
                m_FreeLookCamera.m_XAxis.m_InputAxisName = "";  // x ���̓��͖���
                m_FreeLookCamera.m_YAxis.m_InputAxisName = "";  // y ���̓��͖���
            }
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(StartGameLockControll());    // �Q�[���J�n����̓J��������𖳌�
        }

        IEnumerator StartGameLockControll()
        {
            while (m_StartGameLockedControllerTime > 0.0f)
            {
                m_StartGameLockedControllerTime -= Time.deltaTime;

                if (m_StartGameLockedControllerTime <= 0.0f)
                {
                    if (m_FreeLookCamera)
                    {
                        m_FreeLookCamera.m_XAxis.m_InputAxisName = m_ControllerAxisXName;  // x ���̓��͗L��
                        m_FreeLookCamera.m_YAxis.m_InputAxisName = m_ControllerAxisYName;  // y ���̓��͗L��
                    }
                }

                yield return new WaitForEndOfFrame();   // ���t���[���̃����_�����O������ɍĊJ
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
