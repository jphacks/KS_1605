using MemoTech.Scripts.Utility;

namespace MemoTech.Scripts.ViewModel
{
    class MemoTechViewModel
    {
        private string[] stateTitle = { "Start", "Share" };

        public State buttonState = State.Start;
        public string StateTitle { get { return stateTitle[(int)buttonState]; } }


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
            return buttonState;
        }

        public void StopButton()
        {
            buttonState = State.Start;
        }
    }
}
