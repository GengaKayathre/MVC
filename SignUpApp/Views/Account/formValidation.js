function validateAndSubmit() {
    var username = document.getElementById('username').value;
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    
    var usernameValid = /^[a-zA-Z0-9]+$/.test(username);
    var emailValid = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(email);
    var passwordValid = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.{10,})/.test(password);

    if (usernameValid && emailValid && passwordValid) {
        document.getElementById('signupForm').submit();
    } else {
        if (!usernameValid) {
            document.getElementById('usernameError').innerText = "Invalid username. Only letters and numbers are allowed. No spaces.";
            document.getElementById('usernameError').style.display = "block";
        } else {
            document.getElementById('usernameError').style.display = "none";
        }

        if (!emailValid) {
            document.getElementById('emailError').innerText = "Invalid email format.";
            document.getElementById('emailError').style.display = "block";
        } else {
            document.getElementById('emailError').style.display = "none";
        }

        if (!passwordValid) {
            document.getElementById('passwordError').innerText = "Invalid password format. It should be 10 characters including a number, uppercase, lowercase, and a special character.";
            document.getElementById('passwordError').style.display = "block";
        } else {
            document.getElementById('passwordError').style.display = "none";
        }
    }
}