$(document).ready(function () {
    $('#LoginFH').submit(function (e) {
        e.preventDefault();
        const form = document.getElementById('LoginFH');
        const formData = new FormData(form);
        const json = {};

        for (const [key, value] of formData.entries()) {
            json[key] = value;
        }
        $.ajax({
            url: 'https://localhost:7034/api/Users/SignIn',
            type: 'POST',
            data: JSON.stringify(json),
            processData: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                localStorage.setItem('token', data.token);
                const checkR = decodeJWTToken(data.token);
                if (checkR.role == 'Admin') {
                    window.location.href = '/Admin/Category/CategoryAdmin.html';
                } else {
                    window.location.href = '/User/index.html';
                }
                alert(data.message);
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    });

    $('#RegisterFH').submit(function (e) {
        e.preventDefault();
        const form = document.getElementById('RegisterFH');
        const formData = new FormData(form);
        const json = {};

        for (const [key, value] of formData.entries()) {
            json[key] = value;
        }

        $.ajax({
            url: 'https://localhost:7034/api/Users/SignUp',
            type: "POST",
            data: JSON.stringify(json),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if(!data){
                    alert("Error registered!");
                }else{
                    alert("Sucessfully registered!");
                    window.location.href = '/User/index.html';
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    });
});

const loginBtn = document.getElementById('login');
const signupBtn = document.getElementById('signup');

if (loginBtn) {
    loginBtn.addEventListener('click', (e) => {
        let parent = e.target.parentNode.parentNode;
        Array.from(e.target.parentNode.parentNode.classList).find((element) => {
            if (element !== "slide-up") {
                parent.classList.add('slide-up')
            } else {
                signupBtn.parentNode.classList.add('slide-up')
                parent.classList.remove('slide-up')
            }
        });
    });
}
if (signupBtn) {
    signupBtn.addEventListener('click', (e) => {
        let parent = e.target.parentNode;
        Array.from(e.target.parentNode.classList).find((element) => {
            if (element !== "slide-up") {
                parent.classList.add('slide-up')
            } else {
                loginBtn.parentNode.parentNode.classList.add('slide-up')
                parent.classList.remove('slide-up')
            }
        });
    });
}

$(".toggle-password").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $('#inputPassword');
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

$(".toggle-passwordSignIn").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $('#password');
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

$(".toggle-confirmPassword").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $('#confirmPassword');
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

function decodeJWTToken(token) {
    if (!token) {
        return null;
    }
    const parts = token.split('.');
    const payload = JSON.parse(atob(parts[1]));

    const Id = payload.Id;
    const Role = payload.Role;
    const Name = payload.Name;
    const Thumb = payload.Thumb;

    const decodedToken = {
        id: Id,
        role: Role,
        name: Name,
        thumb: Thumb
    };

    return decodedToken;
}
