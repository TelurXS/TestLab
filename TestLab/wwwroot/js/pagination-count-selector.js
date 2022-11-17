
const selector = document.getElementById("countSelector");
const apply = document.getElementById("apply");

apply.onclick = () =>
{
    var selectedCount = selector.value;

    window.location.assign(`?page=1&count=${selectedCount}`)
}
