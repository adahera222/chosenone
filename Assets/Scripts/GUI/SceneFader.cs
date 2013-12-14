using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    public enum FadeType
    {
        none,
        FadeOutFinished,
        fadeIn,
        fadeOutFailure,
        fadeOutSuccess
    }

    const float standardFadeDuration = 1.0f;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    public Texture2D fadeTexture;

    private Rect _screenRect;
    private Color _currentColor;
    private Color _startColor;
    private Color _targetColor;
    private float _fadeTime = 1.0f;
    private float _currentTime = 0;

    private FadeType _currentFadeType = FadeType.none;

    private Color _guiColor = Color.white;

    // ================================================================================
    //  unity methods
    // --------------------------------------------------------------------------------

    void Awake()
    {
        // create white blank texture
        //if (fadeTexture == null)
        fadeTexture = Utilities.CreateBlankTexture(Color.white);

        _currentColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentFadeType != FadeType.none && _currentFadeType != FadeType.FadeOutFinished)
        {
            _currentTime += Time.deltaTime;
        }

        switch (_currentFadeType)
        {
            case FadeType.none:
                break;
            case FadeType.fadeIn:
                FadeInUpdate();
                break;
            case FadeType.fadeOutFailure:
                FadeOutUpdate();
                break;
            case FadeType.fadeOutSuccess:
                FadeOutUpdate();
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
        if (_currentFadeType != FadeType.none)
        {
            GUI.depth = 0;
            GUI.color = _currentColor;
            GUI.DrawTexture(_screenRect, fadeTexture, ScaleMode.StretchToFill);
        }
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void FadeIn(float? fadeTime = null)
    {
        CalculateScreenRect();

        _currentFadeType = FadeType.fadeIn;
        _startColor = Color.white;
        _targetColor = Color.white;
        _targetColor.a = 0f;
        _currentTime = 0;

        _fadeTime = fadeTime.GetValueOrDefault(standardFadeDuration);
    }

    public void FadeOutFailure(float? fadeTime = null)
    {
        CalculateScreenRect();

        _currentFadeType = FadeType.fadeOutFailure;
        _startColor = Color.white;
        _startColor.a = 0f;
        _targetColor = Color.white;
        _currentTime = 0;

        _fadeTime = fadeTime.GetValueOrDefault(standardFadeDuration);
    }

    public void FadeOutSuccess(float? fadeTime = null)
    {
        CalculateScreenRect();

        _currentFadeType = FadeType.fadeOutSuccess;
        _startColor = Color.white;
        _startColor.a = 0f;
        _targetColor = _guiColor;
        _currentTime = 0;

        _fadeTime = fadeTime.GetValueOrDefault(standardFadeDuration);
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    void CalculateScreenRect()
    {
        _screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    void FadeInUpdate()
    {
        float progress = _currentTime / _fadeTime;
        _currentColor = Color.Lerp(_startColor, _targetColor, progress);

        if (progress >= 1)
        {
            _currentFadeType = FadeType.none;
        }
    }

    void FadeOutUpdate()
    {
        float progress = _currentTime / _fadeTime;
        _currentColor = Color.Lerp(_startColor, _targetColor, _currentTime / _fadeTime);

        if (progress >= 1)
        {
            _currentFadeType = FadeType.FadeOutFinished;
        }
    }
}
