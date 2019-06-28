using UnityEngine;
using System.Collections;
using VRCamera.Scripts;
using UnityEngine.SceneManagement;

// The intro scene explains the experiment to the subject and waits for a response
public class IntroManager : MonoBehaviour
{
    [SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
    [SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
    [SerializeField] private SelectionSlider m_Slider;
    public int nextSceneNum;

    void Start()
    {
        m_Reticle.Show();
        m_Radial.Hide();
    }

    void OnEnable()
    {
        m_Slider.OnBarFilled += SwitchScene;
    }

    void OnDisable()
    {
        m_Slider.OnBarFilled -= SwitchScene;
    }

    void SwitchScene()
    {
        SceneManager.LoadScene(nextSceneNum);
    }

}
