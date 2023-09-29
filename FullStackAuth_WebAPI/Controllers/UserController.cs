using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            try
            {
                var users = _context.Users.ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var user = _context.Users.Include(t => t.Topics)
                                         .Include(c => c.Comments)
                                         .FirstOrDefault(user => user.Id == id);
                if (user is null)
                    return NotFound();

                var topics = _context.Topics.Include(u => u.User).Where(user => user.UserId == id).ToList();
                var comments = _context.Comments.Where(user => user.UserId == id).ToList();

                var userDto = new UserForDisplayDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Likes = user.Likes,
                    RegistrationData = user.RegistrationData,
                    ProfilePicture = user.ProfilePicture,
                    Topics = topics.Select(t => new TopicForDisplayDto
                    {
                        TopicId = t.TopicId,
                        Title = t.Title,
                        Text = t.Text,
                        TimePosted = t.TimePosted,
                        Likes = t.Likes
                    }).ToList(),
                    Comments = comments.Select(c => new CommentFoDisplayingDtoWithoutNav
                    {
                        CommentId = c.CommentId,
                        Text = c.Text,
                        TimePosted = c.TimePosted
                    }).ToList()

                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(string id, [FromBody] User updatedUser)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.Id == id);

                if (user == null)
                    return NotFound();

                var userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                user.UserName = updatedUser.UserName;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.ProfilePicture = updatedUser.ProfilePicture;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.SaveChanges();

                return StatusCode(201, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(string id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.Id == id);
                if (user is null)
                    return NotFound();

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _context.Users.Remove(user);
                _context.SaveChanges();

                return StatusCode(204, $"User with id {user.Id} was deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
