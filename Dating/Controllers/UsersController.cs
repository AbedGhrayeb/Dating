using AutoMapper;
using Dating.Data.Repositories;
using Dating.Helpers;
using Dating.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dating.Controllers
{
    public class UsersController : Controller
    {
       
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repository,IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        // GET: Users
        public async Task<ActionResult> GetUsers(UserParams userParams)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            var user = await _repo.GetUser(currentUserId);
            userParams.UserId = currentUserId;
            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = user.Gender == "male" ? "female" : "male";
            }
            var users = await _repo.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UsersListViewModel>>(users);

             Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalItemsCount, users.TotalPages);

            ViewBag.CurrentPage = users.CurrentPage;
            ViewBag.TotalPages = users.TotalPages;
            ViewBag.TotalItems = users.TotalItemsCount;
            ViewBag.LastActive = users.OrderByDescending(l=>l.LastActive);
            ViewBag.Created = users.OrderByDescending(l=>l.Created);

            return View(usersToReturn);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> GetUser(string id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserDetailesViewModel>(user);
            return View(userToReturn);
        }

       

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (id!=currentUserId)
            {
                return StatusCode(401);
            }
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserDetailesViewModel>(user);

            return View(userToReturn);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Edit(UserDetailesViewModel model )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                 var user = await _repo.GetUser(model.Id);
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (currentUserId!=user.Id)
                        {
                            return StatusCode(401);
                        }
                user.Introduction = model.Introduction;
                user.LookingFor = model.LookingFor;
                user.Interests = model.Interests;
                user.City = model.City;
                user.Country = model.Country;
                await _repo.Update(user);
                         return RedirectToAction(nameof(GetUsers));
                        
                    }

               
            
         
        }
    }
}