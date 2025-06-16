using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Search;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // [Tooltip("The UI element that displays the number of bulls in the combo")] [SerializeField]
    // private TextMeshProUGUI ComboLengthDisplay;
    
    // [Tooltip("The UI element that displays the amount of points in the combo")] [SerializeField]
    // private TextMeshProUGUI ComboPointsDisplay;

    // [Tooltip("The label for the combos")] [SerializeField]
    // private TextMeshProUGUI ComboLabel;

    // [FormerlySerializedAs("XDisplay")] [Tooltip("Multiplier symbol for the combo")] [SerializeField]
    // private TextMeshProUGUI MultiplierDisplay;
    
    [Tooltip("The UI element that displays the total score")] [SerializeField]
    private TextMeshProUGUI TotalPointsDisplay;

    // [Tooltip("The number of points that each bull is worth")] [SerializeField]
    // private int PointsPerBull;  // consider refactoring into a bull script

    // [Tooltip("Count points per second")] [SerializeField]
    // private int PointsPerSecond;

    [Tooltip("Amount of time to spend counting up points")] [SerializeField]
    private float CountPointsDuration;
    
    [FormerlySerializedAs("ScorePointJuice")]
    [Header("Juice")]
    [SerializeField] private Juice ScorePointsJuice;
    [SerializeField] private Juice CountPointsJuice;
    
    // public int ComboLength { get; private set; }
    // private int _comboPoints;
    private int _totalPoints;
    private int _displayedPoints;
    private bool _isCountingPoints;

    private void Awake()
    {
        if (Instance is null)
        {
            ResetScore();
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        UpdateDisplays();
    }

    public void Score(int points)
    {
        // ComboLength++;
        _totalPoints += points;
        
        if (!_isCountingPoints)
            StartCoroutine(CountUpPoints());
        
        if (ScorePointsJuice)
            ScorePointsJuice.Play();
    }

    public void UpdateDisplays()
    {
        // string comboText = ComboLength == 0 ? "" : ComboLength + "";
        // string comboPointsText = _comboPoints == 0 ? "" : _comboPoints.ToString("N0");
        // string multiplierText = ComboLength == 0 ? "" : "x";
        
        // ComboLengthDisplay.text = comboText;
        // ComboPointsDisplay.text = comboPointsText;
        // MultiplierDisplay.text = multiplierText;
        TotalPointsDisplay.text = _displayedPoints.ToString("N0");

        // ComboLabel.gameObject.SetActive(ComboLength != 0);
    }

    // public void EndCombo()
    // {
    //     // int pointsAdded = ComboLength * _comboPoints;
    //     
    //     // No point doing any of this stuff if we're not adding any points
    //     if (pointsAdded == 0)
    //         return;
    //     
    //     _totalPoints += pointsAdded;
    //     // ComboLength = 0;
    //     // _comboPoints = 0;
    //     if (!_isCountingPoints)
    //         StartCoroutine(CountUpPoints(pointsAdded));
    // }

    private IEnumerator CountUpPoints()
    {
        _isCountingPoints = true;
        CountPointsJuice.Play();
        
        float timer = 0.0f;
        int ogPoints = _displayedPoints;
        while (timer < CountPointsDuration)
        {
            _displayedPoints = (int)Mathf.Lerp(ogPoints, _totalPoints, timer / CountPointsDuration);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        _displayedPoints = _totalPoints;
        _isCountingPoints = false;
    }

    public void ResetScore()
    {
        _totalPoints = 0;
        _displayedPoints = 0;
    }
}
