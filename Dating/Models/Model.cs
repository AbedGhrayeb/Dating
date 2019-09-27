using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Models
{
    
        public class DictionaryModel : Model
        {
            public DictionaryModel(Cloudinary cloudinary, Dictionary<string, string> dict)
                : base(cloudinary)
            {
                Dict = dict;
            }

            public Dictionary<string, string> Dict { get; set; }
        }

        public class PhotosModel : Model
        {
            public PhotosModel(Cloudinary cloudinary, IEnumerable<Photo> photos)
                : base(cloudinary)
            {
                Photos = photos;
            }

            public IEnumerable<Photo> Photos { get; set; }
        }

        public class Model
        {
            public Model(Cloudinary cloudinary)
            {
                Cloudinary = cloudinary;
            }

            public Cloudinary Cloudinary { get; set; }
        }
    }

