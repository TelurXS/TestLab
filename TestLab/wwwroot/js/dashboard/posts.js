
const ToHtmlDate = (date) => new Date(date).toISOString().split('T')[0]

var idInput = $("[name=id]");
var authorInput = $("[name=author]");
var titleInput = $("[name=title]");
var descriptionInput = $("[name=description]");
var contentInput = $("[name=content]");
var creationDateInput = $("[name=creationDate]");
var releaseDateInput = $("[name=releaseDate]");
var postImageInput = $("[name=postImage]");
var stateInput = $("[name=state]");

var watchPost = $("#watchPost");
var editPost = $("#editPost");
var postReset = $("[post-reset]");

const FillFields = (id) => {
    $.ajax({
        type: "GET",
        dataType: 'json',
        url: `/api/GetPostById?id=${id}`,

        success: (response) => {

            ResetFieds(response);

            postReset.off("click");

            postReset.click(() => ResetFieds(response));
            watchPost.attr("href", `/news/post?id=${response.id}`);
            editPost.attr("href", `/editor?id=${response.id}`);
        },
        error: () => {
            console.log("Server error");
        }
    });
}

const ResetFieds = (response) =>
{
    idInput.val(response.id);
    authorInput.val(response.authorId);
    titleInput.val(response.title);
    descriptionInput.val(response.description);
    contentInput.val(response.content);
    creationDateInput.val(ToHtmlDate(response.creationDate));
    releaseDateInput.val(ToHtmlDate(response.releaseDate));
    stateInput.val(response.state);
}

var on_modal_open = (id) => { FillFields(id) }

