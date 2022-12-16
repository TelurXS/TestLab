
const delay = 2 * 1000;

const Toggle = (e) =>
{
    var classes = e.target.classList;

    classes.add("active");

    setTimeout(function () {
        classes.remove("active");
    }, delay);    
}

$(".tool-tip").click(Toggle);