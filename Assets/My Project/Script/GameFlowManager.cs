using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
