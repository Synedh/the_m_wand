using Recognizer.NDollar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeScript : MonoBehaviour
{
    public bool isRecording;
    public float minimalRecognitionScore;
    GeometricRecognizer _rec = new GeometricRecognizer();
    List<PointR> _points = new List<PointR>();
    List<List<PointR>> _strokes = new List<List<PointR>>(); 
    bool _isDown = false; // for testing on coputer
    spellManager SpellManager;
    Text shapeText;
	TrailRenderer trail;
    bool waitForInverseSecondPart = false;
    Sound swipeSound;

    String[] fileNames =
    {
        "racine",
        "integrale",
        "square",
		"exponentielle",
		"logarithme",
		"lineaireNegative",
		"lineairePositive",
        "inverse_first_part",
        "inverse_second_part",
        "inverse",
        "derivee"
    };


	// Use this for initialization
	void Start () {
        //Load gesture in files

	    trail = GetComponent<TrailRenderer> ();
	//	
        shapeText = GameObject.Find("ShapeText").GetComponent<Text>();
        int j = 0;
        for (int i = 0; i < fileNames.Length; i++)
        {
            string name = fileNames[i];
            try
            {
                if (_rec.LoadGesture(name))
                {
                    j++;
                }
            }
            catch(Exception e)
            {
                shapeText.text = e.Message + e.StackTrace;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spellManager.Instance.isDragged)
            return;
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
        {
            Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                this.transform.position = mRay.GetPoint(rayDistance);
            }

            FingerMove(Input.mousePosition.x, Input.mousePosition.y);
        }

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            FingerDown(Input.mousePosition.x, Input.mousePosition.y);
			swipeSound = Sound.loadSound("Sounds/write_blackboard");
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            FingerUp(Input.mousePosition.x, Input.mousePosition.y);
            swipeSound.stop();
        }
    }



    void FingerDown(double x, double y)
	{

		trail.enabled = true;
		trail.Clear ();
       
        _isDown = true;
        _points.Clear();
        // only clear strokes if we clicked recognize, Lisa 8/8/2009
        _points.Add(new PointR(x, y));
    }

    void FingerMove(double x, double y)
    {
        if (_isDown)
        {
            //shapeText.text = "FingerMove";
            _points.Add(new PointR(x, y));
           
        }
    }

    // We are allowing multistroke gestures now, so don't clear points just because the pen
    // lifts or makes contact.
    // Lisa 12/22/2007
    void FingerUp(double x, double y)
    {
		
        if (_isDown)
        {
			trail.enabled = false;

            
            _isDown = false;

            // moved the recognize handling code to the Recognize_Click() method
            // Lisa 12/22/2007
            // but when we pick the mouse up we want to store the stroke boundaries
            // Lisa 1/2/2008
            // still need this for recording from the canvas, Lisa 8/8/2009
            /*if (_numPtsInStroke.Count == 0)
                _numPtsInStroke.Add(_points.Count);
            else _numPtsInStroke.Add(_points.Count - (int)(_numPtsInStroke[_numPtsInStroke.Count - 1]));*/

            // revised to save to an ArrayList of Strokes too, Lisa 8/8/2009
            _strokes.Add(new List<PointR>(_points)); // need to copy so they don't get cleared
			//_rec.SaveGesture("lastRecord.xml", _strokes);
			if (isRecording)
                // resample, scale, translate to origin
                _rec.SaveGesture("lastRecord.xml", _strokes);  // strokes, not points; Lisa 8/8/2009 
            else
            {
               
                Recognize();
                _strokes.Clear();
                _points.Clear();
            }
          
        }
    }

    void spellNotFound()
    {
        Sound.loadSound("Sounds/buzz_error");
        Shake.sendShake(0.3f, 0.08f);
    }

    void Recognize()
    {
        if (_points.Count >= 20) // require 5 points for a valid gesture
        {
            if (_rec.NumGestures > 0) // not recording, so testing
            {
                // shapeText.text = "numGestures >0";
                // combine the strokes into one unistroke, Lisa 8/8/2009
                List<PointR> points = new List<PointR>();
                foreach (List<PointR> pts in _strokes)
                {
                    points.AddRange(pts);
                }
                NBestList result = _rec.Recognize(points, _strokes.Count); // where all the action is!!
                if (result.Score <= minimalRecognitionScore || result.Name.Equals("inverseSecondPart") && !waitForInverseSecondPart)
                {
                    spellNotFound();
                }
                else if (result.Name.Equals("inverseFirstPart"))
                {
                    waitForInverseSecondPart = true;
                }
                else
                {
                    String name = result.Name;
                    if (result.Name.Equals("inverseSecondPart") && waitForInverseSecondPart)
                    {
                            name = "inverse";
                            waitForInverseSecondPart = false;
                    }
                    /* 
                    String res = String.Format("{0}: {1} ({2}px, {3}{4})\n[{5} out of {6} comparisons made]",
                                                name,
                                                Math.Round(result.Score, 2),
                                                Math.Round(result.Distance, 2),
                                                Math.Round(result.Angle, 2), (char)176,
                                                result.getActualComparisons(),
                                                result.getTotalComparisons()
                    ); */
                    shapeText.text = String.Format("{0} - {1}", name, Math.Round(result.Score, 2));
                    if (Assets.Scripts.Fonctions.Tree.getDict().ContainsKey(name))
                        spellManager.Instance.addSpell(name);
                    else {
                        // Found but not in Tree for this level.
                        spellNotFound();
                    }
                }
            }
        }
    }
}
