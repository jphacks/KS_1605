using MemoTech.Scripts.Utility;
using Xamarin.Forms;

namespace MemoTech.Scripts.ViewModel
{
    class MemoTechViewModel
    {
        private string[] stateTitle = { "Start", "Share" };
		private string stateKey = "buttonState";

		public State buttonState;
        public string StateTitle { get { return stateTitle[(int)buttonState]; } }

		public MemoTechViewModel()
		{
			if (SaveDataUtility.CheckData(stateKey))
			{
				buttonState = (State)SaveDataUtility.Load<int>(stateKey);
			} else {
				buttonState = State.Start;
			}
		}

        public State MainButton()
        {
            switch (buttonState)
            {
                case State.Start:
					buttonState = State.Share;
                    break;
                case State.Share:
                    buttonState = State.Start;
                    break;
            }
			var cast = (int)buttonState;
			SaveDataUtility.Save<int>(stateKey, cast);
            return buttonState;
        }

        public void StopButton()
        {
            buttonState = State.Start;
			//セーブデータのクリア処理
        }
    }
}
