//Logout Account
$("#logout").click(function () {
    localStorage.clear();
    window.location.href = "/User/Login.html";
});