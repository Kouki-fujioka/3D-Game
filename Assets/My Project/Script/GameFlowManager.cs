using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Unity.Game.Setup
{
    public class GameFlowManager : MonoBehaviour
    {
        [Header("WinScene")]
        [SerializeField, Tooltip("ゲーム勝利時にロードするシーン")] string m_WinScene = "Menu Win";
        [SerializeField, Tooltip("ゲーム勝利時にロードするシーンへの遷移時間")] float m_WinSceneDelay = 5.0f;    // アニメーション終了時間

        [Header("LoseScene")]
        [SerializeField, Tooltip("ゲーム敗北時にロードするシーン")] string m_LoseScene = "Menu Lose";
        [SerializeField, Tooltip("ゲーム敗北時にロードするシーンへの遷移時間")] float m_LoseSceneDelay = 3.0f;

        [Header("StartGameLockedControllerTime")]
        [SerializeField, Tooltip("ゲーム開始時にカメラ操作を無効にする時間")] float m_StartGameLockedControllerTime = 0.3f;

        CinemachineFreeLook m_FreeLookCamera;
        string m_ControllerAxisXName;
        string m_ControllerAxisYName;

        private void Awake()
        {
            m_FreeLookCamera = FindFirstObjectByType<CinemachineFreeLook>();

            #if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;   // カーソルロック
            #endif

            if (m_FreeLookCamera)
            {
                m_ControllerAxisXName = m_FreeLookCamera.m_XAxis.m_InputAxisName;   // x 軸名を格納
                m_ControllerAxisYName = m_FreeLookCamera.m_YAxis.m_InputAxisName;   // y 軸名を格納
                m_FreeLookCamera.m_XAxis.m_InputAxisName = "";  // x 軸の入力無効
                m_FreeLookCamera.m_YAxis.m_InputAxisName = "";  // y 軸の入力無効
            }
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(StartGameLockControll());    // ゲーム開始直後はカメラ操作を無効
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
                        m_FreeLookCamera.m_XAxis.m_InputAxisName = m_ControllerAxisXName;  // x 軸の入力有効
                        m_FreeLookCamera.m_YAxis.m_InputAxisName = m_ControllerAxisYName;  // y 軸の入力有効
                    }
                }

                yield return new WaitForEndOfFrame();   // 現フレームのレンダリング完了後に再開
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
