
namespace PhotoTimer_WindowsFormsApp
{   
        public class PhotoView

        {
        public delegate void EventDelegate();
        public event EventDelegate addPhoto = null;        
        
            public void InvokeEvent()
            {
                addPhoto.Invoke();
            }
        }    
}
