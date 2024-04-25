$(document).ready(function () {
    LoadData();
    // Get all category
    function LoadData() {
        //Load top 5 post
        $.ajax({
            url: "https://localhost:7034/odata/Post?$top=5&$expand=tab,users&$orderby=createdDate desc",
            type: "GET",
            success: function (data) {
                $.each(data.value, function (index, value) {
                    let formatD = formatDate(value["createdDate"]);
                    var row = `<div class="single-blog-area blog-style-2 mb-50 wow fadeInUp" data-wow-delay="0.2s" data-wow-duration="1000ms">
                                    <div class="row align-items-center">
                                        <div class="col-12 col-md-6">
                                            <div class="single-blog-thumbnail">
                                                <img src="../Asset/img/blog-img/${value["thumb"]}" alt="">
                                                <div class="post-date">
                                                    <a href="single-post.html?id=${value["post_id"]}">${formatD}</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6">
                                            <!-- Blog Content -->
                                            <div class="single-blog-content">
                                                <div class="line"></div>
                                                <a href="single-post.html?id=${value["post_id"]}" class="post-tag">${value["Tab"]["tabName"]}</a>
                                                <h4><a href="single-post.html?id=${value["post_id"]}" class="post-headline">${value["title"]}</a></h4>
                                                <p>${value["summary"]}</p>
                                                <div class="post-meta">
                                                    <p>By <a href="single-post.html?id=${value["post_id"]}">${value["Users"]["fullName"]}</a></p>
                                                    <p>3 comments</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>`;
                    $(".container #PostList").append(row);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        
    }
});
