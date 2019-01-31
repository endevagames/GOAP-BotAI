using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.AI;
public class GOAPAgent : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string name;
        public int count;

        public InventoryItem(string name, int num)
        {
            this.name = name;
            count = num;
        }

        public InventoryItem(string name)
        {
            this.name = name;
        }
    }

    //Stores a list of actions for the current agent
    public List<GOAPAction> actionList;
    //Create FSM and states
    private FSM stateMachine;
    private FSM.FSMState idleState; // finds something to do
    private FSM.FSMState goToState; // moves to a target
    private FSM.FSMState animateState; // performs an animation
    public List<GOAPAction> ActionList { get { return actionList; } }
    //We'll use a List of states to define our goalState and worldState
    private List<GOAPState> worldState;
    private List<GOAPState> goalState;

    public List<InventoryItem> backpackList;
    public List<InventoryItem> carryingList;
    public Queue<GOAPAction> actionQueue = new Queue<GOAPAction>();
    public GOAPPlanner planner;
    private bool targetSet;
    private GameObject target;
    public GOAPGetWorldState worldStateGetter;
    private GOAPAction currentAction;
    private bool actionActivated = false;
    
    public GameObject Target { get{ return target;}}
    void Start()
    {
        //Set the planner
        planner = new GOAPPlanner();
        stateMachine = new FSM();
        idleStateInit();
        animateStateInit();
        goToStateInit();
        stateMachine.pushState(idleState);
    }

    void Update()
    {
        stateMachine.Update(this.gameObject);
    }

    //Idle state deals with getting the plan
    void idleStateInit()
    {
		idleState = (fsm, gameObj) => {
            worldState = worldStateGetter.getWorldState(this);
            goalState = worldStateGetter.getGoalState(worldState);
            currentAction = null;
            Queue<GOAPAction> plan = planner.GetPlan(worldState, goalState, actionList);
            if(plan.Count > 0)
            {
                //We have a plan
                actionQueue = plan;
                fsm.popState(); // move to PerformAction state
                Debug.Log(prettyPrint(plan));
                fsm.pushState(animateState);
            }
            else
            {
                fsm.popState();
                fsm.pushState(idleState);
            }
        };
    }
    //Animation state plays an animation such as shooting, swinging sword. The effect of the animation should be programmed outside of the GOAP system.
    //The GOAP system only plays animations and moves the agent
    void animateStateInit()
    {
        animateState = (fsm, gameObj) =>
        {
            GOAPAction action = actionQueue.Peek();
            if (action.isDone(this) && actionActivated)
            {
                // the action is done. Remove it so we can perform the next one
                actionActivated = false;
                actionQueue.Dequeue();
                if(actionQueue.Count == 0)
                {
                    fsm.popState();
                    fsm.pushState(idleState);
                }
            }
            else
            {
                if(action.requiresInRange) 
                {
                    target = FindClosestTarget(action.targetTag);
                    if(target == null)
                    {
                        //No target. Action failed
                        fsm.popState();
                        fsm.pushState(idleState);
                    }
                    else
                    {
                        if(Vector3.Distance(target.transform.position, this.transform.position) > action.range)
                        {
                            targetSet = false;
                            fsm.pushState(goToState);
                        }
                        else if(action != currentAction) 
                        {
                            currentAction = action;
                            currentAction.activate(this);   
                            actionActivated = true;
                        }
                    }
                }
                else if(currentAction != action)
                {
                    currentAction = action;
                    currentAction.activate(this);
                    actionActivated = true;
                }
            }
        };
    }

    void goToStateInit()
    {
        goToState = (fsm, gameObj) =>
        {
            GOAPAction action = actionQueue.Peek();
			if (action.requiresInRange && target == null) {
				Debug.Log("<color=red>Fatal error:</color> Action requires a target but has none. Planning failed. You did not assign the target in your Action.checkProceduralPrecondition()");
				fsm.popState(); // move
				fsm.popState(); // animate
				fsm.pushState(idleState);
				return;
			}

			// get the agent to move itself
			NavMeshAgent navAgent = GetComponentInParent<NavMeshAgent>();
            if(navAgent == null) 
            {
                fsm.popState(); // move
                fsm.popState(); // perform
                fsm.pushState(idleState);
                Debug.Log("<color=red>Fatal error:</color> No NavMeshAgent attached.");
            }
            else
            {
                if(!targetSet)
                {
                    Vector3 goToPoint = target.transform.position;
                    Vector2 offset = Random.insideUnitCircle * action.range;
                    goToPoint.x += offset.x;
                    goToPoint.z += offset.y;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(goToPoint, out hit, action.range, NavMesh.AllAreas)) 
                    {
                        navAgent.SetDestination(hit.position);
                        targetSet = true;
                    }
                    else 
                    {
                        fsm.popState(); // move
                        fsm.popState(); // perform
                        fsm.pushState(idleState);
                    }
                }
                else
                {
                    if (!navAgent.pathPending)
                    {
                        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                        {
                            if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                            {
                                fsm.popState(); 
                            }
                        }
                    }
                }
            }
        };
    }
    public GameObject FindClosestTarget(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public static string prettyPrint(Queue<GOAPAction> actions)
    {
        string s = "START->";
        foreach (GOAPAction a in actions)
        {
            s += a.actionName;
            s += "-> ";
        }
        s += "GOAL";
        return s;
    }

}
