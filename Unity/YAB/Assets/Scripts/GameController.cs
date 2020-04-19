using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private AlignmentController alignmentController;
        private HealthController healthController;
        public static GameController Instance { get; private set; }

        public bool Pause { get; private set; }
        public bool Halted { get; private set; }

        public GameObject EndView;
        public GameObject ResultText;
        public GameObject StoryText;

        public AudioClip[] Sounds;

        void Start()
        {
            Instance = this;

            alignmentController = GetComponent<AlignmentController>();
            healthController = GetComponent<HealthController>();
        }

        private void Update()
        {
            var health = healthController.Health;
            var alignment = alignmentController.Alignment;

            if (health <= 0 && !EndView.activeInHierarchy)
            {
                // TODO: Died
                Pause = true;
                ResultText.GetComponent<Text>().text = "YOU LOST!";
                StoryText.GetComponent<Text>().text = Loss;
                EndView.SetActive(true);
            }

            if (Mathf.Abs(alignment) == alignmentController.MaxAlign && !EndView.activeInHierarchy)
            {
                Pause = true;
                ResultText.GetComponent<Text>().text = "YOU WON!";
                if (alignment >= 0)
                {
                    StoryText.GetComponent<Text>().text = GoodWin;
                }
                else
                {
                    StoryText.GetComponent<Text>().text = EvilWin;
                }
                EndView.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Escape) || (EndView.activeInHierarchy && Input.anyKeyDown))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        public void Ate(GameObject thing)
        {
            Halted = true;

            GetComponent<AudioSource>().PlayOneShot(Sounds[UnityEngine.Random.Range(0, 3)]);

            var alignment = alignmentController.Alignment;

            var spec = thing.GetComponentInChildren<FoodSpecs>();
            alignmentController.AdjustAlignment(spec.Alignment);
            var health = CalcHealthChange(spec, alignment);

            healthController.AddHealth(health);
            GetComponent<ContainersController>().Remove(thing);
        }

        private static int CalcHealthChange(FoodSpecs spec, int alignment)
        {
            var health = spec.Health;
            if (
                (alignment > 0 && spec.Alignment == Alignment.Evil) ||
                (alignment < 0 && spec.Alignment == Alignment.Good)
            )
            {
                health *= -1;
            }

            return health;
        }

        public void GoodToGo()
        {
            Halted = false;
        }

        private const string EvilWin =
            @"The blob found itself as an evil entity, full of food and hope. Because of you it can now go into the world and fulfill it's evil deeds, taking over the world.

Click or press any key to go back to the menu.";

        private const string GoodWin =
            @"The blob found itself as a good entity, full of food and hope. Because of you it can now go into the world and fulfill it's good deeds, saving one old lady after another.

Click or press any key to go back to the menu.";

        private const string Loss =
            @"The blob died of hunger. You didn't manage to keep it alive. Because of you the world have one less blob to marvel at.

Click or press any key to go back to the menu.";
    }
}
