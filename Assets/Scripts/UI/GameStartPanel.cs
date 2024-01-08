using Imodel;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        private IGameModel mGameModel;
        private Text _guess;
        private Text _point;
        private Text _clean;
        private Text _gold;
        private Text _day;

        void Start()
        {
            _day = transform.Find("Model/DayText").GetComponent<Text>();
            _gold = transform.Find("Model/GoldText").GetComponent<Text>();
            _clean = transform.Find("Model/CleanText").GetComponent<Text>();
            _point = transform.Find("Model/ActionPointText").GetComponent<Text>();
            _guess = transform.Find("Model/GuestCountText").GetComponent<Text>();
            transform.Find("BtnStart").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    this.SendCommand<StartGameCommand>();
                });
            

            mGameModel = this.GetModel<IGameModel>();
            Debug.Log(mGameModel);
            mGameModel.Gold.Register(OnGoldValueChanged);
            mGameModel.Cleanliness.Register(OnCleanlinessValueChanged);
            mGameModel.Day.Register(OnDayValueChanged);
            mGameModel.GuestCount.Register(OnGuessValueChanged);
            mGameModel.ActionPoint.Register(OnActionPointValueChanged);

            // 第一次需要调用一下
            OnGoldValueChanged(mGameModel.Gold.Value);
            OnCleanlinessValueChanged(mGameModel.Cleanliness.Value);
            OnDayValueChanged(mGameModel.Day.Value);
            OnGuessValueChanged(mGameModel.GuestCount.Value);
            OnActionPointValueChanged(mGameModel.ActionPoint.Value);
        }

        private void OnActionPointValueChanged(int Point)
        {
            if (Point <= 0)
            {
                this.SendCommand<NewDayCommand>();
            }
            _point.text = "行动点数：" + Point;
        }

        private void OnGuessValueChanged(int GuessCount)
        {
            _guess.text = 
                "客人数/上限：" + GuessCount + "/" + mGameModel.GuestCountLimit.Value;
        }

        private void OnCleanlinessValueChanged(int Clean)
        {
            _clean.text = "整洁度：" + Clean;
        }

        private void OnGoldValueChanged(int gold)
        {
            _gold.text = "金币：" + gold;
        }

        private void OnDayValueChanged(int day)
        {
            _day.text = "天数:" + day;
        }


        private void OnDestroy()
        {
            mGameModel.Gold.UnRegister(OnGoldValueChanged);
            mGameModel.Cleanliness.UnRegister(OnCleanlinessValueChanged);
            mGameModel.Day.UnRegister(OnDayValueChanged);
            mGameModel.GuestCount.UnRegister(OnGuessValueChanged);
            mGameModel.ActionPoint.UnRegister(OnActionPointValueChanged);
            mGameModel = null;
        }

        public IArchitecture GetArchitecture()
        {
            return GameManager.Interface;
        }
    }
}