using System;
using System.Collections.Generic;
using CodeBase.Utils.SmartDebug;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        private readonly DiContainer _container;
        private readonly DSender _sender = new("GameStateMachine");

        public GameStateMachine(DiContainer container)
        {
            _container = container;
            _states = new Dictionary<Type, IState>();
        }
        
        public void RegisterState(IState state)
        {
            var stateType = state.GetType();
            _states[stateType] = state;

            DLogger.Message(_sender)
                .WithText($"Registered state: {stateType}")
                .Log();
        }

        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            _activeState = GetState<TState>();
            _activeState.Enter();

            DLogger.Message(_sender)
                .WithText($"Entering state: {typeof(TState)}")
                .Log();
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}