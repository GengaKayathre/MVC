using Microsoft.AspNetCore.Mvc;
using SignUpApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SignUpApp.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private string _jsonFilePath = "user.json";

        public IActionResult SignUp()
        {
            return View();
        }
        [Route("Account/SignUp")]
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

        public IActionResult SignUpSuccess()
        {
            string message = "Thank you for signing up!";
            ViewBag.SuccessMessage = message;
            return View();
        }
         public IActionResult Login()
        {
            return View();
        }

        [Route("Account/SignUpSuccess")]
        [HttpPost]
        public IActionResult Login(string usernameOrEmail, string password)
        {
            List<User> users;
            try
            {
                var userData = System.IO.File.ReadAllText(_jsonFilePath);
                users = JsonSerializer.Deserialize<List<User>>(userData);
            }
            catch (FileNotFoundException)
            {
                ViewBag.ErrorMessage = "User data file not found. Please try again later.";
                var message = "User data file not found. Please try again later.";
                return View("SignUpSuccess" , message );
            }
            catch (JsonException ex)
            {
                ViewBag.ErrorMessage = "Error processing user data: " + ex.Message;
                var message = "Invalid password. Kindly enter the correct password.";
                return View("SignUpSuccess" , message );
            }

            var user = users.Find(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid Username. If you are a new user, kindly sign up.";
                var message = "Invalid password. Kindly enter the correct password.";
                return View("SignUpSuccess" , message );
            }

            if (user.Password == password)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid password. Kindly enter the correct password.";
                var message = "Invalid password. Kindly enter the correct password.";
                return View("SignUpSuccess" , message );
            }
        }
    }
}
