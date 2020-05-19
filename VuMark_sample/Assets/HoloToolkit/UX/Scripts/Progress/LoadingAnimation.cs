// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;

namespace HoloToolkit.Examples.UX
{
    //but it is better to regularly reference in the project
    public class FadeObjectHTK : MonoBehaviour
    {
        [SerializeField]
        private float fadeTime = 0.5f;

        /// <summary>
        /// How long will the Fade effect last
        /// </summary>
        public float FadeTime
        {
            get
            {
                return fadeTime;
            }

            set
            {
                fadeTime = value;
            }
        }

        private bool autoFadeIn = false;

        /// <summary>
        /// Does the Fade effect occure automatically
        /// </summary>
        public bool AutoFadeIn
        {
            get
            {
                return autoFadeIn;
            }

            set
            {
                autoFadeIn = value;
            }
        }

        private float fadeCounter = 0;
        private Color cachedColor;
        private bool fadingIn = true;

        //cache material to prevent memory leak
        private Material cachedMaterial;

        private void Awake()
        {
            cachedMaterial = this.GetComponent<Renderer>().material;
            cachedColor = cachedMaterial.color;
        }

        private void Start()
        {
            if (AutoFadeIn)
            {
                FadeIn(true);
            }
        }

        private void OnEnable()
        {
            if (AutoFadeIn)
            {
                FadeIn(true);
            }
        }

        /// <summary>
        /// Begins the Fade in effect
        /// </summary>
        /// <param name="resetFade">should value return to original</param>
        public void FadeIn(bool resetFade)
        {
            if (cachedMaterial != null)
            {
                cachedColor = cachedMaterial.color;
            }

            if (resetFade && cachedMaterial)
            {
                cachedColor.a = 0;
                cachedMaterial.color = cachedColor;
            }

            fadeCounter = 0;
            fadingIn = true;
        }

        /// <summary>
        /// Resets the color to original before fade effect
        /// </summary>
        /// <param name="value">value to set the alpha to</param>
        public void ResetFade(float value)
        {
            if (cachedMaterial != null)
            {
                cachedColor = cachedMaterial.color;
                cachedColor.a = value;
                cachedMaterial.color = cachedColor;
            }

            fadeCounter = 0;
        }

        /// <summary>
        /// start the Fade out effect.
        /// </summary>
        /// <param name="resetStartValue">should original value be reset</param>
        public void FadeOut(bool resetStartValue)
        {
            if (cachedMaterial != null)
            {
                cachedColor = cachedMaterial.color;
            }

            if (resetStartValue && cachedMaterial)
            {
                cachedColor.a = 1;
                cachedMaterial.color = cachedColor;
            }

            fadeCounter = 0;
            fadingIn = false;
        }

        private void Update()
        {
            if (fadeCounter < FadeTime)
            {
                fadeCounter += Time.deltaTime;
                if (fadeCounter > FadeTime)
                {
                    fadeCounter = FadeTime;
                }

                float percent = fadeCounter / FadeTime;

                if (!fadingIn)
                {
                    percent = 1 - percent;
                    if (percent < cachedColor.a)
                    {
                        cachedColor.a = percent;
                    }
                }
                else
                {
                    if (percent > cachedColor.a)
                    {
                        cachedColor.a = percent;
                    }
                }

                if (cachedMaterial != null)
                {
                    cachedMaterial.color = cachedColor;
                }
            }
        }

        /// <summary>
        /// Event handler when object is deleted
        /// This cleans up cached material.
        /// </summary>
        public void OnDestroy()
        {
            Destroy(cachedMaterial);
        }
    }
    /// <summary>
    /// This class is used to setup and execute each of the loading animation effects of a progress indicator.
    /// </summary>
    public class LoadingAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] orbs;
        private bool startingLoader = false;
        private int startingIndex;

        public Vector3 CenterPoint = new Vector3();
        public Vector3 Axis = Vector3.forward;
        public float Radius = 0.075f;
        public float RevolutionSpeed = 1.9f;
        public int Revolutions = 3;
        public float AngleSpace = 12;
        public bool IsPaused = false;
        public bool SmoothEaseInOut = false;
        public float SmoothRatio = 0.65f;

        private float angle = 0;
        private float timeValue = 0;
        private int revolutionsCount = 0;
        private bool loopPause = false;
        private int fadeIndex = 0;
        private bool checkLoopPause = false;
        private Vector3 positionVector;
        private Vector3 rotatedPositionVector;
        private LoadingAnimation loadingAnimation;

        public GameObject[] Orbs
        {
            get
            {
                return orbs;
            }

            set
            {
                orbs = value;
            }
        }

        private void Start()
        {
            positionVector = transform.up;

            if (!Mathf.Approximately(Vector3.Angle(Axis, positionVector), 90))
            {
                positionVector = transform.forward;
                if (!Mathf.Approximately(Vector3.Angle(Axis, positionVector), 90))
                {
                    float x = Mathf.Abs(Axis.x);
                    float y = Mathf.Abs(Axis.y);
                    float z = Mathf.Abs(Axis.z);

                    if (x > y && x > z)
                    {
                        // left or right - cross with the z axis
                        positionVector = Vector3.Cross(Axis * Radius, Vector3.forward);
                    }

                    if (z > y && z > x)
                    {
                        // forward or backward - cross with the x axis
                        positionVector = Vector3.Cross(Axis * Radius, Vector3.right);
                    }

                    if (y > z && y > x)
                    {
                        // up or down - cross with the x axis
                        positionVector = Vector3.Cross(Axis * Radius, Vector3.right);
                    }
                }
            }
        }

        public void StartLoader()
        {
            loadingAnimation = gameObject.GetComponent<LoadingAnimation>();
            for (int i = 0; i < Orbs.Length; ++i)
            {
                Orbs[i].SetActive(false);
                FadeObjectHTK fade = Orbs[i].GetComponent<FadeObjectHTK>();
                fade.ResetFade(0);
            }

            startingLoader = true;
            startingIndex = 0;
            revolutionsCount = 0;
            IsPaused = false;
        }

        public void StopLoader()
        {
            for (int i = 0; i < Orbs.Length; ++i)
            {
                Orbs[i].gameObject.SetActive(false);
            }
        }

        public void StartOrbit()
        {
            IsPaused = false;
        }

        public void ResetOrbit()
        {
            angle = 0;
        }

        public float QuartEaseInOut(float s, float e, float v)
        {
            //e -= s;
            if ((v /= 0.5f) < 1)
            {
                return e / 2 * v * v * v * v + s;
            }

            return -e / 2 * ((v -= 2) * v * v * v - 2) + s;
        }

        private void Update()
        {
            if (IsPaused)
            {
                return;
            }

            float percentage = timeValue / RevolutionSpeed;

            for (int i = 0; i < Orbs.Length; ++i)
            {
                GameObject orb = Orbs[i];
                float orbPercentage = percentage - AngleSpace / 360 * i;
                if (orbPercentage < 0)
                {
                    orbPercentage = 1 + orbPercentage;
                }

                if (SmoothEaseInOut)
                {
                    float linearSmoothing = 1 * (orbPercentage * (1 - SmoothRatio));
                    orbPercentage = QuartEaseInOut(0, 1, orbPercentage) * SmoothRatio + linearSmoothing;
                }
                angle = 0 - (orbPercentage) * 360;

                if (startingLoader)
                {
                    if (orbPercentage >= 0 && orbPercentage < 0.5f)
                    {
                        if (i == startingIndex)
                        {
                            orb.SetActive(true);
                            if (i >= Orbs.Length - 1)
                            {
                                startingLoader = false;
                            }
                            startingIndex += 1;
                        }
                    }
                }

                orb.transform.Rotate(Axis, angle);
                rotatedPositionVector = Quaternion.AngleAxis(angle, Axis) * positionVector * Radius;
                orb.transform.localPosition = CenterPoint + rotatedPositionVector;

                if (checkLoopPause != loopPause)
                {
                    if (loopPause && orbPercentage > 0.25f)
                    {
                        if (i == fadeIndex)
                        {
                            FadeObjectHTK fade = orb.GetComponent<FadeObjectHTK>();
                            fade.FadeOut(false);
                            if (i >= Orbs.Length - 1)
                            {
                                checkLoopPause = loopPause;
                            }
                            fadeIndex += 1;
                        }

                    }

                    if (!loopPause && orbPercentage > 0.5f)
                    {
                        if (i == fadeIndex)
                        {
                            FadeObjectHTK fade = orb.GetComponent<FadeObjectHTK>();
                            fade.FadeIn(false);
                            if (i >= Orbs.Length - 1)
                            {
                                checkLoopPause = loopPause;
                            }
                            fadeIndex += 1;
                        }
                    }

                }
            }

            timeValue += Time.deltaTime;
            if (!loopPause)
            {
                if (timeValue >= RevolutionSpeed)
                {
                    timeValue = timeValue - RevolutionSpeed;

                    revolutionsCount += 1;

                    if (revolutionsCount >= Revolutions && Revolutions > 0)
                    {
                        loopPause = true;
                        fadeIndex = 0;
                        revolutionsCount = 0;
                        loadingAnimation.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (timeValue >= RevolutionSpeed)
                {
                    timeValue = 0;
                    revolutionsCount += 1;
                    if (revolutionsCount >= Revolutions * 0.25f)
                    {
                        fadeIndex = 0;
                        loopPause = false;
                        revolutionsCount = 0;
                    }
                }
            }
        }
    }
}

