$(document).ready(function () {

    GetPostById();

    function GetPostById() {
        let params = (new URL(document.location)).searchParams;
        let id = params.get("id");

        $.ajax({
            url: "https://localhost:7034/odata/Post?$expand=tab,users&$filter=post_id eq " + id,
            type: "GET",
            success: function (data) {
                const postData = data.value[0]; // get the first item in the array of posts
                let formatNewD1 = formatDate(postData["createdDate"]);
                var row1 = `<div class="single-blog-thumbnail">
                                <img src="../Asset/img/blog-img/${postData["thumb"]}" alt="">
                                <div class="post-tag-content">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="post-date">
                                                    <a href="#">${formatNewD1}</a>
                                                </div>                                     
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
                $("#postDetail_Img").append(row1);
                // <!-- Blog Content -->
                var row2 = `<div class="single-blog-content">
                                <div class="line"></div>
                                <a href="#" class="post-tag">${postData["Tab"]["tabName"]}</a>
                                <h4><a href="#" class="post-headline mb-0">${postData["title"]}</a></h4>
                                <div class="post-meta mb-50">
                                    <p>By <a href="#">${postData["Users"]["fullName"]}</a></p>
                                </div>
                                <p>${postData["content"]}</p>
                            </div>`;

                $(".container #blogContent").append(row2);
                // <!-- Profile Writer -->
                var row3 = `<div class="author-thumbnail">
                                <img src="../Asset/img/bg-img/${postData["Users"]["thumb"]}" alt="">
                            </div>
                            <div class="author-info">
                                <div class="line"></div>
                                <span class="author-role">Author</span>
                                <h4><a href="#" class="author-name">${postData["Users"]["fullName"]}</a></h4>                         
                            </div>`;
                $(".container #profile_User").append(row3);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        //Load Comment
        $.ajax({
            url: "https://localhost:7034/odata/PostComment?$expand=posts,users&$filter=PostId eq " + id,
            type: "GET",
            success: function (data) {
                $.each(data.value, function (index, value) {
                    let formatNewComment = formatDate(value["created_time"]);
                    let row = `<li class="single_comment_area">
                    <!-- Comment Content -->
                    <div class="comment-content d-flex">
                        <!-- Comment Author -->
                        <div class="comment-author">
                            <img src="../Asset/img/bg-img/${value["Users"]["thumb"]}" alt="author">
                        </div>
                        <!-- Comment Meta -->
                        <div class="comment-meta">
                            <a href="#" class="post-date">${formatNewComment}</a>
                            <p><a href="#" class="post-author">${value["Users"]["fullName"]}</a></p>
                            <p>${value["content"]}</p>
                        </div>
                    </div>                                
                </li>`;
                    $(".comment_area #PostCommentH").append(row);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        //Load Count Comment
        $.ajax({
            url: "https://localhost:7034/odata/PostComment/$count?$filter=PostId eq " + id,
            type: "GET",
            success: function (data) {
                let row = `<i class='bx bx-comment'></i> <p>${data}</p>`;
                $("#postCommentH").append(row);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        //Load Count Like
        $.ajax({
            url: "https://localhost:7034/odata/PostLike/$count?$filter=PostId eq " + id,
            type: "GET",
            success: function (data) {
                let row = `<button onclick="ToggleLike()" id="btnHeart">
                <i class='bx bxs-heart'></i> 
                <p>${data}</p></button>`;
                $("#postLikeH").append(row);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    }

    let paramsPost = (new URL(document.location)).searchParams;
    let postid = paramsPost.get("id");
    const userId = decodeJWTToken(localStorage.getItem('token'));

    $('#add_PostComment').submit(function (e) {
        e.preventDefault();
        if(userId == null){
            window.location.href = '/User/Login.html';
        }else{
            var contentP = document.getElementById("messageP").value;
        const json = {
            "content": contentP,
            "postId": postid,
            "userId": userId.id,
        };

            $.ajax({
                url: 'https://localhost:7034/odata/PostComment',
                type: 'POST',
                data: JSON.stringify(json),
                processData: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + localStorage.getItem("token"));
                },
                success: function (data) {
                    location.reload();
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                }
            });
        }
   
        
    });

});

function ToggleLike() {
    var clickLike = document.getElementById("btnHeart");
    if (clickLike.style.color == 'red') {
        clickLike.style.color = 'grey'
    } else {
        clickLike.style.color = 'red'
        let paramsPostLike = (new URL(document.location)).searchParams;
        let postLikeid = paramsPostLike.get("id");
        const userId = decodeJWTToken(localStorage.getItem('token'));
        var contentP = document.getElementById("messageP").value;
        const json = {
            "postId": postLikeid,
            "userId": userId.id
        };

        $.ajax({
            url: 'https://localhost:7034/odata/PostLike',
            type: 'POST',
            data: JSON.stringify(json),
            processData: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + localStorage.getItem("token"));
            },
            success: function (data) {
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    }
}
