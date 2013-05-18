namespace Solar
{
    public class Timer
    {
        private float myLimit, myElapsed, myTime;

        public float Limit { get { return myLimit; } }
        public float Elapsed { get { return myElapsed; } }
        public float Time { get { return myTime; } }

        public Timer(float Limit)
        {
            myLimit = Limit;
            Stop();
        }

        public void Start()
        {
            myElapsed = 0.0f;
        }

        public void Stop()
        {
            myTime = -1.0f;
        }

        private void UpdateTime()
        {
            myTime = myElapsed / myLimit;
        }

        public bool Update(float elapsedTime)
        {
            bool result = false;
            if (myElapsed >= 0.0f)
            {
                myElapsed += elapsedTime;
                result = Elapsed >= Limit;
                UpdateTime();
            }
            return result;
        }
    }
}
