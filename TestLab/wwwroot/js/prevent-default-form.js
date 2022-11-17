

var form = $("#prevent-default-form")

console.log(form);

form.onsubmit = (e) => {
    e.preventDefault();

    $.ajax(
        {
            type: "post",
            dataType: 'json',
            url: '/account/register',
            data: { }
            success: (servicesData) => 
            {

            }
        }
    );
}
