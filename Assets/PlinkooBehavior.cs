using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlinkooBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plinkoo2D;
    public float overallHappiness;
    public float food = 1;
    public float hunger = 0.02f;
    public float water = 1;
    public float thirst = 0.015f;
    public float poop = 0;
    public float poopiness = 0.01f;
    public float sexdrive = 0.0f;
    public float sexHappyLimit = 0.5f;
    public float lowerLimit = 0.2f;
    public float upperLimit = 0.8f;
    public float movingSpeed = 10f;
    private Transform foodStation;
    private Transform waterStation;
    private Transform pottyStation;
    private Transform sexStation;
    public GameObject myPrefab;
    public List<Transform> randomPoints;
    public GameObject canvasGO;
    public Sprite hungerImage;
    public Sprite waterImage;
    public Sprite poopImage;
    public Sprite sexImage;
    public Image canvasImage;
    IEnumerator waitingCoroutine;
    bool waitTimerRunning = false;
    Transform targetTransform;
    public enum States { Moving,Doing,Picking,Waiting,Suspended,Null };
    public enum Targets { food,water,potty,sex,random};
    Targets activeTarget;
    States state;
    States previouState;
    ToyBehavior theToy;
    public Camera cam;
    public NavMeshAgent agent;
    bool movingTimerStarted = true;
    bool targetPicked = false;
    void Start()
    {
        previouState = States.Waiting;
        state = States.Waiting;
        theToy = GameObject.FindGameObjectWithTag("toy").GetComponent<ToyBehavior>();
        foodStation = GameObject.FindGameObjectWithTag("food").GetComponent<Transform>();
        waterStation = GameObject.FindGameObjectWithTag("water").GetComponent<Transform>();
        pottyStation = GameObject.FindGameObjectWithTag("poo").GetComponent<Transform>();
        sexStation = GameObject.FindGameObjectWithTag("birth").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        Existence();
        MotionCheck();
    }

    void StateMachine()
    {
        
        switch(state)
        {
            case States.Waiting:
                Waiting();
                break;
            case States.Moving:
                Moving();
                break;
            case States.Doing:
                Doing();
                break;
            case States.Picking:
                PickingATarget(null);
                break;
            case States.Suspended:
                break;


        }
    }

   
    void MotionCheck()
    {
        canvasGO.transform.rotation = Quaternion.Euler(90, 0, 0);
       
        if (theToy.suspended && state!= States.Suspended)
        {
            previouState = state;
            state = States.Suspended;
            agent.enabled = false;
            canvasGO.SetActive(false);
            
            
        }
        
        if(!theToy.suspended)
        {
            if (state == States.Suspended)
            {
                state = previouState;
                agent.enabled = true;
                if (previouState == States.Moving) { 
                canvasGO.SetActive(true);
                }
            }
           
        }
       
    }

    void Doing()
    {
        targetPicked = false;
        if(activeTarget == Targets.random)
        {
            state = States.Waiting;
            waitTimerRunning = false;
        }
        if(activeTarget == Targets.food)
        {
            CurrencyBank currency = targetTransform.GetComponent<CurrencyBank>();
            if(currency.UseResource(Targets.food,1))
            {
                overallHappiness += 0.2f;
                food = 1;
            } else
            {
                overallHappiness -= 0.2f;
                food = 1;
            }
            state = States.Waiting;
            waitTimerRunning = false;
        }

        if (activeTarget == Targets.water)
        {
            CurrencyBank currency = targetTransform.GetComponent<CurrencyBank>();
            if (currency.UseResource(Targets.water,1))
            {
                overallHappiness += 0.2f;
                water = 1;
            }
            else
            {
                overallHappiness -= 0.2f;
                water = 1;
            }
            state = States.Waiting;
            waitTimerRunning = false;

        }

        if (activeTarget == Targets.potty)
        {
            CurrencyBank currency = targetTransform.GetComponent<CurrencyBank>();
            if (currency.DepositResource(Targets.potty,1))
            {
                overallHappiness += 0.2f;
                poop = 0;
            }
            else
            {
                overallHappiness -= 0.2f;
                poop = 0;
            }
            state = States.Waiting;
            waitTimerRunning = false;
        }

        if(activeTarget == Targets.sex)
        {
            overallHappiness = 0f;
            GameObject newPlinkoo = Instantiate(myPrefab, transform.position, Quaternion.identity, transform.parent);
            PlinkooBehavior newPlinkooControls = newPlinkoo.GetComponent<PlinkooBehavior>();
            newPlinkooControls.randomPoints = randomPoints;
            newPlinkooControls.myPrefab = myPrefab;
            state = States.Waiting;
            waitTimerRunning = false;
            sexdrive = 0;
        }

        canvasGO.SetActive(false);

    }

    void Moving()
    {
        
        Rigidbody plinkooBody = GetComponent<Rigidbody>();
        Vector3 movingDirection = targetTransform.position - transform.position;
        movingDirection = new Vector3(movingDirection.x, transform.position.y, movingDirection.z);
        
        Vector2 ignoredY = new Vector2(movingDirection.x, movingDirection.z);
        float distance = ignoredY.sqrMagnitude;
        /* 
         if (distance < 0.3f)
         {
             plinkooBody.velocity = Vector3.zero;
             plinkooBody.angularVelocity = Vector3.zero;

             state = States.Doing;
         } else
         {
             plinkooBody.velocity = movingSpeed * movingDirection.normalized;

         }
        */

        if (!movingTimerStarted)
        {
            movingTimerStarted = true;
            agent.SetDestination(targetTransform.position);

            //StartCoroutine(MovingDelayTimer());
        }
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)

                    state = States.Doing;
                    movingTimerStarted = false;
            
                
            }
        }



    }

    private IEnumerator MovingDelayTimer()
    {
        
        yield return new WaitForSeconds(2);
        agent.SetDestination(targetTransform.position);

    }
    void Existence()
    {
        if (food >0)
        {
            food -= hunger;
        } else
        {
            food = 0;
        }
        if( water>0)
        {
            water -= thirst;
        } else
        {
            water = 0;
        }
        if(poop <1)
        {
            poop += poopiness;
        } else
        {
            poop = 1;
        }
        if(overallHappiness >= sexHappyLimit)
        {
            sexdrive = upperLimit;
        }
    }

    void Waiting()
    {
        float waitTime = 3;
        if (!waitTimerRunning)
        {
            waitTimerRunning = true;
            waitingCoroutine = WaitTimer(waitTime);
            StartCoroutine(waitingCoroutine);
            
        }
    }


    public void GoToWait()
    {
        state = States.Waiting;
        targetPicked = false;
        waitTimerRunning = false;
        agent.enabled = true;
    }

    public void GoToSuspend()
    {
        state = States.Suspended;
        agent.enabled = false;
    }

    void PickingATarget(Transform target)
    {
        if (targetPicked == false)
        {
           
            StopCoroutine(waitingCoroutine);
            targetPicked = true;

            if (target != null)
            {
                targetTransform = target;
                
            }
            else
            {

                if (sexdrive >= upperLimit)
                {
                    targetTransform = sexStation;
                    activeTarget = Targets.sex;
                    state = States.Moving;
                    canvasGO.SetActive(true);
                    canvasImage.sprite = sexImage;
                }
                else
                {
                    if (poop >= upperLimit)
                    {
                        targetTransform = pottyStation;
                        activeTarget = Targets.potty;
                        canvasGO.SetActive(true);
                        canvasImage.sprite = poopImage;

                        state = States.Moving;
                    }
                    else
                    {


                        if (food <= lowerLimit)
                        {
                            targetTransform = foodStation;
                            activeTarget = Targets.food;
                            canvasGO.SetActive(true);
                            canvasImage.sprite = hungerImage;

                            state = States.Moving;

                        }
                        else
                        {
                            if (water <= lowerLimit)
                            {
                                targetTransform = waterStation;
                                canvasGO.SetActive(true);
                                activeTarget = Targets.water;
                                canvasImage.sprite = waterImage;
                                state = States.Moving;
                            }
                            else
                            {

                                targetTransform = ReturnARandomPoint();
                                canvasGO.SetActive(false);
                                canvasImage.sprite = null;
                                state = States.Moving;
                                activeTarget = Targets.random;
                            }
                        }
                    }
                }
            }
        }

        
    }

    private Transform ReturnARandomPoint()
    {
        Transform randomTransform = randomPoints[Random.Range(0, randomPoints.Count)];
        return randomTransform;
    }

    IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSeconds(time);

        state = States.Picking;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntryPortal>() != null)
        {
            other.GetComponent<EntryPortal>().RunPortal(gameObject,plinkoo2D);
        }
    }
}
