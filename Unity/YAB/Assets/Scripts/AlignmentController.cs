using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class AlignmentController : MonoBehaviour
    {
        public GameObject AlignmentBar;
        public GameObject Body;
        public GameObject Horns;
        public GameObject Halo;

        public int Alignment = 0;

        readonly Dictionary<Alignment, int> adjustments = new Dictionary<Alignment, int>
        {
            { Scripts.Alignment.Evil, -1 },
            { Scripts.Alignment.Good, 1 },
            { Scripts.Alignment.Neutral, 0 }
        };
        
        public int MaxAlign = 10;
        private const int BarWidth = 400;
        private int dirInc;

        private float lastTick;
        private RectTransform alignTransform;

        public void AdjustAlignment(Alignment type)
        {
            Alignment = Mathf.Clamp(Alignment + adjustments[type], MaxAlign * -1, MaxAlign);
            UpdateAlignmentVisuals();
        }

        // Start is called before the first frame update

        void Start()
        {
            dirInc = BarWidth / 2 / MaxAlign;

            lastTick = Time.time;
            alignTransform = (RectTransform)AlignmentBar.transform;

            Horns.transform.localScale = Vector3.one * (.72f);
            var haloMat = Halo.GetComponent<Renderer>().material;
            haloMat.SetColor("_Color", new Color(1, 1, 0.616f, 0));
            haloMat.SetColor("_EmissionColor", new Color(0.74f, 0.55f, 0, 0));
        }

        // Update is called once per frame

        void Update()
        {
        }

        private void UpdateAlignmentVisuals()
        {
            alignTransform.anchoredPosition = new Vector2(dirInc * Alignment, -85);

            float pct;
            var bodyMat = Body.GetComponent<Renderer>().material;
            var haloMat = Halo.GetComponent<Renderer>().material;

            if (Alignment >= 0)
            {
                pct = (float) Alignment / MaxAlign;
                Horns.transform.localScale = Vector3.one * .72f;
                
                haloMat.SetColor("_Color", new Color(1, 1, 0.616f, pct));
                haloMat.SetColor("_EmissionColor", new Color(0.74f, 0.55f, 0, pct * .5f));

                bodyMat.SetColor("_Color", new Color(.5f - .5f * pct, .5f + .5f * pct, .5f - .5f * pct));
            }
            else
            {
                pct = (float) Mathf.Abs(Alignment) / MaxAlign;
                Horns.transform.localScale = Vector3.one * (.72f + pct * .28f);
            
                haloMat.SetColor("_Color", new Color(1, 1, 0.616f, 0));
                haloMat.SetColor("_EmissionColor", new Color(0.74f, 0.55f, 0, 0));
                
                bodyMat.SetColor("_Color", new Color(.5f + .5f * pct, .5f - .5f * pct, .5f - .5f * pct));
            }

        }
    }
}
