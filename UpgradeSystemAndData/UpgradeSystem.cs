﻿using Game;
using GameGUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradeSystemAndData
{
    /// <summary>
    /// Система по апгрейду юнитов
    /// </summary>
    public class UpgradeSystem
        : MonoBehaviour
    {
        #region Переменные
        [SerializeField, Tooltip("Все бар-элементы системы апгрейда")]
        private GameObject[] upgradeElements;
            [SerializeField, Tooltip("Бар с подтверждением покупки")]
        private GameObject buyButton;
            [SerializeField, Tooltip("Кнопка отмены покупки")]
        private GameObject abortButton;
            [SerializeField, Tooltip("Кнопка назад")]
        private GameObject backButton;
            [SerializeField, Tooltip("Клиент")]
        private GameObject player;

        private DataPlayer.Unit dictionaryOfUnit;
        private List<GameObject> tempUpgradeElements;

        private int numberActiveSkills = 4;
        private int currentMaxSkill;
        private float totalCost;
        private string unitName;
        private float tempCostSkill = 0;
        private float tempValueSkill = 0;
        private int numberPrefab;

            [SerializeField, Tooltip("Общий опыт")]
        private Text totalXP;
            [SerializeField, Tooltip("Текущий опыт")]
        private Text currentXP;
        #endregion

        #region Геттеры/Сеттеры
        public float TotalCost
        {
            get
            {
                return totalCost;
            }

            set
            {
                totalCost = value;
            }
        }

        public Text CurrentXP
        {
            get
            {
                return currentXP;
            }

            set
            {
                currentXP = value;
            }
        }

        public Text TotalXP
        {
            get
            {
                return totalXP;
            }

            set
            {
                totalXP = value;
            }
        }

        public string UnitName
        {
            get
            {
                return unitName;
            }

            set
            {
                unitName = value;
            }
        }

        public GameObject AbortButton
        {
            get
            {
                return abortButton;
            }

            set
            {
                abortButton = value;
            }
        }
        #endregion

        /// <summary>
        /// Проверить, хватает ли опыта у юнита
        /// Вызывает CheckSkillMaxUpgrade()
        /// </summary>
        public void CheckMoneyAndValueForButtons()
        {
            ShowAddButtonsInUpgradeElements();
            foreach (GameObject obj in upgradeElements)
            {
                if (int.Parse(currentXP.text) -
                    (Convert.ToInt32(obj.transform.GetChild(1).
                        GetComponentInChildren<Text>().text)+totalCost) < 0)
                {
                    obj.GetComponent<Skill>().AddButtonReference(false);
                }
            }
            CheckSkillMaxUpgrade();
        }

        /// <summary>
        /// Проверить, не выходят ли значения навыков за свои границы
        /// </summary>
        public void CheckSkillMaxUpgrade()
        {
            for (byte i = 0; i < upgradeElements.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 500)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(500);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 1:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 100)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(50);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 2:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 50)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(50);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 3:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 3)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(3);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 4:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 3)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(3);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 5:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 50)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(50);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 6:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 100)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(100);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 7:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) > 3)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(3);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        else if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) < 0.1f)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(0.01);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 8:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) < 0.1f)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(0.01);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                    case 9:
                        if (float.Parse(upgradeElements[i].transform.GetChild(0).
                            GetComponentInChildren<Text>().text) < 5)
                        {
                            upgradeElements[i].transform.GetChild(0).
                                GetComponentInChildren<Text>().text = Convert.ToString(5);
                            upgradeElements[i].GetComponent<Skill>().AddButtonReference(false);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Показать кнопки для апгрейда
        /// </summary>
        public void ShowAddButtonsInUpgradeElements()
        {
            foreach (GameObject obj in upgradeElements)
                obj.GetComponent<Skill>().AddButtonReference(true);
        }

        /// <summary>
        /// Добавить улучшение к навыку и его стоимость в корзину
        /// </summary>
        public void OnClickUpgradeSkill(int barNumber)
        {
            // Если денег не хватает на покупку навыка - выходим из метода
            tempCostSkill = 
                Convert.ToInt32(upgradeElements[barNumber].
                    transform.GetChild(1).GetComponentInChildren<Text>().text); // получаем стоимость

            if (int.Parse(currentXP.text) - (totalCost + tempCostSkill) < 0) return;

            abortButton.SetActive(true);
            // Если это навык с плавающей точкой - парсим его во float. Иначе в int
            if (!upgradeElements[barNumber].GetComponent<Skill>().IsFloat)
            {
                tempValueSkill =
                    Convert.ToInt32(upgradeElements[barNumber].
                        transform.GetChild(0).GetComponentInChildren<Text>().text);
                tempValueSkill = (int)Math.Ceiling(tempValueSkill * 
                    upgradeElements[barNumber].GetComponent<Skill>().ValueMultiplier);
            }
            else
            {
                tempValueSkill =
                    float.Parse(upgradeElements[barNumber].
                        transform.GetChild(0).GetComponentInChildren<Text>().text);
                tempValueSkill = tempValueSkill * 
                    upgradeElements[barNumber].GetComponent<Skill>().ValueMultiplier;
                if (upgradeElements[barNumber].GetComponent<Skill>().IsDoubleFloat)
                {
                    tempValueSkill = (float)Math.Round(tempValueSkill, 2);
                }
                else
                {
                    tempValueSkill = (float)Math.Round(tempValueSkill, 1);
                }
            }

            // Складываем стоимость навыка с полной стоимостью корзины
            totalCost += tempCostSkill;
            tempCostSkill = (int)(tempCostSkill * upgradeElements[barNumber].GetComponent<Skill>().CostMultiplier);
            if (tempCostSkill > 100000) tempCostSkill = 100000; // значение не должно превышать 100к

            // Обновляем данные в барах-показателях стоимости и величины навыка
            upgradeElements[barNumber].transform.GetChild(1).GetComponentInChildren<Text>().text 
                = tempCostSkill.ToString();
            upgradeElements[barNumber].transform.GetChild(0).GetComponentInChildren<Text>().text 
                = tempValueSkill.ToString();

            // Обновляем стоимость корзины
            if (!buyButton.activeSelf) buyButton.SetActive(true);
            buyButton.transform.GetChild(0).GetComponentInChildren<Text>().text = totalCost.ToString();
            CheckMoneyAndValueForButtons();
        }

        /// <summary>
        /// Подтвердить покупку апгрейдов
        /// </summary>
        public void OnClickAcceptUpgradeBuy()
        {
            currentXP.text = (int.Parse(currentXP.text) - (int)totalCost).ToString();
            RefreshDataOfUnit();
            buyButton.SetActive(false);
            abortButton.SetActive(false);
            totalCost = 0;
        }

        /// <summary>
        /// Обновить параметры префаба
        /// Чтобы расширить метод следует в CASE добавить:
        /// *   методы, которые необходимо обновлять для требуемого экземпляра класса;
        /// *   значения переменных, которые нужно сохранять
        /// *   стоимости навыков которые нужно сохранять
        /// 
        /// ОЧЕНЬ БОЛЬШОЙ И СЛОЖНЫЙ МЕТОД. МОЖНО СДЕЛАТЬ ПРОЩЕ, НО Я НЕ СОБИРАЮСЬ ЭТИМ ЗАНИМАТЬСЯ СЕЙЧАС. 
        /// НЕТ ВРЕМЕНИ.
        /// ОЛЕГ ИЗ БУДУЩЕГО - ЗНАЙ: ОЛЕГ ИЗ ПРОШЛОГО ОЧЕНЬ СОЖАЛЕЕТ О ТОМ, ЧТО НАПИСАЛ ПОДОБНЫЙ МЕТОД.
        /// </summary>
        public void RefreshDataOfUnit()
        {
            GameObject prefab = player.GetComponent<PlayerHelper>().GetPrefab(numberPrefab);
            try
            {
                switch (numberPrefab)
                {
                    case 0: // penguin
                        #region Обновление префаба
                        prefab.GetComponent<PlayerAbstract>().HpTurrel =
                            Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<PlayerAbstract>().StandartDmgNear =
                            float.Parse(upgradeElements[1].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<PlayerAbstract>().StandartRadius =
                            float.Parse(upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        prefab.GetComponent<PlayerAbstract>().SetSizeSadius(prefab.GetComponent<PlayerAbstract>().StandartRadius);

                        prefab.GetComponent<PlayerAbstract>().AttackSpeed =
                            float.Parse(upgradeElements[3].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<PlayerAbstract>().MoveSpeed =
                            float.Parse(upgradeElements[4].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        prefab.GetComponent<PlayerAbstract>().SetNewAgentSpeed();
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<PlayerAbstract>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_standartDmgNear", prefab.GetComponent<PlayerAbstract>().StandartDmgNear);
                        dictionaryOfUnit.ChangeValue("_standartRadius", prefab.GetComponent<PlayerAbstract>().StandartRadius);
                        dictionaryOfUnit.ChangeValue("_moveSpeed", prefab.GetComponent<PlayerAbstract>().MoveSpeed);
                        dictionaryOfUnit.ChangeValue("_attackSpeed", prefab.GetComponent<PlayerAbstract>().AttackSpeed);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgNear", int.Parse(upgradeElements[1].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartRadius", int.Parse(upgradeElements[2].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_moveSpeed", int.Parse(upgradeElements[3].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_attackSpeed", int.Parse(upgradeElements[4].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 1: // archer
                        #region Обновление префаба
                        prefab.GetComponent<LiteArcher>().HpTurrel =
                            Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteArcher>().StandartDmgNear =
                            float.Parse(upgradeElements[1].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteArcher>().StandartRadius =
                            float.Parse(upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        prefab.GetComponent<LiteArcher>().SetSizeSadius(prefab.GetComponent<PlayerAbstract>().StandartRadius);

                        prefab.GetComponent<LiteArcher>().StandartDmgFar =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteArcher>().StandartOfAmmo =
                            Convert.ToInt32(upgradeElements[6].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteArcher>().StandartShootingSpeed =
                            float.Parse(upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<LiteArcher>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_standartDmgNear", prefab.GetComponent<LiteArcher>().StandartDmgNear);
                        dictionaryOfUnit.ChangeValue("_standartRadius", prefab.GetComponent<LiteArcher>().StandartRadius);
                        dictionaryOfUnit.ChangeValue("_standartDmgFar", prefab.GetComponent<LiteArcher>().StandartDmgFar);
                        dictionaryOfUnit.ChangeValue("_standartOfAmmo", prefab.GetComponent<LiteArcher>().StandartOfAmmo);
                        dictionaryOfUnit.ChangeValue("_standartShootingSpeed", prefab.GetComponent<LiteArcher>().StandartShootingSpeed);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgNear", int.Parse(upgradeElements[1].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartRadius", int.Parse(upgradeElements[2].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgFar", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartOfAmmo", int.Parse(upgradeElements[6].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartShootingSpeed", int.Parse(upgradeElements[7].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 2: // burner
                        #region Обновление префаба
                        prefab.GetComponent<LiteBurner>().HpTurrel =
                            Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteBurner>().StandartDmgNear =
                            float.Parse(upgradeElements[1].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteBurner>().StandartRadius =
                            float.Parse(upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        prefab.GetComponent<LiteBurner>().SetSizeSadius(prefab.GetComponent<PlayerAbstract>().StandartRadius);

                        prefab.GetComponent<LiteBurner>().StandartBurnDmg =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteBurner>().StandartOfAmmo =
                            Convert.ToInt32(upgradeElements[6].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteBurner>().StandartShootingSpeed =
                            float.Parse(upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<LiteBurner>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_standartDmgNear", prefab.GetComponent<LiteBurner>().StandartDmgNear);
                        dictionaryOfUnit.ChangeValue("_standartRadius", prefab.GetComponent<LiteBurner>().StandartRadius);
                        dictionaryOfUnit.ChangeValue("_standartBurnDmg", prefab.GetComponent<LiteBurner>().StandartBurnDmg);
                        dictionaryOfUnit.ChangeValue("_standartOfAmmo", prefab.GetComponent<LiteBurner>().StandartOfAmmo);
                        dictionaryOfUnit.ChangeValue("_standartShootingSpeed", prefab.GetComponent<LiteBurner>().StandartShootingSpeed);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgNear", int.Parse(upgradeElements[1].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartRadius", int.Parse(upgradeElements[2].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartBurnDmg", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartOfAmmo", int.Parse(upgradeElements[6].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartShootingSpeed", int.Parse(upgradeElements[7].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 3: // turrel
                        #region Обновление префаба
                        prefab.GetComponent<LiteTurrel>().HpTurrel =
                           Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartRadius =
                            float.Parse(upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        prefab.GetComponent<LiteTurrel>().SetSizeSadius(prefab.GetComponent<PlayerAbstract>().StandartRadius);

                        prefab.GetComponent<LiteTurrel>().StandartDmgFar =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartShootingSpeed =
                            float.Parse(upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartAccuracy =
                            float.Parse(upgradeElements[8].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartTimeToReAlive =
                            float.Parse(upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<LiteTurrel>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_standartRadius", prefab.GetComponent<LiteTurrel>().StandartRadius);
                        dictionaryOfUnit.ChangeValue("_standartDmgFar", prefab.GetComponent<LiteTurrel>().StandartDmgFar);
                        dictionaryOfUnit.ChangeValue("_standartShootingSpeed", prefab.GetComponent<LiteTurrel>().StandartShootingSpeed);
                        dictionaryOfUnit.ChangeValue("_accuracy", prefab.GetComponent<LiteTurrel>().StandartAccuracy);
                        dictionaryOfUnit.ChangeValue("_standartTimeToReAlive", prefab.GetComponent<LiteTurrel>().StandartTimeToReAlive);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartRadius", int.Parse(upgradeElements[2].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgFar", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartShootingSpeed", int.Parse(upgradeElements[7].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_accuracy", int.Parse(upgradeElements[8].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartTimeToReAlive", int.Parse(upgradeElements[9].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 4: // autoTurrel
                        #region Обновление префаба
                        prefab.GetComponent<LiteTurrel>().HpTurrel =
                           Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartRadius =
                            float.Parse(upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        prefab.GetComponent<LiteTurrel>().SetSizeSadius(prefab.GetComponent<PlayerAbstract>().StandartRadius);

                        prefab.GetComponent<LiteTurrel>().StandartDmgFar =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartShootingSpeed =
                            float.Parse(upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteTurrel>().StandartTimeToReAlive =
                            float.Parse(upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<LiteTurrel>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_standartRadius", prefab.GetComponent<LiteTurrel>().StandartRadius);
                        dictionaryOfUnit.ChangeValue("_standartDmgFar", prefab.GetComponent<LiteTurrel>().StandartDmgFar);
                        dictionaryOfUnit.ChangeValue("_standartShootingSpeed", prefab.GetComponent<LiteTurrel>().StandartShootingSpeed);
                        dictionaryOfUnit.ChangeValue("_standartTimeToReAlive", prefab.GetComponent<LiteTurrel>().StandartTimeToReAlive);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartRadius", int.Parse(upgradeElements[2].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgFar", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartShootingSpeed", int.Parse(upgradeElements[7].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartTimeToReAlive", int.Parse(upgradeElements[9].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 5: // manualTurrel
                        #region Обновление префаба
                        prefab.GetComponent<ManualTurrel>().HpTurrel =
                           Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<ManualTurrel>().StandartDmgFar =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<ManualTurrel>().StandartShootingSpeed =
                            float.Parse(upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<ManualTurrel>().StandartAccuracy =
                            float.Parse(upgradeElements[8].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<ManualTurrel>().StandartTimeToReAlive =
                            float.Parse(upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<ManualTurrel>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_standartDmgFar", prefab.GetComponent<ManualTurrel>().StandartDmgFar);
                        dictionaryOfUnit.ChangeValue("_standartShootingSpeed", prefab.GetComponent<ManualTurrel>().StandartShootingSpeed);
                        dictionaryOfUnit.ChangeValue("_accuracy", prefab.GetComponent<ManualTurrel>().StandartAccuracy);
                        dictionaryOfUnit.ChangeValue("_standartTimeToReAlive", prefab.GetComponent<ManualTurrel>().StandartTimeToReAlive);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartDmgFar", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartShootingSpeed", int.Parse(upgradeElements[7].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_accuracy", int.Parse(upgradeElements[8].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartTimeToReAlive", int.Parse(upgradeElements[9].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 6: // mine-turrel
                        #region Обновление префаба
                        prefab.GetComponent<LiteStaticTurrel>().HpTurrel =
                           Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteStaticTurrel>().MineDamage =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteStaticTurrel>().StandartTimeToReAlive =
                            float.Parse(upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<LiteStaticTurrel>().StandartReloadTime =
                            float.Parse(upgradeElements[10].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохранение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<LiteStaticTurrel>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_mineDamage", prefab.GetComponent<LiteStaticTurrel>().MineDamage);
                        dictionaryOfUnit.ChangeValue("_standartTimeToReAlive", prefab.GetComponent<LiteStaticTurrel>().StandartTimeToReAlive);
                        dictionaryOfUnit.ChangeValue("_standartReloadTime", prefab.GetComponent<LiteStaticTurrel>().StandartReloadTime);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_mineDamage", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartTimeToReAlive", int.Parse(upgradeElements[9].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartReloadTime", int.Parse(upgradeElements[10].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                    case 7: // mortira-turrel
                        #region Обновление префаба
                        prefab.GetComponent<MortiraTurrel>().HpTurrel =
                           Convert.ToInt32(upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<MortiraTurrel>().StandartDmgFar =
                            float.Parse(upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<MortiraTurrel>().StandartTimeToReAlive =
                            float.Parse(upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text);

                        prefab.GetComponent<MortiraTurrel>().StandartShootingSpeed =
                            float.Parse(upgradeElements[10].transform.GetChild(0).GetComponentInChildren<Text>().text);
                        #endregion
                        #region Сохнанение значений
                        dictionaryOfUnit.ChangeValue("_hpTurrel", prefab.GetComponent<MortiraTurrel>().HpTurrel);
                        dictionaryOfUnit.ChangeValue("_mineDamage", prefab.GetComponent<MortiraTurrel>().StandartDmgFar);
                        dictionaryOfUnit.ChangeValue("_standartTimeToReAlive", prefab.GetComponent<MortiraTurrel>().StandartTimeToReAlive);
                        dictionaryOfUnit.ChangeValue("_standartReloadTime", prefab.GetComponent<MortiraTurrel>().StandartShootingSpeed);
                        #endregion
                        #region Сохранение стоимостей
                        dictionaryOfUnit.ChangeCost("_hpTurrel", int.Parse(upgradeElements[0].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_mineDamage", int.Parse(upgradeElements[5].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartTimeToReAlive", int.Parse(upgradeElements[9].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        dictionaryOfUnit.ChangeCost("_standartReloadTime", int.Parse(upgradeElements[10].transform.GetChild(1)
                            .GetComponentInChildren<Text>().text));
                        #endregion
                        break;
                }
            }
            catch 
            {
                player.GetComponent<DataPlayer>().PopupManager.text += "Error while refreshing data!\n";
            }
            dictionaryOfUnit.XpForBuy = Convert.ToInt32(currentXP.text);
            dictionaryOfUnit.XpTotal = Convert.ToInt32(totalXP.text);
            player.GetComponent<DataPlayer>().SetDictionaryUnit(unitName, dictionaryOfUnit);

            player.GetComponent<DataPlayer>().PopupManager.text += "Data has been refreshed!\n";
            player.GetComponent<PlayerHelper>().RefreshPrefab(prefab, numberPrefab);
            player.GetComponent<DataPlayer>().SaveCloud();
        }

        /// <summary>
        /// Скрыть все бары с навыками
        /// </summary>
        public void UnshowUpgradeElements()
        {
            foreach (GameObject gO in upgradeElements) gO.SetActive(false);
        }

        /// <summary>
        /// Получить данные из словаря
        /// </summary>
        /// <param name="unitName"></param>
        public void InitialUpgradeUnit(string unitName)
        {
            this.unitName = unitName;
            dictionaryOfUnit 
                = player.GetComponent<DataPlayer>().GetDictionaryUnit(unitName);
            switch (unitName)
            {
                case "Penguin":
                    numberPrefab = 0;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text 
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[1].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartDmgNear").ToString();
                    upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartRadius").ToString();
                    upgradeElements[3].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_moveSpeed").ToString();
                    upgradeElements[4].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_attackSpeed").ToString();

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[1].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartDmgNear").ToString();
                    upgradeElements[2].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartRadius").ToString();
                    upgradeElements[3].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_moveSpeed").ToString();
                    upgradeElements[4].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_attackSpeed").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/healthNormalIcon");
                    upgradeElements[1].SetActive(true);
                    upgradeElements[2].SetActive(true);
                    upgradeElements[3].SetActive(true);
                    upgradeElements[4].SetActive(true);
                    break;
                case "Archer":
                    numberPrefab = 1;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[1].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartDmgNear").ToString();
                    upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartDmgFar").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = false;
                    upgradeElements[6].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartOfAmmo").ToString();
                    upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartShootingSpeed").ToString();
                    upgradeElements[7].GetComponent<Skill>().ValueMultiplier = 1.05f;


                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[1].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartDmgNear").ToString();
                    upgradeElements[2].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartDmgFar").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = false;
                    upgradeElements[6].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartOfAmmo").ToString();
                    upgradeElements[7].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartShootingSpeed").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/healthNormalIcon");
                    upgradeElements[1].SetActive(true);
                    upgradeElements[2].SetActive(true);
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/attackFarDmgIcon");
                    upgradeElements[6].SetActive(true);
                    upgradeElements[7].SetActive(true);
                    break;
                case "Burner":
                    numberPrefab = 2;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[1].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartDmgNear").ToString();
                    upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartBurnDmg").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = true;
                    upgradeElements[6].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartOfAmmo").ToString();
                    upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartShootingSpeed").ToString();
                    upgradeElements[7].GetComponent<Skill>().ValueMultiplier = 1.05f;

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[1].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartDmgNear").ToString();
                    upgradeElements[2].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartBurnDmg").ToString();
                    upgradeElements[6].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartOfAmmo").ToString();
                    upgradeElements[7].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartShootingSpeed").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/healthNormalIcon");
                    upgradeElements[1].SetActive(true);
                    upgradeElements[2].SetActive(true);
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/fireIcon");
                    upgradeElements[6].SetActive(true);
                    upgradeElements[7].SetActive(true);
                    break;
                case "Turrel":
                    numberPrefab = 3;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartDmgFar").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = true;
                    upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartShootingSpeed").ToString();
                    upgradeElements[7].GetComponent<Skill>().ValueMultiplier = 0.95f;

                    upgradeElements[8].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_accuracy").ToString();
                    upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartTimeToReAlive").ToString();

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[2].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartDmgFar").ToString();
                    upgradeElements[7].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartShootingSpeed").ToString();
                    upgradeElements[8].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_accuracy").ToString();
                    upgradeElements[9].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartTimeToReAlive").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/hpTurrelIcon");
                    upgradeElements[2].SetActive(true);
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/attackFarDmgIcon");
                    upgradeElements[7].SetActive(true);
                    upgradeElements[8].SetActive(true);
                    upgradeElements[9].SetActive(true);
                    break;
                case "AutoTurrel":
                    numberPrefab = 4;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[2].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartDmgFar").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = true;
                    upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartShootingSpeed").ToString();
                    upgradeElements[7].GetComponent<Skill>().ValueMultiplier = 0.95f;

                    upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartTimeToReAlive").ToString();

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[2].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartRadius").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartDmgFar").ToString();
                    upgradeElements[7].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartShootingSpeed").ToString();
                    upgradeElements[9].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartTimeToReAlive").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/hpTurrelIcon");
                    upgradeElements[2].SetActive(true);
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/attackFarDmgIcon");
                    upgradeElements[7].SetActive(true);
                    upgradeElements[9].SetActive(true);
                    break;
                case "ManualTurrel":
                    numberPrefab = 5;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartDmgFar").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = true;
                    upgradeElements[7].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartShootingSpeed").ToString();
                    upgradeElements[7].GetComponent<Skill>().ValueMultiplier = 0.95f;

                    upgradeElements[8].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_accuracy").ToString();
                    upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartTimeToReAlive").ToString();

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartDmgFar").ToString();
                    upgradeElements[7].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartShootingSpeed").ToString();
                    upgradeElements[8].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_accuracy").ToString();
                    upgradeElements[9].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartTimeToReAlive").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/hpTurrelIcon");
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/attackFarDmgIcon");
                    upgradeElements[7].SetActive(true);
                    upgradeElements[8].SetActive(true);
                    upgradeElements[9].SetActive(true);
                    break;
                case "MinesTurrel":
                    numberPrefab = 6;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_mineDamage").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = true;
                    upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartTimeToReAlive").ToString();
                    upgradeElements[10].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartReloadTime").ToString();
                    upgradeElements[10].GetComponent<Skill>().ValueMultiplier = 0.95f;

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_mineDamage").ToString();
                    upgradeElements[9].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartTimeToReAlive").ToString();
                    upgradeElements[10].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartReloadTime").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/hpTurrelIcon");
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/mineIcon");
                    upgradeElements[9].SetActive(true);
                    upgradeElements[10].SetActive(true);
                    break;
                case "MortiraTurrel":
                    numberPrefab = 7;
                    upgradeElements[0].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_hpTurrel").ToString();
                    upgradeElements[5].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_mineDamage").ToString();
                    upgradeElements[5].GetComponent<Skill>().IsFloat = true;
                    upgradeElements[5].GetComponent<Skill>().IsDoubleFloat = true;
                    upgradeElements[9].transform.GetChild(0).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueSkill("_standartTimeToReAlive").ToString();
                    upgradeElements[10].transform.GetChild(0).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueSkill("_standartReloadTime").ToString();
                    upgradeElements[10].GetComponent<Skill>().ValueMultiplier = 0.95f;

                    upgradeElements[0].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_hpTurrel").ToString();
                    upgradeElements[5].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_mineDamage").ToString();
                    upgradeElements[9].transform.GetChild(1).GetComponentInChildren<Text>().text
                         = dictionaryOfUnit.GetValueCost("_standartTimeToReAlive").ToString();
                    upgradeElements[10].transform.GetChild(1).GetComponentInChildren<Text>().text
                        = dictionaryOfUnit.GetValueCost("_standartReloadTime").ToString();

                    upgradeElements[0].SetActive(true);
                    upgradeElements[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/hpTurrelIcon");
                    upgradeElements[5].SetActive(true);
                    upgradeElements[5].GetComponent<Image>().sprite = Resources.Load<Sprite>("TexturesIcons/mineIcon");
                    upgradeElements[9].SetActive(true);
                    upgradeElements[10].SetActive(true);
                    break;
            }
            player.GetComponent<TurrelSetControl>().NormalizeSkillsList();

            currentXP.text = dictionaryOfUnit.XpForBuy.ToString();
            totalXP.text = dictionaryOfUnit.XpTotal.ToString();

            CheckMoneyAndValueForButtons();
        }
    }
}
