namespace Resources.Scripts.MVC
{
    public abstract class ControllerBase<TM, TV>
    {
        protected TM Model { get; private set; }
        protected TV View { get; private set; }

        protected ControllerBase(TM model, TV view)
        {
            Model = model;
            View = view;
        }
    }
}