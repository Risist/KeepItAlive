using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class StateMachine
{
    public delegate State StateMethod();
    public delegate bool BoolMethod();
    public delegate float FloatMethod();

    public class State
    {
        public System.Action onBegin = () => { };
        public System.Action onUpdate = () => { };
        public StateMethod getNextState = () => null;
        public BoolMethod returnState = () => true;
        public FloatMethod getUtility = () => 1.0f;
        public BoolMethod canEnter = () => true;

        public State AddOnBegin(System.Action s)
        {
            onBegin += s;
            return this;
        }
        public State AddOnUpdate(System.Action s)
        {
            onUpdate += s;
            return this;
        }
        public State SetGetNextState(StateMethod s)
        {
            getNextState = s;
            return this;
        }
        public State SetReturnState(BoolMethod s)
        {
            returnState = s;
            return this;
        }

        public State SetUtility(FloatMethod s)
        {
            getUtility = s;
            return this;
        }

        public State SetCanEnter(BoolMethod s)
        {
            canEnter = s;
            return this;
        }
    }

    List<State> behaviourStates = new List<State>();
    State currentState;
    State GetNextState()
    {
        if (currentState == null)
        {
            Debug.LogWarning(currentState + " is not initialized to anu value");
            return null;
        }

        return currentState.getNextState();
    }


    public State AddNewState()
    {
        var state = new State();
        behaviourStates.Add(state);
        return state;
    }
    public State AddNewStateAsCurrent()
    {
        var state = AddNewState();
        ChangeState(state);
        return state;
    }
    public void UpdateStates()
    {
        if (currentState == null)
        {
            Debug.LogWarning(currentState + " is not initialized to anu value");
            return;
        }

        currentState.onUpdate();
        if (currentState.returnState())
            ChangeState(GetNextState());
    }
    public void ChangeState(State newState)
    {
        if (newState == null)
        {
            Debug.LogWarning("Changing state to null");
            return;
        }

        currentState = newState;
        newState.onBegin();
    }
    
    public State GetRandomState()
    {
        return behaviourStates[Random.Range(0, behaviourStates.Count)];
    }

    static List<float> utilityCache = new List<float>();
    public State GetNextStateByUtility()
    {
        utilityCache.Clear();

        float sum = 0;
        var behaviours = behaviourStates;
        for (int i = 0; i < behaviours.Count; ++i)
        {
            utilityCache.Add(0);
            if (behaviours[i].canEnter())
            {
                utilityCache[i] = (Mathf.Clamp(behaviours[i].getUtility(), 0, float.MaxValue));
                sum += utilityCache[i];
            }
        }

        if (sum == 0)
            return null;

        float randed = UnityEngine.Random.Range(0, sum);

        float lastSum = 0;
        for (int i = 0; i < behaviours.Count; ++i)
        {
            float utility = utilityCache[i];
            if (behaviours[i].canEnter())
            {
                if (randed >= lastSum && randed <= lastSum + utility)
                {
                    return behaviours[i];
                }
                else
                    lastSum += utility;
            }
        }

        return null;
    }
}

