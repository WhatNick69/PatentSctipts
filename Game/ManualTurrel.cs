﻿using UnityEngine;
using MovementEffects;
using UnityEngine.Networking;

namespace Game
{
    /// <summary>
    /// Ручная пушка, что управляется тапами
    /// </summary>
    public class ManualTurrel
        : LiteTurrel
    {
        protected Vector2 mouse;

        protected override void SetTotalPlayerUnitPower()
        {
            TotalPlayerUnitPower = _hpTurrel + _standartDmgFar + _standartShootingSpeed +
                _standartAccuracy + _standartTimeToReAlive;
            Debug.Log("Посчитано: " + TotalPlayerUnitPower);
        }

        public override void StartMethod()
        {
            RpcSetSizeOfUnitVisibleRadius(0.001f);
            if (_isTurrel)
                _maxCountOfAttackers = 3;
            else
                _maxCountOfAttackers = 1;
        }

        /// <summary>
        /// Предзагрузка на клиенте
        /// </summary>
        public override void OnStartClient()
        {
            SetCamera();
            transform.localEulerAngles = Vector3.zero;
            if (isServer)
            {
                _healthBarUnit.HealthUnit = HpTurrel; // Задаем значение бара
            }
        }

        /// <summary>
        /// Пустая реализация
        /// </summary>
        private new void FixedUpdate()
        {
            return;
        }

        /// <summary>
        /// Обновление
        /// </summary>
        void Update()
        {
            if (isServer)
            {
                Vector2 mouse = Input.mousePosition;
                CmdLookAter(mouse);
                if (Input.GetMouseButton(1))
                {
                    CmdAliveUpdater();
                }
            }
        }

        /// <summary>
        /// Смотреть на тап
        /// </summary>
        [Command]
        void CmdLookAter(Vector2 mouse)
        {
            RpcLookAter(mouse);
        }

        [Client]
        void RpcLookAter(Vector2 mouse)
        {
            if (IsAlive)
            {
                Vector3 target = _mainCamera.ScreenToWorldPoint(mouse);
                target.y = 0;
                _childRotatingTurrel.LookAt(target);
                _childRotatingTurrel.localEulerAngles = 
                    new Vector2(90, _childRotatingTurrel.localEulerAngles.y-90);
            }
        }

        /// <summary>
        /// Part of Update
        /// </summary>
        /// v1.01
        [Command]
        void CmdAliveUpdater()
        {
            RpcAttackAnim();
        }

        /// <summary>
        /// Implements attack-condition of Turrel
        /// Alive behavior
        /// </summary>
        /// v1.01
        [Client]
        void RpcAttackAnim()
        {
            if (_isAlive)
            {
                if (_coroutineReload)
                {
                    Timing.RunCoroutine(ReloadTimer());
                }
            }   
        }
    }
}
