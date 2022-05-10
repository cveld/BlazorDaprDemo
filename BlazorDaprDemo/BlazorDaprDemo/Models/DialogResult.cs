namespace BlazorDaprDemo.Models
{
    public class DialogResult<T>
    {
        public ModalResult Result { get; set; }
        public T Data { get; set; }
    }
    //public class DialogResult
    //{
    //    public ModalResult Result { get; set; }    
    //}

    public enum ModalResult
    {
        Ok,
        Cancel
    }
}
