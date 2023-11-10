using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField] private int target_framerate = 60;
    private void Awake()
    {
        Application.targetFrameRate = target_framerate;
    }
}
