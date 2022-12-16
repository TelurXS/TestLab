
const Copy = (e) =>
{
    navigator.clipboard.writeText(window.location);
}

var copy_buttons = $("button[copy]");

copy_buttons.click(Copy)