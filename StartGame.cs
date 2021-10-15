using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] Mas;
    [SerializeField] GameObject ConScene;
    [SerializeField] ParticleSystem WinParticle;
    [SerializeField] ParticleSystem WinParticle1;
    [SerializeField] ParticleSystem WinParticle2;
    [SerializeField] ParticleSystem WinParticle3;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject GroundUI;
    [SerializeField] string playAnimTrue;
    [SerializeField] string playAnimFalse;
    [SerializeField] AudioClip Audio1;
    [SerializeField] AudioClip Audio2;
    [SerializeField] AudioClip Audio3;
    [SerializeField] AudioClip Audio4;
    [SerializeField] AudioClip Audio5;
    [SerializeField] AudioClip Audio6;
    [SerializeField] AudioClip Audio7;
    [SerializeField] AudioClip Audio8;
    GameObject[] bubbles = new GameObject[6];
    GameObject champion;
    bool Pause=false;
    bool final = true;
    GameObject NULL;


    void Start()
    {
        create();
        winner();
        input();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

           if (!Pause)
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.tag == "Winner")
                {
                        hit.collider.GetComponent<Animator>().Play(playAnimTrue);
                       
                        AudioRandomPohvala();
                        hit.transform.tag = "WinColor";
                        PauseOn();
                        if (final)
                        {
                            findMas();
                            winnerTT();
                            Invoke("AudioChamp", 1.5f);

                        }
                        NULL = GameObject.FindGameObjectWithTag("Winner");
                        if (NULL == null)
                        {
                            Invoke("salut", 1.0f);
                            GetComponent<AudioSource>().PlayOneShot(Audio8);
                            Invoke("PauseMenu", 3.0f);
                        }
                        Invoke("PauseOff", 1.5f);
                    }
                else if (hit.transform.tag == "GameColor" || hit.transform.tag=="WinColor")
                    {
                        hit.collider.GetComponent<Animator>().Play(playAnimFalse);
                        AudioRandomNasar();
                        PauseOn();
                        Invoke("PauseOff", 1f);
                    }
            }
        }
    }
    void PauseMenu()
    {
        PauseMenuUI.SetActive(true);
        GroundUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void create() 
    {
        for (int i = 0; i < 6; i++)
        {
            bubbles[i] = spawnPoints[Random.Range(0, spawnPoints.Length)];
            for (int j = 0; j < i; j++)
            {
                if (bubbles[i] == bubbles[j])
                    i--;
            }
            bubbles[i].transform.tag = "GameColor";
            bubbles[i].GetComponent<AudioSource>().playOnAwake = false;
        }
    }
    
    public void winner()
    {
        champion = bubbles[Random.Range(0, bubbles.Length)];
        champion.transform.tag = "Winner";
        champion.GetComponent<AudioSource>().playOnAwake = true;
    }
    
    void input() 
    {
        int x = 2; 
        float y = -2.5f;
        for (int i = 0; i < 6; i++)
                if (x <= 14)
                {
                    Vector2 pos = new Vector2(x, y);
                    Instantiate(bubbles[i], pos, Quaternion.identity);  
                    x += 5;
                }
                else 
                {
                i--;
                   x = 2;
                   y = 2.5f;
                }
    }  
    void repeatscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void salut()
    {
        WinParticle.Play();
        WinParticle1.Play();
        WinParticle2.Play();
        WinParticle3.Play();
        AudioPlay(Audio8);
    }

    void AudioPlay(AudioClip soundAnimOne)
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(soundAnimOne);
        }
    }
    void PauseOn()
    {
        Pause = true;
    }
    void PauseOff()
    {
        Pause = false;
    }
    void findMas()
    {
        Mas = GameObject.FindGameObjectsWithTag("GameColor");
    }
    void winnerTT() 
    {
        champion = Mas[Random.Range(0, Mas.Length)];
        champion.transform.tag = "Winner";

        if (Mas.Length == 1) final = false;
    }
    void AudioChamp()
    {
        champion.GetComponent<AudioSource>().Play();
    }
    void AudioRandomPohvala()
    {
        int x = Random.Range(0, 3);
        if (x == 0)
            AudioPlay(Audio1);
        if (x == 1)
            AudioPlay(Audio2);
        if (x ==2)
            AudioPlay(Audio3);
    }
    void AudioRandomNasar()
    {
        int x = Random.Range(0, 4);
        if (x == 0)
            AudioPlay(Audio4);
        if (x == 1)
            AudioPlay(Audio5);
        if (x == 2)
            AudioPlay(Audio6);
        if (x == 3)
            AudioPlay(Audio7);
    }
}
