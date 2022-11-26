
const Show = () =>
{
    $("[show-on-change]").removeClass("d-none");
}

const Hide = () =>
{
    $("[show-on-change]").addClass("d-none");
}

$("form.profile-form input").on('propertychange input', Show);
$("form.profile-form button[type=reset]").click(Hide);

console.log($("form.profile-form button[type=reset]"));