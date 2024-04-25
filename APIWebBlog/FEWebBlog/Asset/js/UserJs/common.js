$(document).ready(function () {

    LoadCommon();

    function LoadCommon() {

        //Load top 8 tab
        $.ajax({
            url: "https://localhost:7034/odata/Tab?$top=8&$orderby=tab_id desc",
            type: "GET",
            success: function (data) {
                $.each(data.value, function (index, value) {
                    let row = `<li><a href="#">${value["tabName"]}</a></li>`;
                    $(".sidebar-widget-area #tabList").append(row);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
        //Load category
        $.ajax({
            url: "https://localhost:7034/odata/Category?$orderby=categoryName desc",
            type: "GET",
            success: function (data) {
                $.each(data.value, function (index, value) {
                    let rowC = `<li><a href="#">${value["categoryName"]}</a></li>`;
                    $(".classynav #listCategory").append(rowC);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
        //Load top 4 new post
        $.ajax({
            url: "https://localhost:7034/odata/Post?$top=4&$expand=tab&$orderby=createdDate desc",
            type: "GET",
            success: function (data) {
                $.each(data.value, function (index, value) {
                    let formatNewD = formatDate(value["createdDate"]);
                    let row = `<div class="single-blog-post d-flex align-items-center widget-post">
                                    <!-- Post Thumbnail -->
                                    <div class="post-thumbnail">
                                        <img src="../Asset/img/blog-img/${value["thumb"]}" alt="">
                                    </div>
                                    <!-- Post Content -->
                                    <div class="post-content">
                                        <a href="single-post.html?id=${value["post_id"]}" class="post-tag">${value["Tab"]["tabName"]}</a>
                                        <h4><a href="single-post.html?id=${value["post_id"]}" class="post-headline">${value["title"]}</a></h4>
                                        <div class="post-meta">
                                            <p><a href="single-post.html?id=${value["post_id"]}">${formatNewD}</a></p>
                                        </div>
                                    </div>
                                </div>`;
                    $("#LastPosts").append(row);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    }
});

const checkPermission = decodeJWTToken(localStorage.getItem('token'));

if (checkPermission != null) {
    const navBar = `
            <div class="icon profile-dropdown">
            <img src="../Asset/img/bg-img/${checkPermission.thumb}" class="user-pic" alt="">
            <div class="sub-menu-wrap" id="subMenu">
                <div class="sub-menu">
                    <div class="user-info">
                        <img src="../Asset/img/bg-img/${checkPermission.thumb}" alt="">
                        <h3>${checkPermission.name}</h3>
                    </div>
                    <hr>

                    <a href="userProfile.html" class="sub-menu-link">
                        <i class='bx bx-user' id="setting-icon"></i>
                        <p>Profile</p>
                        <span><i class='bx bx-chevron-right'></i></span>
                    </a>
                    <a class="sub-menu-link" id="logout">
                        <i class='bx bx-log-out' id="setting-icon"></i>
                        <p>Logout</p>
                        <span><i class='bx bx-chevron-right'></i></span>
                    </a>
                </div>
            </div>           
        </div> 
      `;
    // Insert the navigation bar into the DOM
    document.querySelector('#navbar').innerHTML = navBar;
} else {
    const navBar = `
            <li><a href="Login.html">Login</a></li>
            <li><a href="Login.html">Register</a></li>
            `;
    // Insert the navigation bar into the DOM
    document.querySelector('#navbar').innerHTML = navBar;
}

//Logout Account
$("#logout").click(function () {
    localStorage.clear();
    window.location.href = "/User/Login.html";
});

function formatDate(dateString) {
    const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    const date = new Date(dateString);

    const day = date.getDate();
    const month = months[date.getMonth()];

    return `${day} <span>${month}</span>`;
}

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

/* ========== Toggle setting user profile =========== */
let subMenu = document.getElementById("subMenu")
const user_pic = document.querySelector(".user-pic")

if (user_pic) {
    user_pic.addEventListener('click', function () {
        subMenu.classList.toggle("open-menu")
    });
}