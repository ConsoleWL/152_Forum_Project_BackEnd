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
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var comments = _context.Comments.Include(u=>u.User)
                                                .Include(t=>t.Topic)
                                                .ToList();

                //var commentsDto =  new List<CommentFoDisplayingDto>
                //{
                //    // how to make all of the comments throught dto list
                //};

                return StatusCode(200, comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Comment comment)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                comment.UserId = userId;

                _context.Comments.Add(comment);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.SaveChanges();



                return StatusCode(201, comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] Comment comment)
        {
            try
            {
                var existComment = _context.Comments.Include(o => o.User)
                                                    .Include(t => t.Topic)
                                                    .FirstOrDefault(f => f.CommentId == id);
                if (existComment is null)
                    return NotFound();

                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId) || existComment.UserId != userId)
                return Unauthorized();

                existComment.Text = comment.Text;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.SaveChanges();

                var user = new UserForUpdateDto
                {
                    UserName = existComment.User.UserName
                };

                var topic = new TopicForDisplayngCommentDto
                {
                    Title = existComment.Topic.Title
                };

                var existingCommentDto = new CommentFoDisplayingDto
                {
                    CommentId = existComment.CommentId,
                    Text = existComment.Text,
                    TimePosted = existComment.TimePosted,
                    User = user,
                    Topic = topic
                };

                return StatusCode(201, existingCommentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
