using MemoTech.Scripts.Utility;

namespace MemoTech.Scripts.ViewModel
{
    class MemoTechViewModel
    {
        private string[] stateTitle = { "Start", "Share" };

        public State buttonState = State.Start;
        private string stateKey = "buttonState";
        public string StateTitle { get { return stateTitle[(int)buttonState]; } }

        public State MainButton()
        {
            switch (buttonState)
            {
                case State.Start:
                    buttonState = State.Share;
                    //SaveDataUtility.Save<State>(stateKey, buttonState);
                    break;
                case State.Share:
                    buttonState = State.Start;
                    break;
            }
            return buttonState;
        }

        public void StopButton()
        {
            buttonState = State.Start;
        }
    }
}
