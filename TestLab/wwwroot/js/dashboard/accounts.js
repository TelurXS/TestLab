
const ToHtmlDate = (date) => new Date(date).toISOString().split('T')[0]

var idInput = $("[name=id]");
var loginInput = $("[name=login]");
var passwordInput = $("[name=password]");
var nameInput = $("[name=name]");
var descriptionInput = $("[name=description]");
var phoneInput = $("[name=phone]");
var emailInput = $("[name=email]");
var addressInput = $("[name=address]");
var profileImageInput = $("[name=profileImage]");
var birthDateInput = $("[name=birthDate]");
var registrationDateInput = $("[name=registrationDate]");
var stateInput = $("[name=state]");

var watchProfile = $("#watchProfile");

var on_modal_open = (id) => {

    $.ajax({
        type: "GET",
        dataType: 'json',
        url: `/api/GetAccountById?id=${id}`,

        success: (response) => {
            idInput.val(response.id);
            loginInput.val(response.login);
            passwordInput.val(response.password);
            nameInput.val(response.name);
            descriptionInput.val(response.description);
            phoneInput.val(response.phone);
            emailInput.val(response.email);
            addressInput.val(response.address);
            birthDateInput.val(ToHtmlDate(response.birthDate));
            registrationDateInput.val(ToHtmlDate(response.registrationDate));
            stateInput.val(response.state);

            watchProfile.attr("href", `/account/profile?id=${response.id}`);
        },
        error: () => {
            console.log("Server error");
        }
    });
}