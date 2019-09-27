using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dating.Data.Repositories;
using Dating.Helpers;
using Dating.Models;
using Dating.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Dating.Controllers
{

    public class PhotosController : Controller
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryOptions;
        private readonly Cloudinary _cloudinary;
        public PhotosController(IDatingRepository repo, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryOptions)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryOptions = cloudinaryOptions;

            Account account = new Account(
                cloudinaryOptions.Value.CloudName,
            cloudinaryOptions.Value.ApiKey,
            cloudinaryOptions.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        private bool TrueUser(string userId)
        {
           
            var currentuserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentuserId != userId)
            {
                return false;
            }
            return true;
        }
        public async Task<IActionResult> PhotosList(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            if (!TrueUser(userId))
            {
                return StatusCode(401);
            }
            ViewBag.UserId = userId;
            var photos= await _repo.GetPhotos(userId);
            //var photosToReturn = _mapper.Map<IEnumerable<PhotosListViewModel>>(photos);
            return PartialView("PhotosList", new PhotosModel(_cloudinary, photos));
        }
        public ActionResult UploadDirectly(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            if (!TrueUser(userId))
            {
                return StatusCode(401);
            }
            var model = new DictionaryModel(_cloudinary, new Dictionary<string, string>()
            { { "unsigned", "false" } });

            return PartialView("UploadDirectly", model);
        }

        public ActionResult UploadDirectlyUnsigned(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            if (!TrueUser(userId))
            {
                return StatusCode(401);
            }
            var preset = "sample_" + _cloudinary.Api.SignParameters(new SortedDictionary<string, object>()
            { { "api_key", _cloudinary.Api.Account.ApiKey } })
                .Substring(0, 10);
            var result = _cloudinary.CreateUploadPreset(new UploadPresetParams()
            { Name = preset, Unsigned = true, Folder = "preset_folder" }
            );

            var model = new DictionaryModel(_cloudinary, new Dictionary<string, string>()
            { { "unsigned", "true" },
                { "preset", preset } });

            return PartialView("UploadDirectly", model);
        }

        [HttpPost]
        public void UploadDirect(string userId)
        {

            //var headers = HttpContext.Request.Headers;

            //string content = null;
            //using (StreamReader reader = new StreamReader(HttpContext.Request.Form.Files.ToString()))
            //{
            //    content = reader.ReadToEnd();
            //}
            //if (String.IsNullOrEmpty(content)) return;

            //Dictionary<string, string> results = new Dictionary<string, string>();

            //string[] pairs = content.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var pair in pairs)
            //{
            //    string[] splittedPair = pair.Split('=');

            //    if (splittedPair[0].StartsWith("faces"))
            //        continue;

            //    results.Add(splittedPair[0], splittedPair[1]);
            //}
            var files = HttpContext.Request.Form.Files;
            var results = new ImageUploadResult();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream)
                        };
                        results = _cloudinary.Upload(uploadParams);
                    }

                    Photo p = new Photo()
                    {
                       
                        CreatedAt = results.CreatedAt,
                        Format = results.Format,
                        Height = results.Height,
                        
                        PublicId = results.PublicId,
                        ResourceType = results.ResourceType,
                        SecureUrl = results.SecureUri.ToString(),
                        Signature = results.Signature,
                        Type = results.Type,
                        Url = results.Uri.ToString(),
                        Version = Int32.Parse(results.Version),
                       Width=results.Width,
                        AppUserId = userId
                    };
                    _repo.AddPhotos(p);

                }
            }

            
            
        }



    }
}
