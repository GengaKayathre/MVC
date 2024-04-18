using Microsoft.AspNetCore.Mvc;
using SignUpApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SignUpApp.Controllers
{
    public class AccountController : Controller
    {
        private string _jsonFilePath = "user.json";

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            List<User> users;
            try
            {
                var userData = System.IO.File.ReadAllText(_jsonFilePath);
                users = JsonSerializer.Deserialize<List<User>>(userData);
            }
            catch (FileNotFoundException)
            {
                users = new List<User>();
            }
            catch (JsonException ex)
            {
                return BadRequest("Error processing user data: " + ex.Message);
            }

            users.Add(user);

            var jsonUsers = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(_jsonFilePath, jsonUsers);

            return RedirectToAction("SignUpSuccess");
        }

        public ViewResult SignUpSuccess()
        {
            ViewBag.Message = "Thank you for signing up!";
            return View();
        }
    }
}
