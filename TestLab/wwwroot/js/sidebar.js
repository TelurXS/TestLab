
const sidebar = $("#sidebar");
const open = $("#sidebar-open");
const close = $("#sidebar-close");

const Open = () =>
{
    sidebar.removeClass("s-close");
}

const Close = () =>
{
    sidebar.addClass("s-close");
}

open.click(Open);
close.click(Close);