using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class BouncyBall : MonoBehaviour
{
    public static BouncyBall instance;
    private Rigidbody bouncyRigidbody;
    //private SphereCollider sphereCollider;
    private TrailRenderer trail;
    private ParticleSystem explosionPs;
    private ParticleSystem dirtPS;
    private ParticleSystem contactCubeCoin;
    public Color color = Color.white;
    [Range(0, 20)] public float force = 0;
    [Range(1, 2)] public float forceDivide = 1.5F;
    private bool isFirstContact = false;
    public int hitMax = 20;
    public int hitCount = 0;
    public float velocitySpeed = 0;
    // public GameObject goal;
    // public List<GameObject> cubeCoins = new List<GameObject>();
    public int numCoinExist;
    public int score = 0;
    private Vector3 positionOrigin;
    private Vector3 rotationOrigin;
    private int lv = 1;
    private int countReset = 0;
    private const int resetMax = 2;
    private const int lvMax = 4;
    public bool isStop = false;
    public GameObject bouncyBall;
    void Awake()
    {
        BallSetup();
        RigidbodySetup();
        ParticleSetup();
        if (GameController.instance != null)
        {
            lv = GameController.instance.lv;
            score = GameController.instance._score;
        }
        _makeInstance();
        //GoalSetup();
        //ColliderSetup();
    }
    void _makeInstance()
    {
        if (instance != null)
            instance = this;
    }
    private void RigidbodySetup()
    {
        bouncyRigidbody = GetComponent<Rigidbody>();
        bouncyRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    //private void ColliderSetup()
    //{
    //    sphereCollider = GetComponent<SphereCollider>();
    //}

    private void BallSetup()
    {
        positionOrigin = transform.position;
        rotationOrigin = transform.rotation.eulerAngles;
        GetComponent<Renderer>().material.color = color;
        trail = GetComponent<TrailRenderer>();
        trail.startColor = color;
        isFirstContact = true;
    }

    private void ParticleSetup()
    {
        explosionPs = transform.GetChild(0).GetComponent<ParticleSystem>();
        var main = explosionPs.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);

        dirtPS = transform.GetChild(1).GetComponent<ParticleSystem>();

        contactCubeCoin = transform.GetChild(2).GetComponent<ParticleSystem>();
        main = contactCubeCoin.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
    }

    //private void GoalSetup()
    //{
    //    if (goal == null)
    //    {
    //        Debug.LogWarning("ERROR: Goal null.");
    //        return;
    //    }

    //    SpriteRenderer sr = goal.GetComponent<SpriteRenderer>();
    //    sr.color = new Color(color.r, color.g, color.b, sr.color.a);
    //}

    void Start()
    {
        Refresh();
    }
    void Refresh()
    {
        transform.position = positionOrigin;
        transform.rotation = Quaternion.Euler(rotationOrigin);
        bouncyRigidbody.velocity = Vector3.zero;
        trail.Clear();
        bouncyRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        hitCount = 0;
        isFirstContact = true;
        if (GameController.instance != null)
        {
            if (GameController.instance.lifeImage.Count == 0)
            {
                countReset++;
            }
        }
    }
    void Update()
    {
        // Direction ball to point of bouncing direction
        Vector3 normal = bouncyRigidbody.GetPointVelocity(transform.localPosition);
        transform.rotation = Quaternion.LookRotation(normal);
        velocitySpeed = bouncyRigidbody.velocity.magnitude;
        //_checkVelocity();
        _checkLose();
        /*int num = 0;
        for (int i = 0; i < cubeCoins.Count; ++i)
        {
            num += cubeCoins[i].gameObject.activeInHierarchy ? 1 : 0;
        }
        numCoinExist = num;*/
    }
    //public void _checkVelocity() {
    //    if (isStop == false)
    //    {
    //        velocitySpeed = bouncyRigidbody.velocity.magnitude;
    //    }
    //    else
    //    {
    //        isStop = false;
    //        //bouncyRigidbody.AddForce(Vector3.zero * force, ForceMode.Impulse);
    //        bouncyRigidbody.velocity = Vector3.forward*force;
    //        velocitySpeed = bouncyRigidbody.velocity.magnitude;
    //    }
    //    Debug.Log(velocitySpeed);
    //}
    public void _checkLose()
    {
        if (GameController.instance != null)
        {
            if (GameController.instance.lifeImage.Count == 0)
            {
                if (countReset > resetMax)
                {
                    if (Panel.instance != null)
                    {
                        Panel.instance._resetPanel();
                    }
                    countReset = 0;
                    TimeScale.instance.Scale = 0;
                    GameController.instance._setGameOver(score);
                    GameController.instance._resetScoreLv();
                    isStop = true;
                    return;
                }
            }
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("CubeCoin"))
        {
            if (ListCubeCoin.instance.cubeCoins.Contains(c.gameObject))
            {
                c.gameObject.SetActive(false);
                contactCubeCoin.Play();
                int index = ListCubeCoin.instance.cubeCoins.IndexOf(c.gameObject);
                ListCubeCoin.instance.cubeCoins.RemoveAt(index);
                Destroy(c.gameObject);
                score = ++GameController.instance._score;
                Debug.Log("yourScore:" +score);
                Debug.Log("bestScore:" +GameController.instance._getHighScore());
                GameController.instance._setScoreText(score);
                GameController.instance._setHighScore(score);
                Debug.Log("bestScore:" + GameController.instance._getHighScore());
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            if (Goal.instance != null)
            {
                if (collision.gameObject == Goal.instance.goal)
                {
                    if (ListCubeCoin.instance.cubeCoins.Count == 0)
                    {
                        if (lv == lvMax)
                        {
                            if (Panel.instance != null)
                            {
                                Panel.instance._resetPanel();
                            }
                            TimeScale.instance.Scale = 0;
                            GameController.instance._setWin(score);
                            GameController.instance._resetScoreLv();
                            isStop = true;
                            Drawer.instance.drawline = false;
                        }
                        else
                        {
                            if (Panel.instance != null)
                            {
                                Panel.instance._resetPanel();
                            }
                            lv = ++GameController.instance.lv;
                            GameController.instance._ReloadLife();
                            Drawer.instance._ReloadLine();
                            Drawer.instance.mode = Drawer.Mode.Draw;
                            SceneManager.LoadScene("lv" + lv);
                            return;
                        }
                    }
                    else
                    {
                        Refresh();
                        return;
                    }

                }
            }
            if (collision.gameObject.tag == "TerrainContact")
            {
                hitCount++;
                if (hitCount >= hitMax)
                {
                    StartCoroutine(Stop());
                    return;
                }

                dirtPS.Play();
            }

            if (isFirstContact == true)
            {
                bouncyRigidbody.velocity = bouncyRigidbody.velocity / forceDivide;
                isFirstContact = false;
            }

            if (collision.gameObject.tag == "Ball")
            {
                // Debug.Log(name + " contact collision ball " + collision.gameObject.name);
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
            {
                Refresh();
                return;
            }
        }
    }

    private IEnumerator Stop()
    {
        dirtPS.Stop();
        explosionPs.Play();
        GetComponent<MeshRenderer>().enabled = false;
        trail.enabled = false;
        yield return new WaitForSeconds(0.1F);
        explosionPs.Stop();
        GetComponent<MeshRenderer>().enabled = true;
        trail.enabled = true;
        Refresh();
    }
    public void _resetBall()
    {
        Refresh();
    }
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = color;
    //    Vector3 center = transform.position;
    //    Vector3 direction = center + transform.forward * 2;
    //    Gizmos.DrawLine(center, direction);
    //    Gizmos.DrawWireCube(direction, Vector3.one * 0.3F);
    //}
}