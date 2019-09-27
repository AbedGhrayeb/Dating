using CloudinaryDotNet;
using System;
using System.Collections.Generic;

namespace Dating.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string PublicId { get; set; }
        public int Version { get; set; }
        public string Signature { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }
        public string ResourceType { get; set; }
        public int Bytes { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string SecureUrl { get; set; }
        public string Path { get; set; }
        public bool  IsMain { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }

  
}