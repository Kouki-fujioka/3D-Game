using UnityEngine;
using Cinemachine;

namespace Unity.Game.Setup
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] float m_VerticalLookMinSensivity = 0.5f;
        [SerializeField, Range(0.25f, 1.0f)] float m_VerticalLookSensivityStep = 0.25f;

        CinemachineFreeLook m_FreeLookCamera;

        void Awake()
        {
            m_FreeLookCamera = FindFirstObjectByType<CinemachineFreeLook>();
        }
    }
}
