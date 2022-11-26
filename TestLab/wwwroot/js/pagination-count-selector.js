
const selector = $("#countSelector");
const apply = $("#apply");

const Apply = () =>
{
    var count = selector.val();
    window.location.assign(`?page=1&count=${count}`);
}

apply.click(Apply);
