using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dating.Data.Repositories;
using Dating.Helpers;
using Dating.Models;
using Dating.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dating.Controllers
{
  [Authorize]
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
        public async Task<ActionResult> GetUsers()
        {
            var users= await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UsersListViewModel>>(users);
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