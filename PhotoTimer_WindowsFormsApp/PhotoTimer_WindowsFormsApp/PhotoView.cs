
namespace PhotoTimer_WindowsFormsApp
{   
        public class PhotoView

        {
        public delegate void EventDelegate();
        public event EventDelegate addPhoto = null;
        public int CountPhotos = 0;
        public PhotoView()
        {
            CountPhotos++;           
        }

            public void InvokeEvent()

            {
                addPhoto.Invoke();
            }
        }    
}
