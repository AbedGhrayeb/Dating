//using AutoMapper;
//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
//using Dating.Data.Repositories;
//using Dating.Helpers;
//using Dating.Models;
//using Dating.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using System.IO;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Dating.Controllers
//{
//    //[Route("api/photos")]
//    //[ApiController]
//    public class PhotosController : Controller
//    {
//        private readonly IDatingRepository repo;
//        private readonly IMapper mapper;
//        private readonly IOptions<CloudinarySettings> cloudinaryOptions;
//        private readonly Cloudinary cloudinary;

//        public PhotosController(IDatingRepository repo, IMapper mapper,
//            IOptions<CloudinarySettings> cloudinaryOptions)
//        {
//            this.repo = repo;
//            this.mapper = mapper;
//            this.cloudinaryOptions = cloudinaryOptions;

//            Account account = new Account(
//                cloudinaryOptions.Value.CloudName,
//            cloudinaryOptions.Value.ApiKey,
//            cloudinaryOptions.Value.ApiSecret
//            );
//            cloudinary = new Cloudinary(account);
//        }

//        public IActionResult UserPhotos() => View();
//        [HttpGet]
//        public IActionResult UpdatePhotos(string id)
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> UpdatePhotos(CreatePhotoViewModel viewModel)
//        {
//            //get the user with id incomming
//            var user = await repo.GetUser(userId);
//            if (user==null)
//            {
//                return NotFound(user);
//            }
//            //get current user id
//            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
//            //check current user id with incomming user id
//            if (currentUserId!=user.Id)
//            {
//                return Unauthorized();
//            }
//            //get file property from view model
//            var file = viewModel.File;

//            //definde parameter that store imge upload information
//            var uploadResult = new ImageUploadResult();
//            //check if file exist
//            if (file.Length>0)
//            { 
//                //read file stream and save in parameter {stream}
//                using(Stream stream = file.OpenReadStream())
//                {
//                    // store image uploaded
//                    var uploadParams = new ImageUploadParams()
//                    {
//                        File = new FileDescription(file.Name, stream)
//                    };
//                    //upload image to cloudinary
//                    uploadResult = cloudinary.Upload(uploadParams);
//                }
//            }
//            //get image information

//            viewModel.Url = uploadResult.Uri.ToString();
//            viewModel.PublicId = uploadResult.PublicId;
//            //map phot view model to photo class 
//            Photo photo = mapper.Map<Photo>(viewModel);
//            //connect photo to current user
//            photo.AppUser = user;
//            //set main photo if user noot set any photo
//            if (!user.Photos.Any(m=>m.IsMain))
//            {
//                photo.IsMain = true;
//            }
//            //add photo to user
//            user.Photos.Add(photo);
//            if (await repo.SaveAll())
//            {
//                return View(nameof(UserPhotos));
//            }
//            return BadRequest("Could bot add the photo"); ;
//        }

//       //private void UploadPhotos(UserDetailesViewModel viewModel)
//       // {
//       //     //get file property from view model
//       //     var file = viewModel.File;

//       //     //definde parameter that store imge upload information
//       //     var uploadResult = new ImageUploadResult();
//       //     //check if file exist
//       //     if (file.Length > 0)
//       //     {
//       //         //read file stream and save in parameter {stream}
//       //         using (Stream stream = file.OpenReadStream())
//       //         {
//       //             // store image uploaded
//       //             var uploadParams = new ImageUploadParams()
//       //             {
//       //                 File = new FileDescription(file.Name, stream)
//       //             };
//       //             //upload image to cloudinary
//       //             uploadResult = cloudinary.Upload(uploadParams);
//       //         }
//       //     }
//       // }
//    }
//}
