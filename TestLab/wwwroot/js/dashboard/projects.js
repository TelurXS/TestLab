
const ToHtmlDate = (date) => new Date(date).toISOString().split('T')[0]

var idInput = $("[name=id]");
var authorInput = $("[name=author]");
var nameInput = $("[name=name]");
var creationDateInput = $("[name=creationDate]");
var resourceInput = $("[name=resource]");
var resultInput = $("[name=result]");
var stateInput = $("[name=state]");
var accessabilityInput = $("[name=accessability]");
var typeInput = $("[name=type]");
var resultTypeInput = $("[name=resultType]");

var resetButton = $("#project_reset");
var watchButton = $("#watch_project");
var deleteButton = $("#delete_project");


const FillFields = (id) => {
    $.ajax({
        type: "GET",
        dataType: 'json',
        url: `/api/GetProjectById?id=${id}`,

        success: (response) => {

            ResetFields(response);

            resetButton.off("click");

            resetButton.click(() => ResetFields(response))
            watchButton.attr("href", `/project?id=${response.id}`)
            deleteButton.attr("href", `/dashboard/deleteproject?id=${response.id}`)
            
        },
        error: () => {
            console.log("Server error");
        }
    });
}

const ResetFields = (response) =>
{
    idInput.val(response.id);
    authorInput.val(response.authorId);
    nameInput.val(response.name);
    creationDateInput.val(ToHtmlDate(response.creationDate));
    resourceInput.val(response.resource);
    stateInput.val(response.state)
    resultInput.val(response.result);
    accessabilityInput.val(response.accessability);
    typeInput.val(response.type);
    resultTypeInput.val(response.resultType);
}

var on_modal_open = (id) => { FillFields(id) }
