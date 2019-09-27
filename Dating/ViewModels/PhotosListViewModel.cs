using System;

namespace Dating.ViewModels
{
    public class PhotosListViewModel
    {
        public PhotosListViewModel()
        {
            DateAdded = DateTime.Now;
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public string AppUserId { get; set; }
    }
}
