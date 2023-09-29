using AutoMapper.Configuration.Conventions;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Formats.Asn1;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/topic")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TopicController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = _context.Topics.ToList();
                

                // would be noce to know how to get a list of comment and inside list their comments inside 
                // I don;t really think you need to do that but 

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var topic = _context.Topics.FirstOrDefault(topic => topic.TopicId == id);
                if (topic is null)
                    return NotFound();

                return Ok(topic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Topic topic)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                topic.UserId = userId;

                _context.Topics.Add(topic);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.SaveChanges();

                return StatusCode(201, topic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var topic = _context.Topics.FirstOrDefault(topic => topic.TopicId == id);
                if (topic is null)
                    return NotFound();

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || topic.UserId != userId)
                    return Unauthorized();

                _context.Topics.Remove(topic);
                _context.SaveChanges();

                return StatusCode(204);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] Topic topic)
        {
            try
            {
                var existTopic = _context.Topics.Include(u => u.User)
                                                .FirstOrDefault(topic => topic.TopicId == id);
                if (existTopic is null)
                    return NotFound();

                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId) || existTopic.UserId != userId)
                    return Unauthorized();

                existTopic.Title = topic.Title;
                existTopic.Text = topic.Text;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.SaveChanges();

                var userForUpdateDto = new UserForUpdateDto
                {
                    UserName = existTopic.User.UserName
                };

                var topicforUpdateDto = new TopicForUpdateDto
                {
                    Title = existTopic.Title,
                    Text = existTopic.Text,
                    UserId = existTopic.UserId,
                    User = userForUpdateDto
                };

                return StatusCode(201, topicforUpdateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
