using Recognizer.NDollar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeScript : MonoBehaviour {
    private GeometricRecognizer _rec = new GeometricRecognizer();
    List<PointR> _points = new List<PointR>();
    private List<List<PointR>> _strokes = new List<List<PointR>>(); 
    bool _isDown = false; // for testing on coputer
    public bool isRecording = true;
    Text shapeText;
    String[] fileNames =
    {
        "racine",
        "integrale",
        "square"
    };


	// Use this for initialization
	void Start () {
        //Load gesture in files

       
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
            
            //shapeText.text = j.ToString();
        
        
    
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            FingerUp(Input.mousePosition.x, Input.mousePosition.y);
        }
    }



    private void FingerDown(double x, double y)
    {
        print("FingerDown");
        _isDown = true;
        _points.Clear();
        // only clear strokes if we clicked recognize, Lisa 8/8/2009
        _points.Add(new PointR(x, y));
    }

    private void FingerMove(double x, double y)
    {
        if (_isDown)
        {
            print("FingerMove");

            //shapeText.text = "FingerMove";

            _points.Add(new PointR(x, y));
           
        }
    }

    // We are allowing multistroke gestures now, so don't clear points just because the pen
    // lifts or makes contact.
    // Lisa 12/22/2007
    private void FingerUp(double x, double y)
    {
        if (_isDown)
        {
            print("FingerUp");
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

    private void Recognize()
    {
        
        if (_points.Count >= 5) // require 5 points for a valid gesture
        {
           // shapeText.text = "Points > 5";


            if (_rec.NumGestures > 0) // not recording, so testing
            {
                print("numGestures >0");
               // shapeText.text = "numGestures >0";
                // combine the strokes into one unistroke, Lisa 8/8/2009
                List<PointR> points = new List<PointR>();
                foreach (List<PointR> pts in _strokes)
                {
                    points.AddRange(pts);
                }
                NBestList result = _rec.Recognize(points, _strokes.Count); // where all the action is!!
                if (result.Score == -1)
                {
                    print("Not found");
                    //shapeText.text = "Not found";
                }
                else
                {
                    String toto = String.Format("{0}: {1} ({2}px, {3}{4})\n[{5} out of {6} comparisons made]",
                    result.Name,
                    Math.Round(result.Score, 2),
                    Math.Round(result.Distance, 2),
                    Math.Round(result.Angle, 2), (char)176,
                    result.getActualComparisons(),
                    result.getTotalComparisons());
                    shapeText.text = toto;
                }
            }
        }
    }
}
