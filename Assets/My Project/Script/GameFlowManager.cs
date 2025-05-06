using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Unity.Game
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

        public static string PreviousScene { get; private set; }    // ���݂̃V�[����
        public bool GameIsEnding { get; private set; }  // �Q�[���I���t���O
        float m_GameOverSceneTime;  // �Q�[���I���V�[���ւ̑J�ڎ���
        string m_GameOverSceneToLoad;   // �Q�[���I�����ɓǂݍ��ރV�[����
        CinemachineFreeLook m_FreeLookCamera;
        string m_ControllerAxisXName;   // �J������ x ����
        string m_ControllerAxisYName;   // �J������ y ����

        private void Awake()
        {
            EventManager.AddListener<GameOverEvent>(OnGameOver);    // GameOverEvent �u���[�h�L���X�g���� OnGameOver ���s
            m_FreeLookCamera = FindFirstObjectByType<CinemachineFreeLook>();

#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;   // �J�[�\�����b�N
#endif

            if (m_FreeLookCamera)
            {
                // �J�������얳��
                m_ControllerAxisXName = m_FreeLookCamera.m_XAxis.m_InputAxisName;
                m_ControllerAxisYName = m_FreeLookCamera.m_YAxis.m_InputAxisName;
                m_FreeLookCamera.m_XAxis.m_InputAxisName = "";
                m_FreeLookCamera.m_YAxis.m_InputAxisName = "";
            }
        }
        
        void Start()
        {
            StartCoroutine(StartGameLockControll());
        }

        /// <summary>
        /// �Q�[���J�n�����莞�Ԍo�ߌ�ɃJ���������L��
        /// </summary>
        /// <returns></returns>
        IEnumerator StartGameLockControll()
        {
            while (m_StartGameLockedControllerTime > 0.0f)
            {
                m_StartGameLockedControllerTime -= Time.deltaTime;

                if (m_StartGameLockedControllerTime <= 0.0f)
                {
                    if (m_FreeLookCamera)
                    {
                        m_FreeLookCamera.m_XAxis.m_InputAxisName = m_ControllerAxisXName;
                        m_FreeLookCamera.m_YAxis.m_InputAxisName = m_ControllerAxisYName;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        void Update()
        {
            if (GameIsEnding)
            {
                if (Time.time >= m_GameOverSceneTime)
                {

#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.None; // �J�[�\�����b�N����
#endif

                    PreviousScene = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(m_GameOverSceneToLoad);  // ���s�ɉ������V�[�������[�h
                }
            }
        }

        /// <summary>
        /// �Q�[���I���t���O���I��<br/>
        /// ���s�ɉ����ăV�[����, �V�[���J�ڎ��Ԃ��i�[<br/>
        /// �J�������v���C���̒Ǐ]���~
        /// </summary>
        /// <param name="evt"></param>
        void OnGameOver(GameOverEvent evt)
        {
            if (!GameIsEnding)
            {
                GameIsEnding = true;

                if (evt.Win)
                {
                    m_GameOverSceneToLoad = m_WinScene;
                    m_GameOverSceneTime = Time.time + m_WinSceneDelay;
                }
                else
                {
                    m_GameOverSceneToLoad = m_LoseScene;
                    m_GameOverSceneTime = Time.time + m_LoseSceneDelay;

                    if (m_FreeLookCamera)
                    {
                        m_FreeLookCamera.Follow = null;
                    }
                }
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<GameOverEvent>(OnGameOver); // OnGameOver �o�^����
        }
    }
}
