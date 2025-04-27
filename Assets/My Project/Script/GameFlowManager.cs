using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

        //CinemachineFreeLook m_FreeLookCamera;
        string m_ControllerAxisXName;
        string m_ControllerAxisYName;

        private void Awake()
        {
            //m_FreeLookCamera = 
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
