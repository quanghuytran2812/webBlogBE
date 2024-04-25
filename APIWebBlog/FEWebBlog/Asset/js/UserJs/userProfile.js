const checkPermission = decodeJWTToken(localStorage.getItem('token'));

$(document).ready(function () {

    LoadInformationU();

    function LoadInformationU() {

        $.ajax({
            url: "https://localhost:7034/odata/Users?$filter=user_id eq " + checkPermission.id,
            type: "GET",
            success: function (data) {
                const inforData = data.value[0];
                let row = `<div class="card_picInfo">
                                <img src="../Asset/img/bg-img/${inforData["thumb"]}" id="update-img">
                                <div class="round-img">
                                    <input type="file" name="thumb" id="update-file" value="${inforData["thumb"]}">
                                    <i class='bx bx-camera'></i>
                                </div>
                            </div>
                            <div class="card_form">
                                <div class="card_header">
                                    <h1>Edit profile</h1>
                                </div>
                                <div class="form_item">
                                    <span class="form_item_icon bx bx-user"></span>
                                    <input type="text" name="fullName" placeholder="Full Name" id="fullNameU" value="${inforData["fullName"]}">
                                </div>
                                <div class="form_item">
                                    <span class="form_item_icon bx bxs-phone"></span>
                                    <input type="text" name="phone" placeholder="Phone" id="phoneU" value="${inforData["phone"]}">
                                </div>
                                <div class="btnEdit"><button type="submit">Edit</button></div>
                            </div>`;
                $("#editUP").append(row);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    }

    GetTabcategory();

    function GetTabcategory() {

        $.ajax({
            url: "https://localhost:7034/odata/Tab?$filter=published eq 1",
            type: "GET",
            success: function (result, status, xhr) {
                $.each(result.value, function (index, value) {
                    $("#Listtab").append($('<option value=' + value["tab_id"] + '>' + value["tabName"] + '</option>'));
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }
        });
        $.ajax({
            url: "https://localhost:7034/odata/Category?$filter=published eq 1",
            type: "GET",
            success: function (result, status, xhr) {
                $.each(result.value, function (index, value) {
                    $("#ListCategory").append($('<option value=' + value["category_id"] + '>' + value["categoryName"] + '</option>'));
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }
        });
    }

    $('#AddPostbloggerP').submit(function (e) {
        e.preventDefault();
        let thumb = document.querySelector('#AddPostbloggerP #update-file').files[0].name;
        let title = document.querySelector("#AddPostbloggerP #titleP").value;
        let content = document.querySelector("#AddPostbloggerP #contentP").value;
        let summary = document.querySelector("#AddPostbloggerP #summaryP").value;
        let tabid = document.querySelector("#AddPostbloggerP #Listtab").value;
        let categoryid = document.querySelector("#AddPostbloggerP #ListCategory").value;
        const json = {
            "thumb": thumb,
            "title": title,
            "content": content,
            "summary": summary,
            "published": 0,
            "userId": checkPermission.id,
            "tabId": tabid,
            "categoryId": categoryid
        };
        $.ajax({
            url: "https://localhost:7034/odata/Post",
            type: "POST",
            data: JSON.stringify(json),
            contentType: "application/json; charset=utf-8",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + localStorage.getItem("token"));
            },
            success: function (data) {
                alert("You have added success");
                window.location.href = 'userProfile.html';
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    });

    //Load Post
    $.ajax({
        url: "https://localhost:7034/odata/Post?$expand=tab,users&$orderby=createdDate desc&$filter=UserId eq " + checkPermission.id,
        type: "GET",
        success: function (data) {
            $.each(data.value, function (index, value) {
                let formatNewComment = formatDate(value["createdDate"]);
                let row = `<div class="post-container">
                                <div class="user-profile">
                                    <img src="../Asset/img/bg-img/${value["Users"]["thumb"]}" class="user-post-pic" alt="">
                                    <div>
                                        <p>${value["Users"]["fullName"]}</p>
                                        <span>${formatNewComment}</span>
                                    </div>
                                </div>
                                <p class="post-text">${value["content"]}</p>
                                <img src="../Asset/img/blog-img/${value["thumb"]}" class="post-img" alt="">
                            </div>`;
                $("#PostBloggerP").append(row);
            });
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });

    $('#editUP').submit(function (e) {
        e.preventDefault();
        var fileInput = document.querySelector('#editUP #update-file').files[0].name;
        var name = document.querySelector("#editUP #fullNameU").value;
        var phonenumber = document.querySelector("#editUP #phoneU").value;
        const json = {
            "thumb": fileInput,
            "fullName": name,
            "phone": phonenumber,
        };

        $.ajax({
            url: "https://localhost:7034/odata/Users(" + checkPermission.id + ")",
            type: "PUT",
            data: JSON.stringify(json),
            contentType: "application/json; charset=utf-8",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + localStorage.getItem("token"));
            },
            success: function (data) {
                alert("You have edit success");
                window.location.href = 'userProfile.html';
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    });
});


if (checkPermission != null) {
    if (checkPermission.role == "Blogger") {
        const databutton = `
        <button class="btn-account-info editProfile" id="editProfile">
            <i class='bx bx-message-square-edit'></i> 
                Edit profile
        </button>
        <button class="btn-account-info addPost" id="addPost">
            <i class='bx bx-comment-add'></i> 
                Add to post
        </button>
      `;
        // Insert the navigation bar into the DOM
        document.querySelector('#buttonProfileP').innerHTML = databutton;
    }
    if (checkPermission.role == "User") {
        const databutton = `
        <button class="btn-account-info editProfile" id="editProfile">
            <i class='bx bx-message-square-edit'></i> 
                Edit profile
        </button>
      `;
        // Insert the navigation bar into the DOM
        document.querySelector('#buttonProfileP').innerHTML = databutton;
    }

    const mainProfileU = `
        <div class="profile-image"><img src="/Asset/img/bg-img/${checkPermission.thumb}" alt=""></div>
        <div class="profile-names">
            <h1 class="username">${checkPermission.name}</h1>
            <small class="page-title">Love yourself</small>
        </div>
      `;
    // Insert the navigation bar into the DOM
    document.querySelector('#main-profileU').innerHTML = mainProfileU;
} else {
    window.location.href = '/User/Login.html';
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

/* ========== Toggle Edit profile =========== */
const btnProfile = document.querySelector(".editProfile");
const closeProfile = document.querySelector("#modalEditP .close");

if (btnProfile) {
    btnProfile.onclick = () => {
        document.getElementById("modalEditP").classList.add("showModal");
        document.querySelector(".container").classList.add("active");
    };
}

if (closeProfile) {
    closeProfile.onclick = () => {
        document.getElementById("modalEditP").classList.remove("showModal");
        document.querySelector(".container").classList.remove("active");
    };
}

/* ========== Toggle add post =========== */
const btnPost = document.querySelector(".addPost");
const closePost = document.querySelector("#modalAddPost .close");

if (btnPost) {
    btnPost.onclick = () => {
        document.getElementById("modalAddPost").classList.add("showModal");
        document.querySelector(".container").classList.add("active");
    };
}

if (closePost) {
    closePost.onclick = () => {
        document.getElementById("modalAddPost").classList.remove("showModal");
        document.querySelector(".container").classList.remove("active");
    };
}


// /* ==========  choose picture =========== */

const imageChange = document.getElementById("update-img");
const inputImg = document.getElementById("update-file");

if (inputImg) {
    inputImg.addEventListener("change", () => {
        imageChange.src = URL.createObjectURL(inputImg.files[0]);
    });
}

function formatDate(dateString) {
    const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    const date = new Date(dateString);

    const day = date.getDate();
    const month = months[date.getMonth()];

    return `${day} <span>${month}</span>`;
}