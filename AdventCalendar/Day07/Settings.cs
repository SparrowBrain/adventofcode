namespace AdventCalendar.Day07
{
    public class Settings
    {
        public StepSettings StepSettings { get; set; }
        public WorkerSettings WorkerSettings { get; set; }
    }

    public class WorkerSettings
    {
        public int WorkerCount { get; set; }
    }
}